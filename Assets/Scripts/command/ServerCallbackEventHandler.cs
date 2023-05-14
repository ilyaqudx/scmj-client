using DefaultNamespace.message;

namespace command
{
    public class ServerCallbackEventHandler
    {
        public delegate void ServerCallbackEvent<T>(T message);

        // 登陆回调
        public static ServerCallbackEvent<LoginResponse> loginCallback;

        // 排行榜回调
        public static ServerCallbackEvent<RankResponse> rankCallback;

        // 创建房间回调
        public static ServerCallbackEvent<CreateRoomResponse> createRoomCallback;

        // 准备返回
        public static ServerCallbackEvent<ReadyResponse> readyCallback;

        // 加入房间返回
        public static ServerCallbackEvent<JoinRoomResponse> joinRoomCallback;

        // 有人加入房间通知
        public static ServerCallbackEvent<JoinRoomNotice> joinRoomNoticeCallback;

        // 开始游戏
        public static ServerCallbackEvent<StartGameNotice> startGameNoticeCallback;

        public static ServerCallbackEvent<ExitRoomResponse> exitRoomCallback;
    }
}