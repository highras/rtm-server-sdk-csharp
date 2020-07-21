using System;
using System.Collections.Generic;
using System.Text;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public void SendChat(Action<long, int> callback, long fromUid, long toUid, string message, string attrs = "", int timeout = 0)
        {
            SendMessage(callback, fromUid, toUid, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public int SendChat(out long mtime, long fromUid, long toUid, string message, string attrs = "", int timeout = 0)
        {
            return SendMessage(out mtime, fromUid, toUid, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public void SendChats(Action<long, int> callback, long fromUid, HashSet<long> toUids, string message, string attrs = "", int timeout = 0)
        {
            SendMessages(callback, fromUid, toUids, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public int SendChats(out long mtime, long fromUid, HashSet<long> toUids, string message, string attrs = "", int timeout = 0)
        {
            return SendMessages(out mtime, fromUid, toUids, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public void SendGroupChat(Action<long, int> callback, long fromUid, long groupId, string message, string attrs = "", int timeout = 0)
        {
            SendGroupMessage(callback, fromUid, groupId, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public int SendGroupChat(out long mtime, long fromUid, long groupId, string message, string attrs = "", int timeout = 0)
        {
            return SendGroupMessage(out mtime, fromUid, groupId, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public void SendRoomChat(Action<long, int> callback, long fromUid, long roomId, string message, string attrs = "", int timeout = 0)
        {
            SendRoomMessage(callback, fromUid, roomId, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public int SendRoomChat(out long mtime, long fromUid, long roomId, string message, string attrs = "", int timeout = 0)
        {
            return SendRoomMessage(out mtime, fromUid, roomId, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public void BroadcastChat(Action<long, int> callback, long fromUid, string message, string attrs = "", int timeout = 0)
        {
            BroadcastMessage(callback, fromUid, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public int BroadcastChat(out long mtime, long fromUid, string message, string attrs = "", int timeout = 0)
        {
            return BroadcastMessage(out mtime, fromUid, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public void SendCmd(Action<long, int> callback, long fromUid, long toUid, string message, string attrs = "", int timeout = 0)
        {
            SendMessage(callback, fromUid, toUid, (byte)MessageType.Cmd, message, attrs, timeout);
        }

        public int SendCmd(out long mtime, long fromUid, long toUid, string message, string attrs = "", int timeout = 0)
        {
            return SendMessage(out mtime, fromUid, toUid, (byte)MessageType.Cmd, message, attrs, timeout);
        }

        public void SendCmds(Action<long, int> callback, long fromUid, HashSet<long> toUids, string message, string attrs = "", int timeout = 0)
        {
            SendMessages(callback, fromUid, toUids, (byte)MessageType.Cmd, message, attrs, timeout);
        }

        public int SendCmds(out long mtime, long fromUid, HashSet<long> toUids, string message, string attrs = "", int timeout = 0)
        {
            return SendMessages(out mtime, fromUid, toUids, (byte)MessageType.Cmd, message, attrs, timeout);
        }

        public void SendGroupCmd(Action<long, int> callback, long fromUid, long groupId, string message, string attrs = "", int timeout = 0)
        {
            SendGroupMessage(callback, fromUid, groupId, (byte)MessageType.Cmd, message, attrs, timeout);
        }

        public int SendGroupCmd(out long mtime, long fromUid, long groupId, string message, string attrs = "", int timeout = 0)
        {
            return SendGroupMessage(out mtime, fromUid, groupId, (byte)MessageType.Cmd, message, attrs, timeout);
        }

        public void SendRoomCmd(Action<long, int> callback, long fromUid, long roomId, string message, string attrs = "", int timeout = 0)
        {
            SendRoomMessage(callback, fromUid, roomId, (byte)MessageType.Chat, message, attrs, timeout);
        }

        public int SendRoomCmd(out long mtime, long fromUid, long roomId, string message, string attrs = "", int timeout = 0)
        {
            return SendRoomMessage(out mtime, fromUid, roomId, (byte)MessageType.Cmd, message, attrs, timeout);
        }

        public void BroadcastCmd(Action<long, int> callback, long fromUid, string message, string attrs = "", int timeout = 0)
        {
            BroadcastMessage(callback, fromUid, (byte)MessageType.Cmd, message, attrs, timeout);
        }

        public int BroadcastCmd(out long mtime, long fromUid, string message, string attrs = "", int timeout = 0)
        {
            return BroadcastMessage(out mtime, fromUid, (byte)MessageType.Cmd, message, attrs, timeout);
        }

        public void SendAudio(Action<long, int> callback, long fromUid, long toUid, byte[] message, string attrs = "", int timeout = 0)
        {
            SendMessage(callback, fromUid, toUid, (byte)MessageType.Audio, message, attrs, timeout);
        }

        public int SendAudio(out long mtime, long fromUid, long toUid, byte[] message, string attrs = "", int timeout = 0)
        {
            return SendMessage(out mtime, fromUid, toUid, (byte)MessageType.Audio, message, attrs, timeout);
        }

        public void SendAudios(Action<long, int> callback, long fromUid, HashSet<long> toUids, byte[] message, string attrs = "", int timeout = 0)
        {
            SendMessages(callback, fromUid, toUids, (byte)MessageType.Audio, message, attrs, timeout);
        }

        public int SendAudios(out long mtime, long fromUid, HashSet<long> toUids, byte[] message, string attrs = "", int timeout = 0)
        {
            return SendMessages(out mtime, fromUid, toUids, (byte)MessageType.Audio, message, attrs, timeout);
        }

        public void SendGroupAudio(Action<long, int> callback, long fromUid, long groupId, byte[] message, string attrs = "", int timeout = 0)
        {
            SendGroupMessage(callback, fromUid, groupId, (byte)MessageType.Audio, message, attrs, timeout);
        }

        public int SendGroupAudio(out long mtime, long fromUid, long groupId, byte[] message, string attrs = "", int timeout = 0)
        {
            return SendGroupMessage(out mtime, fromUid, groupId, (byte)MessageType.Audio, message, attrs, timeout);
        }

        public void SendRoomAudio(Action<long, int> callback, long fromUid, long roomId, byte[] message, string attrs = "", int timeout = 0)
        {
            SendRoomMessage(callback, fromUid, roomId, (byte)MessageType.Audio, message, attrs, timeout);
        }

        public int SendRoomAudio(out long mtime, long fromUid, long roomId, byte[] message, string attrs = "", int timeout = 0)
        {
            return SendRoomMessage(out mtime, fromUid, roomId, (byte)MessageType.Audio, message, attrs, timeout);
        }

        public void BroadcastAudio(Action<long, int> callback, long fromUid, byte[] message, string attrs = "", int timeout = 0)
        {
            BroadcastMessage(callback, fromUid, (byte)MessageType.Audio, message, attrs, timeout);
        }

        public int BroadcastAudio(out long mtime, long fromUid, byte[] message, string attrs = "", int timeout = 0)
        {
            return BroadcastMessage(out mtime, fromUid, (byte)MessageType.Audio, message, attrs, timeout);
        }

        private static readonly List<byte> chatMTypes = new List<byte> { (byte)MessageType.Chat, (byte)MessageType.Audio, (byte)MessageType.Cmd };

        public void GetGroupChat(HistoryMessageDelegate callback, long userId, long groupId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
        {
            GetGroupMessage(callback, userId, groupId, desc, count, beginMsec, endMsec, lastId, chatMTypes, timeout);
        }

        public int GetGroupChat(out HistoryMessageResult result, long userId, long groupId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
        {
            return GetGroupMessage(out result, userId, groupId, desc, count, beginMsec, endMsec, lastId, chatMTypes, timeout);
        }

        public void GetRoomChat(HistoryMessageDelegate callback, long userId, long roomId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
        {
            GetRoomMessage(callback, userId, roomId, desc, count, beginMsec, endMsec, lastId, chatMTypes, timeout);
        }

        public int GetRoomChat(out HistoryMessageResult result, long userId, long roomId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
        {
            return GetRoomMessage(out result, userId, roomId, desc, count, beginMsec, endMsec, lastId, chatMTypes, timeout);
        }

        public void GetBroadcastChat(HistoryMessageDelegate callback, long userId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
        {
            GetBroadcastMessage(callback, userId, desc, count, beginMsec, endMsec, lastId, chatMTypes, timeout);
        }

        public int GetBroadcastChat(out HistoryMessageResult result, long userId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
        {
            return GetBroadcastMessage(out result, userId, desc, count, beginMsec, endMsec, lastId, chatMTypes, timeout);
        }

        public void GetP2PChat(HistoryMessageDelegate callback, long userId, long peerUserUid, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
        {
            
            GetP2PMessage(callback, userId, peerUserUid, desc, count, beginMsec, endMsec, lastId, chatMTypes, timeout);
        }

        public int GetP2PChat(out HistoryMessageResult result, long userId, long peerUserUid, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
        {
            return GetP2PMessage(out result, userId, peerUserUid, desc, count, beginMsec, endMsec, lastId, chatMTypes, timeout);
        }

        public void DeleteChat(Action<int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
        {
            DeleteMessage(callback, fromUid, toId, messageId, (byte)messageCategory, timeout);
        }

        public int DeleteChat(long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
        {
            return DeleteMessage(fromUid, toId, messageId, (byte)messageCategory, timeout);
        }

        public void GetChat(Action<RetrievedMessage, int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
        {
            GetMessage(callback, fromUid, toId, messageId, (byte)messageCategory, timeout);
        }

        public int GetChat(out RetrievedMessage retrievedMessage, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
        {
            return GetMessage(out retrievedMessage, fromUid, toId, messageId, (byte)messageCategory, timeout);
        }

        public enum TranslateType
        {
            Chat,
            Mail
        }

        public enum ProfanityType
        {
            Off,
            Stop,
            Censor
        }

        public void Translate(Action<TranslatedInfo, int> callback, string text,
            TranslateLanguage destinationLanguage, TranslateLanguage sourceLanguage = TranslateLanguage.None,
            TranslateType type = TranslateType.Chat, ProfanityType profanity = ProfanityType.Off,
            int timeout = 0)
        {
            Translate(callback, text, GetTranslatedLanguage(destinationLanguage),
                GetTranslatedLanguage(sourceLanguage), type, profanity, timeout);
        }

        //-- Action<TranslatedInfo, errorCode>
        private void Translate(Action<TranslatedInfo, int> callback, string text,
            string destinationLanguage, string sourceLanguage = "",
            TranslateType type = TranslateType.Chat, ProfanityType profanity = ProfanityType.Off, long userId = 0, int timeout = 0)
        {
            Quest quest = GenerateQuest("translate");
            quest.Param("text", text);
            quest.Param("dst", destinationLanguage);

            if (sourceLanguage.Length > 0)
                quest.Param("src", sourceLanguage);

            if (type == TranslateType.Mail)
                quest.Param("type", "mail");
            else
                quest.Param("type", "chat");

            switch (profanity)
            {
                case ProfanityType.Stop: quest.Param("profanity", "stop"); break;
                case ProfanityType.Censor: quest.Param("profanity", "censor"); break;
                case ProfanityType.Off: quest.Param("profanity", "off"); break;
            }

            if (userId > 0)
                quest.Param("uid", userId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                TranslatedInfo tm = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        tm = new TranslatedInfo();
                        tm.sourceLanguage = answer.Want<string>("source");
                        tm.targetLanguage = answer.Want<string>("target");
                        tm.sourceText = answer.Want<string>("sourceText");
                        tm.targetText = answer.Want<string>("targetText");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(tm, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int Translate(out TranslatedInfo translatedinfo, string text,
            TranslateLanguage destinationLanguage, TranslateLanguage sourceLanguage = TranslateLanguage.None,
            TranslateType type = TranslateType.Chat, ProfanityType profanity = ProfanityType.Off, long userId = 0, int timeout = 0)
        {
            return Translate(out translatedinfo, text, GetTranslatedLanguage(destinationLanguage),
                GetTranslatedLanguage(sourceLanguage), type, profanity, userId, timeout);
        }

        private int Translate(out TranslatedInfo translatedinfo, string text,
            string destinationLanguage, string sourceLanguage = "",
            TranslateType type = TranslateType.Chat, ProfanityType profanity = ProfanityType.Off, long userId = 0, int timeout = 0)
        {
            translatedinfo = null;

            Quest quest = GenerateQuest("translate");
            quest.Param("text", text);
            quest.Param("dst", destinationLanguage);

            if (sourceLanguage.Length > 0)
                quest.Param("src", sourceLanguage);

            if (type == TranslateType.Mail)
                quest.Param("type", "mail");
            else
                quest.Param("type", "chat");

            switch (profanity)
            {
                case ProfanityType.Stop: quest.Param("profanity", "stop"); break;
                case ProfanityType.Censor: quest.Param("profanity", "censor"); break;
                case ProfanityType.Off: quest.Param("profanity", "off"); break;
            }

            if (userId > 0)
                quest.Param("uid", userId);

            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                translatedinfo = new TranslatedInfo
                {
                    sourceLanguage = answer.Want<string>("source"),
                    targetLanguage = answer.Want<string>("target"),
                    sourceText = answer.Want<string>("sourceText"),
                    targetText = answer.Want<string>("targetText")
                };

                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void Profanity(Action<string, List<string>, int> callback, string text, bool classify = false, long userId = 0, int timeout = 0)
        {
            Quest quest = GenerateQuest("profanity");
            quest.Param("text", text);
            quest.Param("classify", classify);
            if (userId > 0)
                quest.Param("uid", userId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                string resultText = "";
                List<string> classification = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        resultText = answer.Want<string>("text");
                        classification = GetStringList(answer, "classification");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(resultText, classification, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(string.Empty, null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int Profanity(out string resultText, out List<string> classification, string text, bool classify = false, long userId = 0, int timeout = 0)
        {
            resultText = "";
            classification = null;

            Quest quest = GenerateQuest("profanity");
            quest.Param("text", text);
            quest.Param("classify", classify);
            if (userId > 0)
                quest.Param("uid", userId);

            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                resultText = answer.Want<string>("text");
                classification = GetStringList(answer, "classification");

                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        //===========================[ Transcribe ]=========================//
        //-- Action<string text, string language, errorCode>
        public void Transcribe(Action<string, string, int> callback, byte[] audio, long? userId, int timeout = 120)
        {
            TranscribeInternal(callback, audio, null, userId, timeout);
        }

        public void Transcribe(Action<string, string, int> callback, byte[] audio, bool filterProfanity, long? userId, int timeout = 120)
        {
            TranscribeInternal(callback, audio, filterProfanity, userId, timeout);
        }

        private void TranscribeInternal(Action<string, string, int> callback, byte[] audio, bool? filterProfanity, long? userId, int timeout = 120)
        {
            RTMAudioData audioData = new RTMAudioData(audio);
            string cacheText = audioData.RecognitionText;
            string cacheLanguage = audioData.RecognitionLang;
            if (cacheText != "" && cacheLanguage != "")
            {
                ClientEngine.RunTask(() => {
                    callback(cacheText, cacheLanguage, fpnn.ErrorCode.FPNN_EC_OK);
                });
                return;
            }

            Quest quest = new Quest("transcribe");
            quest.Param("audio", audio);
            if (filterProfanity.HasValue)
                quest.Param("profanityFilter", filterProfanity.Value);
            if (userId.HasValue)
                quest.Param("uid", userId.Value);
            
            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                string resultText = "";
                string resultLanguage = "";

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        resultText = answer.Want<string>("text");
                        resultLanguage = answer.Want<string>("lang");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(resultText, resultLanguage, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(string.Empty, string.Empty, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int Transcribe(out string resultText, out string resultLanguage, byte[] audio, long? userId, int timeout = 120)
        {
            return TranscribeInternal(out resultText, out resultLanguage, audio, null, userId, timeout);
        }

        public int Transcribe(out string resultText, out string resultLanguage, byte[] audio, bool filterProfanity, long? userId, int timeout = 120)
        {
            return TranscribeInternal(out resultText, out resultLanguage, audio, filterProfanity, userId, timeout);
        }

        private int TranscribeInternal(out string resultText, out string resultLanguage, byte[] audio, bool? filterProfanity, long? userId, int timeout = 120)
        {
            RTMAudioData audioData = new RTMAudioData(audio);
            string cacheText = audioData.RecognitionText;
            string cacheLanguage = audioData.RecognitionLang;
            if (cacheText != "" && cacheLanguage != "")
            {
                resultText = cacheText;
                resultLanguage = cacheLanguage;
                return fpnn.ErrorCode.FPNN_EC_OK;
            }

            resultText = "";
            resultLanguage = null;

            Quest quest = new Quest("transcribe");
            quest.Param("audio", audio);
            if (filterProfanity.HasValue)
                quest.Param("profanityFilter", filterProfanity.Value);
            if (userId.HasValue)
                quest.Param("uid", userId.Value);

            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                resultText = answer.Want<string>("text");
                resultLanguage = answer.Want<string>("lang");

                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        //===========================[ sTranscribe ]=========================//
        //-- Action<string text, string language, errorCode>
        public void Transcribe(Action<string, string, int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 120)
        {
            TranscribeInternal(callback, fromUid, toId, messageId, messageCategory, null, timeout);
        }

        public void Transcribe(Action<string, string, int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, bool filterProfanity, int timeout = 120)
        {
            TranscribeInternal(callback, fromUid, toId, messageId, messageCategory, filterProfanity, timeout);
        }

        private void TranscribeInternal(Action<string, string, int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, bool? filterProfanity, int timeout = 120)
        {
            Quest quest = new Quest("stranscribe");
            quest.Param("from", fromUid);
            quest.Param("xid", toId);
            quest.Param("mid", messageId);
            quest.Param("type", (byte)messageCategory);

            if (filterProfanity.HasValue)
                quest.Param("profanityFilter", filterProfanity.Value);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                string resultText = "";
                string resultLanguage = "";

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        resultText = answer.Want<string>("text");
                        resultLanguage = answer.Want<string>("lang");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(resultText, resultLanguage, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(string.Empty, string.Empty, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int Transcribe(out string resultText, out string resultLanguage, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 120)
        {
            return TranscribeInternal(out resultText, out resultLanguage, fromUid, toId, messageId, messageCategory, null, timeout);
        }

        public int Transcribe(out string resultText, out string resultLanguage, long fromUid, long toId, long messageId, MessageCategory messageCategory, bool filterProfanity, int timeout = 120)
        {
            return TranscribeInternal(out resultText, out resultLanguage, fromUid, toId, messageId, messageCategory, filterProfanity, timeout);
        }

        private int TranscribeInternal(out string resultText, out string resultLanguage, long fromUid, long toId, long messageId, MessageCategory messageCategory, bool? filterProfanity, int timeout = 120)
        {
            resultText = "";
            resultLanguage = null;

            Quest quest = new Quest("stranscribe");
            quest.Param("from", fromUid);
            quest.Param("xid", toId);
            quest.Param("mid", messageId);
            quest.Param("type", (byte)messageCategory);

            if (filterProfanity.HasValue)
                quest.Param("profanityFilter", filterProfanity.Value);

            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                resultText = answer.Want<string>("text");
                resultLanguage = answer.Want<string>("lang");

                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }
    }
}