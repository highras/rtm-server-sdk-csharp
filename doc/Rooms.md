# RTM Server CSharp SDK Rooms API Docs

# Index

[TOC]

### Add Room Ban

	//-- Async Method
	public void AddRoomBan(Action<int> callback, long roomId, long userId, int banTime, int timeout = 0)
	
	//-- Sync Method
	public int AddRoomBan(long roomId, long userId, int banTime, int timeout = 0)

Add room ban.

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

  Room ID.

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



### Remove Room Ban

	//-- Async Method
	public void RemoveRoomBan(Action<int> callback, long roomId, long userId, int timeout = 0)
	
	//-- Sync Method
	public int RemoveRoomBan(long roomId, long userId, int timeout = 0)

Remove room ban.

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

  Room ID.

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



### Is Ban Of  Room

	//-- Async Method
	public void IsBanOfRoom(Action<bool, int> callback, long roomId, long userId, int timeout = 0)
	
	//-- Sync Method
	public int IsBanOfRoom(out bool ok, long roomId, long userId, int timeout = 0)

Check if is ban of a room.

+ `Action<bool, int> callback`

  Callabck for async method.  
  First `bool` is true means is ban of a room  
  Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

  Room ID.

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



### Add Room Members

	//-- Async Method
	public void AddRoomMember(Action<int> callback, long roomId, long userId, int timeout = 0)
	
	//-- Sync Method
	public int AddRoomMember(long roomId, long userId, int timeout = 0)

Add room members. 

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

  Group id.

+ `long userId`

  User id

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Delete Room Members

	//-- Async Method
	public void DeleteRoomMember(Action<int> callback, long roomId, long userId, int timeout = 0)
	
	//-- Sync Method
	public int DeleteRoomMember(long roomId, long userId, int timeout = 0)

Delete room members. 

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

  Group id.

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



### Set Room Info


	//-- Async Method
	public void SetRoomInfo(Action<int> callback, long roomId, string publicInfo = null, string privateInfo = null, int timeout = 0)
	
	//-- Sync Method
	public int SetRoomInfo(long roomId, string publicInfo = null, string privateInfo = null, int timeout = 0)

Set room public info and private info. Note: Current user MUST in the room.

Parameters:

+ `Action<int> callback`

		First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

	Room id.

+ `string publicInfo`

	New public info for room. `null` means don't change the public info. Max length is 65535 bytes.

+ `string privateInfo`

	New private info for room. `null` means don't change the private info. Max length is 65535 bytes.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.
	
	


### Get Room Info

	//-- Async Method
	public void GetRoomInfo(Action<string, string, int> callback, long roomId, int timeout = 0)
	
	//-- Sync Method
	public int GetRoomInfo(out string publicInfo, out string privateInfo, long roomId, int timeout = 0)

Get room public info and private info. Note: Current user MUST in the room.

Parameters:

+ `Action<string, string, int> callback`

	Callabck for async method.  
	First `string` is gotten public info of this room;  
	Second `string` is gotten private info of this room;  
	Thrid `int` is the error code indicating the calling is successful or the failed reasons.

+ `out string publicInfo`

	The gotten public info of this room.

+ `out string privateInfo`

	The gotten private info of this room.

+ `long roomId`

	Room id.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.