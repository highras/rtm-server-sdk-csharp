# RTM Server CSharp SDK Messages API Docs

# Index

[TOC]

### Send P2P Message

	//-- Async Method
	public void SendMessage(Action<long, int> callback, long fromUid, long toUid, byte mtype, string message, string attrs = "", int timeout = 0)
	public void SendMessage(Action<long, int> callback, long fromUid, long toUid, byte mtype, byte[] message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendMessage(out long messageId, long fromUid, long toUid, byte mtype, string message, string attrs = "", int timeout = 0)
	public int SendMessage(out long messageId, long fromUid, long toUid, byte mtype, byte[] message, string attrs = "", int timeout = 0)

Send P2P message.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

	Message ID.

+ `long fromUid`

	Sender user id.

+ `long toUid`

   Receiver user id.

+ `byte mtype`

	Message type for message. MUST large than 50.

+ `string message`

	Text message.

+ `byte[] message`

	Binary message.

+ `string attrs`

	Message attributes in Json.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

	Others are the reason for sending failed.



### Send Multiple P2P Message

	//-- Async Method
	public void SendMessages(Action<long, int> callback, long fromUid, HashSet<long> toUids, byte mtype, string message, string attrs = "", int timeout = 0)
	public void SendMessages(Action<long, int> callback, long fromUid, HashSet<long> toUids, byte mtype, byte[] message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendMessages(out long messageId, long fromUid, HashSet<long> toUids, byte mtype, string message, string attrs = "", int timeout = 0)
	public int SendMessages(out long messageId, long fromUid, HashSet<long> toUids, byte mtype, byte[] message, string attrs = "", int timeout = 0)

Send Multiple P2P  Message.

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



### Send Group Messsage

	//-- Async Method
	public void SendGroupMessage(Action<long, int> callback, long fromUid, long groupId, byte mtype, string message, string attrs = "", int timeout = 0)
	public void SendGroupMessage(Action<long, int> callback, long fromUid, long groupId, byte mtype, byte[] message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendGroupMessage(out long messageId, long fromUid, long groupId, byte mtype, string message, string attrs = "", int timeout = 0)
	public int SendGroupMessage(out long messageId, long fromUid, long groupId, byte mtype, byte[] message, string attrs = "", int timeout = 0)

Send message in group.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

	Message ID.

+ `long groupId`

	Group id.

+ `byte mtype`

	Message type for message. MUST large than 50.

+ `string message`

	Text message.

+ `byte[] message`

	Binary message.

+ `string attrs`

	Message attributes in Json.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

	Others are the reason for sending failed.


### Send Room Message

	//-- Async Method
	public void SendRoomMessage(Action<long, int> callback, long fromUid, long roomId, byte mtype, string message, string attrs = "", int timeout = 0)
	public void SendRoomMessage(Action<long, int> callback, long fromUid, long roomId, byte mtype, byte[] message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int SendRoomMessage(out long messageId, long fromUid, long roomId, byte mtype, string message, string attrs = "", int timeout = 0)
	public int SendRoomMessage(out long messageId, long fromUid, long roomId, byte mtype, byte[] message, string attrs = "", int timeout = 0)

Send message in room.

Parameters:

+ ``Action<long messageId, int errorCode> callback`

+ `out long messageId`

	Message ID.

+ `long roomId`

	Room id.

+ `byte mtype`

	Message type for message. MUST large than 50.

+ `string message`

	Text message.

+ `byte[] message`

	Binary message.

+ `string attrs`

	Message attributes in Json.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

	Others are the reason for sending failed.



### Broadcast Message

	//-- Async Method
	public void BroadcastMessage(Action<long, int> callback, long fromUid, byte mtype, string message, string attrs = "", int timeout = 0)
	public void BroadcastMessage(Action<long, int> callback, long fromUid, byte mtype, byte[] message, string attrs = "", int timeout = 0)
	
	//-- Sync Method
	public int BroadcastMessage(out long messageId, long fromUid, byte mtype, string message, string attrs = "", int timeout = 0)
	public int BroadcastMessage(out long messageId, long fromUid, byte mtype, byte[] message, string attrs = "", int timeout = 0)

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



### Get P2P Message

	//-- Async Method
	public void GetP2PMessage(HistoryMessageDelegate callback, long userId, long peerUserUid, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
	
	//-- Sync Method
	public int GetP2PMessage(out HistoryMessageResult result, long userId, long peerUserUid, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)

Get history data for P2P message.

Parameters:

+ `HistoryMessageDelegate callback`

		public delegate void HistoryMessageDelegate(int count, long lastId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode);

	Callabck for async method. Please refer [HistoryMessageDelegate](Delegates.md#HistoryMessageDelegate).

+ `out HistoryMessageResult result`

	Fetched history data. Please refer [HistoryMessageResult](Structures.md#HistoryMessageResult).

+ `long peerUid`

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

+ `List<byte> mtypes`

	Message types for retrieved message. `null` means all types.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Get Group Messsage


	//-- Async Method
	public void GetGroupMessage(HistoryMessageDelegate callback, long userId, long groupId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
	
	//-- Sync Method
	public int GetGroupMessage(out HistoryMessageResult result, long userId, long groupId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)

Get history data for group message.

Parameters:

+ `HistoryMessageDelegate callback`

		public delegate void HistoryMessageDelegate(int count, long lastId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode);

	Callabck for async method. Please refer [HistoryMessageDelegate](Delegates.md#HistoryMessageDelegate).

+ `out HistoryMessageResult result`

	Fetched history data. Please refer [HistoryMessageResult](Structures.md#HistoryMessageResult).

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

+ `List<byte> mtypes`

	Message types for retrieved message. `null` means all types.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.
	
	


### Get Room Message

	//-- Async Method
	public void GetRoomMessage(HistoryMessageDelegate callback, long userId, long roomId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
	
	//-- Sync Method
	public int GetRoomMessage(out HistoryMessageResult result, long userId, long roomId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)

Get history data for room message.

Parameters:

+ `HistoryMessageDelegate callback`

		public delegate void HistoryMessageDelegate(int count, long lastId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode);

	Callabck for async method. Please refer [HistoryMessageDelegate](Delegates.md#HistoryMessageDelegate).

+ `out HistoryMessageResult result`

	Fetched history data. Please refer [HistoryMessageResult](Structures.md#HistoryMessageResult).

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

+ `List<byte> mtypes`

	Message types for retrieved message. `null` means all types.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.
	
	

### Get Broadcast Message


	//-- Async Method
	public void GetBroadcastMessage(HistoryMessageDelegate callback, long userId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)
	
	//-- Sync Method
	public int GetBroadcastMessage(out HistoryMessageResult result, long userId, bool desc, int count, long beginMsec = 0, long endMsec = 0, long lastId = 0, List<byte> mtypes = null, int timeout = 0)

Get history data for broadcast message.

Parameters:

+ `HistoryMessageDelegate callback`

		public delegate void HistoryMessageDelegate(int count, long lastId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode);

	Callabck for async method. Please refer [HistoryMessageDelegate](Delegates.md#HistoryMessageDelegate).

+ `out HistoryMessageResult result`

	Fetched history data. Please refer [HistoryMessageResult](Structures.md#HistoryMessageResult).

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

+ `List<byte> mtypes`

	Message types for retrieved message. `null` means all types.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.
	
	

### Delete Message

	//-- Async Method
	public void DeleteMessage(Action<int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
	
	//-- Sync Method
	public int DeleteMessage(long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)

Delete a sent message.

Parameters:

+ `Action<int errorCode> callback`

+ `fromUid`

	Uid of the message sender, which message is wanted to be deleted.

+ `toId`

	If the message is P2P message, `toId` means the uid of peer;  
	If the message is group message, `toId` means the `groupId`;  
	If the message is room message, `toId` means the `roomId`.

+ `messageId`

	Message id for the message which wanted to be deleted.

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
	
	


### Get Message

	//-- Async Method
	public void GetMessage(Action<RetrievedMessage, int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)
	
	//-- Sync Method
	public int GetMessage(out RetrievedMessage retrievedMessage, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 0)

Retrieve a sent message.

Parameters:

+ `Action<RetrievedMessage, int> callback`

	Callabck for async method.  
	First `RetrievedMessage` is retrieved data, please refer [RetrievedMessage](Structures.md#RetrievedMessage);  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `out RetrievedMessage retrievedMessage`

	The retrieved data, please refer [RetrievedMessage](Structures.md#RetrievedMessage).	

+ `fromUid`

	Uid of the message sender, which message is wanted to be retrieved.

+ `toId`

	If the message is P2P message, `toId` means the uid of peer;  
	If the message is group message, `toId` means the `groupId`;  
	If the message is room message, `toId` means the `roomId`;  
	If the message is broadcast message, `toId` is `0`.

+ `messageId`

	Message id for the message which wanted to be retrieved.

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
