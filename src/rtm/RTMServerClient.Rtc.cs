using System;
using System.Collections.Generic;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public void InviteUserIntoVoiceRoom(Action<int> callback, long roomId, HashSet<long> toUids, long fromUid, int timeout = 0)
        {
            Quest quest = GenerateQuest("inviteUserIntoVoiceRoom");
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

        public int InviteUserIntoVoiceRoom(long roomId, HashSet<long> toUids, long fromUid, int timeout = 0)
        {
            Quest quest = GenerateQuest("inviteUserIntoVoiceRoom");
            quest.Param("rid", roomId);
            quest.Param("toUids", toUids);
            quest.Param("fromUid", fromUid);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void CloseVoiceRoom(Action<int> callback, long roomId, int timeout = 0)
        {
            Quest quest = GenerateQuest("closeVoiceRoom");
            quest.Param("rid", roomId);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int CloseVoiceRoom(long roomId, int timeout = 0)
        {
            Quest quest = GenerateQuest("closeVoiceRoom");
            quest.Param("rid", roomId);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void KickoutFromVoiceRoom(Action<int> callback, long userId, long roomId, long fromUid, int timeout = 0)
        {
            Quest quest = GenerateQuest("kickoutFromVoiceRoom");
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

        public int KickoutFromVoiceRoom(long userId, long roomId, long fromUid, int timeout = 0)
        {
            Quest quest = GenerateQuest("kickoutFromVoiceRoom");
            quest.Param("uid", userId);
            quest.Param("rid", roomId);
            quest.Param("fromUid", fromUid);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void GetVoiceRoomList(Action<HashSet<long>, int> callback, int timeout = 0)
        {
            Quest quest = GenerateQuest("getVoiceRoomList");
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

        public int GetVoiceRoomList(out HashSet<long> roomIds, int timeout = 0)
        {
            roomIds = null;

            Quest quest = GenerateQuest("getVoiceRoomList");
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

        public void GetVoiceRoomMembers(Action<HashSet<long>, HashSet<long>, int> callback, long roomId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getVoiceRoomMembers");
            quest.Param("rid", roomId);
            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                HashSet<long> uids = null;
                HashSet<long> managers = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        uids = WantLongHashSet(answer, "uids");
                        managers = WantLongHashSet(answer, "managers");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(uids, managers, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(null, null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetVoiceRoomMembers(out HashSet<long> userIds, out HashSet<long> managerIds, long roomId, int timeout = 0)
        {
            userIds = null;
            managerIds = null;

            Quest quest = GenerateQuest("getVoiceRoomMembers");
            quest.Param("rid", roomId);
            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                userIds = WantLongHashSet(answer, "uids");
                managerIds = WantLongHashSet(answer, "managers");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void GetVoiceRoomMemberCount(Action<int, int> callback, long roomId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getVoiceRoomMemberCount");
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

        public int GetVoiceRoomMemberCount(out int count, long roomId, int timeout = 0)
        {
            count = 0;

            Quest quest = GenerateQuest("getVoiceRoomMemberCount");
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

        public void SetVoiceRoomMicStatus(Action<int> callback, long roomId, bool status, int timeout = 0)
        {
            Quest quest = GenerateQuest("setVoiceRoomMicStatus");
            quest.Param("rid", roomId);
            quest.Param("status", status);

            bool statusReturn = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!statusReturn)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SetVoiceRoomMicStatus(long roomId, bool status, int timeout = 0)
        {
            Quest quest = GenerateQuest("setVoiceRoomMicStatus");
            quest.Param("rid", roomId);
            quest.Param("status", status);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void PullIntoVoiceRoom(Action<int> callback, long roomId, HashSet<long> toUids, int timeout = 0)
        {
            Quest quest = GenerateQuest("pullIntoVoiceRoom");
            quest.Param("rid", roomId);
            quest.Param("toUids", toUids);

            bool statusReturn = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!statusReturn)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int PullIntoVoiceRoom(long roomId, HashSet<long> toUids, int timeout = 0)
        {
            Quest quest = GenerateQuest("pullIntoVoiceRoom");
            quest.Param("rid", roomId);
            quest.Param("toUids", toUids);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

    }
}