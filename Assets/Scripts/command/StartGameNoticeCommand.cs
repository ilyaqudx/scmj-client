using DefaultNamespace;
using DefaultNamespace.component;
using DefaultNamespace.message;
using UnityEngine;

namespace command
{
    public class StartGameNoticeCommand : ResponseCommand
    {
        public override void doCommand(MessageResponse messageResponse)
        {
            var startGameNotice = parseBody<StartGameNotice>(messageResponse);

            Debug.Log(string.Format("{0}-{1} parse body start game notice: {2}", All.currentThreadId(),
                All.formatNow(),
                startGameNotice));

            GlobalComponent.getInstance().addAction(transform =>
            {
                Debug.Log(string.Format(
                    "{0}-{1} execute start game notice callback method before,ServerCallbackEventHandler.startGameNoticeCallback: {2}",
                    All.currentThreadId(), All.formatNow(),
                    ServerCallbackEventHandler.startGameNoticeCallback));
                ServerCallbackEventHandler.startGameNoticeCallback(startGameNotice);
            });
        }
    }
}