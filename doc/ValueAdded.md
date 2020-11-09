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
	First `TranslatedInfo` is translation message result, please refer [TranslatedInfo](Structures.md#TranslatedInfo);  
	Second `int` is the error code indicating the calling is successful or the failed reasons.

+ `out TranslatedInfo translatedinfo`

	The translation message result, please refer [TranslatedInfo](Structures.md#TranslatedInfo).

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


### SpeechToText

	//-- Async Method
	public bool SpeechToText(Action<string, string, int> callback, string audioUrl, string language, string codec = null, int sampleRate = 0, long userId = 0, int timeout = 120);
	public bool SpeechToText(Action<string, string, int> callback, byte[] audioBinaryContent, string language, string codec = null, int sampleRate = 0, long userId = 0, int timeout = 120);
	
	//-- Sync Method
	public int SpeechToText(out string resultText, out string resultLanguage, string audioUrl, string language, string codec = null, int sampleRate = 0, long userId = 0, int timeout = 120);
	public int SpeechToText(out string resultText, out string resultLanguage, byte[] audioBinaryContent, string language, string codec = null, int sampleRate = 0, long userId = 0, int timeout = 120);

Speech Recognition, convert speech to text.

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

+ `string audioUrl`

	Http/https url for speech binary.

+ `byte[] audioBinaryContent`

	Speech binary data.

+ `language`

	Speech language when recording. Available language please refer the documents in [https://www.ilivedata.com/](https://docs.ilivedata.com/stt/production/).

	[Current Chinese document](https://docs.ilivedata.com/stt/production/)

+ `codec`

	Codec for speech binary. If codec is `null` means `AMR_WB`.

+ `sampleRate`

	Sample rate for speech binary. If `0` means 16000.

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


### TextCheck

	//-- Async Method
	public bool TextCheck(Action<TextCheckResult, int> callback, string text, long userId = 0, int timeout = 120);
	
	//-- Sync Method
	public int TextCheck(out TextCheckResult result, string text, long userId = 0, int timeout = 120);

Text moderation.

Parameters:

+ `Action<TextCheckResult, int> callback`

	Callabck for async method.  
	First `TextCheckResult` is the result for text moderation;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.  
	`TextCheckResult` can be refered [TextCheckResult](Structures.md#TextCheckResult).

+ `out TextCheckResult result`

	The result for text moderation. Please refer [TextCheckResult](Structures.md#TextCheckResult).

+ `string text`

	The text need to be audited.

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


### ImageCheck

	//-- Async Method
	public bool ImageCheck(Action<CheckResult, int> callback, string imageUrl, long userId = 0, int timeout = 120);
	public bool ImageCheck(Action<CheckResult, int> callback, byte[] imageContent, long userId = 0, int timeout = 120);
	
	//-- Sync Method
	public int ImageCheck(out CheckResult result, string imageUrl, long userId = 0, int timeout = 120);
	public int ImageCheck(out CheckResult result, byte[] imageContent, long userId = 0, int timeout = 120);

Image review.

Parameters:

+ `Action<CheckResult, int> callback`

	Callabck for async method.  
	First `CheckResult` is the result for image review;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.  
	`CheckResult` can be refered [CheckResult](Structures.md#CheckResult).

+ `out CheckResult result`

	The result for image review. Please refer [CheckResult](Structures.md#CheckResult).

+ `string imageUrl`

	Image's http/https url for auditing.

+ `byte[] imageContent`

	Image binary data for auditing.

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


### AudioCheck

	//-- Async Method
	public bool AudioCheck(Action<CheckResult, int> callback, string audioUrl, string language, string codec = null, int sampleRate = 0, long userId = 0, int timeout = 120);
	public bool AudioCheck(Action<CheckResult, int> callback, byte[] audioContent, string language, string codec = null, int sampleRate = 0, long userId = 0, int timeout = 120);
	
	//-- Sync Method
	public int AudioCheck(out CheckResult result, string audioUrl, string language, string codec = null, int sampleRate = 0, long userId = 0, int timeout = 120);
	public int AudioCheck(out CheckResult result, byte[] audioContent, string language, string codec = null, int sampleRate = 0, long userId = 0, int timeout = 120);

Audio check.

Parameters:

+ `Action<CheckResult, int> callback`

	Callabck for async method.  
	First `CheckResult` is the result for audio checking;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.  
	`CheckResult` can be refered [CheckResult](Structures.md#CheckResult).

+ `out CheckResult result`

	The result for audio checking. Please refer [CheckResult](Structures.md#CheckResult).

+ `string audioUrl`

	Http/https url for speech binary to be checking.

+ `byte[] audioContent`

	Audio binary data for checking.

+ `language`

	Audio language when recording. Available language please refer the documents in [https://www.ilivedata.com/](https://docs.ilivedata.com/stt/production/).

	[Current Chinese document](https://docs.ilivedata.com/audiocheck/techdoc/submit/)  
	[Current Chinese document (live audio)](https://docs.ilivedata.com/audiocheck/livetechdoc/livesubmit/)

+ `codec`

	Codec for audio content. If codec is `null` means `AMR_WB`.

+ `sampleRate`

	Sample rate for audio content. If `0` means 16000.

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


### VideoCheck

	//-- Async Method
	public bool VideoCheck(Action<CheckResult, int> callback, string videoUrl, string videoName, long userId = 0, int timeout = 120);
	public bool VideoCheck(Action<CheckResult, int> callback, byte[] videoContent, string videoName, long userId = 0, int timeout = 120);
	
	//-- Sync Method
	public int VideoCheck(out CheckResult result, string videoUrl, string videoName, long userId = 0, int timeout = 120);
	public int VideoCheck(out CheckResult result, byte[] videoContent, string videoName, long userId = 0, int timeout = 120);

Video review.

Parameters:

+ `Action<CheckResult, int> callback`

	Callabck for async method.  
	First `CheckResult` is the result for video review;  
	Second `int` is the error code indicating the calling is successful or the failed reasons.  
	`CheckResult` can be refered [CheckResult](Structures.md#CheckResult).

+ `out CheckResult result`

	The result for video review. Please refer [CheckResult](Structures.md#CheckResult).

+ `string videoUrl`

	Video's http/https url for auditing.

+ `byte[] videoContent`

	Video binary data for auditing.

+ `string videoName`

	Video name.

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

