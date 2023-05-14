using System;
using UnityEngine.Serialization;

namespace DefaultNamespace.message
{
    [Serializable]
    public class ReadyResponse
    {
        public long playerId;
        public int roomCode;
        public bool ready;

        public override string ToString()
        {
            return string.Format("PlayerId: {0}, RoomUuid: {1}, Ready: {2}", playerId, roomCode, ready);
        }
    }
}