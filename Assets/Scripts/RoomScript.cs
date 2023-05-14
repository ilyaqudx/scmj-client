using System;
using command;
using DefaultNamespace.component;
using DefaultNamespace.entity;
using DefaultNamespace.enums;
using DefaultNamespace.factory;
using DefaultNamespace.message;
using DefaultNamespace.socket;
using DefaultNamespace.ui;
using entity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RoomScript : MonoBehaviour
    {
        private Transform[] playerHeadInfoArray = new Transform[4];
        public Button readyBtn;
        protected static RoomScript instance;

        public RoomScript()
        {
            instance = this;
        }


        private void Awake()
        {
            Debug.Log(string.Format("room script is awake"));
            GlobalComponent.getInstance().changeTransform(transform);
            registerEventCallback();

            playerHeadInfoArray[0] = transform.Find("user_info_list/user_info_node_down");
            playerHeadInfoArray[1] = transform.Find("user_info_list/user_info_node_right");
            playerHeadInfoArray[2] = transform.Find("user_info_list/user_info_node_top");
            playerHeadInfoArray[3] = transform.Find("user_info_list/user_info_node_left");
            this.refreshRoomScene();
        }

        private void registerEventCallback()
        {
            ServerCallbackEventHandler.readyCallback += readyCallback;
            ServerCallbackEventHandler.joinRoomNoticeCallback += joinRoomNoticeCallback;
            ServerCallbackEventHandler.startGameNoticeCallback += startGameNoticeCallback;
            ServerCallbackEventHandler.exitRoomCallback += exitRoomCallback;
        }

        private void unregisterEventCallback()
        {
            ServerCallbackEventHandler.readyCallback -= readyCallback;
            ServerCallbackEventHandler.joinRoomNoticeCallback -= joinRoomNoticeCallback;
            ServerCallbackEventHandler.startGameNoticeCallback -= startGameNoticeCallback;
            ServerCallbackEventHandler.exitRoomCallback -= exitRoomCallback;
        }

        public void onReadyButtonClicked()
        {
            var newHandCard = CardFactory.newDownHandCard(15, transform);
            // check room state
            if (!Global.roomScene.roomState.Equals(RoomState.WAIT.ToString()))
            {
                Toast.show(transform, "当前状态不能准备", 2f);
                return;
            }

            if (Global.roomScene.owner == Global.account.id)
            {
                // check other player is all ready
                var notReadyPlayer = Global.roomScene.players.Find(p => p.id != Global.account.id && !p.ready);
                if (Global.roomScene.players.Count < 4 || null != notReadyPlayer)
                {
                    Debug.Log(string.Format("can't ready because has other player not ready"));
                    Toast.show(transform, "所有玩家准备好才能开始游戏", 2f);
                    return;
                }
            }

            var readyRequest = new ReadyRequest();
            readyRequest.roomCode = Global.roomScene.code;
            readyRequest.ready = true;
            RequestSender.sendReadyRequest(readyRequest);
        }

        public void readyCallback(ReadyResponse readyResponse)
        {
            Debug.Log(string.Format("{0}-{1} on ready callback: {2}", All.currentThreadId(), All.formatNow(),
                readyResponse));

            var isSelf = readyResponse.playerId == Global.account.id;
            if (readyResponse.ready)
            {
                if (isSelf)
                {
                    // [22:55:05:222] MissingReferenceException: The object of type 'Button' has been destroyed but you are still trying to access it.
                    // 进入房间后退出,然后再进入点击准备出现上述异常信息
                    readyBtn.gameObject.SetActive(false);
                }

                var player = Global.roomScene.players.Find(p => p.id == readyResponse.playerId);
                player.ready = readyResponse.ready;
                var showSeat = getPlayerShowSeat(player.seat);
                playerHeadInfoArray[showSeat].Find("ready").gameObject.SetActive(true);
            }
        }

        private void refreshRoomScene()
        {
            // 显示房间号
            var roomNumber = transform.Find("room_number/number").GetComponent<Text>();
            roomNumber.text = string.Format("房间：{0}", Global.roomScene.code);

            // 显示游戏模型
            var gameMode = Global.roomScene.mode;
            if (All.MODE_XZDD.Equals(gameMode))
            {
                transform.Find("room_mode_name_xzdd").GetComponent<Image>().gameObject.SetActive(true);
            }
            else if (All.MODE_XLCH.Equals(gameMode))
            {
                transform.Find("room_mode_name_xlch").GetComponent<Image>().gameObject.SetActive(true);
            }

            // 显示下家玩家信息
            Global.roomScene.players.ForEach(p =>
            {
                var showSeat = getPlayerShowSeat(p.seat);
                playerHeadInfoArray[showSeat].Find("name").GetComponent<Text>().text = p.nickName;
                playerHeadInfoArray[showSeat].Find("gold").GetComponent<Text>().text = p.gold.ToString();
                if (p.ready)
                {
                    playerHeadInfoArray[showSeat].Find("ready").gameObject.SetActive(true);
                }
            });

            // 显示准备按钮
            var self = getSelf();
            if (!self.ready)
            {
                //readyBtn.onClick.AddListener(onReadyButtonClicked); 手动添加点击事件
                readyBtn.gameObject.SetActive(true);
            }
        }

        public void joinRoomNoticeCallback(JoinRoomNotice joinRoomNotice)
        {
            //Toast.show(transform, string.Format("{0} join room", joinRoomNotice.playerId), 2);
            this.refreshRoomScene();
        }

        public void startGameNoticeCallback(StartGameNotice startGameNotice)
        {
            var roomScene = Global.roomScene;
            roomScene.roomState = startGameNotice.roomState;
            roomScene.ingState = startGameNotice.ingState;
            roomScene.banker = startGameNotice.banker;
            roomScene.currentRound = startGameNotice.currentRound;
            var player = getSelf();
            var handCard = startGameNotice.handCard;

            // 循环渲染玩家手牌
            for (var i = 0; i < startGameNotice.handCardCount.Count; i++)
            {
                var playerDirection = getPlayerDirection(i);
                for (int j = 0; j < startGameNotice.handCardCount[i]; j++)
                {
                    if (playerDirection == SeatDirection.DOWN)
                    {
                        var cardValue = handCard[j];
                        var gameObject = CardFactory.newDownHandCard(cardValue, transform);
                        // 从左向右渲染
                        gameObject.transform.localPosition =
                            new Vector2(All.Room.DOWN_HAND_CARD_X + j * All.Room.DOWN_HAND_CARD_LEN,
                                All.Room.DOWN_HAND_CARD_Y);
                        player.handCard.Add(gameObject);
                    }
                    else if (playerDirection == SeatDirection.RIGHT)
                    {
                        var gameObject = CardFactory.newRightHandCard(transform);
                        // 从上向下渲染
                        gameObject.transform.localPosition = new Vector2(All.Room.RIGHT_HAND_CARD_X,
                            All.Room.RIGHT_HAND_CARD_Y - j * All.Room.RIGHT_HAND_CARD_LEN);
                    }
                    else if (playerDirection == SeatDirection.UP)
                    {
                        var gameObject = CardFactory.newUpHandCard(transform);
                        // 从左向右渲染
                        gameObject.transform.localPosition = new Vector2(All.Room.RIGHT_HAND_CARD_X,
                            All.Room.RIGHT_HAND_CARD_Y - j * All.Room.RIGHT_HAND_CARD_LEN);
                    }
                    else if (playerDirection == SeatDirection.LEFT)
                    {
                        var gameObject = CardFactory.newLeftHandCard(transform);
                        // 从上向下渲染
                        gameObject.transform.localPosition = new Vector2(All.Room.RIGHT_HAND_CARD_X,
                            All.Room.RIGHT_HAND_CARD_Y - j * All.Room.RIGHT_HAND_CARD_LEN);
                    }
                }
            }
        }

        private Player getSelf()
        {
            return Global.roomScene.players[getSelfSeat()];
        }

        private int getSelfSeat()
        {
            return Global.roomScene.players.Find(p => p.id.Equals(Global.account.id)).seat;
        }

        /*
         * 获取玩家在房间的方位
         */
        public SeatDirection getPlayerDirection(int seat)
        {
            var playerShowSeat = getPlayerShowSeat(seat);
            return (SeatDirection)Enum.ToObject(typeof(SeatDirection), playerShowSeat);
        }

        /**
         * 获取玩家显示的座位,本家为下家
         * 0 - down
         * 1 - right
         * 2 - up
         * 3 - left
         */
        private int getPlayerShowSeat(int seat)
        {
            var selfSeat = getSelfSeat();
            if (seat == selfSeat)
            {
                return 0;
            }
            else
            {
                var showSeat = seat - selfSeat;
                return showSeat < 0 ? showSeat + 4 : showSeat;
            }
        }

        public void exitRoom()
        {
            ExitRoomRequest exitRoomRequest = new ExitRoomRequest();
            exitRoomRequest.roomCode = Global.roomScene.code;
            RequestSender.sendExitRoomRequest(exitRoomRequest);
        }

        public void exitRoomCallback(ExitRoomResponse exitRoomResponse)
        {
            if (Global.account.id == exitRoomResponse.playerId)
            {
                Global.roomScene = null;
                SceneManager.LoadScene("hall");
            }
            else
            {
                var playerShowSeat = getPlayerShowSeat(exitRoomResponse.seat);
                var playerHeadInfo = playerHeadInfoArray[playerShowSeat];
                playerHeadInfo.Find("name").GetComponent<Text>().text = "待加入";
                playerHeadInfo.Find("gold").GetComponent<Text>().text = "0";
                playerHeadInfo.Find("ready").gameObject.SetActive(false);
                Global.roomScene.players.RemoveAt(exitRoomResponse.seat);
            }
        }

        public static void onSelected(GameObject gameObject)
        {
            Debug.Log(string.Format("{0} is selected by RoomScript", gameObject.GetComponent<Card>().code));
            var handCardList = instance.getSelf().handCard;
            handCardList.ForEach(go =>
            {
                var card = go.GetComponent<Card>();
                if (go != gameObject && card.selected)
                {
                    card.cancelSelected();
                }
            });
        }

        public static void onOutCard(GameObject gameObject)
        {
            Debug.Log(string.Format("{0} is out card by RoomScript", gameObject.GetComponent<Card>().code));
        }

        private void OnDestroy()
        {
            unregisterEventCallback();
            Debug.Log(string.Format("room script is being destroy"));
        }
    }
}