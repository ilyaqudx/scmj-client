using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.entity
{
    [Serializable]
    public class Player
    {
        public long id;
        public string nickName;
        public int sex;
        public string openid;
        public string unionId;
        public long gold;
        public long score;
        public int seat;
        public bool ready;
        public List<GameObject> handCard;
        public List<List<GameObject>> pengCard;
        public List<GameObject> outCard;

        public override string ToString()
        {
            return string.Format(
                "ID: {0}, NickName: {1}, Sex: {2}, Openid: {3}, UnionId: {4}, Gold: {5}, Score: {6}, Seat: {7}", id,
                nickName, sex, openid, unionId, gold, score, seat);
        }
    }
}