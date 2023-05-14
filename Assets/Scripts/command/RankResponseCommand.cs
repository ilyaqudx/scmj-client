using DefaultNamespace;
using DefaultNamespace.component;
using DefaultNamespace.message;
using UnityEngine;

namespace command
{
    public class RankResponseCommand : ResponseCommand
    {
        public override void doCommand(MessageResponse messageResponse)
        {
            var rankResponse = parseBody<RankResponse>(messageResponse);

            Debug.Log(string.Format("received rank response rankResponse: {0}", rankResponse));

            GlobalComponent.getInstance().addAction(transform =>
            {
                Debug.Log(string.Format(
                    "{0}-{1} execute rank callback method before,ServerCallbackEventHandler.rankCallback: {2}",
                    All.currentThreadId(), All.formatNow(),
                    ServerCallbackEventHandler.rankCallback));
                ServerCallbackEventHandler.rankCallback(rankResponse);
            });
        }
    }
}