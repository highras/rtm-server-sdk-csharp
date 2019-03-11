using MessagePack;

namespace com.rtm
{
    [MessagePackObject]
    public class P2PMsg
    {
        [Key(0)]
        public long id { get; set; }
        [Key(1)]
        public byte direction { get; set; }
        [Key(2)]
        public byte mtype { get; set; }
        [Key(3)]
        public long mid { get; set; }
        [Key(4)]
        public bool deleted { get; set; }
        [Key(5)]
        public string msg { get; set; }
        [Key(6)]
        public string attrs { get; set; }
        [Key(7)]
        public long mtime { get; set; }
    }

    [MessagePackObject]
    public class BroadcastMsg
    {
        [Key(0)]
        public long id { get; set; }
        [Key(1)]
        public long from { get; set; }
        [Key(2)]
        public byte mtype { get; set; }
        [Key(3)]
        public long mid { get; set; }
        [Key(4)]
        public bool deleted { get; set; }
        [Key(5)]
        public string msg { get; set; }
        [Key(6)]
        public string attrs { get; set; }
        [Key(7)]
        public long mtime { get; set; }
    }

    [MessagePackObject]
    public class RoomMsg
    {
        [Key(0)]
        public long id { get; set; }
        [Key(1)]
        public long from { get; set; }
        [Key(2)]
        public byte mtype { get; set; }
        [Key(3)]
        public long mid { get; set; }
        [Key(4)]
        public bool deleted { get; set; }
        [Key(5)]
        public string msg { get; set; }
        [Key(6)]
        public string attrs { get; set; }
        [Key(7)]
        public long mtime { get; set; }
    }

    [MessagePackObject]
    public class GroupMsg
    {
        [Key(0)]
        public long id { get; set; }
        [Key(1)]
        public long from { get; set; }
        [Key(2)]
        public byte mtype { get; set; }
        [Key(3)]
        public long mid { get; set; }
        [Key(4)]
        public bool deleted { get; set; }
        [Key(5)]
        public string msg { get; set; }
        [Key(6)]
        public string attrs { get; set; }
        [Key(7)]
        public long mtime { get; set; }
    }
}