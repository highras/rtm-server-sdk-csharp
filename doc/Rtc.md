# RTM Server CSharp SDK Rtc API Docs

# Index

[TOC]

### Invite User Into Voice Room

	//-- Async Method
	public void InviteUserIntoVoiceRoom(Action<int> callback, long roomId, HashSet<long> toUids, long fromUid, int timeout = 0);
	
	//-- Sync Method
	public int InviteUserIntoVoiceRoom(long roomId, HashSet<long> toUids, long fromUid, int timeout = 0)

Invite User Into Voice Room.

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

  Room ID..

+ `HashSet<long> toUids`

  Set of the uids invited.

+ `long fromUid`

  Uid of the inviter.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Close Voice Room

	//-- Async Method
	public void CloseVoiceRoom(Action<int> callback, long roomId, int timeout = 0)
	
	//-- Sync Method
	public int CloseVoiceRoom(long roomId, int timeout = 0)

Close Voice Room.

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

  Room ID.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Kickout From Voice Room

	//-- Async Method
	public void KickoutFromVoiceRoom(Action<int> callback, long userId, long roomId, long fromUid, int timeout = 0)
	
	//-- Sync Method
	public int KickoutFromVoiceRoom(long userId, long roomId, long fromUid, int timeout = 0)

Kickout From Voice Room.

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long userId`

  User ID.

+ `long roomId`

  Room ID.

+ `long fromUid`

  From User ID.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Get Voice Room List

	//-- Async Method
	public void GetVoiceRoomList(Action<HashSet<long>, int> callback, int timeout = 0)
	
	//-- Sync Method
	public int GetVoiceRoomList(out HashSet<long> roomIds, int timeout = 0)

Get Voice Room List. 

Parameters:

+ `Action<HashSet<long>, int> callback`

  First, set of the roomId
  Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Get Voice Room Members

	//-- Async Method
	public void GetVoiceRoomMembers(Action<HashSet<long>, HashSet<long>, int> callback, long roomId, int timeout = 0)
	
	//-- Sync Method
	public int GetVoiceRoomMembers(out HashSet<long> userIds, out HashSet<long> managerIds, long roomId, int timeout = 0)

Get Voice Room Members. 

Parameters:

+ `Action<HashSet<long>, HashSet<long>, int> callback`

  First, set of the users.
  Second set of the managers.
  Third `int` is the error code indicating the calling is successful or the failed reasons.

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



### Get Voice Room Member Count

	//-- Async Method
	public void GetVoiceRoomMemberCount(Action<int, int> callback, long roomId, int timeout = 0)
	
	//-- Sync Method
	public int GetVoiceRoomMemberCount(out int count, long roomId, int timeout = 0)

Get Voice Room Member Count.

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


### Get Room Members

	//-- Async Method
	public void GetRoomMembers(Action<HashSet<long>, int> callback, long roomId, int timeout = 0)
	
	//-- Sync Method
	public int GetRoomMembers(out HashSet<long> userIds, long roomId, int timeout = 0)

Get room members.

+ `Action<HashSet<long>, int> callback`

	Callabck for async method.  
	First `HashSet<long>` is gotten room members' uids;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `out HashSet<long> uids`

	The gotten room members' uids.

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


### Get Room Member Count

	//-- Async Method
	public void GetVoiceRoomMemberCount(Action<int, int> callback, long roomId, int timeout = 0)
	
	//-- Sync Method
	public int GetVoiceRoomMemberCount(out int count, long roomId, int timeout = 0)

Get Room Member Count.

+ `Action<int, int> callback`

  Callabck for async method.  
  First `int` is the member count.
  Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

  Room IDs

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.



### Set Voice RoomM ic Status

	//-- Async Method
	public void SetVoiceRoomMicStatus(Action<int> callback, long roomId, bool status, int timeout = 0)
	
	//-- Sync Method
	public int SetVoiceRoomMicStatus(long roomId, bool status, int timeout = 0)

Set Voice RoomM ic Status.

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

  Room ID..

+ `bool status`

  Default status, false for close, true for open.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.

### Pull Into Voice Room

	//-- Async Method
	public void PullIntoVoiceRoom(Action<int> callback, long roomId, HashSet<long> toUids, int timeout = 0)
	
	//-- Sync Method
	public int PullIntoVoiceRoom(long roomId, HashSet<long> toUids, int timeout = 0)

Pull Into Voice Room.

Parameters:

+ `Action<int> callback`

  First `int` is the error code indicating the calling is successful or the failed reasons.

+ `long roomId`

  Room ID..

+ `HashSet<long> toUids`

  Set of the uids pulled.

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.