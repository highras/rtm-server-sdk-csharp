using System;

namespace com.fpnn.rtm
{
    public class RTMServerConfig
    {
        public static readonly string SDKVersion = "1.1.6";
        public static readonly string InterfaceVersion = "2.7.0";

        public static int globalConnectTimeoutSeconds = 30;
        internal static int globalQuestTimeoutSeconds = 30;
        internal static int fileGateClientHoldingSeconds = 150;
        internal static common.ErrorRecorder errorRecorder = null;

        public int globalConnectTimeout;
        public int globalQuestTimeout;
        public int fileClientHoldingSeconds;
        public common.ErrorRecorder defaultErrorRecorder;

        public RTMServerConfig()
        {
            globalConnectTimeout = RTMServerConfig.globalConnectTimeoutSeconds;
            globalQuestTimeout = RTMServerConfig.globalQuestTimeoutSeconds;
            fileClientHoldingSeconds = RTMServerConfig.fileGateClientHoldingSeconds;
        }

        internal static void Config(RTMServerConfig config)
        {
            globalConnectTimeoutSeconds = config.globalConnectTimeout;
            globalQuestTimeoutSeconds = config.globalQuestTimeout;
            fileGateClientHoldingSeconds = config.fileClientHoldingSeconds;
            errorRecorder = config.defaultErrorRecorder;
        }
    }

    public class RTMServerConfigCenter
    {
        public static void Init()
        {
            Init(null);
        }

        public static void Init(RTMServerConfig config)
        {
            if (config == null)
                return;

            RTMServerConfig.Config(config);
        }
    }

    public enum TranslateLanguage
        {
            ar,             //阿拉伯语
            nl,             //荷兰语
            en,             //英语
            fr,             //法语
            de,             //德语
            el,             //希腊语
            id,             //印度尼西亚语
            it,             //意大利语
            ja,             //日语
            ko,             //韩语
            no,             //挪威语
            pl,             //波兰语
            pt,             //葡萄牙语
            ru,             //俄语
            es,             //西班牙语
            sv,             //瑞典语
            tl,             //塔加路语（菲律宾语）
            th,             //泰语
            tr,             //土耳其语
            vi,             //越南语
            zh_cn,       //中文（简体）
            zh_tw,       //中文（繁体）
            None
        }

        public enum MessageType : byte
        {
            Withdraw = 1,
            GEO = 2,
            MultiLogin = 7,
            Chat = 30,
            Audio = 31,
            Cmd = 32,
            RealAudio = 35,
            RealVideo = 36,
            ImageFile = 40,
            AudioFile = 41,
            VideoFile = 42,
            NormalFile = 50
        }

        public enum MessageCategory : byte
        {
            P2PMessage = 1,
            GroupMessage = 2,
            RoomMessage = 3,
            BroadcastMessage = 4
        }

        public enum FileTokenType
        {
            P2P,
            Group,
            Room,
            Multi,
            Broadcast
        }
}
