using System;
using command;
using DefaultNamespace.component;
using DefaultNamespace.entity;
using DefaultNamespace.message;
using DefaultNamespace.socket;
using UnityEngine;

namespace DefaultNamespace
{
    public class HallScript : MonoBehaviour
    {
        public RankComponent rankComponent;
        public UserInfoComponent userInfoComponent;

        private void Awake()
        {
            GlobalComponent.getInstance().changeTransform(transform);
        }

        private void Start()
        {
            // update user info
            userInfoComponent.updateUserInfo(Global.account);
            // request rank data
            ServerCallbackEventHandler.rankCallback += rankCallback;
            RequestSender.sendRankRequest();
        }

        public void rankCallback(RankResponse rankResponse)
        {
            // update rank list
            rankComponent.setRankList(rankResponse.rankItemList);
        }

        private void OnDestroy()
        {
            ServerCallbackEventHandler.rankCallback -= rankCallback;
        }
    }
}