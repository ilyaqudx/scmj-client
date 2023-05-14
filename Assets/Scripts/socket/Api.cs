namespace DefaultNamespace.socket
{
    public class Api
    {
        public const string HOST = "192.168.1.101";
        public const int PORT = 8889;

        /**
        * 错误响应
        */
        public const int ERROR_RESPONSE = 000000;

        /**
         * 登陆请求
         */
        public const int LOGIN_REQUEST = 200003;

        /**
         * 登陆响应
         */
        public const int LOGIN_RESPONSE = 200004;

        /**
         * 排行榜响应
         */
        public const int RANK_REQUEST = 200005;

        /**
         * 排行榜响应
         */
        public const int RANK_RESPONSE = 200006;

        /**
         * 创建房间请求
         */
        public const int CREATE_ROOM_REQUEST = 200007;

        /**
         * 创建房间响应
         */
        public const int CREATE_ROOM_RESPONSE = 200008;

        /**
          * 玩家已经在房间中
          */
        public const int PLAYER_ALREADY_IN_ROOM = 200010;

        public const int READY_REQUEST = 200011;

        public const int READY_RESPONSE = 200012;
        public const int JOIN_ROOM_REQUEST = 200013;
        public const int JOIN_ROOM_RESPONSE = 200014;
        public const int JOIN_ROOM_NOTICE = 200016;
        public const int START_GAME_NOTICE = 200018;
        public const int EXIT_ROOM_REQUEST = 200025;
        public const int EXIT_ROOM_RESPONSE = 200026;
    }
}