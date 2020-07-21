using System;
using System.Collections.Generic;
using com.fpnn.proto;

namespace com.fpnn.rtm
{
    public partial class RTMServerClient
    {
        private void MergeListenSet(HashSet<long> output, HashSet<long> input)
        {
            if (input == null)
                return;
            HashSet<long>.Enumerator em = input.GetEnumerator();
            while (em.MoveNext())
                output.Add(em.Current);
        }

        private void MergeListenSet(HashSet<string> output, HashSet<string> input)
        {
            if (input == null)
                return;
            HashSet<string>.Enumerator em = input.GetEnumerator();
            while (em.MoveNext())
                output.Add(em.Current);
        }

        private void RemoveListenSet(HashSet<long> output, HashSet<long> input)
        {
            if (input == null)
                return;
            HashSet<long>.Enumerator em = input.GetEnumerator();
            while (em.MoveNext())
                output.Remove(em.Current);
        }

        private void RemoveListenSet(HashSet<string> output, HashSet<string> input)
        {
            if (input == null)
                return;
            HashSet<string>.Enumerator em = input.GetEnumerator();
            while (em.MoveNext())
                output.Remove(em.Current);
        }

        public void AddListen(Action<int> callback, HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)
        {
            Quest quest = GenerateQuest("addlisten");
            if (groupIds != null)
                quest.Param("gids", groupIds);
            if (roomIds != null)
                quest.Param("rids", roomIds);
            if (userIds != null)
                quest.Param("uids", userIds);
            if (events != null)
                quest.Param("events", events);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == com.fpnn.ErrorCode.FPNN_EC_OK) {
                    lock(interLocker) {
                        MergeListenSet(listenStatusInfo.groupIds, groupIds);
                        MergeListenSet(listenStatusInfo.roomIds, roomIds);
                        MergeListenSet(listenStatusInfo.userIds, userIds);
                        MergeListenSet(listenStatusInfo.events, events);
                    }
                }
                callback(errorCode); 
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int AddListen(HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)
        {
            Quest quest = GenerateQuest("addlisten");
            if (groupIds != null)
                quest.Param("gids", groupIds);
            if (roomIds != null)
                quest.Param("rids", roomIds);
            if (userIds != null)
                quest.Param("uids", userIds);
            if (events != null)
                quest.Param("events", events);

            Answer answer = client.SendQuest(quest, timeout);
            int status = answer.ErrorCode();
            if (status == com.fpnn.ErrorCode.FPNN_EC_OK) {
                lock(interLocker) {
                    MergeListenSet(listenStatusInfo.groupIds, groupIds);
                    MergeListenSet(listenStatusInfo.roomIds, roomIds);
                    MergeListenSet(listenStatusInfo.userIds, userIds);
                    MergeListenSet(listenStatusInfo.events, events);
                }
            }
            return status;
        }

        public void RemoveListen(Action<int> callback, HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)
        {
            Quest quest = GenerateQuest("removelisten");
            if (groupIds != null)
                quest.Param("gids", groupIds);
            if (roomIds != null)
                quest.Param("rids", roomIds);
            if (userIds != null)
                quest.Param("uids", userIds);
            if (events != null)
                quest.Param("events", events);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => { 
                if (errorCode == com.fpnn.ErrorCode.FPNN_EC_OK) {
                    lock(interLocker) {
                        RemoveListenSet(listenStatusInfo.groupIds, groupIds);
                        RemoveListenSet(listenStatusInfo.roomIds, roomIds);
                        RemoveListenSet(listenStatusInfo.userIds, userIds);
                        RemoveListenSet(listenStatusInfo.events, events);
                    }
                }
                callback(errorCode); 
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int RemoveListen(HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)
        {
            Quest quest = GenerateQuest("removelisten");
            if (groupIds != null)
                quest.Param("gids", groupIds);
            if (roomIds != null)
                quest.Param("rids", roomIds);
            if (userIds != null)
                quest.Param("uids", userIds);
            if (events != null)
                quest.Param("events", events);

            Answer answer = client.SendQuest(quest, timeout);
            int status = answer.ErrorCode();
            if (status == com.fpnn.ErrorCode.FPNN_EC_OK) {
                lock(interLocker) {
                    RemoveListenSet(listenStatusInfo.groupIds, groupIds);
                    RemoveListenSet(listenStatusInfo.roomIds, roomIds);
                    RemoveListenSet(listenStatusInfo.userIds, userIds);
                    RemoveListenSet(listenStatusInfo.events, events);
                }
            }
            return status;
        }

        public void SetListen(Action<int> callback, HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)
        {
            Quest quest = GenerateQuest("setlisten");
            if (groupIds != null)
                quest.Param("gids", groupIds);
            if (roomIds != null)
                quest.Param("rids", roomIds);
            if (userIds != null)
                quest.Param("uids", userIds);
            if (events != null)
                quest.Param("events", events);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {
                if (errorCode == com.fpnn.ErrorCode.FPNN_EC_OK) {
                    lock(interLocker) {
                        if (groupIds != null)
                            listenStatusInfo.groupIds = groupIds;
                        if (roomIds != null)
                            listenStatusInfo.roomIds = roomIds;
                        if (userIds != null)
                            listenStatusInfo.userIds = userIds;
                        if (events != null)
                            listenStatusInfo.events = events;
                    }
                }
                callback(errorCode); 
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SetListen(HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)
        {
            Quest quest = GenerateQuest("setlisten");
            if (groupIds != null)
                quest.Param("gids", groupIds);
            if (roomIds != null)
                quest.Param("rids", roomIds);
            if (userIds != null)
                quest.Param("uids", userIds);
            if (events != null)
                quest.Param("events", events);

            Answer answer = client.SendQuest(quest, timeout);
            int status = answer.ErrorCode();
            if (status == com.fpnn.ErrorCode.FPNN_EC_OK) {
                lock(interLocker) {
                    if (groupIds != null)
                        listenStatusInfo.groupIds = groupIds;
                    if (roomIds != null)
                        listenStatusInfo.roomIds = roomIds;
                    if (userIds != null)
                        listenStatusInfo.userIds = userIds;
                    if (events != null)
                        listenStatusInfo.events = events;
                }
            }
            return status;
        }

        public void SetListen(Action<int> callback, bool allP2P, bool allGroups, bool allRooms, bool allEvents, int timeout = 0)
        {
            Quest quest = GenerateQuest("setlisten");
            if (allP2P)
                quest.Param("p2p", allP2P);
            if (allGroups)
                quest.Param("group", allGroups);
            if (allGroups)
                quest.Param("room", allRooms);
            if (allEvents)
                quest.Param("ev", allEvents);

            bool status = client.SendQuest(quest, (Answer answer, int errorCode) => {
                if (errorCode == com.fpnn.ErrorCode.FPNN_EC_OK) {
                    lock(interLocker) {
                        listenStatusInfo.allP2P = allP2P;
                        listenStatusInfo.allGroups = allGroups;
                        listenStatusInfo.allRooms = allRooms;
                        listenStatusInfo.allEvents = allEvents;
                    }
                }
                callback(errorCode);
            }, timeout);

            if (!status)
                ClientEngine.RunTask(() =>
                {
                     callback(fpnn.ErrorCode.FPNN_EC_CORE_INVALID_CONNECTION);
                });
        }

        public int SetListen(bool allP2P, bool allGroups, bool allRooms, bool allEvents, int timeout = 0)
        {
            Quest quest = GenerateQuest("setlisten");
            if (allP2P)
                quest.Param("p2p", allP2P);
            if (allGroups)
                quest.Param("group", allGroups);
            if (allGroups)
                quest.Param("room", allRooms);
            if (allEvents)
                quest.Param("ev", allEvents);

            Answer answer = client.SendQuest(quest, timeout);
            int status = answer.ErrorCode();
            if (status == com.fpnn.ErrorCode.FPNN_EC_OK) {
                lock(interLocker) {
                    listenStatusInfo.allP2P = allP2P;
                    listenStatusInfo.allGroups = allGroups;
                    listenStatusInfo.allRooms = allRooms;
                    listenStatusInfo.allEvents = allEvents;
                }
            }
            return status;
        }

        private void ListenStatusRestoration()
        {
            lock(interLocker) {
                if (listenStatusInfo.allP2P || listenStatusInfo.allGroups || listenStatusInfo.allRooms || listenStatusInfo.allEvents) {
                    SetListen((int errorCode) => {
                        if (errorCode != com.fpnn.ErrorCode.FPNN_EC_OK)
                            if (errorRecorder != null)
                                errorRecorder.RecordError("Listen Status Restoration(All Event) errorCode: " + errorCode);
                    }, listenStatusInfo.allP2P, listenStatusInfo.allGroups, listenStatusInfo.allRooms, listenStatusInfo.allEvents);
                }

                if (listenStatusInfo.userIds.Count > 0 || listenStatusInfo.groupIds.Count > 0 || listenStatusInfo.roomIds.Count > 0 || listenStatusInfo.events.Count > 0) {
                    SetListen((int errorCode) => {
                        if (errorCode != com.fpnn.ErrorCode.FPNN_EC_OK)
                            if (errorRecorder != null)
                                errorRecorder.RecordError("Listen Status Restoration(Event Set) errorCode: " + errorCode);
                    }, listenStatusInfo.groupIds, listenStatusInfo.roomIds, listenStatusInfo.userIds, listenStatusInfo.events);
                }
            }
        }
    }
}