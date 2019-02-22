using System;
using System.Text;
using System.Net.Http;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

    class HttpResult {
        public bool ok = false;
        public string response = ""; 
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
        
        private async Task<HttpResult> sendHttpRequest(string method, JObject json)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://" + this.endpoint + "/service/" + method);
                requestMessage.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var resp = await client.SendAsync(requestMessage);
                if (resp.IsSuccessStatusCode) {
                    HttpResult result = new HttpResult();
                    result.ok = true;
                    result.response = await resp.Content.ReadAsStringAsync();
                    return result;
                } else {
                    string response = await resp.Content.ReadAsStringAsync();
                    throw new Exception(response);
                }
            }
        }

        public async Task<long> sendMessage(long from, long to, byte mtype, string msg, string attrs, long mid = 0)
        {
            long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mtype"] = mtype;
            json["from"] = from;
            json["to"] = to;

            if (mid == 0)
                mid = this.genMid();

            json["mid"] = mid;
            json["msg"] = msg;
            json["attrs"] = attrs;
       
            HttpResult result = await this.sendHttpRequest("sendmsg", json);
            if (result.ok)
                return mid;
            else
                return 0;
        }

        public async Task<long> sendMessages(long from, long[] tos, byte mtype, string msg, string attrs, long mid = 0)
        {
            long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mtype"] = mtype;
            json["from"] = from;

            if (mid == 0)
                mid = this.genMid();

            json["mid"] = mid;
            json["msg"] = msg;
            json["attrs"] = attrs;

            JArray arr = new JArray();
            foreach (long l in tos)
            {
                arr.Add(l);
            }
            json["tos"] = arr;

            HttpResult result = await this.sendHttpRequest("sendmsgs", json);
            if (result.ok)
                return mid;
            else
                return 0;
        }

        public async Task<long> sendGroupMessage(long from, long gid, byte mtype, string msg, string attrs, long mid = 0) 
        {
            long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mtype"] = mtype;
            json["from"] = from;
            json["gid"] = gid;

            if (mid == 0)
                mid = this.genMid();

            json["mid"] = mid;
            json["msg"] = msg;
            json["attrs"] = attrs;
            
            HttpResult result = await this.sendHttpRequest("sendgroupmsg", json);
            if (result.ok)
                return mid;
            else
                return 0; 
        }

        public async Task<long> sendRoomMessage(long from, long rid, byte mtype, string msg, string attrs, long mid = 0)
        {
            long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mtype"] = mtype;
            json["from"] = from;
            json["rid"] = rid;

            if (mid == 0)
                mid = this.genMid();

            json["mid"] = mid;
            json["msg"] = msg;
            json["attrs"] = attrs;
            
            HttpResult result = await this.sendHttpRequest("sendroommsg", json);
            if (result.ok)
                return mid;
            else
                return 0;
        }

		public async Task<long> broadcastMessage(long from, byte mtype, string msg, string attrs, long mid = 0)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mtype"] = mtype;
            json["from"] = from;

            if (mid == 0)
                mid = this.genMid();

            json["mid"] = mid;
            json["msg"] = msg;
            json["attrs"] = attrs;
            
            HttpResult result = await this.sendHttpRequest("broadcastmsg", json);
            if (result.ok)
                return mid;
            else
                return 0;
		}

		public async Task<bool> addFriends(long uid, long[] friends)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            JArray arr = new JArray();
            foreach (long l in friends)
            {
                arr.Add(l);                
            }
            json["friends"] = arr;

            HttpResult result = await this.sendHttpRequest("addfriends", json);
            return result.ok;
		}

		public async Task<bool> deleteFriends(long uid, long[] friends)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            JArray arr = new JArray();
            foreach (long l in friends)
            {
                arr.Add(l);                
            }
            json["friends"] = arr;

            HttpResult result =  await this.sendHttpRequest("delfriends", json);
            return result.ok;
		}

		public async Task<long[]> getFriends(long uid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("getfriends", json);

			if (result.ok)
			{
				JArray uidsList = (JArray)JObject.Parse(result.response)["uids"];
				long[] uids = new long[uidsList.Count];
				for (int i = 0; i < uidsList.Count; i++)
					uids[i] = ((long)uidsList[i]);
				return uids;
			}
			return new long[0];
		}

		public async Task<bool> isFriend(long uid, long fuid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;
            json["fuid"] = fuid;

            HttpResult result = await this.sendHttpRequest("isfriend", json);
			if (result.ok) {
				return (bool)JObject.Parse(result.response)["ok"];
			}	
			return false;
		}

		public async Task<long[]> isFriends(long uid, long[] fuids)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

			JArray arr = new JArray();
            foreach (long l in fuids)
            {
                arr.Add(l);                
            }
            json["fuids"] = arr;

            HttpResult result = await this.sendHttpRequest("isfriends", json);

			if (result.ok)
			{
				JArray uidsList = (JArray)JObject.Parse(result.response)["fuids"];
				long[] uids = new long[uidsList.Count];
				for (int i = 0; i < uidsList.Count; i++)
					uids[i] = ((long)uidsList[i]);
				return uids;
			}
			return new long[0];
		}

		public async Task<bool> addGroupMembers(long gid, long[] uids)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;

			JArray arr = new JArray();
            foreach (long l in uids)
            {
                arr.Add(l);                
            }
            json["uids"] = arr;

            HttpResult result = await this.sendHttpRequest("addgroupmembers", json);
            return result.ok;
		}

		public async Task<bool> deleteGroupMembers(long gid, long[] uids)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;

			JArray arr = new JArray();
            foreach (long l in uids)
            {
                arr.Add(l);                
            }
            json["uids"] = arr;

            HttpResult result = await this.sendHttpRequest("delgroupmembers", json);
            return result.ok;
		}

		public async Task<bool> deleteGroup(long gid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;

            HttpResult result = await this.sendHttpRequest("delgroup", json);
            return result.ok;
		}

		public async Task<long[]> getGroupMembers(long gid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;

            HttpResult result = await this.sendHttpRequest("getgroupmembers", json);

			if (result.ok)
			{
				JArray uidsList = (JArray)JObject.Parse(result.response)["uids"];
				long[] uids = new long[uidsList.Count];
				for (int i = 0; i < uidsList.Count; i++)
					uids[i] = ((long)uidsList[i]);
				return uids;
			}
			return new long[0];
		}

		public async Task<bool> isGroupMember(long gid, long uid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("isgroupmember", json);
			if (result.ok) {
				return (bool)JObject.Parse(result.response)["ok"];
			}
			return false;
		}

		public async Task<long[]> getUserGroups(long uid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("getusergroups", json);

			if (result.ok)
			{
				JArray gidsList = (JArray)JObject.Parse(result.response)["gids"];
				long[] gids = new long[gidsList.Count];
				for (int i = 0; i < gidsList.Count; i++)
					gids[i] = ((long)gidsList[i]);
				return gids;
			}
			return new long[0];
		}

		public async Task<string> getToken(long uid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("gettoken", json);
			if (result.ok) {
				return (string)JObject.Parse(result.response)["token"];
			}
			return "";
		}

		public async Task<long[]> getOnlineUsers(long[] uids)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;

			JArray arr = new JArray();
            foreach (long l in uids)
            {
                arr.Add(l);                
            }
            json["uids"] = arr;

            HttpResult result = await this.sendHttpRequest("getonlineusers", json);
			if (result.ok)
			{
				JArray uidsList = (JArray)JObject.Parse(result.response)["uids"];
				long[] uidsArray = new long[uidsList.Count];
				for (int i = 0; i < uidsList.Count; i++)
					uidsArray[i] = ((long)uidsList[i]);
				return uidsArray;
			}
			return new long[0];
		}

		public async Task<bool> addGroupBan(long gid, long uid, int btime)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;
            json["uid"] = uid;
            json["btime"] = btime;

            HttpResult result = await this.sendHttpRequest("addgroupban", json);
            return result.ok;
		}

		public async Task<bool> removeGroupBan(long gid, long uid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("removegroupban", json);
            return result.ok;
		}
		
		public async Task<bool> addRoomBan(long rid, long uid, int btime)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["uid"] = uid;
            json["btime"] = btime;

            HttpResult result = await this.sendHttpRequest("addroomban", json);
            return result.ok;
		}

		public async Task<bool> removeRoomBan(long rid, long uid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("removeroomban", json);
            return result.ok;
		}

		public async Task<bool> addProjectBlack(long uid, int btime)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;
            json["btime"] = btime;

            HttpResult result = await this.sendHttpRequest("addprojectblack", json);
            return result.ok;
		}

		public async Task<bool> removeProjectBlack(long uid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("removeprojectblack", json);
            return result.ok;
		}

		public async Task<bool> isBanOfGroup(long gid, long uid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["gid"] = gid;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("isbanofgroup", json);
			if (result.ok) {
				return (bool)JObject.Parse(result.response)["ok"];
			}	
			return false;
		}

		public async Task<bool> isBanOfRoom(long rid, long uid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("isbanofroom", json);
			if (result.ok) {
				return (bool)JObject.Parse(result.response)["ok"];
			}
			return false;
		}

		public async Task<bool> isProjectBlack(long uid)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("isprojectblack", json);
			if (result.ok) {
				return (bool)JObject.Parse(result.response)["ok"];
			}
			return false;
		}

		public async Task<Dictionary<string, dynamic>> getGroupMessage(long gid, Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
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

            HttpResult result = await this.sendHttpRequest("getgroupmsg", json);
            if (result.ok) {
		        Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
                
                JObject node = JObject.Parse(result.response);
                map["num"] = (int)node["num"];
                map["lastid"] = (long)node["lastid"];
                map["begin"] = (long)node["begin"];
                map["end"] = (long)node["end"];

				JArray msgList = (JArray)node["msgs"];
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

		public async Task<Dictionary<string, dynamic>> getRoomMessage(long rid, Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
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

            HttpResult result = await this.sendHttpRequest("getroommsg", json);
            if (result.ok) {
		        Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
                
                JObject node = JObject.Parse(result.response);
                map["num"] = (int)node["num"];
                map["lastid"] = (long)node["lastid"];
                map["begin"] = (long)node["begin"];
                map["end"] = (long)node["end"];

				JArray msgList = (JArray)node["msgs"];
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
        
        public async Task<Dictionary<string, dynamic>> getBroadcastMessage(Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
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

            HttpResult result = await this.sendHttpRequest("getbroadcastmsg", json);
            if (result.ok) {
		        Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
                
                JObject node = JObject.Parse(result.response);
                map["num"] = (int)node["num"];
                map["lastid"] = (long)node["lastid"];
                map["begin"] = (long)node["begin"];
                map["end"] = (long)node["end"];

				JArray msgList = (JArray)node["msgs"];
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

        public async Task<Dictionary<string, dynamic>> getP2PMessage(long uid, long ouid, Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)
		{
			long salt = this.genSalt();
            JObject json = new JObject();
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

            HttpResult result = await this.sendHttpRequest("getp2pmsg", json);
			if (result.ok) {
		        Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
                
                JObject node = JObject.Parse(result.response);
                map["num"] = (int)node["num"];
                map["lastid"] = (long)node["lastid"];
                map["begin"] = (long)node["begin"];
                map["end"] = (long)node["end"];

				JArray msgList = (JArray)node["msgs"];
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

        public async Task<bool> addRoomMember(long rid, long uid)
        {
            long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("addroommember", json);
            return result.ok;
        }

        public async Task<bool> deleteRoomMember(long rid, long uid)
        {
            long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["rid"] = rid;
            json["uid"] = uid;

            HttpResult result = await this.sendHttpRequest("delroommember", json);
            return result.ok;
        }

        public async Task<bool> deleteMessage(long mid, long from, long xid, byte type)
        {
            long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["mid"] = mid;
            json["from"] = from;
            json["xid"] = xid;
            json["type"] = type;

            HttpResult result = await this.sendHttpRequest("delmsg", json);
            return result.ok;
        }

        public async Task<bool> kickout(long uid, string ce = "")
        {
            long salt = this.genSalt();
            JObject json = new JObject();
            json["pid"] = this.pid;
            json["sign"] = this.genSign(salt);
            json["salt"] = salt;
            json["uid"] = uid;
            if (ce != "")
                json["ce"] = ce;

            HttpResult result = await this.sendHttpRequest("kickout", json);
            return result.ok;
        }
    }
}
