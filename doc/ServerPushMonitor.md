# RTM Server CSharp SDK API Docs: Server Push Monitor

# Index

[TOC]

## Implement sub-class of RTMQuestProcessor

    public class MyQuestProcessor: RTMQuestProcessor
    {
        //-- Event
        public override void PushEvent(int projectId, string eventName, long userId, int eventTime, string endpoint, string data) { }
    
        //-- Messages
        public override void PushMessage(RTMMessage message) { }
        public override void PushGroupMessage(RTMMessage message) { }
        public override void PushRoomMessage(RTMMessage message) { }
    
        //-- Chat
        public override void PushChat(RTMMessage message) { }
        public override void PushGroupChat(RTMMessage message) { }
        public override void PushRoomChat(RTMMessage message) { }
    
        //-- Audio
        public override void PushAudio(RTMMessage message) { }
        public override void PushGroupAudio(RTMMessage message) { }
        public override void PushRoomAudio(RTMMessage message) { }
    
        //-- Cmd
        public override void PushCmd(RTMMessage message) { }
        public override void PushGroupCmd(RTMMessage message) { }
        public override void PushRoomCmd(RTMMessage message) { }
    
        //-- Files
        public override void PushFile(RTMMessage message) { }
        public override void PushGroupFile(RTMMessage message) { }
        public override void PushRoomFile(RTMMessage message) { }
    }



### Set Server Push Monitor

	client.SetServerPushMonitor(new MyQuestProcessor());



### SetListen / AddListen api



#### AddListen

	//-- Async Method
	public void AddListen(Action<int> callback, HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)
	
	//-- Sync Method
	public int AddListen(HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)

Add Listen for Server Push Monitor. **Add incrementally, only valid for the current connection**

Parameters:

+ `callback` 

		Action<int errorCode> 

+ `HashSet<long> groupIds`

	Listen the group message from the groupIds

+ `HashSet<long> roomIds`

   Listen the room message from the roomIds

+ `HashSet<long> userIds`

   Listen the P2P message from the userIds

+ `HashSet<string> events`

   Listen the events, event name support [login, logout] now

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



#### RemoveListen

	//-- Async Method
	public void RemoveListen(Action<int> callback, HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)
	
	//-- Sync Method
	public int RemoveListen(HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)

Remove Listen for Server Push Monitor. Parameters please refer to  AddListen



#### SetListen

	//-- Async Method
	public void SetListen(Action<int> callback, HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)
	
	//-- Sync Method
	public int SetListen(HashSet<long> groupIds, HashSet<long> roomIds, HashSet<long> userIds, HashSet<string> events, int timeout = 0)

Set Listen for Server Push Monitor. Parameters please refer to  AddListen.  **Full coverage, only valid for the current connection**



#### SetListen

	//-- Async Method
	public void SetListen(Action<int> callback, bool allP2P, bool allGroups, bool allRooms, bool allEvents, int timeout = 0)
	
	//-- Sync Method
	public int SetListen(bool allP2P, bool allGroups, bool allRooms, bool allEvents, int timeout = 0)

Set Listen of all message and event  **Full coverage, only valid for the current connection**

Parameters:

+ `callback` 

  Action<int errorCode> 

+ `bool allP2P`

  Listen all the P2P message

+ `bool allGroups`

  Listen all the Group message

+ `bool allRooms`

  Listen all the Room message

+ `bool allEvents`

  Listen all the events

+ `int timeout`

  Timeout in second.

  0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

  0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

  Others are the reason for calling failed.

