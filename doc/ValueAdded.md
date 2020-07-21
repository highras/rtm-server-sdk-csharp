# RTM Server CSharp SDK Value-Added API Docs

# Index

[TOC]

### Translate

	//-- Async Method
	public void Translate(Action<TranslatedInfo, int> callback, string text,
	            TranslateLanguage destinationLanguage, TranslateLanguage sourceLanguage = TranslateLanguage.None,
	            TranslateType type = TranslateType.Chat, ProfanityType profanity = ProfanityType.Off,
	            int timeout = 0)
	
	//-- Sync Method
	public int Translate(out TranslatedInfo translatedinfo, string text,
	            TranslateLanguage destinationLanguage, TranslateLanguage sourceLanguage = TranslateLanguage.None,
	            TranslateType type = TranslateType.Chat, ProfanityType profanity = ProfanityType.Off, long userId = 0, int timeout = 0)

Translate text to target language.

Parameters:

+ `Action<TranslatedInfo>, int> callback`

	Callabck for async method.  
	First `TranslatedInfo` is translation message result, please refer [TranslatedInfo](doc/Structures.md#TranslatedInfo);  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `out TranslatedInfo translatedinfo`

	The translation message result, please refer [TranslatedInfo](doc/Structures.md#TranslatedInfo).

+ `string text`

	The text need to be translated.

+ `TranslateLanguage destinationLanguage`

	Target language enum.

+ `TranslateLanguage sourceLanguage`

	Source language enum. Value `TranslateLanguage.None` means automatic recognition.

+ `TranslateType type`

	TranslateType.Chat or TranslateType.Mail. Default is TranslateType.Chat.

+ `ProfanityType profanity`

	Profanity filter action.

	* ProfanityType.Off (**Default**)
	* ProfanityType.Stop
	* ProfanityType.Censor

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Profanity

	//-- Async Method
	public void Profanity(Action<string, List<string>, int> callback, string text, bool classify = false, long userId = 0, int timeout = 0)
	
	//-- Sync Method
	public int Profanity(out string resultText, out List<string> classification, string text, bool classify = false, long userId = 0, int timeout = 0)

Sensitive words detected and filter.

Parameters:

+ `Action<string, List<string>, int> callback`

	Callabck for async method.  
	First `string` is the processed text by sensitive words detecting and filtering;  
	Second `List<string>` is the classifications of the sensitive words for the original text.  
	If `classify` is `false`, the second parameter will be null.  
	Thrid `int` is the error code indicating the calling is successful or the failed reasons.

+ `out string resultText`

	Processed text by sensitive words detecting and filtering.

+ `out List<string> classification`

	 Classifications of the sensitive words for the original text.  
	 If `classify` is `false`, this parameter will be null.

+ `string text`

	The text need to be detected and filtered.

+ `bool classify`

	Whether to classify the sensitive words.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ bool for Async

	* true: Async calling is start.
	* false: Start async calling is failed.

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.



### Transcribe

	//-- Async Method
	public void Transcribe(Action<string, string, int> callback, byte[] audio, long? userId, int timeout = 120)
	public void Transcribe(Action<string, string, int> callback, byte[] audio, bool filterProfanity, long? userId, int timeout = 120)
	
	public void Transcribe(Action<string, string, int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 120)
	public void Transcribe(Action<string, string, int> callback, long fromUid, long toId, long messageId, MessageCategory messageCategory, bool filterProfanity, int timeout = 120)
	
	//-- Sync Method
	public int Transcribe(out string resultText, out string resultLanguage, byte[] audio, long? userId, int timeout = 120)
	public int Transcribe(out string resultText, out string resultLanguage, byte[] audio, bool filterProfanity, long? userId, int timeout = 120)
	
	public int Transcribe(out string resultText, out string resultLanguage, long fromUid, long toId, long messageId, MessageCategory messageCategory, int timeout = 120)
	public int Transcribe(out string resultText, out string resultLanguage, long fromUid, long toId, long messageId, MessageCategory messageCategory, bool filterProfanity, int timeout = 120)

Speech Recognition.

Parameters:

+ `Action<string, string, int> callback`

	Callabck for async method.  
	First `string` is the text converted from recognized speech;  
	Second `string` is the recognized language.  
	Thrid `int` is the error code indicating the calling is successful or the failed reasons.

+ `out string resultText`

	The text converted from recognized speech.

+ `out string resultLanguage`

	The recognized language.

+ `byte[] audio`

	Speech data.

+ `long? userId`

  User Id.

+ `fromUid`

	Uid of the message sender, which message is wanted to be transcribed.

+ `toId`

	Receiver id of the message.

	If the message is P2P message, `toId` means the uid of receiver;  
	If the message is group message, `toId` means the `groupId`;  
	If the message is room message, `toId` means the `roomId`;  
	If the message is broadcast message, `toId` is `0`.


+ `messageId`

	Message id for the message which wanted to be transcribed.

+ `messageCategory`

	MessageCategory enumeration.

	Can be MessageCategory.P2PMessage, MessageCategory.GroupMessage, MessageCategory.RoomMessage, MessageCategory.BroadcastMessage.

+ `filterProfanity`

	Enable or disable sensitive words detected and filter.

+ `int timeout`

	Timeout in second.

	0 means using default setting.


Return Values:

+ None for Async

+ int for Sync

	0 or com.fpnn.ErrorCode.FPNN_EC_OK means calling successed.

	Others are the reason for calling failed.