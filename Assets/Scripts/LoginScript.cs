using System;
using command;
using DefaultNamespace.component;
using DefaultNamespace.message;
using DefaultNamespace.socket;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class LoginScript : MonoBehaviour
    {
        public Toggle userAgreement;

        private void Awake()
        {
            GlobalComponent.getInstance().changeTransform(transform);
            Debug.Log(userAgreement);
            ServerCallbackEventHandler.loginCallback += loginCallback;
        }

        public void login()
        {
            Debug.Log("login method is called!");
            if (!userAgreement.isOn)
            {
                Debug.Log("userAgreement is not allow!");
            }
            else
            {
                Debug.Log("do login");
                // build default login info
                var loginRequest = new LoginRequest();
                loginRequest.openId = Guid.NewGuid().ToString();
                loginRequest.unionId = Guid.NewGuid().ToString();
                loginRequest.nickName = "张大全";
                loginRequest.sex = 1;
                loginRequest.clientType = All.CLIEN_TYPE_ANDROID;
                loginRequest.version = All.CURRENT_VERSION;

                RequestSender.sendLoginRequest(loginRequest);
            }
        }

        public void loginCallback(LoginResponse message)
        {
            Debug.Log(string.Format("login script callback is called. param: {0}", message));
            SceneManager.LoadScene("hall");
        }

        private void OnDestroy()
        {
            ServerCallbackEventHandler.loginCallback -= loginCallback;
        }
    }
}