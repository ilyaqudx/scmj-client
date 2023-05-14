using System;

namespace entity
{
    [Serializable]
    public class Account
    {
        public long id;
        public string nickName;
        public int sex;
        public string openid;
        public string unionId;
        public long gold;
        public long score;

        public override string ToString()
        {
            return string.Format("ID: {0}, NickName: {1}, Sex: {2}, Openid: {3}, UnionId: {4}, Gold: {5}, Score: {6}", id, nickName, sex, openid, unionId, gold, score);
        }
    }
}