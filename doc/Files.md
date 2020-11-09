# RTM Server CSharp SDK Files API Docs

# Index

[TOC]


### Send P2P File

	//-- Async Method
	public void SendFile(Action<long, int> callback, long fromUid, long peerUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", string attrs = "", int timeout = 120)
	
	//-- Sync Method
	public int SendFile(out long messageId, long fromUid, long peerUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", string attrs = "", int timeout = 120)

Send P2P file.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

	Message ID.

+ `long fromUid`

   Sender user id.

+ `long peerUid`

	Receiver user id.

+ `MessageType type`

	Message type for file.
	* MessageType.NormalFile = 50: Generic file. Server maybe change this value to more suitable values.
	* MessageType.ImageFile = 40: Pictures.
	* MessageType.AudioFile = 41: Audio.
	* MessageType.VideoFile = 42: Video.

+ `byte[] fileContent`

	File content.

+ `string filename`

	File name.

+ `string fileExtension`

	File extension.

+ `string filename`

	File name.

+ `string attrs`

	File message attributes in Json.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

	Others are the reason for sending failed.



### Send Multiple Files

	//-- Async Method
	public void SendFiles(Action<long, int> callback, long fromUid, HashSet<long> peerUids, MessageType type, byte[] fileContent, string filename, string fileExtension = "", string attrs = "", int timeout = 120)
	
	//-- Sync Method
	public int SendFiles(out long messageId, long fromUid, HashSet<long> peerUids, MessageType type, byte[] fileContent, string filename, string fileExtension = "", string attrs = "", int timeout = 120)

Send P2P file.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `HashSet<long> peerUids`

  Receiver user ids.

+ `MessageType type`

  Message type for file.

  * MessageType.NormalFile = 50: Generic file. Server maybe change this value to more suitable values.
  * MessageType.ImageFile = 40: Pictures.
  * MessageType.AudioFile = 41: Audio.
  * MessageType.VideoFile = 42: Video.

+ `byte[] fileContent`

  File content.

+ `string filename`

  File name.

+ `string fileExtension`

  File extension.

+ `string attrs`

	File message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.

  

### Send Group File

	//-- Async Method
	public void SendGroupFile(Action<long, int> callback, long fromUid, long groupId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", string attrs = "", int timeout = 120)
	
	//-- Sync Method
	public int SendGroupFile(out long messageId, long fromUid, long groupId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", string attrs = "", int timeout = 120)

Send file in group.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

	Message ID.

+ `long fromUid`

   Sender user id.

+ `long groupId`

	Group id.

+ `MessageType mtype`

	Message type for file. 
	* MessageType.NormalFile = 50: Generic file. Server maybe change this value to more suitable values.
	* MessageType.ImageFile = 40: Pictures.
	* MessageType.AudioFile = 41: Audio.
	* MessageType.VideoFile = 42: Video.

+ `byte[] fileContent`

	File content.

+ `string filename`

	File name.

+ `string fileExtension`

	File extension.

+ `string attrs`

	File message attributes in Json.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

	Others are the reason for sending failed.
	
	


### Send Room File

	//-- Async Method
	public void SendRoomFile(Action<long, int> callback, long fromUid, long roomId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", string attrs = "", int timeout = 120)
	
	//-- Sync Method
	public int SendRoomFile(out long messageId, long fromUid, long roomId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", string attrs = "", int timeout = 120)

Send file in room.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

	Message ID.

+ `long fromUid`

   Sender user id.

+ `long roomId`

	Room id.

+ `MessageType mtype`

	Message type for file.
	* MessageType.NormalFile = 50: Generic file. Server maybe change this value to more suitable values.
	* MessageType.ImageFile = 40: Pictures.
	* MessageType.AudioFile = 41: Audio.
	* MessageType.VideoFile = 42: Video.

+ `byte[] fileContent`

	File content.

+ `string filename`

	File name.

+ `string fileExtension`

	File extension.

+ `string attrs`

	File message attributes in Json.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

	Others are the reason for sending failed.



### Broadcast File

	//-- Async Method
	public void BroadcastFile(Action<long, int> callback, long fromUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", string attrs = "", int timeout = 120)
	
	//-- Sync Method
	public int BroadcastFile(out long messageId, long fromUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", string attrs = "", int timeout = 120)

Send file in room.

Parameters:

+ `Action<long messageId, int errorCode> callback`

+ `out long messageId`

  Message ID.

+ `long fromUid`

  Sender user id.

+ `MessageType mtype`

  Message type for file.

  * MessageType.NormalFile = 50: Generic file. Server maybe change this value to more suitable values.
  * MessageType.ImageFile = 40: Pictures.
  * MessageType.AudioFile = 41: Audio.
  * MessageType.VideoFile = 42: Video.

+ `byte[] fileContent`

  File content.

+ `string filename`

  File name.

+ `string fileExtension`

  File extension.

+ `string attrs`

	File message attributes in Json.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.

