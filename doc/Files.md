# RTM Server CSharp SDK Files API Docs

# Index

[TOC]

### Get File Token

	//-- Async Method
	public void FileToken(Action<string, string, int> callback, long fromUid, FileTokenType tokenType, long targetId, int timeout = 0)
	public void FileToken(Action<string, string, int> callback, long fromUid, FileTokenType tokenType, HashSet<long> targetIds, int timeout = 0)
	
	//-- Sync Method
	public int FileToken(out string token, out string endpoint, long fromUid, FileTokenType tokenType, long targetId = 0, int timeout = 0)
	public int FileToken(out string token, out string endpoint, long fromUid, FileTokenType tokenType, HashSet<long> targetIds, int timeout = 0)

Send P2P file.

Parameters:

+ `Action<string token, string endpoint, int errorCode> callback`

+ `out long mtime`

  Sending completed time.

+ `long fromUid`

  Sender user id.

+ `long targetId`

  Receiver user id.

+ `HashSet<long> targetIds`

  Receiver user ids.

+ `FileTokenType type`

  File token type for file: P2P, Group, Room, Multi or Broadcast

+ `byte[] fileContent`

  File content.

+ `string filename`

  File name.

+ `string fileExtension`

  File extension.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.



### Send P2P File

	//-- Async Method
	public void SendFile(Action<long, int> callback, long fromUid, long peerUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
	
	//-- Sync Method
	public int SendFile(out long mtime, long fromUid, long peerUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)

Send P2P file.

Parameters:

+ `Action<long mtime, int errorCode> callback`

+ `out long mtime`

	Sending completed time.

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
	public void SendFiles(Action<long, int> callback, long fromUid, HashSet<long> peerUids, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
	
	//-- Sync Method
	public int SendFiles(out long mtime, long fromUid, HashSet<long> peerUids, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)

Send P2P file.

Parameters:

+ `Action<long mtime, int errorCode> callback`

+ `out long mtime`

  Sending completed time.

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
	public void SendGroupFile(Action<long, int> callback, long fromUid, long groupId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
	
	//-- Sync Method
	public int SendGroupFile(out long mtime, long fromUid, long groupId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)

Send file in group.

Parameters:

+ `Action<long mtime, int errorCode> callback`

+ `out long mtime`

	Sending completed time.

+ `long fromUid`

   Sender user id.

+ `long groupId`

	Group id.

+ `byte mtype`

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
	public void SendRoomFile(Action<long, int> callback, long fromUid, long roomId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
	
	//-- Sync Method
	public int SendRoomFile(out long mtime, long fromUid, long roomId, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)

Send file in room.

Parameters:

+ `Action<long mtime, int errorCode> callback`

+ `out long mtime`

	Sending completed time.

+ `long fromUid`

   Sender user id.

+ `long roomId`

	Room id.

+ `byte mtype`

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
	public void BroadcastFile(Action<long, int> callback, long fromUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)
	
	//-- Sync Method
	public int BroadcastFile(out long mtime, long fromUid, MessageType type, byte[] fileContent, string filename, string fileExtension = "", int timeout = 120)

Send file in room.

Parameters:

+ `Action<long mtime, int errorCode> callback`

+ `out long mtime`

  Sending completed time.

+ `long fromUid`

  Sender user id.

+ `byte mtype`

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

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means sending successed.

  Others are the reason for sending failed.

