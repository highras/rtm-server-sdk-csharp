# RTM Server CSharp SDK Token API Docs

# Index

[TOC]

### GetToken

	//-- Async Method
	public void GetToken(Action<string, int> callback, long userId, int timeout = 0)
	
	//-- Sync Method
	public int GetToken(out string token, long userId, int timeout = 0)

Get login token.

Parameters:

+ `Action<string token, int errorCode> callback`

+ `long userId`

	User ID.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### RemoveToken

	//-- Async Method
	public void RemoveToken(Action<int> callback, long userId, int timeout = 0)
	
	//-- Sync Method
	public int RemoveToken(long userId, int timeout = 0)

Remove login token.

Parameters:

+ `Action<int errorCode> callback`

+ `long userId`

  User ID.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



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

