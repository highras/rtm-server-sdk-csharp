using System;
using System.Collections.Generic;
using System.Text;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public void SendMessage(Action<long, int> callback, long fromUid, long toUid, byte mtype, string message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("to", toUid);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            long messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        long mtime = answer.Get<long>("mtime", 0); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(messageId, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SendMessage(out long messageId, long fromUid, long toUid, byte mtype, string message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("to", toUid);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                long mtime = answer.Get<long>("mtime", 0);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void SendMessage(Action<long, int> callback, long fromUid, long toUid, byte mtype, byte[] message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("to", toUid);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            long messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        long mtime = answer.Get<long>("mtime", 0); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(messageId, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SendMessage(out long messageId, long fromUid, long toUid, byte mtype, byte[] message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("to", toUid);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                long mtime = answer.Get<long>("mtime", 0);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void SendMessages(Action<long, int> callback, long fromUid, HashSet<long> toUids, byte mtype, string message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendmsgs");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("tos", toUids);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            long messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        long mtime = answer.Get<long>("mtime", 0); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(messageId, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SendMessages(out long messageId, long fromUid, HashSet<long> toUids, byte mtype, string message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendmsgs");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("tos", toUids);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                long mtime = answer.Get<long>("mtime", 0);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void SendMessages(Action<long, int> callback, long fromUid, HashSet<long> toUids, byte mtype, byte[] message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendmsgs");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("tos", toUids);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            long messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        long mtime = answer.Get<long>("mtime", 0); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(messageId, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SendMessages(out long messageId, long fromUid, HashSet<long> toUids, byte mtype, byte[] message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendmsgs");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("tos", toUids);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                long mtime = answer.Get<long>("mtime", 0);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void SendGroupMessage(Action<long, int> callback, long fromUid, long groupId, byte mtype, string message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendgroupmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("gid", groupId);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            long messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        long mtime = answer.Get<long>("mtime", 0); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(messageId, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SendGroupMessage(out long messageId, long fromUid, long groupId, byte mtype, string message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendgroupmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("gid", groupId);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                long mtime = answer.Get<long>("mtime", 0);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void SendGroupMessage(Action<long, int> callback, long fromUid, long groupId, byte mtype, byte[] message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendgroupmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("gid", groupId);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            long messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        long mtime = answer.Get<long>("mtime", 0); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(messageId, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SendGroupMessage(out long messageId, long fromUid, long groupId, byte mtype, byte[] message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendgroupmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("gid", groupId);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                long mtime = answer.Get<long>("mtime", 0);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void SendRoomMessage(Action<long, int> callback, long fromUid, long roomId, byte mtype, string message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendroommsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("rid", roomId);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            long messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        long mtime = answer.Get<long>("mtime", 0); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(messageId, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SendRoomMessage(out long messageId, long fromUid, long roomId, byte mtype, string message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendroommsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("rid", roomId);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                long mtime = answer.Get<long>("mtime", 0);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void SendRoomMessage(Action<long, int> callback, long fromUid, long roomId, byte mtype, byte[] message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendroommsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("rid", roomId);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            long messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        long mtime = answer.Get<long>("mtime", 0); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(messageId, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SendRoomMessage(out long messageId, long fromUid, long roomId, byte mtype, byte[] message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("sendroommsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("rid", roomId);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                long mtime = answer.Get<long>("mtime", 0);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void BroadcastMessage(Action<long, int> callback, long fromUid, byte mtype, string message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("broadcastmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            long messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        long mtime = answer.Get<long>("mtime", 0); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(messageId, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int BroadcastMessage(out long messageId, long fromUid, byte mtype, string message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("broadcastmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                long mtime = answer.Get<long>("mtime", 0);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void BroadcastMessage(Action<long, int> callback, long fromUid, byte mtype, byte[] message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("broadcastmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            long messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        long mtime = answer.Get<long>("mtime", 0); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(messageId, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int BroadcastMessage(out long messageId, long fromUid, byte mtype, byte[] message, string attrs = "", int timeout = 0)
        {
            Quest quest = GenerateQuest("broadcastmsg");
            quest.Param("mtype", mtype);
            quest.Param("from", fromUid);
            quest.Param("msg", message);
            quest.Param("attrs", attrs);

            messageId = MidGenerator.Gen();
            quest.Param("mid", messageId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                long mtime = answer.Get<long>("mtime", 0);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        //===========================[ History Messages Utilities ]=========================//
        private HistoryMessageResult BuildHistoryMessageResult(long toId, Answer answer)
        {
            HistoryMessageResult result = new HistoryMessageResult();
            result.count = answer.Want<int>("num");
            result.lastCursorId = answer.Want<long>("lastid");
            result.beginMsec = answer.Want<long>("begin");
            result.endMsec = answer.Want<long>("end");
            result.messages = new List<HistoryMessage>();

            List<object> messages = (List<object>)answer.Want("msgs");
            foreach (List<object> items in messages)
            {
                bool deleted = (bool)Convert.ChangeType(items[4], TypeCode.Boolean);
                if (deleted)
                    continue;

                HistoryMessage message = new HistoryMessage();
                message.cursorId = (long)Convert.ChangeType(items[0], TypeCode.Int64);
                message.fromUid = (long)Convert.ChangeType(items[1], TypeCode.Int64);
                message.toId = toId;
                message.messageType = (byte)Convert.ChangeType(items[2], TypeCode.Byte);
                message.messageId = (long)Convert.ChangeType(items[3], TypeCode.Int64);

                if (!CheckBinaryType(items[5]))
                    message.stringMessage = (string)Convert.ChangeType(items[5], TypeCode.String);
                else
                    message.binaryMessage = (byte[])items[5];

                message.attrs = (string)Convert.ChangeType(items[6], TypeCode.String);
                message.modifiedTime = (long)Convert.ChangeType(items[7], TypeCode.Int64);

                if (message.messageType >= 40 && message.messageType <= 50)
                    RTMServerClient.BuildFileInfo(message, errorRecorder);

                result.messages.Add(message);
            }
            result.count = result.messages.Count;
            return result;
        }

        //===========================[ Messages Utilities ]=========================//
        internal static bool CheckBinaryType(object obj)
        {
            string typeFullName = obj.GetType().FullName;
            int idx = typeFullName.IndexOf('`');
            if (idx != -1)
            {
                typeFullName = typeFullName.Substring(0, idx);
            }

            return typeFullName == "System.Byte[]";
        }

        public void GetGroupMessage(HistoryMessageDelegate callback, long userId, long groupId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("getgroupmsg");
            quest.Param("uid", userId);
            quest.Param("gid", groupId);
            quest.Param("desc", desc);
            quest.Param("num", count);

            quest.Param("begin", beginMsec);
            quest.Param("end", endMsec);
            quest.Param("lastid", lastId);

            if (mtypes != null)
                quest.Param("mtypes", mtypes);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    HistoryMessageResult result;
                    try
                    {
                        result = BuildHistoryMessageResult(groupId, answer);
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                        result = new HistoryMessageResult();
                    }
                    callback(result.count, result.lastCursorId, result.beginMsec, result.endMsec, result.messages, errorCode);
                }
                else
                    callback(0, 0, 0, 0, null, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, 0, 0, 0, null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetGroupMessage(out HistoryMessageResult result, long userId, long groupId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
        {
            result = null;

            Quest quest = GenerateQuest("getgroupmsg");
            quest.Param("uid", userId);
            quest.Param("gid", groupId);
            quest.Param("desc", desc);
            quest.Param("num", count);

            quest.Param("begin", beginMsec);
            quest.Param("end", endMsec);
            quest.Param("lastid", lastId);

            if (mtypes != null)
                quest.Param("mtypes", mtypes);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                result = BuildHistoryMessageResult(groupId, answer);
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void GetRoomMessage(HistoryMessageDelegate callback, long userId, long roomId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("getroommsg");
            quest.Param("uid", userId);
            quest.Param("rid", roomId);
            quest.Param("desc", desc);
            quest.Param("num", count);

            quest.Param("begin", beginMsec);
            quest.Param("end", endMsec);
            quest.Param("lastid", lastId);

            if (mtypes != null)
                quest.Param("mtypes", mtypes);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    HistoryMessageResult result;
                    try
                    {
                        result = BuildHistoryMessageResult(roomId, answer);
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                        result = new HistoryMessageResult();
                    }
                    callback(result.count, result.lastCursorId, result.beginMsec, result.endMsec, result.messages, errorCode);
                }
                else
                    callback(0, 0, 0, 0, null, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, 0, 0, 0, null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetRoomMessage(out HistoryMessageResult result, long userId, long roomId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
        {
            result = null;

            Quest quest = GenerateQuest("getroommsg");
            quest.Param("uid", userId);
            quest.Param("rid", roomId);
            quest.Param("desc", desc);
            quest.Param("num", count);

            quest.Param("begin", beginMsec);
            quest.Param("end", endMsec);
            quest.Param("lastid", lastId);

            if (mtypes != null)
                quest.Param("mtypes", mtypes);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                result = BuildHistoryMessageResult(roomId, answer);
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void GetBroadcastMessage(HistoryMessageDelegate callback, long userId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("getbroadcastmsg");
            quest.Param("uid", userId);
            quest.Param("desc", desc);
            quest.Param("num", count);

            quest.Param("begin", beginMsec);
            quest.Param("end", endMsec);
            quest.Param("lastid", lastId);

            if (mtypes != null)
                quest.Param("mtypes", mtypes);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    HistoryMessageResult result;
                    try
                    {
                        result = BuildHistoryMessageResult(0, answer);
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                        result = new HistoryMessageResult();
                    }
                    callback(result.count, result.lastCursorId, result.beginMsec, result.endMsec, result.messages, errorCode);
                }
                else
                    callback(0, 0, 0, 0, null, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, 0, 0, 0, null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetBroadcastMessage(out HistoryMessageResult result, long userId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
        {
            result = null;

            Quest quest = GenerateQuest("getbroadcastmsg");
            quest.Param("uid", userId);
            quest.Param("desc", desc);
            quest.Param("num", count);

            quest.Param("begin", beginMsec);
            quest.Param("end", endMsec);
            quest.Param("lastid", lastId);

            if (mtypes != null)
                quest.Param("mtypes", mtypes);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                result = BuildHistoryMessageResult(0, answer);
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        private void AdjustHistoryMessageResultForP2PMessage(long selfUid, long peerUserUid, HistoryMessageResult result)
        {
            foreach (HistoryMessage hm in result.messages)
            {
                if (hm.fromUid == 1)
                {
                    hm.fromUid = selfUid;
                    hm.toId = peerUserUid;
                }
                else if (hm.fromUid == 2)
                {
                    hm.fromUid = peerUserUid;
                    hm.toId = selfUid;
                }
            }
        }

        public void GetP2PMessage(HistoryMessageDelegate callback, long userId, long peerUserUid, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("getp2pmsg");
            quest.Param("uid", userId);
            quest.Param("ouid", peerUserUid);
            quest.Param("desc", desc);
            quest.Param("num", count);

            quest.Param("begin", beginMsec);
            quest.Param("end", endMsec);
            quest.Param("lastid", lastId);

            if (mtypes != null)
                quest.Param("mtypes", mtypes);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    HistoryMessageResult result;
                    try
                    {
                        result = BuildHistoryMessageResult(0, answer);
                        AdjustHistoryMessageResultForP2PMessage(userId, peerUserUid, result);
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                        result = new HistoryMessageResult();
                    }
                    callback(result.count, result.lastCursorId, result.beginMsec, result.endMsec, result.messages, errorCode);
                }
                else
                    callback(0, 0, 0, 0, null, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, 0, 0, 0, null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetP2PMessage(out HistoryMessageResult result, long userId, long peerUserUid, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
        {
            result = null;

            Quest quest = GenerateQuest("getp2pmsg");
            quest.Param("uid", userId);
            quest.Param("ouid", peerUserUid);
            quest.Param("desc", desc);
            quest.Param("num", count);

            quest.Param("begin", beginMsec);
            quest.Param("end", endMsec);
            quest.Param("lastid", lastId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                result = BuildHistoryMessageResult(0, answer);
                AdjustHistoryMessageResultForP2PMessage(userId, peerUserUid, result);
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        internal void DeleteMessage(Action<int> callback, long fromUid, long toId, long messageId, int type, int timeout = 0)
        {
            Quest quest = GenerateQuest("delmsg");
            quest.Param("from", fromUid);
            quest.Param("mid", messageId);
            quest.Param("xid", toId);
            quest.Param("type", type);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                callback(errorCode); 
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        internal int DeleteMessage(long fromUid, long toId, long messageId, int type, int timeout = 0)
        {
            Quest quest = GenerateQuest("delmsg");
            quest.Param("from", fromUid);
            quest.Param("mid", messageId);
            quest.Param("xid", toId);
            quest.Param("type", type);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void DeleteMessage(Action<int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
        {
            DeleteMessage(callback, fromUid, toId, messageId, (byte)messageCategory, timeout);
        }

        public int DeleteMessage(long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
        {
            return DeleteMessage(fromUid, toId, messageId, (byte)messageCategory, timeout);
        }

        internal static byte[] ConvertStringToByteArray(string data)
        {
            //-- Please refer com.fpnn.msgpack.MsgPacker::UnpackString(...)

            UTF8Encoding utf8Encoding = new UTF8Encoding(false, true);     //-- NO BOM.
            return utf8Encoding.GetBytes(data);
        }

        //-------------[ Get Messages ]---------------------//
        private RetrievedMessage BuildRetrievedMessage(Answer answer)
        {
            RetrievedMessage message = new RetrievedMessage();
            message.cursorId = answer.Want<long>("id");
            message.messageType = answer.Want<byte>("mtype");
            message.attrs = answer.Want<string>("attrs");
            message.modifiedTime = answer.Want<long>("mtime");

            object originalMessage = answer.Want("msg");

            if (!CheckBinaryType(originalMessage))
                message.stringMessage = (string)Convert.ChangeType(originalMessage, TypeCode.String);
            else
                message.binaryMessage = (byte[])originalMessage;

            if (message.messageType >= 40 && message.messageType <= 50)
                RTMServerClient.BuildFileInfo(message, errorRecorder);

            return message;
        }

        internal void GetMessage(Action<RetrievedMessage, int> callback, long fromUid, long toId, long messageId, int type, int timeout = 0)
        {
            Quest quest = GenerateQuest("getmsg");
            quest.Param("from", fromUid);
            quest.Param("mid", messageId);
            quest.Param("xid", toId);
            quest.Param("type", type);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    RetrievedMessage retrievedMessage = null;
                    try
                    {
                        retrievedMessage = BuildRetrievedMessage(answer);
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                    callback(retrievedMessage, errorCode);
                }
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public void GetMessage(Action<RetrievedMessage, int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
        {
            GetMessage(callback, fromUid, toId, messageId, (byte)messageCategory, timeout);
        }

        public int GetMessage(out RetrievedMessage retrievedMessage, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
        {
            return GetMessage(out retrievedMessage, fromUid, toId, messageId, (byte)messageCategory, timeout);
        }
        internal int GetMessage(out RetrievedMessage retrievedMessage, long fromUid, long toId, long messageId, int type, int timeout = 0)
        {
            retrievedMessage = null;

            Quest quest = GenerateQuest("getmsg");
            quest.Param("from", fromUid);
            quest.Param("mid", messageId);
            quest.Param("xid", toId);
            quest.Param("type", type);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                retrievedMessage = BuildRetrievedMessage(answer);
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void GetMessageNum(Action<int, int, int> callback, MessageCategory messageCategory, long targetId, HashSet<byte> mTypes = null, long beginMsec = 0, long endMsec = 0, int timeout = 0)
        {
            byte type = 99;
            switch (messageCategory)
            {
                case MessageCategory.GroupMessage:
                    type = 2; break;
                case MessageCategory.RoomMessage:
                    type = 3; break;
            }
            if (type > 3) {
                ClientEngine.RunTask(() =>
                {
                    callback(0, 0, rtm.RTMErrorCode.RTM_EC_INVALID_PARAMETER);
                });
                return;
            }
            Quest quest = GenerateQuest("getmsgnum");
            quest.Param("type", type);
            quest.Param("xid", targetId);
            if (mTypes != null)
                quest.Param("mtypes", mTypes);
            if (beginMsec > 0)
                quest.Param("begin", beginMsec);
            if (endMsec > 0)
                quest.Param("end", endMsec);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                int sender = 0;
                int num = 0;
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        sender = answer.Get<int>("sender", 0); 
                        num = answer.Get<int>("num", 0); 
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(sender, num, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(0, 0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetMessageNum(out int sender, out int num, MessageCategory messageCategory, long targetId, HashSet<byte> mTypes = null, long beginMsec = 0, long endMsec = 0, int timeout = 0)
        {
            sender = 0;
            num = 0;
            byte type = 99;
            switch (messageCategory)
            {
                case MessageCategory.GroupMessage:
                    type = 2; break;
                case MessageCategory.RoomMessage:
                    type = 3; break;
            }
            if (type > 3) {
                return rtm.RTMErrorCode.RTM_EC_INVALID_PARAMETER;
            }
            Quest quest = GenerateQuest("getmsgnum");
            quest.Param("type", type);
            quest.Param("xid", targetId);
            if (mTypes != null)
                quest.Param("mtypes", mTypes);
            if (beginMsec > 0)
                quest.Param("begin", beginMsec);
            if (endMsec > 0)
                quest.Param("end", endMsec);

            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                sender = answer.Get<int>("sender", 0); 
                num = answer.Get<int>("num", 0); 
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

    }
}