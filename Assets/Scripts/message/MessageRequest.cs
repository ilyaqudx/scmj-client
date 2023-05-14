using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class MessagePacket
    {
        public long accountId;
        public int clientType;
        public int version;
        public int cmd;
        public string data;
    }
}