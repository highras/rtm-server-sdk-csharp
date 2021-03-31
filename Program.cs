using System;
using System.Threading;
using System.Collections.Generic;
using com.fpnn.rtm;

namespace com.fpnn.rtm.example
{
    public class MyQuestProcessor: RTMQuestProcessor
    {
        //-- Event
        public override void PushEvent(int projectId, string eventName, long userId, int eventTime, string endpoint, string data) { 
            Console.WriteLine("[ServerPush PushEvent] projectId: " + projectId + " eventName: " + eventName + " userId: " + userId + " eventTime: " + eventTime + " endpoint: " + endpoint + " data: " + data);
        }

        //-- Messages
        public override void PushMessage(RTMMessage message) {
            ShowMessage("PushMessage", message);
        }
        public override void PushGroupMessage(RTMMessage message) {
            ShowMessage("PushGroupMessage", message);
        }
        public override void PushRoomMessage(RTMMessage message) {
            ShowMessage("PushRoomMessage", message);
        }

        //-- Chat
        public override void PushChat(RTMMessage message) {
            ShowMessage("PushChat", message);
        }
        public override void PushGroupChat(RTMMessage message) {
            ShowMessage("PushGroupChat", message);
        }
        public override void PushRoomChat(RTMMessage message) {
            ShowMessage("PushRoomChat", message);
        }

        //-- Cmd
        public override void PushCmd(RTMMessage message) {
            ShowMessage("PushCmd", message);
        }
        public override void PushGroupCmd(RTMMessage message) {
            ShowMessage("PushGroupCmd", message);
        }
        public override void PushRoomCmd(RTMMessage message) {
            ShowMessage("PushRoomCmd", message);
        }

        //-- Files
        public override void PushFile(RTMMessage message) {
            ShowMessage("PushFile", message);
        }
        public override void PushGroupFile(RTMMessage message) {
            ShowMessage("PushGroupFile", message);
        }
        public override void PushRoomFile(RTMMessage message) {
            ShowMessage("PushRoomFile", message);
        }

        private void ShowMessage(string method, RTMMessage message) 
        {
            string outStr = "[ServerPush " + method + "] fromUid: " + message.fromUid + " toId: " + message.toId + " messageType: " + (byte)message.messageType + " messageId: " + message.messageId + " attrs: " + message.attrs + " modifiedTime: " + message.modifiedTime;
            if (message.stringMessage != null)
                outStr += " stringMessage: " + message.stringMessage;
            if (message.binaryMessage != null)
                outStr += " binaryMessage.Length: " + message.binaryMessage.Length;
            if (message.messageType == (byte)MessageType.Chat && message.translatedInfo != null) {
                outStr += " translatedInfo.sourceLanguage: " + message.translatedInfo.sourceLanguage + " translatedInfo.targetLanguage: " + message.translatedInfo.targetLanguage + " translatedInfo.sourceText: " + message.translatedInfo.sourceText + " translatedInfo.targetText: " + message.translatedInfo.targetText;
            }
            if (message.fileInfo != null) {
                outStr += " fileInfo.url: " + message.fileInfo.url + " fileInfo.size: " + message.fileInfo.size + " fileInfo.surl: " + message.fileInfo.surl + " fileInfo.isRTMAudio: " + message.fileInfo.isRTMAudio + " fileInfo.language: " + message.fileInfo.language + " fileInfo.duration: " + message.fileInfo.duration;
            }
            Console.WriteLine(outStr);
        }
    }

    class Program
    {
        static int testProjectID = 11000001;
        static string testSecretKey = "ef3617e5-e886-4a4e-9eef-7263c0320628";
        static string testEndpoint = "161.189.171.91:13315";
        static long testFromUserID = 111;
        static long testToUserId = 123456;
        static long testGroupId = 123;
        static long testRoomId = 456;
        static string testMessage = "test message";
        static string testAttrs = "test attrs";

        static RTMServerClient client;

        static void TokenTest()
        {
            int errorCode = 0;
            client.GetToken((string token, int errorCode) => {
                Console.WriteLine("[GetToken Async] errorCode: " + errorCode + " token: " + token);
            }, testFromUserID);

            string token;
            errorCode = client.GetToken(out token, testFromUserID);
            Console.WriteLine("[GetToken Sync] errorCode: " + errorCode + " token: " + token);

            client.RemoveToken((int errorCode) => {
                Console.WriteLine("[RemoveToken Async] errorCode: " + errorCode);
            }, testFromUserID);

            errorCode = client.RemoveToken(testFromUserID);
            Console.WriteLine("[RemoveToken Sync] errorCode: " + errorCode + " token: " + token);

            client.Kickout((int errorCode) => {
                Console.WriteLine("[Kickout Async] errorCode: " + errorCode);
            }, testToUserId);

            errorCode = client.Kickout(testToUserId);
            Console.WriteLine("[Kickout Sync] errorCode: " + errorCode);
        }
        static void MessageTest()
        {
            int errorCode = 0;
            client.SendMessage((long mtime, int errorCode) => 
            {
                Console.WriteLine("[SendMessage Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, testToUserId, 51, testMessage, testAttrs);

            long mtime;
            errorCode = client.SendMessage(out mtime, testFromUserID, testToUserId, 51, testMessage, testAttrs);
            Console.WriteLine("[SendMessage Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.SendMessages((long mtime, int errorCode) => 
            {
                Console.WriteLine("[SendMessages Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, new HashSet<long>(){testToUserId}, 51, testMessage, testAttrs);

            errorCode = client.SendMessages(out mtime, testFromUserID, new HashSet<long>(){testToUserId}, 51, testMessage, testAttrs);
            Console.WriteLine("[SendMessages Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.SendGroupMessage((long mtime, int errorCode) =>
            {
                Console.WriteLine("[SendGroupMessage Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, testGroupId, 51, testMessage, testAttrs);

            errorCode = client.SendGroupMessage(out mtime, testFromUserID, testGroupId, 51, testMessage, testAttrs);
            Console.WriteLine("[SendGroupMessage Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.SendRoomMessage((long mtime, int errorCode) =>
            {
                Console.WriteLine("[SendRoomMessage Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, testRoomId, 51, testMessage, testAttrs);

            errorCode = client.SendRoomMessage(out mtime, testFromUserID, testRoomId, 51, testMessage, testAttrs);
            Console.WriteLine("[SendRoomMessage Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.BroadcastMessage((long mtime, int errorCode) =>
            {
                Console.WriteLine("[BroadcastMessage Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, 51, testMessage, testAttrs);

            errorCode = client.BroadcastMessage(out mtime, testFromUserID, 51, testMessage, testAttrs);
            Console.WriteLine("[BroadcastMessage Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.GetGroupMessage((int count, long lastCursorId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode) =>
            {
                Console.WriteLine("[GetGroupMessage Async] errorCode: " + errorCode + " count: " + count);
            }, testFromUserID, testGroupId, true, 10);

            HistoryMessageResult msgResult;
            errorCode = client.GetGroupMessage(out msgResult, testFromUserID, testGroupId, true, 10);
            Console.WriteLine("[GetGroupMessage Sync] errorCode: " + errorCode + " count: " + msgResult.count);

            client.GetRoomMessage((int count, long lastCursorId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode) =>
            {
                Console.WriteLine("[GetRoomMessage Async] errorCode: " + errorCode + " count: " + count);
            }, testFromUserID, testRoomId, true, 10);

            errorCode = client.GetRoomMessage(out msgResult, testFromUserID, testRoomId, true, 10);
            Console.WriteLine("[GetRoomMessage Sync] errorCode: " + errorCode + " count: " + msgResult.count);

            client.GetBroadcastMessage((int count, long lastCursorId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode) =>
            {
                Console.WriteLine("[GetBroadcastMessage Async] errorCode: " + errorCode + " count: " + count);
            }, testFromUserID, true, 10);

            errorCode = client.GetBroadcastMessage(out msgResult, testFromUserID, true, 10);
            Console.WriteLine("[GetBroadcastMessage Sync] errorCode: " + errorCode + " count: " + msgResult.count);

            long cacheMid = 0;
            client.GetP2PMessage((int count, long lastCursorId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode) =>
            {
                Console.WriteLine("[GetP2PMessage Async] errorCode: " + errorCode + " count: " + count);
                foreach (var m in messages)
                    cacheMid = m.messageId;
            }, testFromUserID, testToUserId, true, 10);

            errorCode = client.GetP2PMessage(out msgResult, testFromUserID, testToUserId, true, 10);
            Console.WriteLine("[GetP2PMessage Sync] errorCode: " + errorCode + " count: " + msgResult.count);

            client.DeleteMessage((int errorCode) => 
            {
                Console.WriteLine("[DeleteMessage Async] errorCode: " + errorCode);
            }, testFromUserID, testToUserId, 112233, MessageCategory.P2PMessage);

            errorCode = client.DeleteMessage(testFromUserID, testToUserId, 112233, MessageCategory.P2PMessage);
            Console.WriteLine("[DeleteMessage Sync] errorCode: " + errorCode);

            client.GetMessage((RetrievedMessage message, int errorCode) =>
            {
                Console.WriteLine("[GetMessage Async] errorCode: " + errorCode);
            }, testFromUserID, testToUserId, cacheMid, MessageCategory.P2PMessage);

            RetrievedMessage message;
            errorCode = client.GetMessage(out message, testFromUserID, testToUserId, cacheMid, MessageCategory.P2PMessage);
            Console.WriteLine("[GetMessage Sync] errorCode: " + errorCode);

            client.GetMessageNum((int sender, int num, int errorCode) =>
            {
                Console.WriteLine("[GetMessageNum Async] errorCode: " + errorCode + " sender: " + sender + " num: " + num);
            }, MessageCategory.GroupMessage, 666);

            int sender, num;
            errorCode = client.GetMessageNum(out sender, out num, MessageCategory.GroupMessage, 666);
            Console.WriteLine("[GetMessageNum Sync] errorCode: " + errorCode + " sender: " + sender + " num: " + num);
        }

        static void ChatTest()
        {
            int errorCode = 0;
            client.SendChat((long mtime, int errorCode) => 
            {
                Console.WriteLine("[SendChat Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, testToUserId, testMessage, testAttrs);

            long mtime;
            errorCode = client.SendChat(out mtime, testFromUserID, testToUserId, testMessage, testAttrs);
            Console.WriteLine("[SendChat Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.SendChats((long mtime, int errorCode) => 
            {
                Console.WriteLine("[SendChats Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, new HashSet<long>(){testToUserId}, testMessage, testAttrs);

            errorCode = client.SendChats(out mtime, testFromUserID, new HashSet<long>(){testToUserId}, testMessage, testAttrs);
            Console.WriteLine("[SendChats Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.SendGroupChat((long mtime, int errorCode) =>
            {
                Console.WriteLine("[SendGroupChat Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, testGroupId, testMessage, testAttrs);

            errorCode = client.SendGroupChat(out mtime, testFromUserID, testGroupId, testMessage, testAttrs);
            Console.WriteLine("[SendGroupChat Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.SendRoomChat((long mtime, int errorCode) =>
            {
                Console.WriteLine("[SendRoomChat Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, testRoomId, testMessage, testAttrs);

            errorCode = client.SendRoomChat(out mtime, testFromUserID, testRoomId, testMessage, testAttrs);
            Console.WriteLine("[SendRoomChat Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.BroadcastChat((long mtime, int errorCode) =>
            {
                Console.WriteLine("[BroadcastChat Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, testMessage, testAttrs);

            errorCode = client.BroadcastChat(out mtime, testFromUserID, testMessage, testAttrs);
            Console.WriteLine("[BroadcastChat Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.GetGroupChat((int count, long lastCursorId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode) =>
            {
                Console.WriteLine("[GetGroupChat Async] errorCode: " + errorCode + " count: " + count);
            }, testFromUserID, testGroupId, true, 10);

            HistoryMessageResult msgResult;
            errorCode = client.GetGroupChat(out msgResult, testFromUserID, testGroupId, true, 10);
            Console.WriteLine("[GetGroupChat Sync] errorCode: " + errorCode + " count: " + msgResult.count);

            client.GetRoomChat((int count, long lastCursorId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode) =>
            {
                Console.WriteLine("[GetRoomChat Async] errorCode: " + errorCode + " count: " + count);
            }, testFromUserID, testRoomId, true, 10);

            errorCode = client.GetRoomChat(out msgResult, testFromUserID, testRoomId, true, 10);
            Console.WriteLine("[GetRoomChat Sync] errorCode: " + errorCode + " count: " + msgResult.count);

            client.GetBroadcastChat((int count, long lastCursorId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode) =>
            {
                Console.WriteLine("[GetBroadcastChat Async] errorCode: " + errorCode + " count: " + count);
            }, testFromUserID, true, 10);

            errorCode = client.GetBroadcastChat(out msgResult, testFromUserID, true, 10);
            Console.WriteLine("[GetBroadcastChat Sync] errorCode: " + errorCode + " count: " + msgResult.count);

            long cacheMid = 0;
            client.GetP2PChat((int count, long lastCursorId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode) =>
            {
                Console.WriteLine("[GetP2PChat Async] errorCode: " + errorCode + " count: " + count);
                foreach (var m in messages)
                    cacheMid = m.messageId;
            }, testFromUserID, testToUserId, true, 10);

            errorCode = client.GetP2PChat(out msgResult, testFromUserID, testToUserId, true, 10);
            Console.WriteLine("[GetP2PChat Sync] errorCode: " + errorCode + " count: " + msgResult.count);

            TextCheckResult result;
            errorCode = client.TextCheck(out result, "测试 共产党 aa", 123);
            Console.WriteLine("[TextCheck Sync] errorCode: " + errorCode + " result: " + result.text);
            client.TextCheck((TextCheckResult resultAsync, int errorCode) => {
                Console.WriteLine("[TextCheck Async] errorCode: " + errorCode + " result: " + resultAsync.text);
            }, "测试 共产党 aa", 123);
        }

        static void DataTest()
        {
            int errorCode;
            client.DataSet((int errorCode) =>
            {
                Console.WriteLine("[DataSet Async] errorCode: " + errorCode);
            }, testFromUserID, "key", "val");

            errorCode = client.DataSet(testFromUserID, "key", "val");
            Console.WriteLine("[DataSet Sync] errorCode: " + errorCode);

            client.DataGet((string val, int errorCode) =>
            {
                Console.WriteLine("[DataGet Async] errorCode: " + errorCode + " val: " + val);
            }, testFromUserID, "key");
            string val;
            errorCode = client.DataGet(out val, testFromUserID, "key");
            Console.WriteLine("[DataGet Sync] errorCode: " + errorCode + " val: " + val);

            client.DataDelete((int errorCode) => 
            {
                Console.WriteLine("[DataDelete Async] errorCode: " + errorCode);
            }, testFromUserID, "key");
            errorCode =  client.DataDelete(testFromUserID, "key");
            Console.WriteLine("[DataDelete Sync] errorCode: " + errorCode);
        }

        static void FileTest()
        {
            client.FileToken((string token, string endpoint, int errorCode) =>
            {
                Console.WriteLine("[FileToken Async] errorCode: " + errorCode + " token: " + token + " endpoint: " + endpoint);
            }, testFromUserID, FileTokenType.P2P, testToUserId);

            int errorCode;
            string token, endpoint;
            errorCode = client.FileToken(out token, out endpoint, testFromUserID, FileTokenType.P2P, testToUserId);
            Console.WriteLine("[FileToken Sync] errorCode: " + errorCode + " token: " + token + " endpoint: " + endpoint);

            client.SendFile((long mtime, int errorCode) =>
            {
                Console.WriteLine("[SendFile Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, testToUserId, MessageType.NormalFile, new byte[1], "test.bin");

            long mtime;
            errorCode = client.SendFile(out mtime, testFromUserID, testToUserId, MessageType.NormalFile, new byte[1], "test.bin");
            Console.WriteLine("[SendFile Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.SendGroupFile((long mtime, int errorCode) =>
            {
                Console.WriteLine("[SendGroupFile Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, testGroupId, MessageType.NormalFile, new byte[1], "test.bin");

            errorCode = client.SendGroupFile(out mtime, testFromUserID, testGroupId, MessageType.NormalFile, new byte[1], "test.bin");
            Console.WriteLine("[SendGroupFile Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.SendRoomFile((long mtime, int errorCode) =>
            {
                Console.WriteLine("[SendRoomFile Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, testRoomId, MessageType.NormalFile, new byte[1], "test.bin");

            errorCode = client.SendRoomFile(out mtime, testFromUserID, testRoomId, MessageType.NormalFile, new byte[1], "test.bin");
            Console.WriteLine("[SendRoomFile Sync] errorCode: " + errorCode + " mtime: " + mtime);

            client.BroadcastFile((long mtime, int errorCode) =>
            {
                Console.WriteLine("[BroadcastFile Async] errorCode: " + errorCode + " mtime: " + mtime);
            }, testFromUserID, MessageType.NormalFile, new byte[1], "test.bin");

            errorCode = client.BroadcastFile(out mtime, testFromUserID, MessageType.NormalFile, new byte[1], "test.bin");
            Console.WriteLine("[BroadcastFile Sync] errorCode: " + errorCode + " mtime: " + mtime);
        }

        static void FriendsTest()
        {
            client.AddFriends((int errorCode) => 
            {
                Console.WriteLine("[AddFriends Async] errorCode: " + errorCode);
            }, testFromUserID, new HashSet<long>() {1,2,3});

            int errorCode;

            errorCode = client.AddFriends(testFromUserID, new HashSet<long>() {1,2,3});
            Console.WriteLine("[AddFriends Sync] errorCode: " + errorCode);

            client.DeleteFriends((int errorCode) => 
            {
                Console.WriteLine("[DeleteFriends Async] errorCode: " + errorCode);
            }, testFromUserID, new HashSet<long>() {1});

            errorCode = client.DeleteFriends(testFromUserID, new HashSet<long>() {1});
            Console.WriteLine("[DeleteFriends Sync] errorCode: " + errorCode);

            client.GetFriends((HashSet<long> friends, int errorCode) =>
            {
                Console.WriteLine("[GetFriends Async] errorCode: " + errorCode + " friends.Count: " + friends.Count);
            }, testFromUserID);

            HashSet<long> friends;
            errorCode = client.GetFriends(out friends, testFromUserID);
            Console.WriteLine("[GetFriends Sync] errorCode: " + errorCode + " friends.Count: " + friends.Count);

            client.IsFriend((bool ok, int errorCode) => 
            {
                Console.WriteLine("[IsFriend Async] errorCode: " + errorCode + " ok: " + ok);
            }, testFromUserID, 1);
            bool ok;
            errorCode = client.IsFriend(out ok, testFromUserID, 1);
            Console.WriteLine("[IsFriend Sync] errorCode: " + errorCode + " ok: " + ok);

            client.IsFriends((HashSet<long> fuids, int errorCode) => 
            {
                Console.WriteLine("[IsFriends Async] errorCode: " + errorCode + " fuids.Count: " + fuids.Count);
            }, testFromUserID, new HashSet<long>(){1,2,3});

            errorCode = client.IsFriends(out friends, testFromUserID, new HashSet<long>(){1,2,3});
            Console.WriteLine("[IsFriends Sync] errorCode: " + errorCode + " fuids.Count: " + friends.Count);

            client.AddBlacklist((int errorCode) => 
            {
                Console.WriteLine("[AddBlacklist Async] errorCode: " + errorCode);
            }, testFromUserID, new HashSet<long>() {2,3});

            errorCode = client.AddBlacklist(testFromUserID, new HashSet<long>() {2,3});
            Console.WriteLine("[AddBlacklist Sync] errorCode: " + errorCode);

            client.DeleteBlacklist((int errorCode) => 
            {
                Console.WriteLine("[DeleteBlacklist Async] errorCode: " + errorCode);
            }, testFromUserID, new HashSet<long>() {2,3});

            errorCode = client.DeleteBlacklist(testFromUserID, new HashSet<long>() {2,3});
            Console.WriteLine("[DeleteBlacklist Sync] errorCode: " + errorCode);

            client.IsInBlackList((bool ok, int errorCode) => 
            {
                Console.WriteLine("[IsInBlackList Async] errorCode: " + errorCode + " ok: " + ok);
            }, testFromUserID, 2);

            errorCode = client.IsInBlackList(out ok, testFromUserID, 2);
            Console.WriteLine("[IsInBlackList Sync] errorCode: " + errorCode + " ok: " + ok);

            client.IsInBlackList((HashSet<long> blackUids, int errorCode) => 
            {
                Console.WriteLine("[IsInBlackList Async] errorCode: " + errorCode + " blackUids.Count: " + blackUids.Count);
            }, testFromUserID, new HashSet<long>(){1,2,3});

            errorCode = client.IsInBlackList(out friends, testFromUserID, new HashSet<long>(){1,2,3});
            Console.WriteLine("[IsInBlackList Sync] errorCode: " + errorCode + " blackUids.Count: " + friends.Count);
        }

        static void GroupTest()
        {
            client.AddGroupMembers((int errorCode) => 
            {
                Console.WriteLine("[AddGroupMembers Async] errorCode: " + errorCode);
            }, testGroupId, new HashSet<long>() {testFromUserID, 1,2,3});

            int errorCode;

            errorCode = client.AddGroupMembers(testGroupId, new HashSet<long>() {testFromUserID, 1,2,3});
            Console.WriteLine("[AddGroupMembers Sync] errorCode: " + errorCode);

            client.DeleteGroupMembers((int errorCode) => 
            {
                Console.WriteLine("[DeleteGroupMembers Async] errorCode: " + errorCode);
            }, testGroupId, new HashSet<long>() {3});

            errorCode = client.DeleteGroupMembers(testGroupId, new HashSet<long>() {3});
            Console.WriteLine("[DeleteGroupMembers Sync] errorCode: " + errorCode);

            client.DeleteGroup((int errorCode) => 
            {
                Console.WriteLine("[DeleteGroup Async] errorCode: " + errorCode);
            }, testGroupId + 1);

            errorCode = client.DeleteGroup(testGroupId + 1);
            Console.WriteLine("[DeleteGroup Sync] errorCode: " + errorCode);

            client.GetGroupMembers((HashSet<long> uids, int errorCode) =>
            {
                Console.WriteLine("[GetGroupMembers Async] errorCode: " + errorCode + " uids.Count: " + uids.Count);
            }, testGroupId);

            HashSet<long> uids;
            errorCode = client.GetGroupMembers(out uids, testGroupId);
            Console.WriteLine("[GetGroupMembers Sync] errorCode: " + errorCode + " uids.Count: " + uids.Count);

            client.IsGroupMember((bool ok, int errorCode) => 
            {
                Console.WriteLine("[IsGroupMember Async] errorCode: " + errorCode + " ok: " + ok);
            }, testGroupId, testFromUserID);
            bool ok;
            errorCode = client.IsGroupMember(out ok, testGroupId, testFromUserID);
            Console.WriteLine("[IsGroupMember Sync] errorCode: " + errorCode + " ok: " + ok);

            client.GetUserGroups((HashSet<long> groupIds, int errorCode) =>
            {
                Console.WriteLine("[GetUserGroups Async] errorCode: " + errorCode + " groupIds.Count: " + groupIds.Count);
            }, testFromUserID);

            errorCode = client.GetUserGroups(out uids, testGroupId);
            Console.WriteLine("[GetUserGroups Sync] errorCode: " + errorCode + " groupIds.Count: " + uids.Count);

            client.AddGroupBan((int errorCode) => 
            {
                Console.WriteLine("[AddGroupBan Async] errorCode: " + errorCode);
            }, testGroupId, 1, 10);

            errorCode = client.AddGroupBan(testGroupId, 1, 10);
            Console.WriteLine("[AddGroupBan Sync] errorCode: " + errorCode);

            client.RemoveGroupBan((int errorCode) => 
            {
                Console.WriteLine("[RemoveGroupBan Async] errorCode: " + errorCode);
            }, testGroupId, 1);

            errorCode = client.RemoveGroupBan(testGroupId, 1);
            Console.WriteLine("[RemoveGroupBan Sync] errorCode: " + errorCode);

            client.IsBanOfGroup((bool ok, int errorCode) => 
            {
                Console.WriteLine("[IsBanOfGroup Async] errorCode: " + errorCode + " ok: " + ok);
            }, testFromUserID, 1);

            errorCode = client.IsBanOfGroup(out ok, testFromUserID, 1);
            Console.WriteLine("[IsBanOfGroup Sync] errorCode: " + errorCode + " ok: " + ok);
        }

        static void RoomTest()
        {
            client.AddRoomMember((int errorCode) => 
            {
                Console.WriteLine("[AddRoomMembers Async] errorCode: " + errorCode);
            }, testRoomId, testFromUserID);

            int errorCode;

            errorCode = client.AddRoomMember(testRoomId, testFromUserID);
            Console.WriteLine("[AddRoomMember Sync] errorCode: " + errorCode);

            client.DeleteRoomMember((int errorCode) => 
            {
                Console.WriteLine("[DeleteRoomMember Async] errorCode: " + errorCode);
            }, testRoomId, 3);

            errorCode = client.DeleteRoomMember(testGroupId, 3);
            Console.WriteLine("[DeleteRoomMember Sync] errorCode: " + errorCode);

            
            client.IsGroupMember((bool ok, int errorCode) => 
            {
                Console.WriteLine("[IsGroupMember Async] errorCode: " + errorCode + " ok: " + ok);
            }, testGroupId, testFromUserID);
            bool ok;
            errorCode = client.IsGroupMember(out ok, testGroupId, testFromUserID);
            Console.WriteLine("[IsGroupMember Sync] errorCode: " + errorCode + " ok: " + ok);

            client.AddRoomBan((int errorCode) => 
            {
                Console.WriteLine("[AddRoomBan Async] errorCode: " + errorCode);
            }, testRoomId, 1, 10);

            errorCode = client.AddRoomBan(testRoomId, 1, 10);
            Console.WriteLine("[AddRoomBan Sync] errorCode: " + errorCode);

            client.AddRoomBan((int errorCode) => 
            {
                Console.WriteLine("[AddRoomBan Async] errorCode: " + errorCode);
            }, 1, 10);

            errorCode = client.AddRoomBan(1, 10);
            Console.WriteLine("[AddRoomBan Sync] errorCode: " + errorCode);

            client.RemoveRoomBan((int errorCode) => 
            {
                Console.WriteLine("[RemoveRoomBan Async] errorCode: " + errorCode);
            }, testRoomId, 1);

            errorCode = client.RemoveRoomBan(testRoomId, 1);
            Console.WriteLine("[RemoveRoomBan Sync] errorCode: " + errorCode);

            client.RemoveRoomBan((int errorCode) => 
            {
                Console.WriteLine("[RemoveRoomBan Async] errorCode: " + errorCode);
            }, 1);

            errorCode = client.RemoveRoomBan(1);
            Console.WriteLine("[RemoveRoomBan Sync] errorCode: " + errorCode);

            client.IsBanOfRoom((bool ok, int errorCode) => 
            {
                Console.WriteLine("[IsBanOfRoom Async] errorCode: " + errorCode + " ok: " + ok);
            }, testFromUserID, 1);

            errorCode = client.IsBanOfRoom(out ok, testFromUserID, 1);
            Console.WriteLine("[IsBanOfRoom Sync] errorCode: " + errorCode + " ok: " + ok);

            client.GetRoomMembers((HashSet<long> uids, int errorCode) => {
                Console.WriteLine("[GetRoomMembers Async] errorCode: " + errorCode + " uids.Count: " + uids.Count);
            }, 1);

            HashSet<long> uids;
            errorCode = client.GetRoomMembers(out uids, 1);
            Console.WriteLine("[GetRoomMembers Sync] errorCode: " + errorCode + " uids.Count: " + uids.Count);

            client.GetRoomMemberCount((Dictionary<long, int> counts, int errorCode) => {
                Console.WriteLine("[GetRoomMemberCount Async] errorCode: " + errorCode);
            }, new HashSet<long>(){1});

            Dictionary<long, int> counts;
            client.GetRoomMemberCount(out counts, new HashSet<long>(){1});
            Console.WriteLine("[GetRoomMemberCount Sync] errorCode: " + errorCode);
        }

        static void SystemTest()
        {
            int errorCode;
            client.AddDevicePushOption((int errorCode) => {
                Console.WriteLine("[AddDevicePushOption Async] errorCode: " + errorCode);
            }, 123, MessageCategory.P2PMessage, 222);

            errorCode = client.AddDevicePushOption(123, MessageCategory.P2PMessage, 222, new HashSet<byte>(){70, 80, 90});

            client.GetDevicePushOption((Dictionary<long, HashSet<byte>> p2p, Dictionary<long, HashSet<byte>> group, int errorCode) => {
                Console.WriteLine("[GetDevicePushOption Async] errorCode: " + errorCode);
                foreach (var item in p2p)
                {
                    Console.WriteLine("uid: " + item.Key);
                    foreach (var mtype in item.Value)
                    {
                        Console.WriteLine(mtype);
                    }
                }

                foreach (var item in group)
                {
                    Console.WriteLine("gid: " + item.Key);
                    foreach (var mtype in item.Value)
                    {
                        Console.WriteLine(mtype);
                    }
                }

            }, 123);

            client.RemoveDevicePushOption((int errorCode) => {
                Console.WriteLine("[RemoveDevicePushOption Async] errorCode: " + errorCode);
            }, 123, MessageCategory.P2PMessage, 222);

            errorCode = client.RemoveDevicePushOption(123, MessageCategory.P2PMessage, 222, new HashSet<byte>(){70});
            Console.WriteLine("[RemoveDevicePushOption Sync] errorCode: " + errorCode);

            Dictionary<long, HashSet<byte>> p2p;
            Dictionary<long, HashSet<byte>> group;
            errorCode = client.GetDevicePushOption(out p2p, out group, 123);
            Console.WriteLine("[GetDevicePushOption Sync] errorCode: " + errorCode);
            foreach (var item in p2p)
            {
                Console.WriteLine("uid: " + item.Key);
                foreach (var mtype in item.Value)
                {
                    Console.WriteLine(mtype);
                }
            }

            foreach (var item in group)
            {
                Console.WriteLine("gid: " + item.Key);
                foreach (var mtype in item.Value)
                {
                    Console.WriteLine(mtype);
                }
            }
        }

        static void UserTest()
        {
            client.GetOnlineUsers((HashSet<long> onlineUids, int errorCode) =>
            {
                Console.WriteLine("[GetOnlineUsers Async] errorCode: " + errorCode + " onlineUids.Count: " + onlineUids.Count);
            }, new HashSet<long>(){1, 2, 3});

            HashSet<long> uids;
            int errorCode;
            errorCode = client.GetOnlineUsers(out uids, new HashSet<long>(){1, 2, 3});
            Console.WriteLine("[GetOnlineUsers Sync] errorCode: " + errorCode + " onlineUids.Count: " + uids.Count);

            client.AddProjectBlack((int errorCode) => 
            {
                Console.WriteLine("[AddProjectBlack Async] errorCode: " + errorCode);
            }, 1, 10);

            errorCode = client.AddProjectBlack(1, 10);
            Console.WriteLine("[AddProjectBlack Sync] errorCode: " + errorCode);

            client.RemoveProjectBlack((int errorCode) => 
            {
                Console.WriteLine("[RemoveProjectBlack Async] errorCode: " + errorCode);
            }, 1, 10);

            errorCode = client.RemoveProjectBlack(1, 10);
            Console.WriteLine("[RemoveProjectBlack Sync] errorCode: " + errorCode);

            client.IsProjectBlack((bool ok, int errorCode) => 
            {
                Console.WriteLine("[IsProjectBlack Async] errorCode: " + errorCode + " ok: " + ok);
            }, 1);
            bool ok;
            errorCode = client.IsProjectBlack(out ok, 1);
            Console.WriteLine("[IsProjectBlack Sync] errorCode: " + errorCode + " ok: " + ok);
        }

        static void SetListen()
        {
            client.SetServerPushMonitor(new MyQuestProcessor());
            client.SetListen((int errorCode) => 
            {
                Console.WriteLine("[SetListen Async] errorCode: " + errorCode);
            }, true, true, true, true);
        }

        static void RtcTest() 
        {
            client.InviteUserIntoVoiceRoom((int errorCode) => 
            {
                Console.WriteLine("[InviteUserIntoVoiceRoom Async] errorCode: " + errorCode);
            }, 111, new HashSet<long>(){1, 2, 3}, 666);

            int errorCode;

            errorCode = client.InviteUserIntoVoiceRoom(111, new HashSet<long>(){1, 2, 3}, 666);
            Console.WriteLine("[InviteUserIntoVoiceRoom Sync] errorCode: " + errorCode);

            client.CloseVoiceRoom((int errorCode) => 
            {
                Console.WriteLine("[CloseVoiceRoom Async] errorCode: " + errorCode);
            }, 666);

            errorCode = client.CloseVoiceRoom(666);
            Console.WriteLine("[CloseVoiceRoom Sync] errorCode: " + errorCode);

            client.KickoutFromVoiceRoom((int errorCode) => 
            {
                Console.WriteLine("[KickoutFromVoiceRoom Async] errorCode: " + errorCode);
            }, 666, 111, 222);

            errorCode = client.KickoutFromVoiceRoom(666, 111, 222);
            Console.WriteLine("[KickoutFromVoiceRoom Sync] errorCode: " + errorCode);

            client.GetVoiceRoomList((HashSet<long> roomIds, int errorCode) => 
            {
                Console.WriteLine("[GetVoiceRoomList Async] errorCode: " + errorCode);
            });

            HashSet<long> roomIds;
            errorCode = client.GetVoiceRoomList(out roomIds);
            Console.WriteLine("[GetVoiceRoomList Sync] errorCode: " + errorCode);

            client.GetVoiceRoomMembers((HashSet<long> uids, HashSet<long> managers, int errorCode) => 
            {
                Console.WriteLine("[GetVoiceRoomMembers Async] errorCode: " + errorCode);
            }, 111);

            HashSet<long> uids;
            HashSet<long> managers;
            errorCode = client.GetVoiceRoomMembers(out uids, out managers, 111);
            Console.WriteLine("[GetVoiceRoomMembers Sync] errorCode: " + errorCode);

            client.GetVoiceRoomMemberCount((int count, int errorCode) => 
            {
                Console.WriteLine("[GetVoiceRoomMemberCount Async] errorCode: " + errorCode);
            }, 111);

            int count;
            errorCode = client.GetVoiceRoomMemberCount(out count, 111);
            Console.WriteLine("[GetVoiceRoomMemberCount Sync] errorCode: " + errorCode);

            client.SetVoiceRoomMicStatus((int errorCode) => 
            {
                Console.WriteLine("[SetVoiceRoomMicStatus Async] errorCode: " + errorCode);
            }, 666, true);

            errorCode = client.SetVoiceRoomMicStatus(666, true);
            Console.WriteLine("[SetVoiceRoomMicStatus Sync] errorCode: " + errorCode);

            client.PullIntoVoiceRoom((int errorCode) => 
            {
                Console.WriteLine("[PullIntoVoiceRoom Async] errorCode: " + errorCode);
            }, 111, new HashSet<long>(){1, 2, 3});

            errorCode = client.PullIntoVoiceRoom(111, new HashSet<long>(){1, 2, 3});
            Console.WriteLine("[PullIntoVoiceRoom Sync] errorCode: " + errorCode);
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Start Function Test");

            Console.WriteLine(com.fpnn.rtm.RTMServerConfig.SDKVersion);
            Console.WriteLine(com.fpnn.rtm.RTMServerConfig.InterfaceVersion);
            Console.WriteLine(com.fpnn.Config.Version);

            ClientEngine.Init();
            
            client = new RTMServerClient(testProjectID, testSecretKey, testEndpoint);

            client.SetConnectionConnectedDelegate((Int64 connectionId, string endpoint, bool connected, bool isReconnect) =>
            {
                Console.WriteLine("[ConnectedDelegate] connectionId: " + connectionId + " endpoint: " + endpoint + " connected: " + connected + " isReconnect: " + isReconnect);
            });

            client.SetConnectionCloseDelegate((Int64 connectionId, string endpoint, bool causedByError, bool isReconnect) =>
            {
                Console.WriteLine("[CloseDelegate] connectionId: " + connectionId + " endpoint: " + endpoint + " causedByError: " + causedByError + " isReconnect: " + isReconnect);
            });

            SetListen();
            TokenTest();
            MessageTest();
            ChatTest();
            DataTest();
            FileTest();
            FriendsTest();
            GroupTest();
            RoomTest();
            UserTest();
            SystemTest();
            RtcTest();

            Thread.Sleep(5000);
        }
    }
}
