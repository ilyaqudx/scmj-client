using DefaultNamespace.component;
using DefaultNamespace.message;
using DefaultNamespace.ui;
using UnityEngine;

namespace command
{
    public class ErrorResponseCommand : ResponseCommand
    {
        public override void doCommand(MessageResponse messageResponse)
        {
            var errorResponse = parseBody<ErrorResponse>(messageResponse);

            GlobalComponent.getInstance().addAction(transform =>
            {
                Debug.Log(string.Format("error response command show toast before"));
                Toast.show(transform, errorResponse.error, 2);
                Debug.Log(string.Format("error response command show toast after"));
            });
        }
    }
}