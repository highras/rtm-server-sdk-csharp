using System;
using System.Collections.Generic;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public void AddRoomBan(Action<int> callback, long roomId, long userId, int banTime, int timeout = 0)
        {
            Quest quest = GenerateQuest("addroomban");
            quest.Param("rid", roomId);
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

        public int AddRoomBan(long roomId, long userId, int banTime, int timeout = 0)
        {
            Quest quest = GenerateQuest("addroomban");
            quest.Param("rid", roomId);
            quest.Param("uid", userId);
            quest.Param("btime", banTime);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void AddRoomBan(Action<int> callback, long userId, int banTime, int timeout = 0)
        {
            Quest quest = GenerateQuest("addroomban");
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

        public int AddRoomBan(long userId, int banTime, int timeout = 0)
        {
            Quest quest = GenerateQuest("addroomban");
            quest.Param("uid", userId);
            quest.Param("btime", banTime);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void RemoveRoomBan(Action<int> callback, long roomId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("removeroomban");
            quest.Param("rid", roomId);
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

        public int RemoveRoomBan(long roomId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("removeroomban");
            quest.Param("rid", roomId);
            quest.Param("uid", userId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void RemoveRoomBan(Action<int> callback, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("removeroomban");
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

        public int RemoveRoomBan(long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("removeroomban");
            quest.Param("uid", userId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void IsBanOfRoom(Action<bool, int> callback, long roomId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("isbanofroom");
            quest.Param("rid", roomId);
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

        public int IsBanOfRoom(out bool ok, long roomId, long userId, int timeout = 0)
        {
            ok = false;

            Quest quest = GenerateQuest("isbanofroom");
            quest.Param("rid", roomId);
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

        public void AddRoomMember(Action<int> callback, long roomId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("addroommember");
            quest.Param("rid", roomId);
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

        public int AddRoomMember(long roomId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("addroommember");
            quest.Param("rid", roomId);
            quest.Param("uid", userId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void DeleteRoomMember(Action<int> callback, long roomId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("delroommember");
            quest.Param("rid", roomId);
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

        public int DeleteRoomMember(long roomId, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("delroommember");
            quest.Param("rid", roomId);
            quest.Param("uid", userId);

            Answer answer = client.SendQuest(quest, timeout);
            if (answer.IsException())
                return answer.ErrorCode();

            return fpnn.ErrorCode.FPNN_EC_OK;
        }

        public void SetRoomInfo(Action<int> callback, long roomId, string publicInfo = null, string privateInfo = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("setroominfo");
            quest.Param("rid", roomId);
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

        public int SetRoomInfo(long roomId, string publicInfo = null, string privateInfo = null, int timeout = 0)
        {
            Quest quest = GenerateQuest("setroominfo");
            quest.Param("rid", roomId);
            if (publicInfo != null)
                quest.Param("oinfo", publicInfo);
            if (privateInfo != null)
                quest.Param("pinfo", privateInfo);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void GetRoomInfo(Action<string, string, int> callback, long roomId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getroominfo");
            quest.Param("rid", roomId);

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
                    callback("", "", fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetRoomInfo(out string publicInfo, out string privateInfo, long roomId, int timeout = 0)
        {
            publicInfo = null;
            privateInfo = null;

            Quest quest = GenerateQuest("getroominfo");
            quest.Param("rid", roomId);
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

        public void GetRoomMembers(Action<HashSet<long>, int> callback, long roomId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getroommembers");
            quest.Param("rid", roomId);

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

        public int GetRoomMembers(out HashSet<long> userIds, long roomId, int timeout = 0)
        {
            userIds = null;

            Quest quest = GenerateQuest("getroommembers");
            quest.Param("rid", roomId);
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

        public void GetRoomMemberCount(Action<Dictionary<long, int>, int> callback, HashSet<long> roomIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("getroomcount");
            quest.Param("rids", roomIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                Dictionary<long, int> counts = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        counts = WantLongIntDictionary(answer, "cn"); 
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(counts, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetRoomMemberCount(out Dictionary<long, int> counts, HashSet<long> roomIds, int timeout = 0)
        {
            counts = null;

            Quest quest = GenerateQuest("getroomcount");
            quest.Param("rids", roomIds);

            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                counts = WantLongIntDictionary(answer, "cn");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }
    }
}