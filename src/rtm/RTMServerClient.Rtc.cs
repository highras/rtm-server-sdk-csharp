using System;
using System.Collections.Generic;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public void InviteUserIntoRTCRoom(Action<int> callback, long roomId, HashSet<long> toUids, long fromUid, int timeout = 0)
        {
            Quest quest = GenerateQuest("inviteUserIntoRTCRoom");
            quest.Param("rid", roomId);
            quest.Param("toUids", toUids);
            quest.Param("fromUid", fromUid);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int InviteUserIntoRTCRoom(long roomId, HashSet<long> toUids, long fromUid, int timeout = 0)
        {
            Quest quest = GenerateQuest("inviteUserIntoRTCRoom");
            quest.Param("rid", roomId);
            quest.Param("toUids", toUids);
            quest.Param("fromUid", fromUid);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void CloseRTCRoom(Action<int> callback, long roomId, int timeout = 0)
        {
            Quest quest = GenerateQuest("closeRTCRoom");
            quest.Param("rid", roomId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int CloseRTCRoom(long roomId, int timeout = 0)
        {
            Quest quest = GenerateQuest("closeRTCRoom");
            quest.Param("rid", roomId);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void KickoutFromRTCRoom(Action<int> callback, long userId, long roomId, long fromUid, int timeout = 0)
        {
            Quest quest = GenerateQuest("kickoutFromRTCRoom");
            quest.Param("uid", userId);
            quest.Param("rid", roomId);
            quest.Param("fromUid", fromUid);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int KickoutFromRTCRoom(long userId, long roomId, long fromUid, int timeout = 0)
        {
            Quest quest = GenerateQuest("kickoutFromRTCRoom");
            quest.Param("uid", userId);
            quest.Param("rid", roomId);
            quest.Param("fromUid", fromUid);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void GetRTCRoomList(Action<HashSet<long>, int> callback, int timeout = 0)
        {
            Quest quest = GenerateQuest("getRTCRoomList");
            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                HashSet<long> rids = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        rids = WantLongHashSet(answer, "rids");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(rids, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetRTCRoomList(out HashSet<long> roomIds, int timeout = 0)
        {
            roomIds = null;

            Quest quest = GenerateQuest("getRTCRoomList");
            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                roomIds = WantLongHashSet(answer, "rids");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void GetRTCRoomMembers(Action<HashSet<long>, HashSet<long>, long, int> callback, long roomId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getRTCRoomMembers");
            quest.Param("rid", roomId);
            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                HashSet<long> uids = null;
                HashSet<long> administrators = null;
                long owner = -1;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        uids = WantLongHashSet(answer, "uids");
                        administrators = WantLongHashSet(answer, "administrators");
                        owner = answer.Get<long>("owner", -1);
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(uids, administrators, owner, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(null, null, -1, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetRTCRoomMembers(out HashSet<long> userIds, out HashSet<long> managerIds, out long owner, long roomId, int timeout = 0)
        {
            userIds = null;
            managerIds = null;
            owner = -1;

            Quest quest = GenerateQuest("getRTCRoomMembers");
            quest.Param("rid", roomId);
            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                userIds = WantLongHashSet(answer, "uids");
                managerIds = WantLongHashSet(answer, "administrators");
                owner = answer.Get<long>("owner", -1);
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void GetRTCRoomMemberCount(Action<int, int> callback, long roomId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getRTCRoomMemberCount");
            quest.Param("rid", roomId);
            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                int count = 0;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        count = answer.Get<int>("count", 0); 
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(count, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(0, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetRTCRoomMemberCount(out int count, long roomId, int timeout = 0)
        {
            count = 0;

            Quest quest = GenerateQuest("getRTCRoomMemberCount");
            quest.Param("rid", roomId);
            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                count = answer.Get<int>("count", 0); 
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void SetRTCRoomMicStatus(Action<int> callback, long roomId, bool status, int timeout = 0)
        {
            Quest quest = GenerateQuest("setRTCRoomMicStatus");
            quest.Param("rid", roomId);
            quest.Param("status", status);

            bool statusReturn = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!statusReturn)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SetRTCRoomMicStatus(long roomId, bool status, int timeout = 0)
        {
            Quest quest = GenerateQuest("setRTCRoomMicStatus");
            quest.Param("rid", roomId);
            quest.Param("status", status);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void PullIntoRTCRoom(Action<int> callback, long roomId, HashSet<long> toUids, int type, int timeout = 0)
        {
            Quest quest = GenerateQuest("pullIntoRTCRoom");
            quest.Param("rid", roomId);
            quest.Param("toUids", toUids);
            quest.Param("type", type);

            bool statusReturn = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!statusReturn)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int PullIntoRTCRoom(long roomId, HashSet<long> toUids, int type, int timeout = 0)
        {
            Quest quest = GenerateQuest("pullIntoRTCRoom");
            quest.Param("rid", roomId);
            quest.Param("toUids", toUids);
            quest.Param("type", type);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void AdminCommand(Action<int> callback, long roomId, HashSet<long> uids, int command, int timeout = 0)
        {
            Quest quest = GenerateQuest("adminCommand");
            quest.Param("rid", roomId);
            quest.Param("uids", uids);
            quest.Param("command", command);

            bool statusReturn = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!statusReturn)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int AdminCommand(long roomId, HashSet<long> uids, int command, int timeout = 0)
        {
            Quest quest = GenerateQuest("adminCommand");
            quest.Param("rid", roomId);
            quest.Param("uids", uids);
            quest.Param("command", command);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }
    }
}