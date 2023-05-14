using System;
using System.Collections.Generic;

namespace DefaultNamespace.message
{
    [Serializable]
    public class StartGameNotice
    {
        public long playerId;
        public long banker;
        public string roomState;
        public string ingState;

        public int currentRound;

        // 本家手牌信息
        public List<int> handCard;

        // 所有玩家手牌数量
        public List<int> handCardCount;
    }
}