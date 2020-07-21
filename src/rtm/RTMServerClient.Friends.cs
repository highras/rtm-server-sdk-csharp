using System;
using System.Collections.Generic;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        public void AddFriends(Action<int> callback, long userId, HashSet<long> friendUserIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("addfriends");
            quest.Param("uid", userId);
            quest.Param("friends", friendUserIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int AddFriends(long userId, HashSet<long> friendUserIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("addfriends");
            quest.Param("uid", userId);
            quest.Param("friends", friendUserIds);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void DeleteFriends(Action<int> callback, long userId, HashSet<long> friendUserIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("delfriends");
            quest.Param("uid", userId);
            quest.Param("friends", friendUserIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int DeleteFriends(long userId, HashSet<long> friendUserIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("delfriends");
            quest.Param("uid", userId);
            quest.Param("friends", friendUserIds);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void GetFriends(Action<HashSet<long>, int> callback, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getfriends");
            quest.Param("uid", userId);
            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                HashSet<long> friends = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        friends = WantLongHashSet(answer, "uids");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(friends, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetFriends(out HashSet<long> friends, long userId, int timeout = 0)
        {
            friends = null;

            Quest quest = GenerateQuest("getfriends");
            quest.Param("uid", userId);
            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                friends = WantLongHashSet(answer, "uids");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void IsFriend(Action<bool, int> callback, long userId, long otherUserId, int timeout = 0)
        {
            Quest quest = GenerateQuest("isfriend");
            quest.Param("uid", userId);
            quest.Param("fuid", otherUserId);

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

        public int IsFriend(out bool ok, long userId, long otherUserId, int timeout = 0)
        {
            ok = false;

            Quest quest = GenerateQuest("isfriend");
            quest.Param("uid", userId);
            quest.Param("fuid", otherUserId);

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

        public void IsFriends(Action<HashSet<long>, int> callback, long userId, HashSet<long> otherUserIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("isfriends");
            quest.Param("uid", userId);
            quest.Param("fuids", otherUserIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                HashSet<long> fuids = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        fuids = WantLongHashSet(answer, "fuids");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(fuids, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int IsFriends(out HashSet<long> fuids, long userId, HashSet<long> otherUserIds, int timeout = 0)
        {
            fuids = null;

            Quest quest = GenerateQuest("isfriends");
            quest.Param("uid", userId);
            quest.Param("fuids", otherUserIds);

            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                fuids = WantLongHashSet(answer, "fuids");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void AddBlacklist(Action<int> callback, long userId, HashSet<long> blackUserIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("addblacks");
            quest.Param("uid", userId);
            quest.Param("blacks", blackUserIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int AddBlacklist(long userId, HashSet<long> blackUserIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("addblacks");
            quest.Param("uid", userId);
            quest.Param("blacks", blackUserIds);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void DeleteBlacklist(Action<int> callback, long userId, HashSet<long> blackUserIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("delblacks");
            quest.Param("uid", userId);
            quest.Param("blacks", blackUserIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { callback(errorCode); }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int DeleteBlacklist(long userId, HashSet<long> blackUserIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("delblacks");
            quest.Param("uid", userId);
            quest.Param("blacks", blackUserIds);

            Answer answer = client.SendQuest(quest, timeout);
            return answer.ErrorCode();
        }

        public void IsInBlackList(Action<bool, int> callback, long userId, long otherUserId, int timeout = 0)
        {
            Quest quest = GenerateQuest("isblack");
            quest.Param("uid", userId);
            quest.Param("buid", otherUserId);

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

        public int IsInBlackList(out bool ok, long userId, long otherUserId, int timeout = 0)
        {
            ok = false;

            Quest quest = GenerateQuest("isblack");
            quest.Param("uid", userId);
            quest.Param("buid", otherUserId);

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

        public void IsInBlackList(Action<HashSet<long>, int> callback, long userId, HashSet<long> otherUserIds, int timeout = 0)
        {
            Quest quest = GenerateQuest("isblacks");
            quest.Param("uid", userId);
            quest.Param("buids", otherUserIds);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                HashSet<long> friends = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        friends = WantLongHashSet(answer, "buids");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                    callback(friends, errorCode);
                }
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int IsInBlackList(out HashSet<long> blackUids, long userId, HashSet<long> otherUserIds, int timeout = 0)
        {
            blackUids = null;

            Quest quest = GenerateQuest("isblacks");
            quest.Param("uid", userId);
            quest.Param("buids", otherUserIds);

            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                blackUids = WantLongHashSet(answer, "buids");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }

        public void GetBlacklist(Action<HashSet<long>, int> callback, long userId, int timeout = 0)
        {
            Quest quest = GenerateQuest("getblacks");
            quest.Param("uid", userId);
            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {

                HashSet<long> friends = null;

                if (errorCode == fpnn.ErrorCode.FPNN_EC_OK)
                {
                    try
                    {
                        friends = WantLongHashSet(answer, "uids");
                    }
                    catch (Exception)
                    {
                        errorCode = fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
                    }
                }
                callback(friends, errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                    callback(null, fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int GetBlacklist(out HashSet<long> uids, long userId, int timeout = 0)
        {
            uids = null;

            Quest quest = GenerateQuest("getblacks");
            quest.Param("uid", userId);
            Answer answer = client.SendQuest(quest, timeout);

            if (answer.IsException())
                return answer.ErrorCode();

            try
            {
                uids = WantLongHashSet(answer, "uids");
                return fpnn.ErrorCode.FPNN_EC_OK;
            }
            catch (Exception)
            {
                return fpnn.ErrorCode.FPNN_EC_CORE_INVALID_PACKAGE;
            }
        }



    }
}