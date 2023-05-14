using DefaultNamespace.socket;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameApplication : MonoBehaviour
    {
        private void Awake()
        {
            // SocketService init
            SocketService.getInstance().connect();
        }

        private void Start()
        {
        }

        private void FixedUpdate()
        {
        }
    }
}