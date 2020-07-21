# RTM Server CSharp SDK API Docs: Delegates

# Index

[TOC]

### RTMConnectionConnectedDelegate

	public delegate void RTMConnectionConnectedDelegate(Int64 connectionId, string endpoint, bool connected, bool isReconnect);

Parameters:

+ `Int64 connectionId`

  Unique id when the connection is connected. When the connection closing callback returned, this id may be reused by another connection.

+ `string endpoint`

  Endpoint of the target server for this connection.

+ `bool connected`

  Connecting successful or not.

+ `bool isReconnect`

  Is reconnect or not.

### RTMConnectionCloseDelegate

	public delegate void RTMConnectionCloseDelegate(Int64 connectionId, string endpoint, bool causedByError, bool isReconnect);

Parameters:

+ `Int64 connectionId`

	Unique id when the connection is connected. When the connection closing callback returned, this id may be reused by another connection.

+ `string endpoint`

  Endpoint of the target server for this connection.

+ `bool causedByError`

  Connection closing is triggered by error or normal close (e.g. motivated calling Close function, or normal shutdown).

+ `bool isReconnect`

  Is reconnect or not.



### HistoryMessageDelegate

	public delegate void HistoryMessageDelegate(int count, long lastCursorId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode);

Parameters:

+ `int count`

	Retrieved messages count.

+ `long lastCursorId`

	When calling history functions for fetching following messsges, using this for corresponding patameter.

+ `long beginMsec`

	When calling history functions for fetching following messsges, using this for corresponding patameter.

+ `long endMsec`

	When calling history functions for fetching following messsges, using this for corresponding patameter.

+ `List<HistoryMessage> messages`

	Retrieved history messages. Declaration of struct HistoryMessage can be found at [HistoryMessage](Structures.md#HistoryMessage).

+ `int errorCode`

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means successed.

	Others are the reason for failed.