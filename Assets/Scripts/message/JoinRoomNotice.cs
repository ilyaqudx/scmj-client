using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class JoinRoomNotice
    {
        public long playerId;

        public RoomScene roomScene;

        public override string ToString()
        {
            return string.Format("PlayerId: {0}, RoomScene: {1}", playerId, roomScene);
        }
    }
}