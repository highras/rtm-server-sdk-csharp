# RTM Server CSharp SDK Data API Docs

# Index

[TOC]

### Get Data

	//-- Async Method
	public void DataGet(Action<string, int> callback, long userId, string key, int timeout = 0)
	
	//-- Sync Method
	public int DataGet(out string value, long userId, string key, int timeout = 0)

Get user's data.

Parameters:

+ `Action<string, int> callback`

	Callabck for async method.  
	First `string` is gotten data associated the inputted `string key`;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `out string value`

	The gotten data associated the inputted `string key`.

+ `long userId`

  User id.

+ `string key`

	The key of wanted to be gotten data.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.
	
	

### Set Data

	//-- Async Method
	public void DataSet(Action<int> callback, long userId, string key, string value, int timeout = 0)
	
	//-- Sync Method
	public int DataSet(long userId, string key, string value, int timeout = 0)

Set user's data.

Parameters:

+ `Action<int> callback`

		First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

   User id.

+ `string key`

	The key of user's data. Max 128 bytes length.

+ `string value`

	The value of user's data. Max 65535 bytes length.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.
	
	

### Delete Data

	//-- Async Method
	public void DataDelete(Action<int> callback, long userId, string key, int timeout = 0)
	
	//-- Sync Method
	public int DataDelete(long userId, string key, int timeout = 0)

Delete user's data.

Parameters:

+ `Action<int> callback`

		First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

   User id.

+ `string key`

	The key of user's data.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.