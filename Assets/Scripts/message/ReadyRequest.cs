using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class ReadyRequest
    {
        public int roomCode;
        public bool ready;
    }
}