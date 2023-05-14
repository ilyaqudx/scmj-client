using System;
using System.Collections.Generic;
using DefaultNamespace.entity;
using UnityEngine;

namespace DefaultNamespace.message
{
    [Serializable]
    public class RoomScene
    {
        public long id;
        public int code;
        public long owner;
        public long banker;
        public List<Player> players;
        public int roomType;
        public string roomState;
        public string ingState;
        public int totalRound;
        public int currentRound;

        public GameObject selectCard;
        public Player currentPlayer;

        // 下家手牌区域偏移量
        public Vector2 DOWN_HAND_CARD_OFFSET = new Vector2(-430, -285);

        // 玩法: xzdd-血战到底 xlch-血流成河
        public string mode;

        // 局数: 8/16/32
        public int round;

        // 番数: 8/16/32
        public int fan;

        // 自摸: 1-加底 2-加番
        public int zimo;

        // 刮风下雨
        public bool guaFengXiaYu;

        // 呼叫转移
        public bool HuJiaoZhuanYi;

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

        public override string ToString()
        {
            return string.Format(
                "ID: {0}, Code: {1}, Owner: {2}, Players: {3}, RoomType: {4}, RoomState: {5}, TotalRound: {6}, CurrentRound: {7}, Mode: {8}, Round: {9}, Fan: {10}, Zimo: {11}, GuaFengXiaYu: {12}, HuJiaoZhuanYi: {13}, HuanSanZhang: {14}, JinGouDiao: {15}, HaiDiLao: {16}, HaiDiPao: {17}, DuanYaojiu: {18}, YaoJiu: {19}",
                id, code, owner, players, roomType, roomState, totalRound, currentRound, mode, round, fan, zimo,
                guaFengXiaYu, HuJiaoZhuanYi, huanSanZhang, jinGouDiao, haiDiLao, haiDiPao, duanYaojiu, yaoJiu);
        }
    }
}