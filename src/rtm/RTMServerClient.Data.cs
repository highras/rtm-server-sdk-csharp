using System;
using System.Collections.Generic;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public void DataGet(Action<string, int> callback, long userId, string key, int timeout = 0)
        {
            Quest quest = GenerateQuest("dataget");
            quest.Param("uid", userId);
            quest.Param("key", key);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                string value = null;
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    { value = answer.Get<string>("val", null); }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(value, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int DataGet(out string value, long userId, string key, int timeout = 0)
        {
            value = null;

            Quest quest = GenerateQuest("dataget");
            quest.Param("uid", userId);
            quest.Param("key", key);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                value = answer.Get<string>("val", null);
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void DataSet(Action<int> callback, long userId, string key, string value, int timeout = 0)
        {
            Quest quest = GenerateQuest("dataset");
            quest.Param("uid", userId);
            quest.Param("key", key);
            quest.Param("val", value);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int DataSet(long userId, string key, string value, int timeout = 0)
        {
            Quest quest = GenerateQuest("dataset");
            quest.Param("uid", userId);
            quest.Param("key", key);
            quest.Param("val", value);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void DataDelete(Action<int> callback, long userId, string key, int timeout = 0)
        {
            Quest quest = GenerateQuest("datadel");
            quest.Param("uid", userId);
            quest.Param("key", key);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int DataDelete(long userId, string key, int timeout = 0)
        {
            Quest quest = GenerateQuest("datadel");
            quest.Param("uid", userId);
            quest.Param("key", key);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }
    }
}