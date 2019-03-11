# fpnn rtm sdk csharp #


#### 依赖 ####
* MessagePack
* Newtonsoft.Json


#### 例子 ####

```csharp

RTMServerClient client = new RTMServerClient(11000001, "ef3617e5-e886-4a4e-9eef-7263c0320628", "52.83.245.22", 13315, true, 5000);


// 添加服务端监听
client.AddPushListener(RTMConfig.SERVER_PUSH.recvMessage, (data) =>
{
    // `data` is a Hashtable map
});

// 建立连接回调
client.ConnectedCallback = delegate()
{
    client.SendMessage(1, 123, 2, "a msg", "a attrs", 0, 0, (data, exception) =>
    {
        if (exception == null)
        {
            // success, the `data` is answer Hashtable map
        }
        else
        {
            Console.WriteLine(exception.Message);
        }
    });
};

// 连接关闭
client.ClosedCallback = delegate()
{
    Console.WriteLine("closed");
};

// 错误
client.ErrorCallback = delegate(Exception e)
{
    Console.WriteLine(e.Message);
};

client.Connect();
// client.Close();

```


#### 支持的服务端监听类型 ####

请参考 `RTMConfig.SERVER_PUSH` 成员

* `pushmsg`: RTMGate主动推送P2P消息
    * `data.from`: **(long)** 发送者 id
    * `data.to`: **(long)** 接收者 id
    * `data.mtype`: **(byte)** 消息类型
    * `data.mid`: **(long)** 消息 id, 当前链接会话内唯一
    * `data.msg`: **(string)** 消息内容
    * `data.attrs`: **(string)** 发送时附加的自定义内容
    * `data.mtime`: **(long)** 

* `pushgroupmsg`: RTMGate主动推送Group消息
    * `data.from`: **(long)** 发送者 id
    * `data.gid`: **(long)** Group id
    * `data.mtype`: **(byte)** 消息类型
    * `data.mid`: **(long)** 消息 id, 当前链接会话内唯一
    * `data.msg`: **(string)** 消息内容
    * `data.attrs`: **(string)** 发送时附加的自定义内容
    * `data.mtime`: **(long)** 

* `pushroommsg`: RTMGate主动推送Room消息
    * `data.from`: **(long)** 发送者 id
    * `data.rid`: **(long)** Room id
    * `data.mtype`: **(byte)** 消息类型
    * `data.mid`: **(long)** 消息 id, 当前链接会话内唯一
    * `data.msg`: **(String)** 消息内容
    * `data.attrs`: **(String)** 发送时附加的自定义内容
    * `data.mtime`: **(long)** 

* `pushfile`: RTMGate主动推送P2P文件
    * `data.from`: **(long)** 发送者 id
    * `data.to`: **(long)** 接收者 id
    * `data.mtype`: **(byte)** 文件类型, 请参考 `RTMConfig.FILE_TYPE` 成员
    * `data.mid`: **(long)** 消息 id, 当前链接会话内唯一
    * `data.msg`: **(string)** 文件获取地址(url)
    * `data.attrs`: **(string)** 发送时附加的自定义内容
    * `data.mtime`: **(long)** 

* `pushgroupfile`: RTMGate主动推送Group文件
    * `data.from`: **(long)** 发送者 id
    * `data.gid`: **(long)** Group id
    * `data.mtype`: **(byte)** 文件类型, 请参考 `RTMConfig.FILE_TYPE` 成员
    * `data.mid`: **(long)** 消息 id, 当前链接会话内唯一
    * `data.msg`: **(string)** 文件获取地址(url)
    * `data.attrs`: **(string)** 发送时附加的自定义内容
    * `data.mtime`: **(long)** 

* `pushroomfile`: RTMGate主动推送Room文件
    * `data.from`: **(long)** 发送者 id
    * `data.rid`: **(long)** Room id
    * `data.mtype`: **(byte)** 文件类型, 请参考 `RTMConfig.FILE_TYPE` 成员
    * `data.mid`: **(long)** 消息 id, 当前链接会话内唯一
    * `data.msg`: **(string)** 文件获取地址(url)
    * `data.attrs`: **(string)** 发送时附加的自定义内容
    * `data.mtime`: **(long)** 

* `pushevent`: RTMGate主动推送事件 (目前仅支持2个event: login和logout)
    * `data.event`: **(string)** 事件名称, 请参考 `RTMConfig.SERVER_EVENT` 成员
    * `data.uid`: **(long)** 触发者 id
    * `data.time`: **(int)** 触发时间(s)
    * `data.endpoint`: **(string)** 对应的RTMGate地址
    * `data.data`: **(string)** `预留`
    

#### API ####

* `constructor(int pid, string secret, string host, int port, bool reconnect, int timeout)`: 构造RTMServerClient
    * `pid`: **(int)** 应用编号, RTM提供
    * `secret`: **(String)** 应用加密, RTM提供
    * `host`: **(String)** 地址, RTM提供
    * `port`: **(int)** 端口, RTM提供
    * `reconnect`: **(boolean)** 是否自动重连
    * `timeout`: **(int)** 连接超时时间(ms)

* `Connect()`: 建立连接

* `Reconnect()`: 重连

* `Close()`: 断开连接并销毁

* `AddPushListener(string name, RTMPushCallbackDelegate cb)` : 添加服务端监听

* `SendMessage(long from, long to, byte mtype, string msg, string attrs, long mid, int timeout, CallbackDelegate cb)`: 发送消息
    * `from`: **(long)** 发送方 id
    * `to`: **(long)** 接收方uid
    * `mtype`: **(byte)** 消息类型
    * `msg`: **(string)** 消息内容
    * `attrs`: **(string)** 消息附加信息, 没有可传`""`
    * `mid`: **(long)** 消息 id, 用于过滤重复消息, 非重发时为`0`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.mid`: **(long)** 消息的mid
            * `data.mtime`: **(long)** 毫秒时间戳
        * `exception`: **Exception** 当成功时exception=null
        
* `SendMessages(long from, long[] tos, byte mtype, string msg, string attrs, long mid, int timeout, CallbackDelegate cb)`: 发送多人消息
    * `from`: **(long)** 发送方 id
    * `tos`: **(long[])** 接收方uids
    * `mtype`: **(byte)** 消息类型
    * `msg`: **(string)** 消息内容
    * `attrs`: **(string)** 消息附加信息, 没有可传`""`
    * `mid`: **(long)** 消息 id, 用于过滤重复消息, 非重发时为`0`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.mid`: **(long)** 消息的mid
            * `data.mtime`: **(long)** 毫秒时间戳
        * `exception`: **Exception** 当成功时exception=null
        
* `SendGroupMessage(long from, long gid, byte mtype, string msg, string attrs, long mid, int timeout, CallbackDelegate cb)`: 发送group消息
    * `from`: **(long)** 发送方 id
    * `gid`: **(long)** group id
    * `mtype`: **(byte)** 消息类型
    * `msg`: **(string)** 消息内容
    * `attrs`: **(string)** 消息附加信息, 可传`""`
    * `mid`: **(long)** 消息 id, 用于过滤重复消息, 非重发时为`0`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.mid`: **(long)** 消息的mid
            * `data.mtime`: **(long)** 毫秒时间戳
        * `exception`: **Exception** 当成功时exception=null

* `SendRoomMessage(long from, long rid, byte mtype, string msg, string attrs, long mid, int timeout, CallbackDelegate cb)`: 发送room消息
    * `from`: **(long)** 发送方 id
    * `rid`: **(long)** room id
    * `mtype`: **(byte)** 消息类型
    * `msg`: **(string)** 消息内容
    * `attrs`: **(string)** 消息附加信息, 可传`""`
    * `mid`: **(long)** 消息 id, 用于过滤重复消息, 非重发时为`0`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.mid`: **(long)** 消息的mid
            * `data.mtime`: **(long)** 毫秒时间戳
        * `exception`: **Exception** 当成功时exception=null
        
* `BroadcastMessage(long from, byte mtype, string msg, string attrs, long mid, int timeout, CallbackDelegate cb)`: 广播消息(andmin id)
    * `from`: **(long)** admin id
    * `mtype`: **(byte)** 消息类型
    * `msg`: **(string)** 消息内容
    * `attrs`: **(string)** 消息附加信息, 可传`""`
    * `mid`: **(long)** 消息 id, 用于过滤重复消息, 非重发时为`0`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.mid`: **(long)** 消息的mid
            * `data.mtime`: **(long)** 毫秒时间戳
        * `exception`: **Exception** 当成功时exception=null

* `AddFriends(long uid, long[] friends, int timeout, CallbackDelegate cb)`: 添加好友，每次最多添加100人
    * `uid`: **(long)** 用户 id
    * `friends`: **(long[])** 多个好友 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `DeleteFriends(long uid, long[] friends, int timeout, CallbackDelegate cb))`: 删除好友, 每次最多删除100人
    * `uid`: **(long)** 用户 id
    * `friends`: **(long[])** 多个好友 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.uids`: **(long)** 消息的mid
        * `exception`: **Exception** 当成功时exception=null

* `GetFriends(long uid, int timeout, CallbackDelegate cb)`: 获取好友
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.uids`: **(long[])** 
        * `exception`: **Exception** 当成功时exception=null

* `IsFriend(long uid, long fuid, int timeout, CallbackDelegate cb)`: 判断好友关系
    * `uid`: **(long)** 用户 id
    * `fuid`: **(long)** 好友 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.ok`: **(bool)** 
        * `exception`: **Exception** 当成功时exception=null

* `IsFriends(long uid, long[] fuids, int timeout, CallbackDelegate cb)`: 过滤好友关系, 每次最多过滤100人
    * `uid`: **(long)** 用户 id
    * `fuids`: **(long[])** 多个好友 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.fuids`: **(long[])** 
        * `exception`: **Exception** 当成功时exception=null

* `AddGroupMembers(long gid, long[] uids, int timeout, CallbackDelegate cb)`: 添加group成员, 每次最多添加100人
    * `gid`: **(long)** group id
    * `uids`: **(long[])** 多个用户 id
    * `timeout`: **(int)** 超时时间(ms)
     * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `DeleteGroupMembers(long gid, long[] uids, int timeout, CallbackDelegate cb)`:  删除group成员, 每次最多删除100人
    * `gid`: **(long)** group id
    * `uids`: **(long[])** 多个用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `DeleteGroup(long gid, int timeout, CallbackDelegate cb)`: 删除group
    * `gid`: **(long)** group id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `GetGroupMembers(long gid, int timeout, CallbackDelegate cb)`: 获取group成员
    * `gid`: **(long)** group id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.uids`: **(long[])** 
        * `exception`: **Exception** 当成功时exception=null

* `IsGroupMember(long gid, long uid, int timeout, CallbackDelegate cb)`: 是否group成员
    * `gid`: **(long)** group id
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.ok`: **(bool)** 
        * `exception`: **Exception** 当成功时exception=null

* `GetUserGroups(long uid, int timeout, CallbackDelegate cb)`: 获取用户的group
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.gids`: **(long[])** 
        * `exception`: **Exception** 当成功时exception=null

* `GetToken(long uid, int timeout, CallbackDelegate cb)`: 获取auth token
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.token`: **(string)** 
        * `exception`: **Exception** 当成功时exception=null

* `GetOnlineUsers(long[] uids, int timeout, CallbackDelegate cb)`: 获取在线用户, 每次最多获取200个
    * `uids`: **(long[])** 多个用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.uids`: **(long[])** 
        * `exception`: **Exception** 当成功时exception=null

* `AddGroupBan(long gid, long uid, int btime, int timeout, CallbackDelegate cb)`: 阻止用户消息(group)
    * `gid`: **(long)** group id
    * `uid`: **(long)** 用户 id
    * `btime`: **(int)** 阻止时间(s)
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `RemoveGroupBan(long gid, long uid, int timeout, CallbackDelegate cb)`: 取消阻止(group)
    * `gid`: **(long)** group id
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `AddRoomBan(long rid, long uid, int btime, int timeout, CallbackDelegate cb)`: 阻止用户消息(room)
    * `rid`: **(long)** room id
    * `uid`: **(long)** 用户 id
    * `btime`: **(int)** 阻止时间(s)
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `RemoveRoomBan(long rid, long uid, int timeout, CallbackDelegate cb)`: 取消阻止(room)
    * `rid`: **(long)** room id
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null
        
* `AddProjectBlack(long uid, int btime, int timeout, CallbackDelegate cb)`: 阻止用户消息(project)
    * `uid`: **(long)** 用户 id
    * `btime`: **(int)** 阻止时间(s)
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `RemoveProjectBlack(int uid, int timeout, CallbackDelegate cb)`: 取消阻止(project)
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `IsBanOfGroup(long gid, long uid, int timeout, CallbackDelegate cb)`: 检查阻止(group)
    * `gid`: **(long)** group id
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.ok`: **(bool)** 
        * `exception`: **Exception** 当成功时exception=null

* `IsBanOfRoom(long rid, long uid, int timeout, CallbackDelegate cb)`: 检查阻止(room)
    * `rid`: **(long)** room id
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.ok`: **(bool)** 
        * `exception`: **Exception** 当成功时exception=null

* `IsProjectBlack(long uid, int timeout, CallbackDelegate cb)`: 检查阻止(project)
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.ok`: **(bool)** 
        * `exception`: **Exception** 当成功时exception=null

* `FileToken(long from, string cmd, long[] tos, long to, long rid, long gid, int timeout, CallbackDelegate cb)`: 获取发送文件的token
    * `from`: **(long)** 发送方 id
    * `cmd`: **(string)** 文件发送方式`sendfile | sendfiles | sendroomfile | sendgroupfile | broadcastfile`
    * `tos`: **(long[])** 接收方 uids
    * `to`: **(long)** 接收方 uid
    * `rid`: **(long)** Room id
    * `gid`: **(long)** Group id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.token`: **(string)** 
            * `data.endpoint`: **(string)** 
        * `exception`: **Exception** 当成功时exception=null

* `GetGroupMessage(long gid, bool desc, int num, long begin, long end, long lastid, int timeout, CallbackDelegate cb)`: 获取Group历史消息
    * `gid`: **(long)** Group id
    * `desc`: **(bool)** `true`: 则从`end`的时间戳开始倒序翻页, `false`: 则从`begin`的时间戳顺序翻页
    * `num`: **(int)** 获取数量, **一次最多获取20条, 建议10条**
    * `begin`: **(long)** 开始时间戳, 毫秒, 默认`0`, 条件：`>=`
    * `end`: **(long)** 结束时间戳, 毫秒, 默认`0`, 条件：`<=`
    * `lastid`: **(long)** 最后一条消息的id, 第一次默认传`0`, 条件：`> or <`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.num` **(long)**
            * `data.lastid` **(long)**
            * `data.begin` **(long)**
            * `data.end` **(long)**
            * `data.msgs` **(GroupMsg[])**
                * `GroupMsg.id` **(long)**
                * `GroupMsg.from` **(long)**
                * `GroupMsg.mtype` **(byte)**
                * `GroupMsg.mid` **(long)**
                * `GroupMsg.deleted` **(bool)**
                * `GroupMsg.msg` **(string)**
                * `GroupMsg.attrs` **(string)**
                * `GroupMsg.mtime` **(long)**
        * `exception`: **Exception** 当成功时exception=null

* `GetRoomMessage(long rid, bool desc, int num, long begin, long end, long lastid, int timeout, CallbackDelegate cb)`: 获取Room历史消息
    * `rid`: **(long)** Room id
    * `desc`: **(bool)** `true`: 则从`end`的时间戳开始倒序翻页, `false`: 则从`begin`的时间戳顺序翻页
    * `num`: **(int)** 获取数量, **一次最多获取20条, 建议10条**
    * `begin`: **(long)** 开始时间戳, 毫秒, 默认`0`, 条件：`>=`
    * `end`: **(long)** 结束时间戳, 毫秒, 默认`0`, 条件：`<=`
    * `lastid`: **(long)** 最后一条消息的id, 第一次默认传`0`, 条件：`> or <`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.num` **(long)**
            * `data.lastid` **(long)**
            * `data.begin` **(long)**
            * `data.end` **(long)**
            * `data.msgs` **(RoomMsg[])**
                * `RoomMsg.id` **(long)**
                * `RoomMsg.from` **(long)**
                * `RoomMsg.mtype` **(byte)**
                * `RoomMsg.mid` **(long)**
                * `RoomMsg.deleted` **(bool)**
                * `RoomMsg.msg` **(string)**
                * `RoomMsg.attrs` **(string)**
                * `RoomMsg.mtime` **(long)**
        * `exception`: **Exception** 当成功时exception=null

* `GetBroadcastMessage(bool desc, int num, long begin, long end, long lastid, int timeout, CallbackDelegate cb)`: 获取广播历史消息
    * `desc`: **(bool)** `true`: 则从`end`的时间戳开始倒序翻页, `false`: 则从`begin`的时间戳顺序翻页
    * `num`: **(int)** 获取数量, **一次最多获取20条, 建议10条**
    * `begin`: **(long)** 开始时间戳, 毫秒, 默认`0`, 条件：`>=`
    * `end`: **(long)** 结束时间戳, 毫秒, 默认`0`, 条件：`<=`
    * `lastid`: **(long)** 最后一条消息的id, 第一次默认传`0`, 条件：`> or <`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.num` **(long)**
            * `data.lastid` **(long)**
            * `data.begin` **(long)**
            * `data.end` **(long)**
            * `data.msgs` **(BroadcastMsg[])**
                * `BroadcastMsg.id` **(long)**
                * `BroadcastMsg.from` **(long)**
                * `BroadcastMsg.mtype` **(byte)**
                * `BroadcastMsg.mid` **(long)**
                * `BroadcastMsg.deleted` **(bool)**
                * `BroadcastMsg.msg` **(string)**
                * `BroadcastMsg.attrs` **(string)**
                * `BroadcastMsg.mtime` **(long)**
        * `exception`: **Exception** 当成功时exception=null

* `GetP2PMessage(long uid, long ouid, bool desc, int num, long begin, long end, long lastid, int timeout, CallbackDelegate cb)`: 获取P2P历史消息
    * `uid`: **(long)** 获取和两个用户之间的历史消息
    * `ouid`: **(long)** 获取和两个用户之间的历史消息
    * `desc`: **(bool)** `true`: 则从`end`的时间戳开始倒序翻页, `false`: 则从`begin`的时间戳顺序翻页
    * `num`: **(int)** 获取数量, **一次最多获取20条, 建议10条**
    * `begin`: **(long)** 开始时间戳, 毫秒, 默认`0`, 条件：`>=`
    * `end`: **(long)** 结束时间戳, 毫秒, 默认`0`, 条件：`<=`
    * `lastid`: **(long)** 最后一条消息的id, 第一次默认传`0`, 条件：`> or <`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `data.num` **(long)**
            * `data.lastid` **(long)**
            * `data.begin` **(long)**
            * `data.end` **(long)**
            * `data.msgs` **(P2PMsg[])**
                * `P2PMsg.id` **(long)**
                * `P2PMsg.direction` **(byte)**
                * `P2PMsg.mtype` **(byte)**
                * `P2PMsg.mid` **(long)**
                * `P2PMsg.deleted` **(boolean)**
                * `P2PMsg.msg` **(String)**
                * `P2PMsg.attrs` **(String)**
                * `P2PMsg.mtime` **(long)**
        * `exception`: **Exception** 当成功时exception=null

* `AddRoomMember(long rid, long uid, int timeout, CallbackDelegate cb)`: 添加Room成员
    * `rid`: **(long)** Room id
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `DeleteRoomMember(long rid, long uid, int timeout, CallbackDelegate cb)`: 删除Room成员
    * `rid`: **(long)** Room id
    * `uid`: **(long)** 用户 id
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `AddListen(long[] gids, long[] rids, bool p2p, string[] events, int timeout, CallbackDelegate cb)`: 添加 `事件` / `消息` 监听
    * `gids`: **(long[])** 多个Group id
    * `rids`: **(long[])** 多个Room id
    * `p2p`: **(bool)** P2P消息
    * `events`: **(string[])** 多个事件名称, 请参考 `RTMConfig.SERVER_EVENT` 成员
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `RemoveListen(long[] gids, long[] rids, bool p2p, string[] events, int timeout, CallbackDelegate cb)`: 删除 `事件` / `消息` 监听
    * `gids`: **(long[])** 多个Group id
    * `rids`: **(long[])** 多个Room id
    * `p2p`: **(bool)** P2P消息
    * `events`: **(string[])** 多个事件名称, 请参考 `RTMConfig.SERVER_EVENT` 成员
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `SetListen(bool all, int timeout, CallbackDelegate cb)`: 更新 `事件` / `消息` 监听
    * `all`: **(bool)** `true`: 监听所有 `事件` / `消息`, `false`: 取消所有 `事件` / `消息` 监听
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null
        
* `SetListen(long[] gids, long[] rids, bool p2p, string[] events, int timeout, CallbackDelegate cb)`: 更新 `事件` / `消息` 监听
    * `gids`: **(long[])** 多个Group id
    * `rids`: **(long[])** 多个Room id
    * `p2p`: **(bool)** P2P消息, `true`: 监听, `false`: 取消监听
    * `events`: **(string[])** 多个事件名称, 请参考 `RTMConfig.SERVER_EVENT` 成员
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `AddDevice(long uid, string apptype, string devicetoken, int timeout, CallbackDelegate cb)`: 添加设备, 应用信息
    * `uid`: **(long)** 用户 id
    * `apptype`: **(string)** 应用信息
    * `devicetoken`: **(string)** 设备 token
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null
        
* `RemoveDevice(long uid, string devicetoken, int timeout, CallbackDelegate cb)`: 删除设备, 应用信息
    * `uid`: **(long)** 用户 id
    * `devicetoken`: **(string)** 设备 token
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `DeleteMessage(long mid, long from, long xid, byte type, int timeout, CallbackDelegate cb)`: 删除消息
    * `mid`: **(long)** 消息 id
    * `from`: **(long)** 发送方 id
    * `xid`: **(long)** 接收放 id, `rid/gid/to`
    * `type`: **(byte)** 消息发送分类 `1:P2P, 2:Group, 3:Room, 4:Broadcast`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null

* `Kickout(long uid, string ce, int timeout, CallbackDelegate cb)`: 踢掉一个用户或者一个链接
    * `uid`: **(long)** 用户 id
    * `ce`: **(string)** 踢掉`ce`对应链接, 多用户登录情况
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
        * `exception`: **Exception** 当成功时exception=null
        
* `SendFile(long from, long to, byte mtype, byte[] fileBytes, long mid, int timeout, CallbackDelegate cb)`: 发送文件
    * `from`: **(long)** 发送方 id
    * `to`: **(long)** 接收方 uid
    * `mtype`: **(byte)** 文件类型
    * `fileBytes`: **(byte[])** 文件内容
    * `mid`: **(long)** 消息 id, 用于过滤重复消息, 非重发时为`0`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `mid`: **(long)**
            * `mtime`: **(long)**
        * `exception`: **Exception** 当成功时exception=null

* `SendFiles(long from, long[] tos, byte mtype, byte[] fileBytes, long mid, int timeout, CallbackDelegate cb)`: 给多人发送文件
    * `from`: **(long)** 发送方 id
    * `tos`: **(long)** 接收方 uids
    * `mtype`: **(byte)** 文件类型
    * `fileBytes`: **(byte[])** 文件内容
    * `mid`: **(long)** 消息 id, 用于过滤重复消息, 非重发时为`0`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `mid`: **(long)**
            * `mtime`: **(long)**
        * `exception`: **Exception** 当成功时exception=null

* `SendGroupFile(long from, long gid, byte mtype, byte[] fileBytes, long mid, int timeout, CallbackDelegate cb)`: 给Group发送文件
    * `from`: **(long)** 发送方 id
    * `gid`: **(long)** Group id
    * `mtype`: **(byte)** 文件类型
    * `fileBytes`: **(byte[])** 文件内容
    * `mid`: **(long)** 消息 id, 用于过滤重复消息, 非重发时为`0`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `mid`: **(long)**
            * `mtime`: **(long)**
        * `exception`: **Exception** 当成功时exception=null

* `SendRoomFile(long from, long rid, byte mtype, byte[] fileBytes, long mid, int timeout, CallbackDelegate cb)`: 给Room发送文件
    * `from`: **(long)** 发送方 id
    * `rid`: **(long)** Room id
    * `mtype`: **(byte)** 文件类型
    * `fileBytes`: **(byte[])** 文件内容
    * `mid`: **(long)** 消息 id, 用于过滤重复消息, 非重发时为`0`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `mid`: **(long)**
            * `mtime`: **(long)**
        * `exception`: **Exception** 当成功时exception=null

* `BroadcastFile(long from, byte mtype, byte[] fileBytes, long mid, int timeout, CallbackDelegate cb)`: 给整个Project发送文件
    * `from`: **(long)** 发送方 id
    * `mtype`: **(byte)** 文件类型
    * `fileBytes`: **(byte[])** 文件内容
    * `mid`: **(long)** 消息 id, 用于过滤重复消息, 非重发时为`0`
    * `timeout`: **(int)** 超时时间(ms)
    * `cb`: **CallbackDelegate(Hashtable data, Exception exception)** 回调方法
        * `data`: **Hashtable**
            * `mid`: **(long)**
            * `mtime`: **(long)**
        * `exception`: **Exception** 当成功时exception=null

* `byte[] LoadFile(string filePath)`: load一个文件

