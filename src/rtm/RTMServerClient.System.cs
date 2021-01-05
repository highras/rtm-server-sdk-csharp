using System;
using System.Collections.Generic;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public void Kickout(Action<int> callback, long userId, string clientEndpoint = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("kickout");
            quest.Param("uid", userId);
            if (clientEndpoint != null)
                quest.Param("ce", clientEndpoint);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                callback(errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int Kickout(long userId, string clientEndpoint = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("kickout");
            quest.Param("uid", userId);
            if (clientEndpoint != null)
                quest.Param("ce", clientEndpoint);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }
        
        public void AddDevice(Action<int> callback, long userId, string appType, string deviceToken, int timeout = 0)
        {
            Quest quest = GenerateQuest("adddevice");
            quest.Param("uid", userId);
            quest.Param("apptype", appType);
            quest.Param("devicetoken", deviceToken);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                callback(errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int AddDevice(long userId, string appType, string deviceToken, int timeout = 0)
        {
            Quest quest = GenerateQuest("adddevice");
            quest.Param("uid", userId);
            quest.Param("apptype", appType);
            quest.Param("devicetoken", deviceToken);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void RemoveDevice(Action<int> callback, long userId, string deviceToken, int timeout = 0)
        {
            Quest quest = GenerateQuest("removedevice");
            quest.Param("uid", userId);
            quest.Param("devicetoken", deviceToken);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                callback(errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int RemoveDevice(long userId, string deviceToken, int timeout = 0)
        {
            Quest quest = GenerateQuest("removedevice");
            quest.Param("uid", userId);
            quest.Param("devicetoken", deviceToken);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void AddDevicePushOption(Action<int> callback, long userId, MessageCategory messageCategory, long targetId, HashSet<byte> mTypes = null, int timeout = 0)
        {
            byte type = 99;
            switch (messageCategory)
            {
                case MessageCategory.P2PMessage:
                    type = 0; break;
                case MessageCategory.GroupMessage:
                    type = 1; break;
            }
            if (type > 1) {
                ClientEngine.RunTask(() =>
                {
                    callback(rtm.RTMErrorCode.RTM_EC_INVALID_PARAMETER);
                });
                return;
            }
            Quest quest = GenerateQuest("addoption");
            quest.Param("uid", userId);
            quest.Param("type", type);
            quest.Param("xid", targetId);
            if (mTypes != null)
                quest.Param("mtypes", mTypes);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                callback(errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int AddDevicePushOption(long userId, MessageCategory messageCategory, long targetId, HashSet<byte> mTypes = null, int timeout = 0)
        {
            byte type = 99;
            switch (messageCategory)
            {
                case MessageCategory.P2PMessage:
                    type = 0; break;
                case MessageCategory.GroupMessage:
                    type = 1; break;
            }
            if (type > 1) {
                return rtm.RTMErrorCode.RTM_EC_INVALID_PARAMETER;
            }
            Quest quest = GenerateQuest("addoption");
            quest.Param("uid", userId);
            quest.Param("type", type);
            quest.Param("xid", targetId);
            if (mTypes != null)
                quest.Param("mtypes", mTypes);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void RemoveDevicePushOption(Action<int> callback, long userId, MessageCategory messageCategory, long targetId, HashSet<byte> mTypes = null, int timeout = 0)
        {
            byte type = 99;
            switch (messageCategory)
            {
                case MessageCategory.P2PMessage:
                    type = 0; break;
                case MessageCategory.GroupMessage:
                    type = 1; break;
            }
            if (type > 1) {
                ClientEngine.RunTask(() =>
                {
                    callback(rtm.RTMErrorCode.RTM_EC_INVALID_PARAMETER);
                });
                return;
            }
            Quest quest = GenerateQuest("removeoption");
            quest.Param("uid", userId);
            quest.Param("type", type);
            quest.Param("xid", targetId);
            if (mTypes != null)
                quest.Param("mtypes", mTypes);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                callback(errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int RemoveDevicePushOption(long userId, MessageCategory messageCategory, long targetId, HashSet<byte> mTypes = null, int timeout = 0)
        {
            byte type = 99;
            switch (messageCategory)
            {
                case MessageCategory.P2PMessage:
                    type = 0; break;
                case MessageCategory.GroupMessage:
                    type = 1; break;
            }
            if (type > 1) {
                return rtm.RTMErrorCode.RTM_EC_INVALID_PARAMETER;
            }
            Quest quest = GenerateQuest("removeoption");
            quest.Param("uid", userId);
            quest.Param("type", type);
            quest.Param("xid", targetId);
            if (mTypes != null)
                quest.Param("mtypes", mTypes);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }
        private Dictionary<long, HashSet<byte>> WantLongByteHashSetDictionary(Message message, string key)
        {
            Dictionary <long, HashSet<byte>> rev = new Dictionary<long, HashSet<byte>>();

            Dictionary<object, object> originalDict = (Dictionary<object, object>)message.Want(key);
            foreach (KeyValuePair<object, object> kvp in originalDict)
            {
                List<object> originalList = (List<object>)(kvp.Value);
                HashSet<byte> resultSet = new HashSet<byte>();

                foreach (object obj in originalList)
                {
                    resultSet.Add((byte)Convert.ChangeType(obj, TypeCode.Byte));
                }
                
                rev.Add((long)Convert.ChangeType(kvp.Key, TypeCode.Int64), resultSet);
            }

            return rev;
        }

        public void GetDevicePushOption(Action<Dictionary<long, HashSet<byte>>, Dictionary<long, HashSet<byte>>, int> callback, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getoption");
            quest.Param("uid", userId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {
                Dictionary<long, HashSet<byte>> p2pDictionary = null;
                Dictionary<long, HashSet<byte>> groupDictionary = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        p2pDictionary = WantLongByteHashSetDictionary(answer, "p2p");
                        groupDictionary = WantLongByteHashSetDictionary(answer, "group");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(p2pDictionary, groupDictionary, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetDevicePushOption(out Dictionary<long, HashSet<byte>> p2pDictionary, out Dictionary<long, HashSet<byte>> groupDictionary, long userId, int timeout = 0)
        {
            p2pDictionary = null;
            groupDictionary = null;

            Quest quest = GenerateQuest("getoption");
            quest.Param("uid", userId);
            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                p2pDictionary = WantLongByteHashSetDictionary(answer, "p2p");
                groupDictionary = WantLongByteHashSetDictionary(answer, "group");

                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

    }
}