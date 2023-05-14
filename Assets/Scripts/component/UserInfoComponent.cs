using entity;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.component
{
    public class UserInfoComponent : MonoBehaviour
    {
        private Text nickName;
        private Text goldNum;
        private Text scoreNum;

        private void Awake()
        {
            nickName = transform.Find("userinfo_node/user_name").GetComponent<Text>();
            goldNum = transform.Find("userinfo_node/gold_node/num").GetComponent<Text>();
            scoreNum = transform.Find("userinfo_node/roomcard_node/num").GetComponent<Text>();
        }

        public void updateUserInfo(Account account)
        {
            nickName.text = account.nickName;
            goldNum.text = account.gold.ToString();
            scoreNum.text = account.score.ToString();
        }
    }
}