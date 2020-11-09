# RTM Server CSharp SDK Chat API Docs

# Index

[TOC]

### Send P2P Chat

	//-- Async Method
	public void SendChat(Action<long, int> callback, long fromUid, long toUid, string message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendChat(out long messageId, long fromUid, long toUid, string message, string attrs = "", int timeout = 0)

Send P2P text chat.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

	Message ID.

+ `long fromUid`

	Sender user id.

+ `long toUid

   Receiver user id.

+ `string message`

	Chat message.

+ `string attrs`

	Chat message attributes in Json.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

	Others are the reason for sending failed.



### Send Multiple P2P Chat

	//-- Async Method
	public void SendChats(Action<long, int> callback, long fromUid, HashSet<long> toUids, string message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendChats(out long messageId, long fromUid, HashSet<long> toUids, string message, string attrs = "", int timeout = 0)

Send Multiple P2P text chat.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `HashSet<long> toUids`

  Receiver user id set.

+ `string message`

  Chat message.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send Group Chat

	//-- Async Method
	public void SendGroupChat(Action<long, int> callback, long fromUid, long groupId, string message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendGroupChat(out long messageId, long fromUid, long groupId, string message, string attrs = "", int timeout = 0)

Send Group text chat.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `long groupId`

  Group ID.

+ `string message`

  Chat message.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send Room Chat

	//-- Async Method
	public void SendRoomChat(Action<long, int> callback, long fromUid, long roomId, string message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendRoomChat(out long messageId, long fromUid, long roomId, string message, string attrs = "", int timeout = 0)

Send  Room text chat.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `long roomId`

  Room ID.

+ `string message`

  Chat message.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Broadcast Chat

	//-- Async Method
	public void BroadcastChat(Action<long, int> callback, long fromUid, string message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int BroadcastChat(out long messageId, long fromUid, string message, string attrs = "", int timeout = 0)

Broadcast text chat.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `string message`

  Chat message.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send P2P Cmd

	//-- Async Method
	public void SendCmd(Action<long, int> callback, long fromUid, long toUid, string message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendCmd(out long messageId, long fromUid, long toUid, string message, string attrs = "", int timeout = 0)

Send P2P cmd.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `long toUid

  Receiver user id.

+ `string message`

  Chat message.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send Multiple P2P Cmd

	//-- Async Method
	public void SendCmds(Action<long, int> callback, long fromUid, HashSet<long> toUids, string message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendCmds(out long messageId, long fromUid, HashSet<long> toUids, string message, string attrs = "", int timeout = 0)

Send Multiple P2P cmd.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `HashSet<long> toUids`

  Receiver user id set.

+ `string message`

  Chat message.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send Group Cmd

	//-- Async Method
	public void SendGroupCmd(Action<long, int> callback, long fromUid, long groupId, string message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendGroupCmd(out long messageId, long fromUid, long groupId, string message, string attrs = "", int timeout = 0)

Send Group cmd.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `long groupId`

  Group ID.

+ `string message`

  Chat message.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send Room Cmd

	//-- Async Method
	public void SendRoomCmd(Action<long, int> callback, long fromUid, long roomId, string message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendRoomCmd(out long messageId, long fromUid, long roomId, string message, string attrs = "", int timeout = 0)

Send  Room cmd.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `long roomId`

  Room ID.

+ `string message`

  Chat message.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Broadcast Cmd

	//-- Async Method
	public void BroadcastCmd(Action<long, int> callback, long fromUid, string message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int BroadcastCmd(out long messageId, long fromUid, string message, string attrs = "", int timeout = 0)

Broadcast cmd.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `string message`

  Chat message.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send P2P Audio

	//-- Async Method
	public void SendAudio(Action<long, int> callback, long fromUid, long toUid, byte[] message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendAudio(out long messageId, long fromUid, long toUid, byte[] message, string attrs = "", int timeout = 0)

Send P2P audio.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `long toUid

  Receiver user id.

+ `byte[] message`

  Audio data.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send Multiple P2P Audio

	//-- Async Method
	public void SendAudios(Action<long, int> callback, long fromUid, HashSet<long> toUids, byte[] message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendAudios(out long messageId, long fromUid, HashSet<long> toUids, byte[] message, string attrs = "", int timeout = 0)

Send Multiple P2P audio.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `HashSet<long> toUids`

  Receiver user id set.

+ `byte[] message`

  Audio data.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send Group Audio

	//-- Async Method
	public void SendGroupAudio(Action<long, int> callback, long fromUid, long groupId, byte[] message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendGroupAudio(out long messageId, long fromUid, long groupId, byte[] message, string attrs = "", int timeout = 0)

Send Group audio.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `long groupId`

  Group ID.

+ `byte[] message`

  Audio data.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send Room Audio

	//-- Async Method
	public void SendRoomAudio(Action<long, int> callback, long fromUid, long roomId, byte[] message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendRoomAudio(out long messageId, long fromUid, long roomId, byte[] message, string attrs = "", int timeout = 0)

Send  Room audio.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `long roomId`

  Room ID.

+ `byte[] message`

  Audio data.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Broadcast Audio

	//-- Async Method
	public void BroadcastAudio(Action<long, int> callback, long fromUid, byte[] message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int BroadcastAudio(out long messageId, long fromUid, byte[] message, string attrs = "", int timeout = 0)

Broadcast audio.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `byte[] message`

  Audio data.

+ `string attrs`

  Chat message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Get Group Chat


	//-- Async Method
	ublic void GetGroupChat(HistoryMessageDelegate callback, long userId, long groupId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
	
	//-- Sync Method
	public int GetGroupChat(out HistoryMessageResult result, long userId, long groupId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)

Get history data for group chat, including text chat, text cmd and binary audio.

Parameters:

+ `HistoryMessageDelegate callback`

		public delegate void HistoryMessageDelegate(int count, long lastId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode);

	Callabck for async method. Please refer [HistoryMessageDelegate](Delegates.md#HistoryMessageDelegate).

+ `out HistoryMessageResult result`

	Fetched history data. Please refer [HistoryMessageResult](Structures.md#HistoryMessageResult).

+ `long userId`

   User id.

+ `long groupId`

	Group id.

+ `bool desc`

	* true: desc order;
	* false: asc order.

+ `int count`

	Count for retrieving. Max is 20 for each calling.

+ `long beginMsec`

	Beginning timestamp in milliseconds.

+ `long endMsec`

	Ending timestamp in milliseconds.

+ `long lastId`

	Last data id returned when last calling. First calling using 0.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Get Room Chat

	//-- Async Method
	public void GetRoomChat(HistoryMessageDelegate callback, long userId, long roomId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
	
	//-- Sync Method
	public int GetRoomChat(out HistoryMessageResult result, long userId, long roomId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)

Get history data for room chat, including text chat, text cmd and binary audio.

Parameters:

+ `HistoryMessageDelegate callback`

		public delegate void HistoryMessageDelegate(int count, long lastId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode);

	Callabck for async method. Please refer [HistoryMessageDelegate](Delegates.md#HistoryMessageDelegate).

+ `out HistoryMessageResult result`

	Fetched history data. Please refer [HistoryMessageResult](Structures.md#HistoryMessageResult).

+ `long userId`

   User id.

+ `long roomId`

	Room id.

+ `bool desc`

	* true: desc order;
	* false: asc order.

+ `int count`

	Count for retrieving. Max is 20 for each calling.

+ `long beginMsec`

	Beginning timestamp in milliseconds.

+ `long endMsec`

	Ending timestamp in milliseconds.

+ `long lastId`

	Last data id returned when last calling. First calling using 0.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Get Broadcast Chat


	//-- Async Method
	public void GetBroadcastChat(HistoryMessageDelegate callback, long userId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
	
	//-- Sync Method
	public int GetBroadcastChat(out HistoryMessageResult result, long userId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)

Get history data for broadcast chat, including text chat, text cmd and binary audio.

Parameters:

+ `HistoryMessageDelegate callback`

		public delegate void HistoryMessageDelegate(int count, long lastId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode);

	Callabck for async method. Please refer [HistoryMessageDelegate](Delegates.md#HistoryMessageDelegate).

+ `out HistoryMessageResult result`

	Fetched history data. Please refer [HistoryMessageResult](Structures.md#HistoryMessageResult).

+ `long userId`

   User id.

+ `bool desc`

	* true: desc order;
	* false: asc order.

+ `int count`

	Count for retrieving. Max is 20 for each calling.

+ `long beginMsec`

	Beginning timestamp in milliseconds.

+ `long endMsec`

	Ending timestamp in milliseconds.

+ `long lastId`

	Last data id returned when last calling. First calling using 0.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Get P2P Chat


	//-- Async Method
	public void GetP2PChat(HistoryMessageDelegate callback, long userId, long peerUserUid, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)
	
	//-- Sync Method
	public int GetP2PChat(out HistoryMessageResult result, long userId, long peerUserUid, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, int timeout = 0)

Get history data for P2P chat, including text chat, text cmd and binary audio.

Parameters:

+ `HistoryMessageDelegate callback`

  public delegate void HistoryMessageDelegate(int count, long lastId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode);

  Callabck for async method. Please refer [HistoryMessageDelegate](Delegates.md#HistoryMessageDelegate).

+ `out HistoryMessageResult result`

  Fetched history data. Please refer [HistoryMessageResult](Structures.md#HistoryMessageResult).

+ `long userId`

  User id.

+ `long peerUserUid

  Peer user id.

+ `bool desc`

  * true: desc order;
  * false: asc order.

+ `int count`

  Count for retrieving. Max is 20 for each calling.

+ `long beginMsec`

  Beginning timestamp in milliseconds.

+ `long endMsec`

  Ending timestamp in milliseconds.

+ `long lastId`

  Last data id returned when last calling. First calling using 0.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Delete Chat

	//-- Async Method
	public void DeleteChat(Action<int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
	
	//-- Sync Method
	public int DeleteChat(long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)

Delete a sent chat message.

Parameters:

+ Action<int errorCode> callback`

+ `fromUid`

	Uid of the chat sender, which chat is wanted to be deleted.

+ `toId`

	If the chat is P2P chat, `toId` means the uid of peer;  
	If the chat is group chat, `toId` means the `groupId`;  
	If the chat is room chat, `toId` means the `roomId`.

+ `messageId`

	Message id for the chat message which wanted to be deleted.

+ `messageCategory`

	MessageCategory enumeration.

	Can be MessageCategory.P2PMessage, MessageCategory.GroupMessage, MessageCategory.RoomMessage.


+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Get Chat Message

	//-- Async Method
	public void GetChat(Action<RetrievedMessage, int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
	
	//-- Sync Method
	public int GetChat(out RetrievedMessage retrievedMessage, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)

Retrieve a sent chat message.

Parameters:

+ `Action<RetrievedMessage, int> callback`

	Callabck for async method.  
	First `RetrievedMessage` is retrieved data, please refer [RetrievedMessage](Structures.md#RetrievedMessage);  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `out RetrievedMessage retrievedMessage`

	The retrieved data, please refer [RetrievedMessage](Structures.md#RetrievedMessage).

+ `fromUid`

	Uid of the chat sender, which chat is wanted to be retrieved.

+ `toId`

	If the chat is P2P chat, `toId` means the uid of peer;  
	If the chat is group chat, `toId` means the `groupId`;  
	If the chat is room chat, `toId` means the `roomId`;  
	If the chat is broadcast chat, `toId` is `0`.

+ `messageId`

	Message id for the chat message which wanted to be retrieved.

+ `messageCategory`

	MessageCategory enumeration.

	Can be MessageCategory.P2PMessage, MessageCategory.GroupMessage, MessageCategory.RoomMessage, MessageCategory.BroadcastMessage.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.
