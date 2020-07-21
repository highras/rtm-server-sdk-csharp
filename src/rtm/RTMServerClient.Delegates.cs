using System;
using System.Collections.Generic;

namespace com.fpnn.rtm
{
    public delegate void RTMConnectionConnectedDelegate(Int64 connectionId, string endpoint, bool connected, bool isReconnect);
    public delegate void RTMConnectionCloseDelegate(Int64 connectionId, string endpoint, bool causedByError, bool isReconnect);

    public class RetrievedMessage
    {
        public long cursorId;
        public byte messageType;
        public string stringMessage = null;
        public byte[] binaryMessage = null;
        public string attrs;
        public long modifiedTime;
    }

    public class AudioInfo
    {
        public string sourceLanguage;
        public string recognizedLanguage;
        public string recognizedText;
        public int duration;
    }

    public class TranslatedInfo
    {
        public string sourceLanguage;
        public string targetLanguage;
        public string sourceText;
        public string targetText;
    }

    public class RTMMessage
    {
        public long fromUid;
        public long toId;                   //-- xid
        public byte messageType;
        public long messageId;
        public string stringMessage = null;
        public byte[] binaryMessage = null;
        public string attrs;
        public long modifiedTime;
        public AudioInfo audioInfo = null;
        public TranslatedInfo translatedInfo = null;
    }

    public class HistoryMessage : RTMMessage
    {
        public long cursorId;
    }

    public class HistoryMessageResult
    {
        public int count;
        public long lastCursorId;
        public long beginMsec;
        public long endMsec;
        public List<HistoryMessage> messages;
    }

    public delegate void HistoryMessageDelegate(int count, long lastCursorId, long beginMsec, long endMsec, List<HistoryMessage> messages, int errorCode);

}