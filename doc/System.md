# RTM Server CSharp SDK System API Docs

# Index

[TOC]


### Kickout

	//-- Async Method
	public void Kickout(Action<int> callback, long userId, string clientEndpoint = null, int timeout = 0)
	
	//-- Sync Method
	public int Kickout(long userId, string clientEndpoint = null, int timeout = 0)

Kickout a login user.

Parameters:

+ `Action<int errorCode> callback`

+ `long userId`

  User ID.

+ `string clientEndpoint`

  Default is null, the rtm endpoint of the user logined, used for multi-user login

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.


### AddDevice

	//-- Async Method
	public void AddDevice(Action<int> callback, long userId, string appType, string deviceToken, int timeout = 0)
	
	//-- Sync Method
	public int AddDevice(long userId, string appType, string deviceToken, int timeout = 0)

Add Device for mobile notifications.

Parameters:

+ `Action<int errorCode> callback`

+ `long userId`

  User ID.

+ `string appType`

  apns or pcm

+ `string deviceToken`

  device token

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### RemoveDevice

	//-- Async Method
	public void RemoveDevice(Action<int> callback, long userId, string deviceToken, int timeout = 0)
	
	//-- Sync Method
	public int RemoveDevice(long userId, string deviceToken, int timeout = 0)

Remove Device for mobile notifications.

Parameters:

+ `Action<int errorCode> callback`

+ `long userId`

  User ID.

+ `string deviceToken`

  device token

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.

### Add Device Push Option

	//-- Async Method
	public void AddDevicePushOption(Action<int> callback, long userId, MessageCategory messageCategory, long targetId, HashSet<byte> mTypes = null, int timeout = 0);
	
	//-- Sync Method
	public int AddDevicePushOption(long userId, MessageCategory messageCategory, long targetId, HashSet<byte> mTypes = null, int timeout = 0);

Set disabled session for chat & messages push.

Parameters:

+ `Action<int errorCode> callback`

+ `long userId`

  User ID.

+ `MessageCategory messageCategory`

	Only `MessageCategory.P2PMessage` & `MessageCategory.GroupMessage` can be used.

+ `long targetId`

	Peer uid for `MessageCategory.P2PMessage` and group id for `MessageCategory.GroupMessage`.

+ `HashSet<byte> mTypes`

	Disabled message types. If `mTypes` is `null` or empty, means all message types are disalbed for push.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.


### Remove Device Push Option

	//-- Async Method
	public void RemoveDevicePushOption(Action<int> callback, long userId, MessageCategory messageCategory, long targetId, HashSet<byte> mTypes = null, int timeout = 0);
	
	//-- Sync Method
	public int RemoveDevicePushOption(long userId, MessageCategory messageCategory, long targetId, HashSet<byte> mTypes = null, int timeout = 0);

Remove disabled option for chat & messages push.

Parameters:

+ `Action<int errorCode> callback`

+ `long userId`

  User ID.

+ `MessageCategory messageCategory`

	Only `MessageCategory.P2PMessage` & `MessageCategory.GroupMessage` can be used.

+ `long targetId`

	Peer uid for `MessageCategory.P2PMessage` and group id for `MessageCategory.GroupMessage`.

+ `HashSet<byte> mTypes`

	Disabled message types. If `mTypes` is `null` or empty, means all message types are removed disalbe attributes for push.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.


### Get Device Push Option

	//-- Async Method
	public void GetDevicePushOption(Action<Dictionary<long, HashSet<byte>>, Dictionary<long, HashSet<byte>>, int> callback, long userId, int timeout = 0);
	
	//-- Sync Method
	public int GetDevicePushOption(out Dictionary<long, HashSet<byte>> p2pDictionary, out Dictionary<long, HashSet<byte>> groupDictionary, long userId, int timeout = 0);

Get disabled option for chat & messages push.

Parameters:

+ `Action<Dictionary<long, HashSet<byte>>, Dictionary<long, HashSet<byte>>, int> callback`

	Callabck for async method.  
	First `Dictionary<long, HashSet<byte>>` is peer user id with associated disabled message types set for P2P sessions;  
	Second `Dictionary<long, HashSet<byte>>` is group id with associated disabled message types set for group sessions;  
	Thrid `int` is the error code indicating the calling is successful or the failed reasons.

+ `out Dictionary<long, HashSet<byte>> p2pDictionary`

	Peer user id with associated disabled message types set dictionary.

+ `out Dictionary<long, HashSet<byte>> groupDictionary`

	Group id with associated disabled message types set dictionary.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async


+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.
