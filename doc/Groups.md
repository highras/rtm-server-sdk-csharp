# RTM Server CSharp SDK Groups API Docs

# Index

[TOC]

### Add Group Members

	//-- Async Method
	public void AddGroupMembers(Action<int> callback, long groupId, HashSet<long> userIds, int timeout = 0)
	
	//-- Sync Method
	public int AddGroupMembers(long groupId, HashSet<long> userIds, int timeout = 0)

Add group members. Note: Current user MUST be the group member.

Parameters:

+ `Action<int> callback`

		First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long groupId`

	Group id.

+ `HashSet<long> userIds`

	New members' uids set. Max 100 users for each calling.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Delete Group Members

	//-- Async Method
	public void DeleteGroupMembers(Action<int> callback, long groupId, HashSet<long> userIds, int timeout = 0)
	
	//-- Sync Method
	public int DeleteGroupMembers(long groupId, HashSet<long> userIds, int timeout = 0)

Delete group members. Note: Current user MUST be the group member.

Parameters:

+ `Action<int> callback`

		First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long groupId`

	Group id.

+ `HashSet<long> userIds`

	The members' uids set. Max 100 users for each calling.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Delete Group

	//-- Async Method
	public void DeleteGroup(Action<int> callback, long groupId, int timeout = 0)
	
	//-- Sync Method
	public int DeleteGroup(long groupId, int timeout = 0)

Delete group.

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long groupId`

  Group id.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Get Group Members

	//-- Async Method
	public void GetGroupMembers(Action<HashSet<long>, int> callback, long groupId, int timeout = 0)
	
	//-- Sync Method
	public int GetGroupMembers(out HashSet<long> userIds, long groupId, int timeout = 0)

Get group members.

+ `Action<HashSet<long>, int> callback`

	Callabck for async method.  
	First `HashSet<long>` is gotten group members' uids;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `out HashSet<long> uids`

	The gotten group members' uids.

+ `long groupId`

	Group id.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Is Group Member

	//-- Async Method
	public void IsGroupMember(Action<bool, int> callback, long groupId, long userId, int timeout = 0)
	
	//-- Sync Method
	public int IsGroupMember(out bool ok, long groupId, long userId, int timeout = 0)

Check is friends.

+ `Action<bool, int> callback`

  Callabck for async method.  
  First `bool` is true means is group member  
  Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `long groupId`

  Group ID.

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



### Get User Groups

	//-- Async Method
	public void GetUserGroups(Action<HashSet<long>, int> callback, long userId, int timeout = 0)
	
	//-- Sync Method
	public int GetUserGroups(out HashSet<long> groupIds, long userId, int timeout = 0)

Get current user's all groups.

+ `Action<HashSet<long>, int> callback`

	Callabck for async method.  
	First `HashSet<long>` is gotten current user's group ids;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `out HashSet<long> groupIds`

	The gotten current user's group ids.

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



### Add Group Ban

	//-- Async Method
	public void AddGroupBan(Action<int> callback, long groupId, long userId, int banTime, int timeout = 0)
	
	//-- Sync Method
	public int AddGroupBan(long groupId, long userId, int banTime, int timeout = 0)

Add group ban.

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long groupId`

  Group ID.

+ `long userId`

  User ID.

+ `int banTime`

  Ban time in seconds.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Remove Group Ban

	//-- Async Method
	public void RemoveGroupBan(Action<int> callback, long groupId, long userId, int timeout = 0)
	
	//-- Sync Method
	public int RemoveGroupBan(long groupId, long userId, int timeout = 0)

Remove group ban.

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long groupId`

  Group ID.

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



### Is Ban Of  Group

	//-- Async Method
	public void IsBanOfGroup(Action<bool, int> callback, long groupId, long userId, int timeout = 0)
	
	//-- Sync Method
	public int IsBanOfGroup(out bool ok, long groupId, long userId, int timeout = 0)

Check if is ban of a group.

+ `Action<bool, int> callback`

  Callabck for async method.  
  First `bool` is true means is ban of a group  
  Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `long groupId`

  Group ID.

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



### Set Group Info


	//-- Async Method
	public void SetGroupInfo(Action<int> callback, long groupId, string publicInfo = null, string privateInfo = null, int timeout = 0)
	
	//-- Sync Method
	public int SetGroupInfo(long groupId, string publicInfo = null, string privateInfo = null, int timeout = 0)

Set group public info and private info. Note: Current user MUST be the group member.

Parameters:

+ `Action<int> callback`

		First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long groupId`

	Group id.

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



### Get Group Info

	//-- Async Method
	public void GetGroupInfo(Action<string, string, int> callback, long groupId, int timeout = 0)
	
	//-- Sync Method
	public int GetGroupInfo(out string publicInfo, out string privateInfo, long groupId, int timeout = 0)

Get group public info and private info. Note: Current user MUST be the group member.

Parameters:

+ `Action<string, string, int> callback`

	Callabck for async method.  
	First `string` is gotten public info of this group;  
	Second `string` is gotten private info of this group;  
	Thrid `int` is the error code indicating the calling is successful or the failed reasons.

+ `out string publicInfo`

	The gotten public info of this group.

+ `out string privateInfo`

	The gotten private info of this group.

+ `long groupId`

	Group id.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.