using System;
using System.Collections.Generic;
using System.Threading;
using com.fpnn.common;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public class RTMQuestProcessor
    {
        //-- Event
        public virtual void PushEvent(int projectId, string eventName, long userId, int eventTime, string endpoint, string data) { }

        //-- Messages
        public virtual void PushMessage(RTMMessage message) { }
        public virtual void PushGroupMessage(RTMMessage message) { }
        public virtual void PushRoomMessage(RTMMessage message) { }

        //-- Chat
        public virtual void PushChat(RTMMessage message) { }
        public virtual void PushGroupChat(RTMMessage message) { }
        public virtual void PushRoomChat(RTMMessage message) { }

        //-- Cmd
        public virtual void PushCmd(RTMMessage message) { }
        public virtual void PushGroupCmd(RTMMessage message) { }
        public virtual void PushRoomCmd(RTMMessage message) { }

        //-- Files
        public virtual void PushFile(RTMMessage message) { }
        public virtual void PushGroupFile(RTMMessage message) { }
        public virtual void PushRoomFile(RTMMessage message) { }
    }


    internal class RTMMasterProcessor: IQuestProcessor
    {
        private RTMQuestProcessor questProcessor;
        private DuplicatedMessageFilter duplicatedFilter;
        private ErrorRecorder errorRecorder;
        private Int64 connectionId;
        private readonly Dictionary<string, QuestProcessDelegate> methodMap;

        public RTMMasterProcessor()
        {
            duplicatedFilter = new DuplicatedMessageFilter();

            methodMap = new Dictionary<string, QuestProcessDelegate> {
                { "ping", Ping },

                { "pushmsg", PushMessage },
                { "pushgroupmsg", PushGroupMessage },
                { "pushroommsg", PushRoomMessage },

                { "pushevent", PushEvent },
            };
        }

        public void SetProcessor(RTMQuestProcessor processor)
        {
            questProcessor = processor;
        }

        public void SetErrorRecorder(ErrorRecorder recorder)
        {
            errorRecorder = recorder;
        }

        public void SetConnectionId(Int64 connId)
        {
            connectionId = connId;
        }

        public QuestProcessDelegate GetQuestProcessDelegate(string method)
        {
            if (methodMap.TryGetValue(method, out QuestProcessDelegate process))
            {
                return process;
            }

            return null;
        }

        //----------------------[ RTM Operations ]-------------------//
        public Answer Ping(Int64 connectionId, string endpoint, Quest quest)
        {
            AdvanceAnswer.SendAnswer(new Answer(quest));
            return null;
        }

        //----------------------[ RTM Messagess Utilities ]-------------------//
        private TranslatedInfo ProcessChatMessage(Quest quest)
        {
            TranslatedInfo tm = new TranslatedInfo();

            try
            {
                Dictionary<object, object> msg = quest.Want<Dictionary<object, object>>("msg");
                if (msg.TryGetValue("source", out object source))
                {
                    tm.sourceLanguage = (string)source;
                }
                else
                    tm.sourceLanguage = string.Empty;

                if (msg.TryGetValue("target", out object target))
                {
                    tm.targetLanguage = (string)target;
                }
                else
                    tm.targetLanguage = string.Empty;

                if (msg.TryGetValue("sourceText", out object sourceText))
                {
                    tm.sourceText = (string)sourceText;
                }
                else
                    tm.sourceText = string.Empty;

                if (msg.TryGetValue("targetText", out object targetText))
                {
                    tm.targetText = (string)targetText;
                }
                else
                    tm.targetText = string.Empty;

                return tm;
            }
            catch (InvalidCastException e)
            {
                if (errorRecorder != null)
                    errorRecorder.RecordError("ProcessChatMessage failed.", e);

                return null;
            }
        }

        private class MessageInfo
        {
            public bool isBinary;
            public byte[] binaryData;
            public string message;
        }

        private MessageInfo BuildMessageInfo(Quest quest)
        {
            MessageInfo info = new MessageInfo();

            object message = quest.Want("msg");
            info.isBinary = RTMServerClient.CheckBinaryType(message);
            if (info.isBinary)
                info.binaryData = (byte[])message;
            else
                info.message = (string)message;

            return info;
        }

        private RTMMessage BuildRTMMessage(Quest quest, long from, long to, long mid)
        {
            RTMMessage rtmMessage = new RTMMessage
            {
                fromUid = from,
                toId = to,
                messageId = mid,
                messageType = quest.Want<byte>("mtype"),
                attrs = quest.Want<string>("attrs"),
                modifiedTime = quest.Want<long>("mtime")
            };

            if (rtmMessage.messageType == (byte)MessageType.Chat)
            {
                rtmMessage.translatedInfo = ProcessChatMessage(quest);
                if (rtmMessage.translatedInfo != null)
                {
                    if (rtmMessage.translatedInfo.targetText.Length > 0)
                        rtmMessage.stringMessage = rtmMessage.translatedInfo.targetText;
                    else
                        rtmMessage.stringMessage = rtmMessage.translatedInfo.sourceText;
                }
            }
            else if (rtmMessage.messageType == (byte)MessageType.Cmd)
            {
                rtmMessage.stringMessage = quest.Want<string>("msg");
            }
            else if (rtmMessage.messageType >= 40 && rtmMessage.messageType <= 50)
            {
                rtmMessage.stringMessage = quest.Want<string>("msg");
                RTMServerClient.BuildFileInfo(rtmMessage, errorRecorder);
            }
            else
            {
                MessageInfo messageInfo = BuildMessageInfo(quest);
                if (messageInfo.isBinary)
                {
                    rtmMessage.binaryMessage = messageInfo.binaryData;
                }
                else
                    rtmMessage.stringMessage = messageInfo.message;
            }

            return rtmMessage;
        }

        //----------------------[ RTM Messagess ]-------------------//
        public Answer PushMessage(Int64 connectionId, string endpoint, Quest quest)
        {
            AdvanceAnswer.SendAnswer(new Answer(quest));

            if (questProcessor == null)
                return null;

            long from = quest.Want<long>("from");
            long to = quest.Want<long>("to");
            long mid = quest.Want<long>("mid");

            if (duplicatedFilter.CheckP2PMessage(from, mid) == false)
                return null;

            RTMMessage rtmMessage = BuildRTMMessage(quest, from, to, mid);

            if (rtmMessage.messageType == (byte)MessageType.Chat)
            {
                questProcessor.PushChat(rtmMessage);
            }
            else if (rtmMessage.messageType == (byte)MessageType.Cmd)
            {
                questProcessor.PushCmd(rtmMessage);
            }
            else if (rtmMessage.messageType >= 40 && rtmMessage.messageType <= 50)
            {
                questProcessor.PushFile(rtmMessage);
            }
            else
            {
                questProcessor.PushMessage(rtmMessage);
            }

            return null;
        }

        public Answer PushGroupMessage(Int64 connectionId, string endpoint, Quest quest)
        {
            AdvanceAnswer.SendAnswer(new Answer(quest));

            if (questProcessor == null)
                return null;

            long groupId = quest.Want<long>("gid");
            long from = quest.Want<long>("from");
            long mid = quest.Want<long>("mid");

            if (duplicatedFilter.CheckGroupMessage(groupId, from, mid) == false)
                return null;

            RTMMessage rtmMessage = BuildRTMMessage(quest, from, groupId, mid);

            if (rtmMessage.messageType == (byte)MessageType.Chat)
            {
                if (rtmMessage.translatedInfo != null)
                    questProcessor.PushGroupChat(rtmMessage);
            }
            else if (rtmMessage.messageType == (byte)MessageType.Cmd)
            {
                questProcessor.PushGroupCmd(rtmMessage);
            }
            else if (rtmMessage.messageType >= 40 && rtmMessage.messageType <= 50)
            {
                questProcessor.PushGroupFile(rtmMessage);
            }
            else
            {
                questProcessor.PushGroupMessage(rtmMessage);
            }

            return null;
        }

        public Answer PushRoomMessage(Int64 connectionId, string endpoint, Quest quest)
        {
            AdvanceAnswer.SendAnswer(new Answer(quest));

            if (questProcessor == null)
                return null;

            long from = quest.Want<long>("from");
            long roomId = quest.Want<long>("rid");
            long mid = quest.Want<long>("mid");

            if (duplicatedFilter.CheckRoomMessage(roomId, from, mid) == false)
                return null;

            RTMMessage rtmMessage = BuildRTMMessage(quest, from, roomId, mid);

            if (rtmMessage.messageType == (byte)MessageType.Chat)
            {
                if (rtmMessage.translatedInfo != null)
                    questProcessor.PushRoomChat(rtmMessage);
            }
            else if (rtmMessage.messageType == (byte)MessageType.Cmd)
            {
                questProcessor.PushRoomCmd(rtmMessage);
            }
            else if (rtmMessage.messageType >= 40 && rtmMessage.messageType <= 50)
            {
                questProcessor.PushRoomFile(rtmMessage);
            }
            else
            {
                questProcessor.PushRoomMessage(rtmMessage);
            }

            return null;
        }

        public Answer PushEvent(Int64 connectionId, string endpoint, Quest quest)
        {
            AdvanceAnswer.SendAnswer(new Answer(quest));

            if (questProcessor == null)
                return null;

            int projectId = quest.Want<int>("pid");
            string eventName = quest.Want<string>("event");
            long userId = quest.Want<long>("uid");
            int eventTime = quest.Want<int>("time");
            string eventEndpoint = quest.Want<string>("endpoint");
            string data = quest.Get<string>("endpoint", string.Empty);

            questProcessor.PushEvent(projectId, eventName, userId, eventTime, eventEndpoint, data);
            return null;
        }

    }
    
}