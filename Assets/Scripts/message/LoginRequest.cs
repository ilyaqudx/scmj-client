using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class LoginRequest
    {
        public string openId;
    
        public string unionId;

        public string nickName;

        public int sex;

        public int clientType;

        public int version;
    }
}