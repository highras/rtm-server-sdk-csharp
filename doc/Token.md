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

