using System;
using System.Text;
using command;
using DefaultNamespace.component;
using DefaultNamespace.entity;
using DefaultNamespace.message;
using entity;
using UnityEngine;

namespace DefaultNamespace.command
{
    public class LoginResponseCommand : ResponseCommand
    {
        public override void doCommand(MessageResponse messageResponse)
        {
            var loginResponse = parseBody<LoginResponse>(messageResponse);

            var account = new Account();
            account.id = loginResponse.id;
            account.nickName = loginResponse.nickName;
            account.sex = loginResponse.sex;
            account.openid = loginResponse.openId;
            account.unionId = loginResponse.unionId;
            account.gold = loginResponse.gold;
            account.score = loginResponse.score;
            Global.account = account;

            Debug.Log(string.Format("{0}-{1} parse body loginResponse: {2}", All.currentThreadId(), All.formatNow(),
                loginResponse));

            GlobalComponent.getInstance().addAction(transform =>
            {
                Debug.Log(string.Format(
                    "{0}-{1} execute login callback method before,ServerCallbackEventHandler.loginCallback: {2}",
                    All.currentThreadId(), All.formatNow(),
                    ServerCallbackEventHandler.loginCallback));
                ServerCallbackEventHandler.loginCallback(loginResponse);
            });
        }
    }
}