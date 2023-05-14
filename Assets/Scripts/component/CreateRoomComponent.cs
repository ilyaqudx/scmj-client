using System.Collections.Generic;
using System.Linq;
using command;
using DefaultNamespace.message;
using DefaultNamespace.socket;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace.component
{
    public class CreateRoomComponent : MonoBehaviour
    {
        public GameObject createRoomPanel;

        // 局数单选组
        public ToggleGroup roundToggleGroup;

        // 番数单选组
        public ToggleGroup fanToggleGroup;

        // 游戏玩法单选组
        public ToggleGroup modeToggleGroup;

        // 多选组
        public ToggleGroup ruleMultipleSelectToggleGroup;

        // 创建房间按钮
        public Button createRoomBtn;

        // 取消创建房间按钮
        public Button cancelCreateRoomBtn;

        private void Awake()
        {
        }

        /**
         * 显示创建房间面板
         */
        public void showCreateRoomPanel()
        {
            createRoomPanel.SetActive(true);
        }

        /**
         * 点击创建房间: 发送请求
         */
        public void confirmCreateRoom()
        {
            var roundToggles = roundToggleGroup.ActiveToggles();
            var fanToggles = fanToggleGroup.ActiveToggles();
            var modeToggles = modeToggleGroup.ActiveToggles();
            var ruleToggles = ruleMultipleSelectToggleGroup.GetComponentsInChildren<Toggle>();

            var selectedRoundToggle = roundToggles.ToList().Find(toggle => toggle.isOn);
            var selectedFanToggle = fanToggles.ToList().Find(toggle => toggle.isOn);
            var selectedModeToggle = modeToggles.ToList().Find(toggle => toggle.isOn);

            Debug.Log(string.Format("selected round toggle: {0}", selectedRoundToggle.name));
            Debug.Log(string.Format("selected fan toggle: {0}", selectedFanToggle.name));
            Debug.Log(string.Format("selected mode toggle: {0}", selectedModeToggle.name));

            var ruleMap = new Dictionary<string, bool>();
            foreach (var toggle in ruleToggles)
            {
                ruleMap.Add(toggle.name, toggle.isOn);
                Debug.Log(string.Format("selected rule toggle: {0}-{1}", toggle.name, toggle.isOn));
            }


            // 注册创建房间回调函数
            ServerCallbackEventHandler.createRoomCallback += createRoomCallback;

            // 发送创建房间请求
            var createRoomRequest = new CreateRoomRequest();
            createRoomRequest.mode = selectedModeToggle.name;
            createRoomRequest.fan = int.Parse(selectedFanToggle.name);
            createRoomRequest.round = int.Parse(selectedRoundToggle.name);
            createRoomRequest.duanYaojiu = ruleMap["duanyaojiu"];
            createRoomRequest.yaoJiu = ruleMap["yaojiu"];
            createRoomRequest.haiDiLao = ruleMap["haidilao"];
            createRoomRequest.haiDiPao = ruleMap["haidipao"];
            createRoomRequest.huanSanZhang = ruleMap["huansanzhang"];
            createRoomRequest.jinGouDiao = ruleMap["jingoudiao"];

            RequestSender.sendCreateRoomRequest(createRoomRequest);
        }

        public void createRoomCallback(CreateRoomResponse createRoomResponse)
        {
            Debug.Log(string.Format("{0}-{1} create room call back: {2}", All.currentThreadId(), All.formatNow(),
                createRoomResponse.roomScene));

            SceneManager.LoadScene("room");
        }

        public void hideCreateRoomPanel()
        {
            resetToggle();
            createRoomPanel.SetActive(false);
        }

        private void resetToggle()
        {
            var roundToggles = roundToggleGroup.ActiveToggles();
            var fanToggles = fanToggleGroup.ActiveToggles();
            var modeToggles = modeToggleGroup.ActiveToggles();
            var ruleToggles = ruleMultipleSelectToggleGroup.GetComponentsInChildren<Toggle>();
            roundToggles.ToList().ForEach(toggle =>
            {
                Debug.Log(string.Format("round toggle reset , current toggle name: {0}", toggle.name));
                if (toggle.name.Equals("round8"))
                {
                    toggle.isOn = true;
                }
                else
                {
                    toggle.isOn = false;
                }
            });
            fanToggles.ToList().ForEach(toggle =>
            {
                if (toggle.name.Equals("times8"))
                {
                    toggle.isOn = true;
                }
                else
                {
                    toggle.isOn = false;
                }
            });
            modeToggles.ToList().ForEach(toggle =>
            {
                if (toggle.name.Equals("xzdd"))
                {
                    toggle.isOn = true;
                }
                else
                {
                    toggle.isOn = false;
                }
            });
            ruleToggles.ToList().ForEach(toggle => toggle.isOn = false);
        }
    }
}