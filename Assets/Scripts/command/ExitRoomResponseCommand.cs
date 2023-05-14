using DefaultNamespace;
using DefaultNamespace.component;
using DefaultNamespace.message;
using UnityEngine;

namespace command
{
    public class ExitRoomResponseCommand : ResponseCommand
    {
        public override void doCommand(MessageResponse messageResponse)
        {
            var exitRoomResponse = parseBody<ExitRoomResponse>(messageResponse);

            Debug.Log(string.Format("{0}-{1} parse body exit room response: {2}", All.currentThreadId(),
                All.formatNow(),
                exitRoomResponse));

            GlobalComponent.getInstance().addAction(transform =>
            {
                Debug.Log(string.Format(
                    "{0}-{1} execute exit room response callback method before,ServerCallbackEventHandler.exitRoomResponseCallback: {2}",
                    All.currentThreadId(), All.formatNow(),
                    ServerCallbackEventHandler.exitRoomCallback));
                ServerCallbackEventHandler.exitRoomCallback(exitRoomResponse);
            });
        }
    }
}