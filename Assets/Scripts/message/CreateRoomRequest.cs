using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class CreateRoomRequest
    {
        // 玩法: xzdd-血战到底 xlch-血流成河
        public string mode;

        // 局数: 8/16/32
        public int round;

        // 番数: 8/16/32
        public int fan;

        // 换三张规则 
        public bool huanSanZhang;

        // 金钩钓规则
        public bool jinGouDiao;

        // 海底捞规则
        public bool haiDiLao;

        // 海底炮规则
        public bool haiDiPao;

        // 断么九规则
        public bool duanYaojiu;

        // 么九规则
        public bool yaoJiu;
    }
}