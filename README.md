#### API ####

* `bool sendMessage(long from, long to, byte mtype, string msg, string attrs)`: 发送消息
    * `from`: **(long)** 发送方 id
    * `to`: **(long)** 接收方uid
    * `mtype`: **(byte)** 消息类型
    * `msg`: **(String)** 消息内容
    * `attrs`: **(String)** 消息附加信息, 没有可传`""`


* `bool sendMessages(long from, long[] tos, byte mtype, string msg, string attrs)`: 发送多人消息
    * `from`: **(long)** 发送方 id
    * `tos`: **(List(Long))** 接收方uids
    * `mtype`: **(byte)** 消息类型
    * `msg`: **(String)** 消息内容
    * `attrs`: **(String)** 消息附加信息, 没有可传`""`


* `bool sendGroupMessage(long from, long gid, byte mtype, string msg, string attrs)`: 发送group消息
    * `from`: **(long)** 发送方 id
    * `gid`: **(long)** group id
    * `mtype`: **(byte)** 消息类型
    * `msg`: **(String)** 消息内容
    * `attrs`: **(String)** 消息附加信息, 可传`""`


* `bool sendRoomMessage(long from, long rid, byte mtype, string msg, string attrs)`: 发送room消息
    * `from`: **(long)** 发送方 id
    * `rid`: **(long)** room id
    * `mtype`: **(byte)** 消息类型
    * `msg`: **(String)** 消息内容
    * `attrs`: **(String)** 消息附加信息, 可传`""`


* `bool broadcastMessage(long from, byte mtype, string msg, string attrs)`: 广播消息(andmin id)
    * `from`: **(long)** admin id
    * `mtype`: **(byte)** 消息类型
    * `msg`: **(String)** 消息内容
    * `attrs`: **(String)** 消息附加信息, 可传`""`

* `bool addFriends(long uid, long[] friends)`: 添加好友，每次最多添加100人
    * `uid`: **(long)** 用户 id
    * `friends`: **(List(Long))** 多个好友 id


* `bool deleteFriends(long uid, long[] friends)`: 删除好友, 每次最多删除100人
    * `uid`: **(long)** 用户 id
    * `friends`: **(List(Long))** 多个好友 id


* `long[] getFriends(long uid)`: 获取好友
    * `uid`: **(long)** 用户 id


* `bool isFriend(long uid, long fuid)`: 判断好友关系
    * `uid`: **(long)** 用户 id
    * `fuid`: **(long)** 好友 id


* `long[] isFriends(long uid, long[] fuids)`: 过滤好友关系, 每次最多过滤100人
    * `uid`: **(long)** 用户 id
    * `fuids`: **(List(Long))** 多个好友 id


* `bool addGroupMembers(long gid, long[] uids)`: 添加group成员, 每次最多添加100人
    * `gid`: **(long)** group id
    * `uids`: **(List(Long))** 多个用户 id


* `bool deleteGroupMembers(long gid, long[] uids)`:  删除group成员, 每次最多删除100人
    * `gid`: **(long)** group id
    * `uids`: **(List(Long))** 多个用户 id


* `bool deleteGroup(long gid)`: 删除group
    * `gid`: **(long)** group id


* `long[] getGroupMembers(long gid)`: 获取group成员
    * `gid`: **(long)** group id


* `bool isGroupMember(long gid, long uid)`: 是否group成员
    * `gid`: **(long)** group id
    * `uid`: **(long)** 用户 id


* `long[] getUserGroups(long uid)`: 获取用户的group
    * `uid`: **(long)** 用户 id


* `string getToken(long uid)`: 获取auth token
    * `uid`: **(long)** 用户 id

* `long[] getOnlineUsers(long[] uids)`: 获取在线用户, 每次最多获取200个
    * `uids`: **(List(Long))** 多个用户 id


* `bool addGroupBan(long gid, long uid, int btime)`: 阻止用户消息(group)
    * `gid`: **(long)** group id
    * `uid`: **(long)** 用户 id
    * `btime`: **(int)** 阻止时间(s)


* `bool removeGroupBan(long gid, long uid)`: 取消阻止(group)
    * `gid`: **(long)** group id
    * `uid`: **(long)** 用户 id


* `bool addRoomBan(long rid, long uid, int btime)`: 阻止用户消息(room)
    * `rid`: **(long)** room id
    * `uid`: **(long)** 用户 id
    * `btime`: **(int)** 阻止时间(s)


* `bool removeRoomBan(long rid, long uid)`: 取消阻止(room)
    * `rid`: **(long)** room id
    * `uid`: **(long)** 用户 id


* `bool addProjectBlack(long uid, int btime)`: 阻止用户消息(project)
    * `uid`: **(long)** 用户 id
    * `btime`: **(int)** 阻止时间(s)


* `bool removeProjectBlack(int uid)`: 取消阻止(project)
    * `uid`: **(long)** 用户 id


* `bool isBanOfGroup(long gid, long uid)`: 检查阻止(group)
    * `gid`: **(long)** group id
    * `uid`: **(long)** 用户 id


* `bool isBanOfRoom(long rid, long uid)`: 检查阻止(room)
    * `rid`: **(long)** room id
    * `uid`: **(long)** 用户 id


* `bool isProjectBlack(long uid)`: 检查阻止(project)
    * `uid`: **(long)** 用户 id


* `Dictionary<string, dynamic> getGroupMessage(long gid, Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)`: 获取Group历史消息
    * `gid`: **(long)** Group id
    * `desc`: **(boolean)** `true`: 则从`end`的时间戳开始倒序翻页, `false`: 则从`begin`的时间戳顺序翻页
    * `num`: **(int)** 获取数量, **一次最多获取20条, 建议10条**
    * `begin`: **(long)** 开始时间戳, 毫秒, 默认`0`, 条件：`>=`
    * `end`: **(long)** 结束时间戳, 毫秒, 默认`0`, 条件：`<=`
    * `lastid`: **(long)** 最后一条消息的id, 第一次默认传`0`, 条件：`> or <`
    * 返回值：
        * Dictionary(num:int,lastid:long,begin:long,end:long,msgs:GroupMsg[])
            * `GroupMsg.id` **(long)**
            * `GroupMsg.from` **(long)**
            * `GroupMsg.mtype` **(byte)**
            * `GroupMsg.mid` **(long)**
            * `GroupMsg.deleted` **(boolean)**
            * `GroupMsg.msg` **(String)**
            * `GroupMsg.attrs` **(String)**
            * `GroupMsg.mtime` **(long)**


* `Dictionary<string, dynamic> getRoomMessage(long rid, Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)`: 获取Room历史消息
    * `rid`: **(long)** Room id
    * `desc`: **(boolean)** `true`: 则从`end`的时间戳开始倒序翻页, `false`: 则从`begin`的时间戳顺序翻页
    * `num`: **(int)** 获取数量, **一次最多获取20条, 建议10条**
    * `begin`: **(long)** 开始时间戳, 毫秒, 默认`0`, 条件：`>=`
    * `end`: **(long)** 结束时间戳, 毫秒, 默认`0`, 条件：`<=`
    * `lastid`: **(long)** 最后一条消息的id, 第一次默认传`0`, 条件：`> or <`
    * 返回值：
        * Dictionary(num:int,lastid:long,begin:long,end:long,msgs:RoomMsg[])
            * `RoomMsg.id` **(long)**
            * `RoomMsg.from` **(long)**
            * `RoomMsg.mtype` **(byte)**
            * `RoomMsg.mid` **(long)**
            * `RoomMsg.deleted` **(boolean)**
            * `RoomMsg.msg` **(String)**
            * `RoomMsg.attrs` **(String)**
            * `RoomMsg.mtime` **(long)**


* `Dictionary<string, dynamic> getBroadcastMessage(Int16 num, bool desc, long begin = 0, long end = 0, long lastId = 0)`: 获取广播历史消息
    * `desc`: **(boolean)** `true`: 则从`end`的时间戳开始倒序翻页, `false`: 则从`begin`的时间戳顺序翻页
    * `num`: **(int)** 获取数量, **一次最多获取20条, 建议10条**
    * `begin`: **(long)** 开始时间戳, 毫秒, 默认`0`, 条件：`>=`
    * `end`: **(long)** 结束时间戳, 毫秒, 默认`0`, 条件：`<=`
    * `lastid`: **(long)** 最后一条消息的id, 第一次默认传`0`, 条件：`> or <`
    * 返回值：
        * Dictionary(num:int,lastid:long,begin:long,end:long,msgs:BroadcastMsg[])
            * `BroadcastMsg.id` **(long)**
            * `BroadcastMsg.from` **(long)**
            * `BroadcastMsg.mtype` **(byte)**
            * `BroadcastMsg.mid` **(long)**
            * `BroadcastMsg.deleted` **(boolean)**
            * `BroadcastMsg.msg` **(String)**
            * `BroadcastMsg.attrs` **(String)**
            * `BroadcastMsg.mtime` **(long)**


* `Dictionary<string, dynamic> getP2PMessage(long uid, long ouid, Int16 num, bool desc, long begin = 0, long end = 0, long lastid = 0)`: 获取P2P历史消息
    * `uid`: **(long)** 获取和两个用户之间的历史消息
    * `ouid`: **(long)** 获取和两个用户之间的历史消息
    * `desc`: **(boolean)** `true`: 则从`end`的时间戳开始倒序翻页, `false`: 则从`begin`的时间戳顺序翻页
    * `num`: **(int)** 获取数量, **一次最多获取20条, 建议10条**
    * `begin`: **(long)** 开始时间戳, 毫秒, 默认`0`, 条件：`>=`
    * `end`: **(long)** 结束时间戳, 毫秒, 默认`0`, 条件：`<=`
    * `lastid`: **(long)** 最后一条消息的id, 第一次默认传`0`, 条件：`> or <`
    * 返回值：
        * Dictionary(num:int,lastid:long,begin:long,end:long,msgs:P2PMsg[])
            * `P2PMsg.id` **(long)**
            * `P2PMsg.direction` **(byte)**
            * `P2PMsg.mtype` **(byte)**
            * `P2PMsg.mid` **(long)**
            * `P2PMsg.deleted` **(boolean)**
            * `P2PMsg.msg` **(String)**
            * `P2PMsg.attrs` **(String)**
            * `P2PMsg.mtime` **(long)**


* `bool addRoomMember(long rid, long uid)`: 添加Room成员
    * `rid`: **(long)** Room id
    * `uid`: **(long)** 用户 id


* `bool deleteRoomMember(long rid, long uid)`: 删除Room成员
    * `rid`: **(long)** Room id
    * `uid`: **(long)** 用户 id


* `bool deleteMessage(long mid, long from, long xid, byte type)`: 删除消息
    * `mid`: **(long)** 消息 id
    * `from`: **(long)** 发送方 id
    * `xid`: **(long)** 接收放 id, `rid/gid/to`
    * `type`: **(byte)** 消息发送分类 `1:P2P, 2:Group, 3:Room, 4:Broadcast`


* `bool kickout(long uid, string ce = "")`: 踢掉一个用户或者一个链接
    * `uid`: **(long)** 用户 id
    * `ce`: **(String)** 踢掉`ce`对应链接, 多用户登录情况
