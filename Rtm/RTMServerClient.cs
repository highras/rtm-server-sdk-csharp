using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using com.fpnn;

namespace com.rtm
{
    public delegate void RTMPushCallbackDelegate(Hashtable data);
    public delegate void CallbackDelegate(Hashtable data, Exception exception);

    public class RTMServerClient
    {
        private static class MidGenerator {
            static private long Count = 0;
            static private Object Lock = new Object();

            static public long Gen() {
                long c = 0;
                lock(Lock)
                {
                    if (++Count >= 999)
                        Count = 0;
                    c = Count;
                }
                return Convert.ToInt64(Convert.ToString(FPCommon.GetMilliTimestamp()) + Convert.ToString(c));
            }
        }

        private int Pid;
        private string Secret;
        private string Host;
        private int Port;
        private bool AutoReconnect;
        private int Timeout;
        private FPClient Client;

        public ConnectedCallbackDelegate ConnectedCallback { 
            get { return Client.ConnectedCallback; }
            set {
                Client.ConnectedCallback = value;
            }
        }

        public ClosedCallbackDelegate ClosedCallback { 
            get { return Client.ClosedCallback; }
            set {
                Client.ClosedCallback = value;
            }
        }

        public ErrorCallbackDelegate ErrorCallback { 
            get { return Client.ErrorCallback; }
            set {
                Client.ErrorCallback = value;
            }
        }

        public RTMServerClient(int pid, string secret, string host, int port, bool reconnect, int timeout)
        {
            Pid = pid;
            Secret = secret;
            Host = host;
            Port = port;
            AutoReconnect = reconnect;
            Timeout = timeout;
            Init();
        }

        private void Init()
        {
            Client = new FPClient(Host, Port, AutoReconnect, Timeout);
        }

        public void Connect() 
        {
            Client.Connect();
        }

        public void Reconnect()
        {
            Client.Reconnect();
        }

        public void Close()
        {
            Client.Close();
        }

        private long GenMessageSalt() 
        {
            return MidGenerator.Gen();
        }

        private string CalcMd5(string str, bool upper)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
            return CalcMd5(inputBytes, upper);
        }

        private string CalcMd5(byte[] bytes, bool upper)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(bytes);
            string f = "x2";
            if (upper)
                f = "X2";
            StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString(f));
			}
			return sb.ToString();
        }

        private string GenMessageSign(long salt) {
            string str = Convert.ToString(Pid) + ":" + Secret + ":" + Convert.ToString(salt);
            return CalcMd5(str, true);
        }

        private Exception CheckException(Hashtable mp)
        {
            if (mp.ContainsKey("ex") && mp.ContainsKey("code"))
            {
                int errorCode = Convert.ToInt32(mp["code"]);
                if (errorCode > 0 && errorCode <= 30002)
                    Reconnect();

                string errorMsg = Convert.ToString(errorCode) + " : " + mp["ex"];
                return new Exception(errorMsg);
            }
            return null;
        }

        public void SendMessage(long from, long to, byte mtype, string msg, string attrs, long mid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            if (mid == 0) {
                mid = MidGenerator.Gen();
            }

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"mtype", mtype},
                {"from", from},
                {"to", to},
                {"mid", mid},
                {"msg", msg},
                {"attrs", attrs}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("sendmsg");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    result.Add("mid", mid);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(result, ex);
                }
                else
                {
                    Hashtable result = new Hashtable();
                    result.Add("mid", mid);
                    cb(result, cbd.Exception);
                }
            }, timeout);
        }

        public void SendMessages(long from, long[] tos, byte mtype, string msg, string attrs, long mid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            if (mid == 0) {
                mid = MidGenerator.Gen();
            }

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"mtype", mtype},
                {"from", from},
                {"tos", tos},
                {"mid", mid},
                {"msg", msg},
                {"attrs", attrs}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("sendmsgs");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    result.Add("mid", mid);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(result, ex);
                }
                else
                {
                    Hashtable result = new Hashtable();
                    result.Add("mid", mid);
                    cb(result, cbd.Exception);
                }
            }, timeout);
        }

        public void SendGroupMessage(long from, long gid, byte mtype, string msg, string attrs, long mid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            if (mid == 0) {
                mid = MidGenerator.Gen();
            }

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"mtype", mtype},
                {"from", from},
                {"gid", gid},
                {"mid", mid},
                {"msg", msg},
                {"attrs", attrs}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("sendgroupmsg");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    result.Add("mid", mid);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(result, ex);
                }
                else
                {
                    Hashtable result = new Hashtable();
                    result.Add("mid", mid);
                    cb(result, cbd.Exception);
                }
            }, timeout);
        }

        public void SendRoomMessage(long from, long rid, byte mtype, string msg, string attrs, long mid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            if (mid == 0) {
                mid = MidGenerator.Gen();
            }

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"mtype", mtype},
                {"from", from},
                {"rid", rid},
                {"mid", mid},
                {"msg", msg},
                {"attrs", attrs}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("sendroommsg");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    result.Add("mid", mid);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(result, ex);
                }
                else
                {
                    Hashtable result = new Hashtable();
                    result.Add("mid", mid);
                    cb(result, cbd.Exception);
                }
            }, timeout);
        }

        public void BroadcastMessage(long from, byte mtype, string msg, string attrs, long mid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            if (mid == 0) {
                mid = MidGenerator.Gen();
            }

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"mtype", mtype},
                {"from", from},
                {"mid", mid},
                {"msg", msg},
                {"attrs", attrs}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("broadcastmsg");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    result.Add("mid", mid);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(result, ex);
                }
                else
                {
                    Hashtable result = new Hashtable();
                    result.Add("mid", mid);
                    cb(result, cbd.Exception);
                }
            }, timeout);
        }

        public void AddFriends(long uid, long[] friends, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid},
                {"friends", friends}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("addfriends");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void DeleteFriends(long uid, long[] friends, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid},
                {"friends", friends}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("delfriends");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void GetFriends(long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("getfriends");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void IsFriend(long uid, long fuid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid},
                {"fuid", fuid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("isfriend");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void IsFriends(long uid, long[] fuids, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid},
                {"fuids", fuids}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("isfriends");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void AddGroupMembers(long gid, long[] uids, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gid", gid},
                {"uids", uids}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("addgroupmembers");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void DeleteGroupMembers(long gid, long[] uids, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gid", gid},
                {"uids", uids}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("delgroupmembers");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }
        
        public void DeleteGroup(long gid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gid", gid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("delgroup");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void GetGroupMembers(long gid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gid", gid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("getgroupmembers");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void IsGroupMember(long gid, long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gid", gid},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("isgroupmember");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void GetUserGroups(long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("getusergroups");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void GetToken(long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("gettoken");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void GetOnlineUsers(long[] uids, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uids", uids}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("getonlineusers");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void AddGroupBan(long gid, long uid, int btime, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gid", gid},
                {"uid", uid},
                {"btime", btime}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("addgroupban");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void RemoveGroupBan(long gid, long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gid", gid},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("removegroupban");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void AddRoomBan(long rid, long uid, int btime, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"rid", rid},
                {"uid", uid},
                {"btime", btime}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("addroomban");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void RemoveRoomBan(long rid, long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"rid", rid},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("removeroomban");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void AddProjectBlack(long uid, int btime, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid},
                {"btime", btime}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("addprojectblack");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void RemoveProjectBlack(long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("removeprojectblack");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void IsBanOfGroup(long gid, long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gid", gid},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("isbanofgroup");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void IsBanOfRoom(long rid, long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"rid", rid},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("isbanofroom");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void IsProjectBlack(long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("isprojectblack");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void FileToken(long from, string cmd, long[] tos, long to, long rid, long gid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"from", from},
                {"cmd", cmd}
            };

            if (tos != null && tos.Length > 0)
                mp.Add("tos", tos);

            if (to > 0)
                mp.Add("to", to);

            if (rid > 0)
                mp.Add("rid", rid); 

            if (gid > 0)
                mp.Add("gid", gid); 

            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("filetoken");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void GetGroupMessage(long gid, bool desc, int num, long begin, long end, long lastid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gid", gid},
                {"desc", desc},
                {"num", num},
                {"begin", begin},
                {"end", end},
                {"lastid", lastid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("getgroupmsg");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                    {
                        Hashtable msgResult = new Hashtable();

                        msgResult["num"] = Convert.ToInt32(result["num"]);
                        msgResult["lastid"] = Convert.ToInt64(result["lastid"]);
                        msgResult["begin"] = Convert.ToInt64(result["begin"]);
                        msgResult["end"] = Convert.ToInt64(result["end"]);

                        Object[] msgList = (Object[])result["msgs"];
                        GroupMsg[] msgs = new GroupMsg[msgList.Length];
                        for (int i = 0; i < msgList.Length; i++)
                        {
                            Object[] subList = (Object[])msgList[i];
                            GroupMsg msg = new GroupMsg();
                            msg.id = Convert.ToInt64(subList[0]);
                            msg.from = Convert.ToInt64(subList[1]);
                            msg.mtype = Convert.ToByte(subList[2]);
                            msg.mid = Convert.ToInt64(subList[3]);
                            msg.deleted = Convert.ToBoolean(subList[4]);
                            msg.msg = Convert.ToString(subList[5]);
                            msg.attrs = Convert.ToString(subList[6]);
                            msg.mtime = Convert.ToInt64(subList[7]);
                            msgs[i] = msg;
                        }
                        msgResult["msgs"] = msgs;

                        cb(msgResult, null);
                    }
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void GetRoomMessage(long rid, bool desc, int num, long begin, long end, long lastid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"rid", rid},
                {"desc", desc},
                {"num", num},
                {"begin", begin},
                {"end", end},
                {"lastid", lastid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("getroommsg");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                    {
                        Hashtable msgResult = new Hashtable();

                        msgResult["num"] = Convert.ToInt32(result["num"]);
                        msgResult["lastid"] = Convert.ToInt64(result["lastid"]);
                        msgResult["begin"] = Convert.ToInt64(result["begin"]);
                        msgResult["end"] = Convert.ToInt64(result["end"]);

                        Object[] msgList = (Object[])result["msgs"];
                        RoomMsg[] msgs = new RoomMsg[msgList.Length];
                        for (int i = 0; i < msgList.Length; i++)
                        {
                            Object[] subList = (Object[])msgList[i];
                            RoomMsg msg = new RoomMsg();
                            msg.id = Convert.ToInt64(subList[0]);
                            msg.from = Convert.ToInt64(subList[1]);
                            msg.mtype = Convert.ToByte(subList[2]);
                            msg.mid = Convert.ToInt64(subList[3]);
                            msg.deleted = Convert.ToBoolean(subList[4]);
                            msg.msg = Convert.ToString(subList[5]);
                            msg.attrs = Convert.ToString(subList[6]);
                            msg.mtime = Convert.ToInt64(subList[7]);
                            msgs[i] = msg;
                        }
                        msgResult["msgs"] = msgs;

                        cb(msgResult, null);
                    }
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void GetBroadcastMessage(bool desc, int num, long begin, long end, long lastid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"desc", desc},
                {"num", num},
                {"begin", begin},
                {"end", end},
                {"lastid", lastid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("getbroadcastmsg");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                    {
                        Hashtable msgResult = new Hashtable();

                        msgResult["num"] = Convert.ToInt32(result["num"]);
                        msgResult["lastid"] = Convert.ToInt64(result["lastid"]);
                        msgResult["begin"] = Convert.ToInt64(result["begin"]);
                        msgResult["end"] = Convert.ToInt64(result["end"]);

                        Object[] msgList = (Object[])result["msgs"];
                        BroadcastMsg[] msgs = new BroadcastMsg[msgList.Length];
                        for (int i = 0; i < msgList.Length; i++)
                        {
                            Object[] subList = (Object[])msgList[i];
                            BroadcastMsg msg = new BroadcastMsg();
                            msg.id = Convert.ToInt64(subList[0]);
                            msg.from = Convert.ToInt64(subList[1]);
                            msg.mtype = Convert.ToByte(subList[2]);
                            msg.mid = Convert.ToInt64(subList[3]);
                            msg.deleted = Convert.ToBoolean(subList[4]);
                            msg.msg = Convert.ToString(subList[5]);
                            msg.attrs = Convert.ToString(subList[6]);
                            msg.mtime = Convert.ToInt64(subList[7]);
                            msgs[i] = msg;
                        }
                        msgResult["msgs"] = msgs;

                        cb(msgResult, null);
                    }
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void GetP2PMessage(long uid, long ouid, bool desc, int num, long begin, long end, long lastid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid},
                {"ouid", ouid},
                {"desc", desc},
                {"num", num},
                {"begin", begin},
                {"end", end},
                {"lastid", lastid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("getp2pmsg");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                    {
                        Hashtable msgResult = new Hashtable();

                        msgResult["num"] = Convert.ToInt32(result["num"]);
                        msgResult["lastid"] = Convert.ToInt64(result["lastid"]);
                        msgResult["begin"] = Convert.ToInt64(result["begin"]);
                        msgResult["end"] = Convert.ToInt64(result["end"]);

                        Object[] msgList = (Object[])result["msgs"];
                        P2PMsg[] msgs = new P2PMsg[msgList.Length];
                        for (int i = 0; i < msgList.Length; i++)
                        {
                            Object[] subList = (Object[])msgList[i];
                            P2PMsg msg = new P2PMsg();
                            msg.id = Convert.ToInt64(subList[0]);
                            msg.direction = Convert.ToByte(subList[1]);
                            msg.mtype = Convert.ToByte(subList[2]);
                            msg.mid = Convert.ToInt64(subList[3]);
                            msg.deleted = Convert.ToBoolean(subList[4]);
                            msg.msg = Convert.ToString(subList[5]);
                            msg.attrs = Convert.ToString(subList[6]);
                            msg.mtime = Convert.ToInt64(subList[7]);
                            msgs[i] = msg;
                        }
                        msgResult["msgs"] = msgs;

                        cb(msgResult, null);
                    }
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void AddRoomMember(long rid, long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"rid", rid},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("addroommember");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void DeleteRoomMember(long rid, long uid, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"rid", rid},
                {"uid", uid}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("delroommember");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void AddListen(long[] gids, long[] rids, bool p2p, string[] events, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gids", gids},
                {"rids", rids},
                {"p2p", p2p},
                {"events", events}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("addlisten");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void RemoveListen(long[] gids, long[] rids, bool p2p, string[] events, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gids", gids},
                {"rids", rids},
                {"p2p", p2p},
                {"events", events}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("removelisten");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void SetListen(long[] gids, long[] rids, bool p2p, string[] events, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"gids", gids},
                {"rids", rids},
                {"p2p", p2p},
                {"events", events}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("setlisten");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void SetListen(bool all, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"all", all}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("setlisten");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void AddDevice(long uid, string apptype, string devicetoken, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid},
                {"apptype", apptype},
                {"devicetoken", devicetoken}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("adddevice");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void RemoveDevice(long uid, string devicetoken, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid},
                {"devicetoken", devicetoken}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("removedevice");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void DeleteMessage(long mid, long from, long xid, byte type, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"mid", mid},
                {"from", from},
                {"xid", xid},
                {"type", type}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("delmsg");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void Kickout(long uid, String ce, int timeout, CallbackDelegate cb)
        {
            long salt = GenMessageSalt();

            Hashtable mp = new Hashtable {
                {"pid", Pid},
                {"sign", GenMessageSign(salt)},
                {"salt", salt},
                {"uid", uid},
                {"ce", ce}
            };
            byte[] payload = MessagePackSerializer.Serialize<Hashtable>(mp);

            FPData data = new FPData();
            data.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
            data.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
            data.SetMethod("kickout");
            data.SetPayload(payload);

            Client.SendQuest(data, (CallbackData cbd) =>
            {
                if (cbd.Exception == null)
                {
                    Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                    Exception ex = CheckException(result);

                    if (ex == null)
                        cb(result, null);
                    else
                        cb(null, ex);
                }
                else
                {
                    cb(null, cbd.Exception);
                }
            }, timeout);
        }

        public void SendFile(long from, long to, byte mtype, byte[] fileBytes, long mid, int timeout, CallbackDelegate cb)
        {
            if (fileBytes == null || fileBytes.Length <= 0) {
                if (ErrorCallback != null)
                    ErrorCallback(new Exception("empty file bytes"));
                return;
            }
            
            Hashtable ops = new Hashtable {
                {"cmd", "sendfile"},
                {"from", from},
                {"to", to},
                {"mtype", mtype},
                {"file", fileBytes}
            };

            SendFileProcess(ops, mid, timeout, cb);
        }

        public void SendFiles(long from, long[] tos, byte mtype, byte[] fileBytes, long mid, int timeout, CallbackDelegate cb)
        {
            if (fileBytes == null || fileBytes.Length <= 0) {
                if (ErrorCallback != null)
                    ErrorCallback(new Exception("empty file bytes"));
                return;
            }
            
            Hashtable ops = new Hashtable {
                {"cmd", "sendfiles"},
                {"from", from},
                {"tos", tos},
                {"mtype", mtype},
                {"file", fileBytes}
            };

            SendFileProcess(ops, mid, timeout, cb);
        }

        public void SendGroupFile(long from, long gid, byte mtype, byte[] fileBytes, long mid, int timeout, CallbackDelegate cb)
        {
            if (fileBytes == null || fileBytes.Length <= 0) {
                if (ErrorCallback != null)
                    ErrorCallback(new Exception("empty file bytes"));
                return;
            }
            
            Hashtable ops = new Hashtable {
                {"cmd", "sendgroupfile"},
                {"from", from},
                {"gid", gid},
                {"mtype", mtype},
                {"file", fileBytes}
            };

            SendFileProcess(ops, mid, timeout, cb);
        }

        public void SendRoomFile(long from, long rid, byte mtype, byte[] fileBytes, long mid, int timeout, CallbackDelegate cb)
        {
            if (fileBytes == null || fileBytes.Length <= 0) {
                if (ErrorCallback != null)
                    ErrorCallback(new Exception("empty file bytes"));
                return;
            }
            
            Hashtable ops = new Hashtable {
                {"cmd", "sendroomfile"},
                {"from", from},
                {"rid", rid},
                {"mtype", mtype},
                {"file", fileBytes}
            };

            SendFileProcess(ops, mid, timeout, cb);
        }

        public void BroadcastFile(long from, byte mtype, byte[] fileBytes, long mid, int timeout, CallbackDelegate cb)
        {
            if (fileBytes == null || fileBytes.Length <= 0) {
                if (ErrorCallback != null)
                    ErrorCallback(new Exception("empty file bytes"));
                return;
            }
            
            Hashtable ops = new Hashtable {
                {"cmd", "broadcastfile"},
                {"from", from},
                {"mtype", mtype},
                {"file", fileBytes}
            };

            SendFileProcess(ops, mid, timeout, cb);
        }

        private void SendFileProcess(Hashtable ops, long mid, int timeout, CallbackDelegate cb)
        {
            Hashtable mp = new Hashtable();

            if (mid == 0)
                mid = MidGenerator.Gen();
            
            long from = (long)ops["from"];
            string cmd = (string)ops["cmd"];
            long[] tos;
            if (ops.ContainsKey("tos"))
                tos = (long[])ops["tos"];
            else
                tos = new long[0];

            long to = 0;
            if (ops.ContainsKey("to"))
                to = (long)ops["to"];

            long rid = 0;
            if (ops.ContainsKey("rid"))
                rid = (long)ops["rid"];

            long gid = 0;
            if (ops.ContainsKey("gid"))
                gid = (long)ops["gid"];

            FileToken(from, cmd, tos, to, rid, gid, timeout, (data, exception) =>
            {
                if (exception != null)
                {
                    cb(null, exception);
                    return;
                }

                if (!data.ContainsKey("token") || !data.ContainsKey("endpoint"))
                {
                    cb(null, new Exception("Get Token Or Endpoint Fail"));
                    return;
                }

                string token = (string)data["token"];
                string endpoint = (string)data["endpoint"];

                FPClient fileClient = new FPClient(endpoint, false);
                fileClient.ErrorCallback = delegate (Exception e)
                {
                    cb(null, e);
                    fileClient.Close();
                };
                fileClient.ConnectedCallback = delegate()
                {
                    Hashtable payload = new Hashtable();
                    payload.Add("pid", Pid);
                    payload.Add("token", token);
                    if (ops.ContainsKey("mtype"))
                        payload.Add("mtype", ops["mtype"]);
                    payload.Add("mid", mid);
                    if (ops.ContainsKey("from"))
                        payload.Add("from", ops["from"]);
                    if (ops.ContainsKey("tos"))
                        payload.Add("tos", ops["tos"]);
                    if (ops.ContainsKey("to"))
                        payload.Add("to", ops["to"]);
                    if (ops.ContainsKey("rid"))
                        payload.Add("rid", ops["rid"]);
                    if (ops.ContainsKey("gid"))
                        payload.Add("gid", ops["gid"]);
                    payload.Add("file", ops["file"]);

                    string fileMd5 = CalcMd5((byte[])ops["file"], false);
                    string sign = CalcMd5(fileMd5 + ":" + token, false);

                    JObject attrsJson = new JObject();
                    attrsJson["sign"] = sign;

                    payload.Add("attrs", attrsJson.ToString());

                    FPData fpData = new FPData();
                    fpData.SetFlag(FP_FLAG.FP_FLAG_MSGPACK);
                    fpData.SetMType(FP_MSG_TYPE.FP_MT_TWOWAY);
                    fpData.SetMethod(cmd);
                    fpData.SetPayload(MessagePackSerializer.Serialize<Hashtable>(payload));

                    fileClient.SendQuest(fpData, (CallbackData cbd) =>
                    {
                        if (cbd.Exception == null)
                        {
                            Hashtable result = MessagePackSerializer.Deserialize<Hashtable>(cbd.Data.payload);
                            result.Add("mid", mid);
                            Exception ex = CheckException(result);

                            if (ex == null)
                                cb(result, null);
                            else
                                cb(result, ex);
                        }
                        else
                        {
                            Hashtable result = new Hashtable();
                            result.Add("mid", mid);
                            cb(result, cbd.Exception);
                        }

                        fileClient.Close();
                    }, timeout);
                };
                fileClient.Connect();
            });
        }

        public byte[] LoadFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public void AddPushListener(string name, RTMPushCallbackDelegate cb)
        {
            Client.Processor.AddListener(name, (data) =>
            {
                cb(MessagePackSerializer.Deserialize<Hashtable>(data.payload));
            });
        }

    }
}
