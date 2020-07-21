# RTM Server CSharp SDK

[TOC]

## Depends

* [msgpack-csharp](https://github.com/highras/msgpack-csharp)

* [fpnn-sdk-csharp](https://github.com/highras/fpnn-sdk-csharp)



### Compatibility Version:

C# .Net Standard 2.0



### Capability in Funture

Encryption Capability, depending on FPNN C# SDK.

## Usage

### Using package

	using com.fpnn.rtm;



### Init

**Init MUST in the main thread.**

#### FPNN SDK Init (REQUIRED)

	using com.fpnn;
	ClientEngine.Init();
	ClientEngine.Init(Config config);



### Create

	RTMServerClient client = new RTMServerClient(int projectId, string secretKey, string endpoint);

Please get your project params from RTM Console.



### RTMServerClient Instance Configure

#### Configure Functions List:

	public void SetConnectTimeout(int seconds)
	public void SetQuestTimeout(int seconds)
	public void SetServerPushMonitor(RTMQuestProcessor questProcessor)
	public void SetRegressiveConnectStrategy(RegressiveStrategy strategy)
	public void SetConnectionConnectedDelegate(RTMConnectionConnectedDelegate ccd)
	public void SetConnectionCloseDelegate(RTMConnectionCloseDelegate cwcd)
	public void SetAutoConnect(bool autoConnect)
	public void SetAudoReconnect(bool autoReconnect)
	public void SetErrorRecorder(common.ErrorRecorder recorder)



* SetConnectTimeout

  ```
  public void SetConnectTimeout(int seconds)
  ```

  Connecting timeout for this RTMServerClient instance. Default is 0, meaning using the global config. 

  

* SetQuestTimeout

  ```
  public void SetQuestTimeout(int seconds)
  ```

  Quest timeout for this RTMServerClient instance. Default is 0, meaning using the global config.

  

* SetServerPushMonitor

  ```
  public void SetServerPushMonitor(RTMQuestProcessor questProcessor)
  ```

  Set server push monitor processor implemented com.fpnn.rtm.RTMQuestProcessor. Please refer [Server Push Monitor](doc/ServerPushMonitor.md)

  

* SetRegressiveConnectStrategy

  ```
  public void SetRegressiveConnectStrategy(RegressiveStrategy strategy)
  ```

  Set the regressive connect strategy when connection is disconnected.



* SetConnectionConnectedDelegate

  ```
  public void SetConnectionConnectedDelegate(RTMConnectionConnectedDelegate ccd)
  ```

  Set the connection connected delegate.

  

* SetConnectionCloseDelegate

  ```
  public void SetConnectionCloseDelegate(RTMConnectionCloseDelegate cwcd)
  ```

  Set the connection close delegate.

  

* SetAutoConnect

  ```
  public void SetAutoConnect(bool autoConnect)
  ```

  Enable/disable auto connect.
  
  

* SetAudoReconnect

  ```
  public void SetAudoReconnect(bool autoReconnect)
  ```

  Enable/disable auto reconnect.

  

* SetErrorRecorder

  ```
  public void SetErrorRecorder(common.ErrorRecorder recorder)
  ```

  Config the ErrorRecorder instance for this RTMServerClient. Default is null.



### SDK Version

	Console.WriteLine(com.fpnn.rtm.RTMServerConfig.SDKVersion);   // csharp sdk version
	Console.WriteLine(com.fpnn.rtm.RTMServerConfig.InterfaceVersion);  // rtm server api version
	Console.WriteLine(com.fpnn.Config.Version);  // fpnn sdk version


### Structures

Please refer [RTM Structures](doc/Structures.md)



### Delegates

Please refer [RTM Delegates](doc/Delegates.md)



### Listening & Server Push Monitor

Please refer [Server Push Monitor](doc/ServerPushMonitor.md)



### Token Functions

Please refer [Token Functions](doc/Token.md)



### Chat Functions

Please refer [Chat Functions](doc/Chat.md)



### Messages Functions

Please refer [Messages Functions](doc/Messages.md)



### Files Functions

Please refer [Files Functions](doc/Files.md)



### Friends Functions

Please refer [Friends Functions](doc/Friends.md)



### Groups Functions

Please refer [Groups Functions](doc/Groups.md)



### Rooms Functions

Please refer [Rooms Functions](doc/Rooms.md)



### Users Functions

Please refer [Users Functions](doc/Users.md)



### Data Functions

Please refer [Data Functions](doc/Data.md)



### ValueAdded Functions

Please refer [ValueAdded Functions](doc/ValueAdded.md)

