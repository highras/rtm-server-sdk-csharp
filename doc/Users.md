# RTM Server CSharp SDK Users API Docs

# Index

[TOC]

### Get Online Users

	//-- Async Method
	public void GetOnlineUsers(Action<HashSet<long>, int> callback, HashSet<long> userIds, int timeout = 0)
	
	//-- Sync Method
	public int GetOnlineUsers(out HashSet<long> onlineUids, HashSet<long> userIds, int timeout = 0)

Get online users.

+ `Action<HashSet<long>, int> callback`

	Callabck for async method.  
	First `HashSet<long>` is the online users' ids;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `out HashSet<long> onlineUids`

	The online users' ids.

+ `HashSet<long> userIds`

	The users' ids which want to be checked.

	Max 200 uids for each calling.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Add Project Black

	//-- Async Method
	public void AddProjectBlack(Action<int> callback, long userId, int banTime, int timeout = 0)
	
	//-- Sync Method
	public int AddProjectBlack(long uid, int banTime, int timeout = 0)

Add Project black

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

  User id.

+ `int banTime`

  The ban time in seconds.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Remove Project Black

	//-- Async Method
	public void RemoveProjectBlack(Action<int> callback, long userId, int timeout = 0)
	
	//-- Sync Method
	public int RemoveProjectBlack(long userId, int timeout = 0)

Remove Project black

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

  User id.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Is Project Black

	//-- Async Method
	public void IsProjectBlack(Action<bool, int> callback, long userId, int timeout = 0)
	
	//-- Sync Method
	public int IsProjectBlack(out bool ok, long userId, int timeout = 0)

Check is friends.

+ `Action<bool, int> callback`

  Callabck for async method.  
  First `bool` is true means is group member  
  Second `int` is the error code indicating the calling is successful or the failed reasons.

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



### Set User Info


	//-- Async Method
	public void SetUserInfo(Action<int> callback, long userId, string publicInfo = null, string privateInfo = null, int timeout = 0)
	
	//-- Sync Method
	public int SetUserInfo(long userId, string publicInfo = null, string privateInfo = null, int timeout = 0)

Set user's public info and private info.

Parameters:

+ `DoneDelegate callback`

		public delegate void DoneDelegate(int errorCode);

	Callabck for async method. Please refer [DoneDelegate](Delegates.md#DoneDelegate).

+ `long userId`

   User ID.

+ `string publicInfo`

	New public info for group. `null` means don't change the public info. Max length is 65535 bytes.

+ `string privateInfo`

	New private info for group. `null` means don't change the private info. Max length is 65535 bytes.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.
	
	


### Get User Info

	//-- Async Method
	public void GetUserInfo(Action<string, string, int> callback, long userId, int timeout = 0)
	
	//-- Sync Method
	public int GetUserInfo(out string publicInfo, out string privateInfo, long userId, int timeout = 0)

Get user's public info and private info.

Parameters:

+ `Action<string, string, int> callback`

	Callabck for async method.  
	First `string` is gotten public info of current user;  
	Second `string` is gotten private info of current user;  
	Thrid `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

  User ID.

+ `out string publicInfo`

	The gotten public info of current user.

+ `out string privateInfo`

	The gotten private info of current user.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.