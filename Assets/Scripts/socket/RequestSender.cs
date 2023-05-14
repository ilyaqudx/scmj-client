using System;
using System.Text;
using DefaultNamespace.entity;
using DefaultNamespace.message;
using UnityEngine;
using Object = System.Object;

namespace DefaultNamespace.socket
{
    public class RequestSender
    {
        public static void sendLoginRequest(LoginRequest loginRequest)
        {
            MessagePacket messagePacket = createMessagePacket(Api.LOGIN_REQUEST, loginRequest);
            SocketService.getInstance().send(messagePacket);
        }

        public static void sendRankRequest()
        {
            // 去请求排行榜数据
            var messagePacket = createMessagePacket(Api.RANK_REQUEST, null);

            SocketService.getInstance().send(messagePacket);
        }

        public static void sendCreateRoomRequest(CreateRoomRequest createRoomRequest)
        {
            MessagePacket messagePacket = createMessagePacket(Api.CREATE_ROOM_REQUEST, createRoomRequest);
            SocketService.getInstance().send(messagePacket);
        }

        public static void sendReadyRequest(ReadyRequest readyRequest)
        {
            MessagePacket messagePacket = createMessagePacket(Api.READY_REQUEST, readyRequest);
            SocketService.getInstance().send(messagePacket);
        }

        public static void sendJoinRoomRequest(JoinRoomRequest joinRoomRequest)
        {
            MessagePacket messagePacket = createMessagePacket(Api.JOIN_ROOM_REQUEST, joinRoomRequest);
            SocketService.getInstance().send(messagePacket);
        }

        public static void sendExitRoomRequest(ExitRoomRequest exitRoomRequest)
        {
            MessagePacket messagePacket = createMessagePacket(Api.EXIT_ROOM_REQUEST, exitRoomRequest);
            SocketService.getInstance().send(messagePacket);
        }

        private static MessagePacket createMessagePacket(int cmd, Object data)
        {
            MessagePacket messagePacket = new MessagePacket();
            messagePacket.cmd = cmd;
            messagePacket.accountId = Global.account == null ? 0 : Global.account.id;
            messagePacket.version = All.CURRENT_VERSION;
            messagePacket.clientType = All.CLIEN_TYPE_ANDROID;
            if (data != null)
            {
                var bytes = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
                var base64String = Convert.ToBase64String(bytes);
                messagePacket.data = base64String;
            }

            return messagePacket;
        }

        private static byte[] getBytes(Object obj)
        {
            return Encoding.UTF8.GetBytes(JsonUtility.ToJson(obj));
        }
    }
}