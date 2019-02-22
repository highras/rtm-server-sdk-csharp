using System;
using System.Text;
using System.Net.Http;
using System.Security.Cryptography;
using System.Json;
using System.Collections.Generic;

namespace com.rtm
{

    class P2PMsg
    {
        public long id = 0;
        public byte direction = 0;
        public byte mtype = 0;
        public long mid = 0;
        public bool deleted = false;
        public string msg = "";
        public string attrs = "";
        public long mtime = 0;
    }

    class BroadcastMsg
    {
        public long id = 0;
        public long from = 0;
        public byte mtype = 0;
        public long mid = 0;
        public bool deleted = false;
        public string msg = "";
        public string attrs = "";
        public long mtime = 0;
    }

    class RoomMsg
    {
        public long id = 0;
        public long from = 0;
        public byte mtype = 0;
        public long mid = 0;
        public bool deleted = false;
        public string msg = "";
        public string attrs = "";
        public long mtime = 0;
    }

    class GroupMsg
    {
        public long id = 0;
        public long from = 0;
        public byte mtype = 0;
        public long mid = 0;
        public bool deleted = false;
        public string msg = "";
        public string attrs = "";
        public long mtime = 0;
    }

    class RTMServerClient
    {
        private int pid;
        private string secretKey;
        private string endpoint;
        private uint midSeq = 0;
        private uint saltSeq = 0;

        public RTMServerClient(int pid, string secretKey, string endpoint)
        {
            this.pid = pid;
            this.secretKey = secretKey;
            this.endpoint = endpoint;
        }

        private long genMid()
        {
            long currentTicks = DateTime.Now.Ticks;
            DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long currentMillis = (currentTicks - dtFrom.Ticks) / 10000;
            return Convert.ToInt64(Convert.ToString(currentMillis) + Convert.ToString(this.midSeq++));
        }
        
        private long genSalt()
        {
            long currentTicks = DateTime.Now.Ticks;
            DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long currentMillis = (currentTicks - dtFrom.Ticks) / 10000;
            return Convert.ToInt64(Convert.ToString(currentMillis) + Convert.ToString(this.saltSeq++));
        }

        private string genSign(long salt)
        {
			string str = Convert.ToString(this.pid) + ":" + this.secretKey + ":" + Convert.ToString(salt);

			MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
			byte[] hash = md5.ComputeHash(inputBytes);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}
			return sb.ToString();
        }

        private bool sendHttpRequest(string method, JsonObject json, out string response)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://" + this.endpoint + "/service/" + method);
                requestMessage.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var resp = client.SendAsync(requestMessage).Result; 
                if (resp.IsSuccessStatusCode) {
                    response = resp.Content.ReadAsStringAsync().Result;
                    return true;
                } else {
                    response = resp.Content.ReadAsStringAsync().Result;
                    throw new Exception(response);
                }
            }
        }

        public bool sendMessage(long from, long to, byte mtype, string msg, string attrs)
        {
            long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mtype"] = mtype;
            json["from"] = from;
            json["to"] = to;
            json["mid"] = this.genMid();
            json["msg"] = msg;
            json["attrs"] = attrs;
       
            string response;
            return this.sendHttpRequest("sendmsg", json, out response);
        }

        public bool sendMessages(long from, long[] tos, byte mtype, string msg, string attrs)
        {
            long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mtype"] = mtype;
            json["from"] = from;
            json["mid"] = this.genMid();
            json["msg"] = msg;
            json["attrs"] = attrs;

            JsonArray arr = new JsonArray();
            foreach (long l in tos)
            {
                arr.Add(l);                
            }
            json["tos"] = arr;

            string response;
            return this.sendHttpRequest("sendmsgs", json, out response);
        }

        public bool sendGroupMessage(long from, long gid, byte mtype, string msg, string attrs) 
        {
            long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mtype"] = mtype;
            json["from"] = from;
            json["gid"] = gid;
            json["mid"] = this.genMid();
            json["msg"] = msg;
            json["attrs"] = attrs;
            
            string response;
            return this.sendHttpRequest("sendgroupmsg", json, out response);
        }

        public bool sendRoomMessage(long from, long rid, byte mtype, string msg, string attrs)
        {
            long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mtype"] = mtype;
            json["from"] = from;
            json["rid"] = rid;
            json["mid"] = this.genMid();
            json["msg"] = msg;
            json["attrs"] = attrs;
            
            string response;
            return this.sendHttpRequest("sendroommsg", json, out response);
        }

		public bool broadcastMessage(long from, byte mtype, string msg, string attrs)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mtype"] = mtype;
            json["from"] = from;
            json["mid"] = this.genMid();
            json["msg"] = msg;
            json["attrs"] = attrs;
            
            string response;
            return this.sendHttpRequest("broadcastmsg", json, out response);
		}

		public bool addFriends(long uid, long[] friends)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            JsonArray arr = new JsonArray();
            foreach (long l in friends)
            {
                arr.Add(l);                
            }
            json["friends"] = arr;

            string response;
            return this.sendHttpRequest("addfriends", json, out response);
		}

		public bool deleteFriends(long uid, long[] friends)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            JsonArray arr = new JsonArray();
            foreach (long l in friends)
            {
                arr.Add(l);                
            }
            json["friends"] = arr;

            string response;
            return this.sendHttpRequest("delfriends", json, out response);
		}

		public long[] getFriends(long uid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            string response;
            bool ok = this.sendHttpRequest("getfriends", json, out response);

			if (ok)
			{
				JsonValue uidsList = JsonValue.Parse(response)["uids"];
				long[] uids = new long[uidsList.Count];
				for (int i = 0; i < uidsList.Count; i++)
					uids[i] = ((long)uidsList[i]);
				return uids;
			}
			return new long[0];
		}

		public bool isFriend(long uid, long fuid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;
            json["fuid"] = fuid;

            string response;
            bool ok = this.sendHttpRequest("isfriend", json, out response);
			if (ok) {
				return (bool)JsonValue.Parse(response)["ok"];
			}	
			return false;
		}

		public long[] isFriends(long uid, long[] fuids)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

			JsonArray arr = new JsonArray();
            foreach (long l in fuids)
            {
                arr.Add(l);                
            }
            json["fuids"] = arr;

			string response;
            bool ok = this.sendHttpRequest("isfriends", json, out response);

			if (ok)
			{
				JsonValue uidsList = JsonValue.Parse(response)["fuids"];
				long[] uids = new long[uidsList.Count];
				for (int i = 0; i < uidsList.Count; i++)
					uids[i] = ((long)uidsList[i]);
				return uids;
			}
			return new long[0];
		}

		public bool addGroupMembers(long gid, long[] uids)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;

			JsonArray arr = new JsonArray();
            foreach (long l in uids)
            {
                arr.Add(l);                
            }
            json["uids"] = arr;

			string response;
            return this.sendHttpRequest("addgroupmembers", json, out response);
		}

		public bool deleteGroupMembers(long gid, long[] uids)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;

			JsonArray arr = new JsonArray();
            foreach (long l in uids)
            {
                arr.Add(l);                
            }
            json["uids"] = arr;

			string response;
            return this.sendHttpRequest("delgroupmembers", json, out response);
		}

		public bool deleteGroup(long gid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;

			string response;
            return this.sendHttpRequest("delgroup", json, out response);
		}

		public long[] getGroupMembers(long gid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;

			string response;
            bool ok = this.sendHttpRequest("getgroupmembers", json, out response);

			if (ok)
			{
				JsonValue uidsList = JsonValue.Parse(response)["uids"];
				long[] uids = new long[uidsList.Count];
				for (int i = 0; i < uidsList.Count; i++)
					uids[i] = ((long)uidsList[i]);
				return uids;
			}
			return new long[0];
		}

		public bool isGroupMember(long gid, long uid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;
            json["uid"] = uid;

            string response;
            bool ok = this.sendHttpRequest("isgroupmember", json, out response);
			if (ok) {
				return (bool)JsonValue.Parse(response)["ok"];
			}	
			return false;
		}

		public long[] getUserGroups(long uid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

			string response;
            bool ok = this.sendHttpRequest("getusergroups", json, out response);

			if (ok)
			{
				JsonValue gidsList = JsonValue.Parse(response)["gids"];
				long[] gids = new long[gidsList.Count];
				for (int i = 0; i < gidsList.Count; i++)
					gids[i] = ((long)gidsList[i]);
				return gids;
			}
			return new long[0];
		}

		public string getToken(long uid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            string response;
            bool ok = this.sendHttpRequest("gettoken", json, out response);
			if (ok) {
				return (string)JsonValue.Parse(response)["token"];
			}	
			return "";
		}

		public long[] getOnlineUsers(long[] uids)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;

			JsonArray arr = new JsonArray();
            foreach (long l in uids)
            {
                arr.Add(l);                
            }
            json["uids"] = arr;

			string response;
            bool ok = this.sendHttpRequest("getonlineusers", json, out response);
			if (ok)
			{
				JsonValue uidsList = JsonValue.Parse(response)["uids"];
				long[] uidsArray = new long[uidsList.Count];
				for (int i = 0; i < uidsList.Count; i++)
					uidsArray[i] = ((long)uidsList[i]);
				return uidsArray;
			}
			return new long[0];
		}

		public bool addGroupBan(long gid, long uid, int btime)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;
            json["uid"] = uid;
            json["btime"] = btime;

			string response;
            return this.sendHttpRequest("addgroupban", json, out response);
		}

		public bool removeGroupBan(long gid, long uid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;
            json["uid"] = uid;

			string response;
            return this.sendHttpRequest("removegroupban", json, out response);
		}
		
		public bool addRoomBan(long rid, long uid, int btime)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["uid"] = uid;
            json["btime"] = btime;

			string response;
            return this.sendHttpRequest("addroomban", json, out response);
		}

		public bool removeRoomBan(long rid, long uid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["uid"] = uid;

			string response;
            return this.sendHttpRequest("removeroomban", json, out response);
		}

		public bool addProjectBlack(long uid, int btime)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;
            json["btime"] = btime;

			string response;
            return this.sendHttpRequest("addprojectblack", json, out response);
		}

		public bool removeProjectBlack(long uid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

			string response;
            return this.sendHttpRequest("removeprojectblack", json, out response);
		}

		public bool isBanOfGroup(long gid, long uid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;
            json["uid"] = uid;

            string response;
            bool ok = this.sendHttpRequest("isbanofgroup", json, out response);
			if (ok) {
				return (bool)JsonValue.Parse(response)["ok"];
			}	
			return false;
		}

		public bool isBanOfRoom(long rid, long uid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["uid"] = uid;

            string response;
            bool ok = this.sendHttpRequest("isbanofroom", json, out response);
			if (ok) {
				return (bool)JsonValue.Parse(response)["ok"];
			}
			return false;
		}

		public bool isProjectBlack(long uid)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            string response;
            bool ok = this.sendHttpRequest("isprojectblack", json, out response);
			if (ok) {
				return (bool)JsonValue.Parse(response)["ok"];
			}
			return false;
		}

		public Dictionary<string, dynamic> getGroupMessage(long gid, Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;
            json["num"] = num;
            json["desc"] = desc;
			if (begin > 0)
				json["begin"] = begin;
			if (end > 0)
				json["end"] = end;
			if (lastId > 0)
				json["lastId"] = lastId;

            string response;
            bool ok = this.sendHttpRequest("getgroupmsg", json, out response);
            if (ok) {
		        Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
                
                JsonValue node = JsonValue.Parse(response);
                map["num"] = (int)node["num"];
                map["lastid"] = (long)node["lastid"];
                map["begin"] = (long)node["begin"];
                map["end"] = (long)node["end"];

				JsonValue msgList = node["msgs"];
                GroupMsg[] msgs = new GroupMsg[msgList.Count];
                for (int i = 0; i < msgList.Count; i++)
                {
                    GroupMsg msg = new GroupMsg();
                    msg.id = (long)msgList[i][0];
                    msg.from = (long)msgList[i][1];
                    msg.mtype = (byte)msgList[i][2];
                    msg.mid = (long)msgList[i][3];
                    msg.deleted = (bool)msgList[i][4];
                    msg.msg = (string)msgList[i][5];
                    msg.attrs = (string)msgList[i][6];
                    msg.mtime = (long)msgList[i][7];

                    msgs[i] = msg;
                }
                map["msgs"] = msgs;

                return map; 
            }
			return new Dictionary<string, dynamic>();
		}

		public Dictionary<string, dynamic> getRoomMessage(long rid, Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["num"] = num;
            json["desc"] = desc;
			if (begin > 0)
				json["begin"] = begin;
			if (end > 0)
				json["end"] = end;
			if (lastId > 0)
				json["lastId"] = lastId;

            string response;
            bool ok = this.sendHttpRequest("getroommsg", json, out response);
            if (ok) {
		        Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
                
                JsonValue node = JsonValue.Parse(response);
                map["num"] = (int)node["num"];
                map["lastid"] = (long)node["lastid"];
                map["begin"] = (long)node["begin"];
                map["end"] = (long)node["end"];

				JsonValue msgList = node["msgs"];
                RoomMsg[] msgs = new RoomMsg[msgList.Count];
                for (int i = 0; i < msgList.Count; i++)
                {
                    RoomMsg msg = new RoomMsg();
                    msg.id = (long)msgList[i][0];
                    msg.from = (long)msgList[i][1];
                    msg.mtype = (byte)msgList[i][2];
                    msg.mid = (long)msgList[i][3];
                    msg.deleted = (bool)msgList[i][4];
                    msg.msg = (string)msgList[i][5];
                    msg.attrs = (string)msgList[i][6];
                    msg.mtime = (long)msgList[i][7];

                    msgs[i] = msg;
                }
                map["msgs"] = msgs;

                return map; 
            }
			return new Dictionary<string, dynamic>();
		}
        
        public Dictionary<string, dynamic> getBroadcastMessage(Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["num"] = num;
            json["desc"] = desc;
			if (begin > 0)
				json["begin"] = begin;
			if (end > 0)
				json["end"] = end;
			if (lastId > 0)
				json["lastId"] = lastId;

            string response;
            bool ok = this.sendHttpRequest("getbroadcastmsg", json, out response);
            if (ok) {
		        Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
                
                JsonValue node = JsonValue.Parse(response);
                map["num"] = (int)node["num"];
                map["lastid"] = (long)node["lastid"];
                map["begin"] = (long)node["begin"];
                map["end"] = (long)node["end"];

				JsonValue msgList = node["msgs"];
                BroadcastMsg[] msgs = new BroadcastMsg[msgList.Count];
                for (int i = 0; i < msgList.Count; i++)
                {
                    BroadcastMsg msg = new BroadcastMsg();
                    msg.id = (long)msgList[i][0];
                    msg.from = (long)msgList[i][1];
                    msg.mtype = (byte)msgList[i][2];
                    msg.mid = (long)msgList[i][3];
                    msg.deleted = (bool)msgList[i][4];
                    msg.msg = (string)msgList[i][5];
                    msg.attrs = (string)msgList[i][6];
                    msg.mtime = (long)msgList[i][7];

                    msgs[i] = msg;
                }
                map["msgs"] = msgs;

                return map; 
            }
			return new Dictionary<string, dynamic>();
		}

        public Dictionary<string, dynamic> getP2PMessage(long uid, long ouid, Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)
		{
			long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;
            json["ouid"] = ouid;
            json["num"] = num;
            json["desc"] = desc;
			if (begin > 0)
				json["begin"] = begin;
			if (end > 0)
				json["end"] = end;
			if (lastId > 0)
				json["lastId"] = lastId;

            string response;
            bool ok = this.sendHttpRequest("getp2pmsg", json, out response);
			if (ok) {
		        Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
                
                JsonValue node = JsonValue.Parse(response);
                map["num"] = (int)node["num"];
                map["lastid"] = (long)node["lastid"];
                map["begin"] = (long)node["begin"];
                map["end"] = (long)node["end"];

				JsonValue msgList = node["msgs"];
                P2PMsg[] msgs = new P2PMsg[msgList.Count];
                for (int i = 0; i < msgList.Count; i++)
                {
                    P2PMsg msg = new P2PMsg();
                    msg.id = (long)msgList[i][0];
                    msg.direction = (byte)msgList[i][1];
                    msg.mtype = (byte)msgList[i][2];
                    msg.mid = (long)msgList[i][3];
                    msg.deleted = (bool)msgList[i][4];
                    msg.msg = (string)msgList[i][5];
                    msg.attrs = (string)msgList[i][6];
                    msg.mtime = (long)msgList[i][7];

                    msgs[i] = msg;
                }
                map["msgs"] = msgs;

                return map; 
            }
			return new Dictionary<string, dynamic>();
		}

        public bool addRoomMember(long rid, long uid)
        {
            long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["uid"] = uid;

			string response;
            return this.sendHttpRequest("addroommember", json, out response);
        }

        public bool deleteRoomMember(long rid, long uid)
        {
            long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["uid"] = uid;

			string response;
            return this.sendHttpRequest("delroommember", json, out response);
        }

        public bool deleteMessage(long mid, long from, long xid, byte type)
        {
            long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mid"] = mid;
            json["from"] = from;
            json["xid"] = xid;
            json["type"] = type;

			string response;
            return this.sendHttpRequest("delmsg", json, out response);
        }

        public bool kickout(long uid, string ce = "")
        {
            long salt = this.genSalt();
            JsonObject json = new JsonObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;
            if (ce != "")
                json["ce"] = ce;

			string response;
            return this.sendHttpRequest("kickout", json, out response);
        }
    }
}
