using System;
using System.Collections.Generic;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        //===========================[ Get Online Users ]=========================//
        //-- Action<online_uids, errorCode>
        public void GetOnlineUsers(Action<HashSet<long>, int> callback, HashSet<long> userIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("getonlineusers");
            quest.Param("uids", userIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                HashSet<long> onlineUids = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        onlineUids = WantLongHashSet(answer, "uids");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(onlineUids, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetOnlineUsers(out HashSet<long> onlineUids, HashSet<long> userIds, int timeout = 0)
        {
            onlineUids = null;

            Quest quest = GenerateQuest("getonlineusers");
            quest.Param("uids", userIds);

            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                onlineUids = WantLongHashSet(answer, "uids");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void AddProjectBlack(Action<int> callback, long userId, int banTime, int timeout = 0)
        {
            Quest quest = GenerateQuest("addprojectblack");
            quest.Param("uid", userId);
            quest.Param("btime", banTime);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                callback(errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int AddProjectBlack(long uid, int banTime, int timeout = 0)
        {
            Quest quest = GenerateQuest("addprojectblack");
            quest.Param("uid", uid);
            quest.Param("btime", banTime);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void RemoveProjectBlack(Action<int> callback, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("removeprojectblack");
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

        public int RemoveProjectBlack(long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("removeprojectblack");
            quest.Param("uid", userId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void IsProjectBlack(Action<bool, int> callback, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("isprojectblack");
            quest.Param("uid", userId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                bool ok = false;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        ok = answer.Get<bool>("ok", false); 
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(ok, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(false, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int IsProjectBlack(out bool ok, long userId, int timeout = 0)
        {
            ok = false;

            Quest quest = GenerateQuest("isprojectblack");
            quest.Param("uid", userId);

            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                ok = answer.Get<bool>("ok", false); 
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        //===========================[ Set User Info ]=========================//
        public void SetUserInfo(Action<int> callback, long userId, string publicInfo = null, string privateInfo = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("setuserinfo");
            quest.Param("uid", userId);
            if (publicInfo != null)
                quest.Param("oinfo", publicInfo);
            if (privateInfo != null)
                quest.Param("pinfo", privateInfo);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SetUserInfo(long userId, string publicInfo = null, string privateInfo = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("setuserinfo");
            quest.Param("uid", userId);
            if (publicInfo != null)
                quest.Param("oinfo", publicInfo);
            if (privateInfo != null)
                quest.Param("pinfo", privateInfo);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        //===========================[ Get User Info ]=========================//
        //-- Action<publicInfo, privateInfo, errorCode>
        public void GetUserInfo(Action<string, string, int> callback, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getuserinfo");
            quest.Param("uid", userId);
            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                string publicInfo = null;
                string privateInfo = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        publicInfo = answer.Want<string>("oinfo");
                        privateInfo = answer.Want<string> ("pinfo");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(publicInfo, privateInfo, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(null, null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetUserInfo(out string publicInfo, out string privateInfo, long userId, int timeout = 0)
        {
            publicInfo = null;
            privateInfo = null;

            Quest quest = GenerateQuest("getuserinfo");
            quest.Param("uid", userId);
            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                publicInfo = answer.Want<string>("oinfo");
                privateInfo = answer.Want<string>("pinfo");

                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        //===========================[ Get User Open Info ]=========================//
        //-- Action<Dictionary<string_uid, public_info>, errorCode>
        public void GetUserPublicInfo(Action<Dictionary<string, string>, int> callback, HashSet<long> userIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("getuseropeninfo");
            quest.Param("uids", userIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                Dictionary<string, string> publicInfos = null;
                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        publicInfos = WantStringDictionary(answer, "info");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(publicInfos, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetUserPublicInfo(out Dictionary<string, string> publicInfos, HashSet<long> userIds, int timeout = 0)
        {
            publicInfos = null;

            Quest quest = GenerateQuest("getuseropeninfo");
            quest.Param("uids", userIds);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                publicInfos = WantStringDictionary(answer, "info");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }
    }
}