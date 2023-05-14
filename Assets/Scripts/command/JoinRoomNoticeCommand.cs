using DefaultNamespace;
using DefaultNamespace.component;
using DefaultNamespace.entity;
using DefaultNamespace.message;
using UnityEngine;

namespace command
{
    public class JoinRoomNoticeCommand : ResponseCommand
    {
        public override void doCommand(MessageResponse messageResponse)
        {
            var joinRoomNotice = parseBody<JoinRoomNotice>(messageResponse);

            Global.roomScene = joinRoomNotice.roomScene;

            Debug.Log(string.Format("{0}-{1} parse body join room notice: {2}", All.currentThreadId(),
                All.formatNow(),
                joinRoomNotice));

            GlobalComponent.getInstance().addAction(transform =>
            {
                Debug.Log(string.Format(
                    "{0}-{1} execute join room notice callback method before,ServerCallbackEventHandler.joinRoomNoticeCallback: {2}",
                    All.currentThreadId(), All.formatNow(),
                    ServerCallbackEventHandler.joinRoomNoticeCallback));
                ServerCallbackEventHandler.joinRoomNoticeCallback(joinRoomNotice);
            });
        }
    }
}