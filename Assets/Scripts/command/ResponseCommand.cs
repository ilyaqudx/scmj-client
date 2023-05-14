using System;
using System.Text;
using DefaultNamespace.message;
using UnityEngine;

namespace command
{
    public abstract class ResponseCommand
    {
        public abstract void doCommand(MessageResponse messageResponse);

        public T parseBody<T>(MessageResponse messageResponse)
        {
            var bytes = Convert.FromBase64String(messageResponse.data);
            var json = Encoding.UTF8.GetString(bytes);
            return JsonUtility.FromJson<T>(json);
        }
    }
}