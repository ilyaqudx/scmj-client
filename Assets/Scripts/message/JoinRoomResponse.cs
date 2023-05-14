using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class JoinRoomResponse
    {
        public int roomCode;
        public RoomScene roomScene;

        public override string ToString()
        {
            return string.Format("RoomCode: {0}, RoomScene: {1}, Success: {2}", roomCode, roomScene, success);
        }

        public bool success;
    }
}