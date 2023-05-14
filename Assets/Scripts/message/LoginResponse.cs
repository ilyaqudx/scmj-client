using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class LoginResponse
    {
        public long id;
        public string openId;
        public string unionId;
        public string nickName;
        public int sex;
        public long gold;
        public long score;
        public int clientType;
        public int version;
        public DateTime createTime;
        public DateTime updateTime;

        public override string ToString()
        {
            return string.Format("ID: {0}, OpenId: {1}, UnionId: {2}, NickName: {3}, Sex: {4}, Gold: {5}, Score: {6}, ClientType: {7}, Version: {8}, CreateTime: {9}, UpdateTime: {10}", id, openId, unionId, nickName, sex, gold, score, clientType, version, createTime, updateTime);
        }
    }
}