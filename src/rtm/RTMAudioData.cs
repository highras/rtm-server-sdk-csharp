using System;
using System.Collections.Generic;
using com.fpnn.msgpack;

namespace com.fpnn.rtm
{
    public class RTMAudioData
    {
        private RTMAudioHeader rtmAudioHeader;
        private readonly byte[] audio;  // Processed audio data with rtm-header
        private long duration;   // Duration in ms
        private int frequency;

        public RTMAudioData(byte[] audio)
        {
            this.audio = audio;
            ParseAudioHeader();
        }

        private void ParseAudioHeader()
        {
            rtmAudioHeader = new RTMAudioHeader();

            int offset = 0;
            if (audio.Length < 4)
                return;
            rtmAudioHeader.version = audio[0];
            rtmAudioHeader.containerType = (RTMAudioHeader.ContainerType)audio[1];
            rtmAudioHeader.codecType = (RTMAudioHeader.CodecType)audio[2];

            byte infoDataCount = audio[3];

            offset += 4;

            for (byte i = 0; i < infoDataCount; i++) {
                int sectionLength = BitConverter.ToInt32(audio, offset);
                offset += 4;

                Dictionary<Object, Object> infoData = MsgUnpacker.Unpack(audio, offset, sectionLength);
                object value;
                if (infoData.TryGetValue("lang", out value))
                {
                    rtmAudioHeader.lang = (string)value;
                }
                if (infoData.TryGetValue("dur", out value))
                {
                    rtmAudioHeader.duration = Convert.ToInt64(value);
                    duration = rtmAudioHeader.duration;
                }
                if (infoData.TryGetValue("srate", out value))
                {
                    rtmAudioHeader.sampleRate = Convert.ToInt32(value);
                    frequency = rtmAudioHeader.sampleRate;
                }
                if (infoData.TryGetValue("rtext", out value))
                {
                    rtmAudioHeader.rtext = (string)value;
                }
                if (infoData.TryGetValue("rlang", out value))
                {
                    rtmAudioHeader.rlang = (string)value;
                }

                offset += sectionLength;
            }
        }

        public byte[] Audio
        {
            get
            {
                return audio;
            }
        }

        public long Duration
        {
            get
            {
                return duration;
            }
        }

        public int Frequency
        {
            get
            {
                return frequency;
            }
        }

        public RTMAudioHeader RtmAudioHeader
        {
            get
            {
                return rtmAudioHeader;
            }
        }

        public string RecognitionText
        {
            get
            {
                return rtmAudioHeader.rtext;
            }
        }

        public string RecognitionLang
        {
            get
            {
                return rtmAudioHeader.rlang;
            }
        }
        
    }
}
