using System;
using System.Collections.Generic;

namespace com.fpnn.rtm
{
    public delegate void RTMConnectionConnectedDelegate(Int64 connectionId, string endpoint, bool connected, bool isReconnect);
    public delegate void RTMConnectionCloseDelegate(Int64 connectionId, string endpoint, bool causedByError, bool isReconnect);

    public class CheckResult
    {
        public int result;
        public List<int> tags;
    }

    public class TextCheckResult : CheckResult
    {
        public string text;
        public List<string> wlist;
    }

    public class FileInfo
    {
        //-- Common
        public string url;          //-- File url
        public int size = 0;        //-- File size

        //-- For image type
        public string surl;         //-- Thumb url, only for image type.

        //-- For RTM audio
        public bool isRTMAudio = false;
        public string language;
        public int duration = 0;
    }

    public class BaseMessage
    {
        public byte messageType;
        public string stringMessage = null;
        public byte[] binaryMessage = null;
        public string attrs;
        public long modifiedTime;
        public FileInfo fileInfo = null;
    }

    public class RetrievedMessage : BaseMessage
    {
        public long cursorId;
    }

    public class TranslatedInfo
    {
        public string sourceLanguage;
        public string targetLanguage;
        public string sourceText;
        public string targetText;
    }

    public class RTMMessage : BaseMessage
    {
        public long fromUid;
        public long toId;                   //-- xid
        public long messageId;
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