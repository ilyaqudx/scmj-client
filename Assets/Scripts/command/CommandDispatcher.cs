using System.Collections.Generic;
using command;
using DefaultNamespace.command;
using DefaultNamespace.message;
using UnityEngine;

namespace DefaultNamespace.socket
{
    public class CommandDispatcher
    {
        private static Dictionary<int, ResponseCommand> commandMap = new Dictionary<int, ResponseCommand>();

        static CommandDispatcher()
        {
            commandMap[Api.ERROR_RESPONSE] = new ErrorResponseCommand();
            commandMap[Api.LOGIN_RESPONSE] = new LoginResponseCommand();
            commandMap[Api.RANK_RESPONSE] = new RankResponseCommand();
            commandMap[Api.CREATE_ROOM_RESPONSE] = new CreateRoomResponseCommand();
            commandMap[Api.READY_RESPONSE] = new ReadyResponseCommand();
            commandMap[Api.JOIN_ROOM_RESPONSE] = new JoinRoomResponseCommand();
            commandMap[Api.JOIN_ROOM_NOTICE] = new JoinRoomNoticeCommand();
            commandMap[Api.START_GAME_NOTICE] = new StartGameNoticeCommand();
            commandMap[Api.EXIT_ROOM_RESPONSE] = new ExitRoomResponseCommand();
        }

        public static void dispatch(MessageResponse messageResponse)
        {
            var command = (ResponseCommand)commandMap[messageResponse.cmd];
            if (null != command)
            {
                command.doCommand(messageResponse);
            }
            else
            {
                Debug.Log(string.Format("can not found command: {0}", messageResponse.cmd));
            }
        }
    }
}