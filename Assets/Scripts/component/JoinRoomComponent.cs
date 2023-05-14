using System;
using command;
using DefaultNamespace.message;
using DefaultNamespace.socket;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.component
{
    public class JoinRoomComponent : MonoBehaviour
    {
        private int roomCodeSize;
        private Text[] inputRoomCodeArray = new Text[6];
        public const string RESET = "reset";
        public const string DEL = "del";

        private void Awake()
        {
            for (var i = 1; i <= 6; i++)
            {
                var text = transform.Find(string.Format("join_room_node/input_room_code/{0}/num", i))
                    .GetComponent<Text>();
                inputRoomCodeArray[i - 1] = text;
            }

            Debug.Log(string.Format("inputRoomCodeArray length: {0}", inputRoomCodeArray.Length));
        }

        public void joinRoomCallback(JoinRoomResponse joinRoomResponse)
        {
            Debug.Log(string.Format("{0}-{1} join room call back: {2}", All.currentThreadId(), All.formatNow(),
                joinRoomResponse));

            SceneManager.LoadScene("room");
        }

        public void clickKeyBoard(string code)
        {
            if (RESET.Equals(code))
            {
                Array.ForEach(inputRoomCodeArray, text => text.text = "");
                roomCodeSize = 0;
            }
            else if (DEL.Equals(code))
            {
                if (roomCodeSize > 0)
                {
                    inputRoomCodeArray[roomCodeSize - 1].text = "";
                    roomCodeSize--;
                }
            }
            else
            {
                if (roomCodeSize >= 6)
                {
                    return;
                }

                inputRoomCodeArray[roomCodeSize++].text = code;
                if (roomCodeSize >= 6)
                {
                    var roomCode = "";
                    Array.ForEach(inputRoomCodeArray, text => roomCode += text.text);
                    var joinRoomRequest = new JoinRoomRequest();
                    joinRoomRequest.roomCode = int.Parse(roomCode);

                    Debug.Log(string.Format("parsed join room code is {0}", roomCode));

                    ServerCallbackEventHandler.joinRoomCallback += joinRoomCallback;

                    RequestSender.sendJoinRoomRequest(joinRoomRequest);
                }
            }
        }

        public void show()
        {
            transform.Find("join_room_node").gameObject.SetActive(true);
        }

        public void close()
        {
            roomCodeSize = 0;
            Array.ForEach(inputRoomCodeArray, text => text.text = "");
            transform.Find("join_room_node").gameObject.SetActive(false);
        }
    }
}