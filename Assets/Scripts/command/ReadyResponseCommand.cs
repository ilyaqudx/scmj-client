using DefaultNamespace;
using DefaultNamespace.component;
using DefaultNamespace.message;
using UnityEngine;

namespace command
{
    public class ReadyResponseCommand : ResponseCommand
    {
        public override void doCommand(MessageResponse messageResponse)
        {
            var readyResponse = parseBody<ReadyResponse>(messageResponse);

            Debug.Log(string.Format("{0}-{1} parse body game ready response: {2}", All.currentThreadId(),
                All.formatNow(),
                readyResponse));

            GlobalComponent.getInstance().addAction(transform =>
            {
                Debug.Log(string.Format(
                    "{0}-{1} execute ready callback method before,ServerCallbackEventHandler.readyCallback: {2}",
                    All.currentThreadId(), All.formatNow(),
                    ServerCallbackEventHandler.readyCallback));
                ServerCallbackEventHandler.readyCallback(readyResponse);
            });
        }
    }
}