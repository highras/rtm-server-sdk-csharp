# RTM Server CSharp SDK Friends API Docs

# Index

[TOC]

### Add Friends

	//-- Async Method
	public void AddFriends(Action<int> callback, long userId, HashSet<long> friendUserIds, int timeout = 0)
	
	//-- Sync Method
	public int AddFriends(long userId, HashSet<long> friendUserIds, int timeout = 0)

Add friends.

Parameters:

+ `Action<int errorCode> callback`

+ `long userId`

		User ID.

+ `HashSet<long> friendUserIds`

  Friends' uids set. Max 100 users for each calling.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Delete Friends

	//-- Async Method
	public void DeleteFriends(Action<int> callback, long userId, HashSet<long> friendUserIds, int timeout = 0)
	
	//-- Sync Method
	public int DeleteFriends(long userId, HashSet<long> friendUserIds, int timeout = 0)

Delete friends.

Parameters:

+ ``Action<int errorCode> callback``

+ `long userId`

	User ID.

+ `HashSet<long> friendUserIds`

   Friends' uids set. Max 100 users for each calling.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Get Friends

	//-- Async Method
	public void GetFriends(Action<HashSet<long>, int> callback, long userId, int timeout = 0)
	
	//-- Sync Method
	public int GetFriends(out HashSet<long> friends, long userId, int timeout = 0)

Get friends.

+ `Action<HashSet<long>, int> callback`

	Callabck for async method.  
	First `HashSet<long>` is gotten friends' uids;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

  User ID.

+ `out HashSet<long> friends`

	The gotten friends' uids.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Is Friend

	//-- Async Method
	public void IsFriend(Action<bool, int> callback, long userId, long otherUserId, int timeout = 0)
	
	//-- Sync Method
	public int IsFriend(out bool ok, long userId, long otherUserId, int timeout = 0)

Check is friends.

+ `Action<bool, int> callback`

  Callabck for async method.  
  First `bool` is true means is friend  
  Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

  User ID.

+ `long otherUserId`

  Other user ID.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Is Friends

	//-- Async Method
	public void IsFriends(Action<HashSet<long>, int> callback, long userId, HashSet<long> otherUserIds, int timeout = 0)
	
	//-- Sync Method
	public int IsFriends(out HashSet<long> fuids, long userId, HashSet<long> otherUserIds, int timeout = 0)

Check is friends.

+ `Action<HashSet<long>, int> callback`

  Callabck for async method.  
  First `HashSet<long>` is gotten friends' uids;  

  Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

  User ID.

+ `HashSet<long> otherUserIds`

  Other user IDs.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Add Blacklist

	//-- Async Method
	public void AddBlacklist(Action<int> callback, long userId, HashSet<long> blackUserIds, int timeout = 0)
	
	//-- Sync Method
	public int AddBlacklist(long userId, HashSet<long> blackUserIds, int timeout = 0)

Add users to blacklist.

Parameters:

+ `Action<int> callback`

		First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

   User ID.

+ `HashSet<long> blackUserIds`

	Uids set. Max 100 users for each calling.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.
	
	


### Delete Blacklist

	//-- Async Method
	public void DeleteBlacklist(Action<int> callback, long userId, HashSet<long> blackUserIds, int timeout = 0)
	
	//-- Sync Method
	public int DeleteBlacklist(long userId, HashSet<long> blackUserIds, int timeout = 0)

Delete from blacklist.

Parameters:

+ `Action<int> callback`

   First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

   User ID.

+ `HashSet<long> blackUserIds`

   Uids set. Max 100 users for each calling.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Is In Blacklist

	//-- Async Method
	public void IsInBlackList(Action<bool, int> callback, long userId, long otherUserId, int timeout = 0)
	
	//-- Sync Method
	public int IsInBlackList(out bool ok, long userId, long otherUserId, int timeout = 0)

Check is in blacklist.

Parameters:

+ `Action<bool, int> callback`

  First `bool` is true means is in blacklist

  Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

  User ID.

+ `long otherUserId`

  Other user ID.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Is In Blacklist(Multi users)

	//-- Async Method
	public void IsInBlackList(Action<HashSet<long>, int> callback, long userId, HashSet<long> otherUserIds, int timeout = 0)
	
	//-- Sync Method
	public int IsInBlackList(out HashSet<long> blackUids, long userId, HashSet<long> otherUserIds, int timeout = 0)

Check is in blacklist.

Parameters:

+ `Action<HashSet<long>, int> callback`

  Callabck for async method.  
  First `HashSet<long>` is gotten uids which in blacklist;  
  Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

  User ID.

+ `HashSet<long> otherUserIds`

  Other user IDs.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.

  

### Get Blacklist

	//-- Async Method
	public void GetBlacklist(Action<HashSet<long>, int> callback, long userId, int timeout = 0)
	
	//-- Sync Method
	public int GetBlacklist(out HashSet<long> uids, long userId, int timeout = 0)

Get blocked uids from blacklist.

+ `Action<HashSet<long>, int> callback`

	Callabck for async method.  
	First `HashSet<long>` is gotten uids;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

  User ID.

+ `out HashSet<long> uids`

	The gotten uids.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.