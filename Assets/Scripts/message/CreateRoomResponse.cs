using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class CreateRoomResponse
    {
        public RoomScene roomScene;

        public override string ToString()
        {
            return string.Format("RoomScene: {0}", roomScene);
        }
    }
}