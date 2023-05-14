using DefaultNamespace;
using DefaultNamespace.component;
using DefaultNamespace.entity;
using DefaultNamespace.message;
using UnityEngine;

namespace command
{
    public class JoinRoomResponseCommand : ResponseCommand
    {
        public override void doCommand(MessageResponse messageResponse)
        {
            var joinRoomResponse = parseBody<JoinRoomResponse>(messageResponse);

            Global.roomScene = joinRoomResponse.roomScene;

            Debug.Log(string.Format("{0}-{1} parse body join room response: {2}", All.currentThreadId(),
                All.formatNow(),
                joinRoomResponse));

            GlobalComponent.getInstance().addAction(transform =>
            {
                Debug.Log(string.Format(
                    "{0}-{1} execute join room callback method before,ServerCallbackEventHandler.joinRoomCallback: {2}",
                    All.currentThreadId(), All.formatNow(),
                    ServerCallbackEventHandler.joinRoomCallback));
                ServerCallbackEventHandler.joinRoomCallback(joinRoomResponse);
            });
        }
    }
}