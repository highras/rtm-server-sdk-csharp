using System;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public void GetToken(Action<string, int> callback, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("gettoken");
            quest.Param("uid", userId);
            quest.Param("version", "csharp-" + RTMServerConfig.SDKVersion);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                string token = null;
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try { 
                        token = answer.Get<string>("token", null); 
                    } catch (Exception) {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(token, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetToken(out string token, long userId, int timeout = 0)
        {
            token = null;

            Quest quest = GenerateQuest("gettoken");
            quest.Param("uid", userId);
            quest.Param("version", "csharp-" + RTMServerConfig.SDKVersion);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                token = answer.Get<string>("token", null);
                return fpnn.ErrorCode.FPNN_EC_OK;
            } catch (Exception) {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void RemoveToken(Action<int> callback, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("removetoken");
            quest.Param("uid", userId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                callback(errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int RemoveToken(long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("removetoken");
            quest.Param("uid", userId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

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

    }
}