using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class ExitRoomResponse
    {
        public long playerId;
        public int seat;
        public int roomCode;
    }
}