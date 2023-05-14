using DefaultNamespace;
using DefaultNamespace.component;
using DefaultNamespace.entity;
using DefaultNamespace.message;
using UnityEngine;

namespace command
{
    public class CreateRoomResponseCommand : ResponseCommand
    {
        public override void doCommand(MessageResponse messageResponse)
        {
            var createRoomResponse = parseBody<CreateRoomResponse>(messageResponse);
            Global.roomScene = createRoomResponse.roomScene;

            Debug.Log(string.Format("{0}-{1} parse body create room response: {2}", All.currentThreadId(),
                All.formatNow(),
                createRoomResponse));

            GlobalComponent.getInstance().addAction(transform =>
            {
                Debug.Log(string.Format(
                    "{0}-{1} execute create room callback method before,ServerCallbackEventHandler.createRoomCallback: {2}",
                    All.currentThreadId(), All.formatNow(),
                    ServerCallbackEventHandler.createRoomCallback));
                ServerCallbackEventHandler.createRoomCallback(createRoomResponse);
            });
        }
    }
}