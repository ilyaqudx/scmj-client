using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class MessageResponse
    {
        public int cmd;
        public int status;
        public string data;

        public override string ToString()
        {
            return string.Format("Cmd: {0}, Status: {1}, data: {2}", cmd, status, data);
        }
    }
}