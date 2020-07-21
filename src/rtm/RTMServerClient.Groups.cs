using System;
using System.Collections.Generic;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public void AddGroupMembers(Action<int> callback, long groupId, HashSet<long> userIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("addgroupmembers");
            quest.Param("gid", groupId);
            quest.Param("uids", userIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int AddGroupMembers(long groupId, HashSet<long> userIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("addgroupmembers");
            quest.Param("gid", groupId);
            quest.Param("uids", userIds);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void DeleteGroupMembers(Action<int> callback, long groupId, HashSet<long> userIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("delgroupmembers");
            quest.Param("gid", groupId);
            quest.Param("uids", userIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int DeleteGroupMembers(long groupId, HashSet<long> userIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("delgroupmembers");
            quest.Param("gid", groupId);
            quest.Param("uids", userIds);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void DeleteGroup(Action<int> callback, long groupId, int timeout = 0)
        {
            Quest quest = GenerateQuest("delgroup");
            quest.Param("gid", groupId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int DeleteGroup(long groupId, int timeout = 0)
        {
            Quest quest = GenerateQuest("delgroup");
            quest.Param("gid", groupId);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void GetGroupMembers(Action<HashSet<long>, int> callback, long groupId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getgroupmembers");
            quest.Param("gid", groupId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                HashSet<long> uids = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        uids = WantLongHashSet(answer, "uids");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(uids, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetGroupMembers(out HashSet<long> userIds, long groupId, int timeout = 0)
        {
            userIds = null;

            Quest quest = GenerateQuest("getgroupmembers");
            quest.Param("gid", groupId);
            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                userIds = WantLongHashSet(answer, "uids");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void IsGroupMember(Action<bool, int> callback, long groupId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("isgroupmember");
            quest.Param("gid", groupId);
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

        public int IsGroupMember(out bool ok, long groupId, long userId, int timeout = 0)
        {
            ok = false;

            Quest quest = GenerateQuest("isgroupmember");
            quest.Param("gid", groupId);
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

        public void GetUserGroups(Action<HashSet<long>, int> callback, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getusergroups");
            quest.Param("uid", userId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                HashSet<long> groupIds = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        groupIds = WantLongHashSet(answer, "gids");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(groupIds, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetUserGroups(out HashSet<long> groupIds, long userId, int timeout = 0)
        {
            groupIds = null;

            Quest quest = GenerateQuest("getusergroups");
            quest.Param("uid", userId);
            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                groupIds = WantLongHashSet(answer, "gids");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void AddGroupBan(Action<int> callback, long groupId, long userId, int banTime, int timeout = 0)
        {
            Quest quest = GenerateQuest("addgroupban");
            quest.Param("gid", groupId);
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

        public int AddGroupBan(long groupId, long userId, int banTime, int timeout = 0)
        {
            Quest quest = GenerateQuest("addgroupban");
            quest.Param("gid", groupId);
            quest.Param("uid", userId);
            quest.Param("btime", banTime);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void RemoveGroupBan(Action<int> callback, long groupId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("removegroupban");
            quest.Param("gid", groupId);
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

        public int RemoveGroupBan(long groupId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("removegroupban");
            quest.Param("gid", groupId);
            quest.Param("uid", userId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void IsBanOfGroup(Action<bool, int> callback, long groupId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("isbanofgroup");
            quest.Param("gid", groupId);
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

        public int IsBanOfGroup(out bool ok, long groupId, long userId, int timeout = 0)
        {
            ok = false;

            Quest quest = GenerateQuest("isbanofgroup");
            quest.Param("gid", groupId);
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

        public void SetGroupInfo(Action<int> callback, long groupId, string publicInfo = null, string privateInfo = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("setgroupinfo");
            quest.Param("gid", groupId);
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

        public int SetGroupInfo(long groupId, string publicInfo = null, string privateInfo = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("setgroupinfo");
            quest.Param("gid", groupId);
            if (publicInfo != null)
                quest.Param("oinfo", publicInfo);
            if (privateInfo != null)
                quest.Param("pinfo", privateInfo);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void GetGroupInfo(Action<string, string, int> callback, long groupId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getgroupinfo");
            quest.Param("gid", groupId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                string publicInfo = "";
                string privateInfo = "";

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        publicInfo = answer.Want<string>("oinfo");
                        privateInfo = answer.Want<string>("pinfo");
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

        public int GetGroupInfo(out string publicInfo, out string privateInfo, long groupId, int timeout = 0)
        {
            publicInfo = null;
            privateInfo = null;

            Quest quest = GenerateQuest("getgroupinfo");
            quest.Param("gid", groupId);
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

    }
}