using System;
namespace com.fpnn.rtm
{
    internal class MidGenerator
    {
        static private int count = 0;
        static private int randId = 0;
        static private int randBits = 8;
        static private int sequenceBits = 6;
        static private int sequenceMask = -1 ^ (-1 << sequenceBits);
        static private long lastTime = 0;
        static private object interLocker = new object();

        static public void Init()
        {
            Random random = new Random();
            randId = random.Next(1, 255);
        }

        static private long GetNextMillis(long lastTimestamp)
        {
            long timestamp = ClientEngine.GetCurrentMilliseconds();
            while (timestamp <= lastTimestamp) 
            {
                timestamp = ClientEngine.GetCurrentMilliseconds();
            }
            return timestamp;
        }

        static public long Gen()
        {
            lock (interLocker)
            {
                long time = ClientEngine.GetCurrentMilliseconds();
                count = (count + 1) & sequenceMask;
                if(count == 0)
                {
                    time = GetNextMillis(lastTime);
                }
                lastTime = time;
                long id = (time << (randBits + sequenceBits)) | (uint)(randId << sequenceBits) | (uint)count;
                return id;
            }
        }
    }
}
