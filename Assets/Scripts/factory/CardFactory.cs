using DefaultNamespace.ui;
using entity;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.factory
{
    public class CardFactory
    {
        public static GameObject newDownHandCard(int value, Transform parent)
        {
            // 加载prefab
            var downHandCardObject = ResourcesManager.getInstance().get<Object>(ResourcesManager.DOWN_HAND_CARD);
            // 实例化prefab为gameObject
            var gameObject = UIAll.newInstance(downHandCardObject, parent);
            // 加载具体牌sprite
            var sprite = ResourcesManager.getInstance().get<Sprite>(All.getCardName(value));
            // 设置gameObject的具体值sprite
            gameObject.transform.Find("value").GetComponent<Image>().sprite = sprite;
            // 获取gameObject组件Card
            var card = gameObject.GetComponent<Card>();
            // 设置card组件相关属性和事件
            card.code = All.getCardName(value);
            card.value = value;
            card.selected = false;
            card.register();
            Debug.Log(string.Format("create hand card: {0}", sprite.name));
            return gameObject;
        }

        public static GameObject newRightHandCard(Transform parent)
        {
            var rightHandCardObject = ResourcesManager.getInstance().get<Object>(ResourcesManager.RIGHT_HAND_CARD);
            var gameObject = UIAll.newInstance(rightHandCardObject, parent);
            return gameObject;
        }

        public static GameObject newUpHandCard(Transform parent)
        {
            var rightHandCardObject = ResourcesManager.getInstance().get<Object>(ResourcesManager.UP_HAND_CARD);
            var gameObject = UIAll.newInstance(rightHandCardObject, parent);
            return gameObject;
        }

        public static GameObject newLeftHandCard(Transform parent)
        {
            var rightHandCardObject = ResourcesManager.getInstance().get<Object>(ResourcesManager.LEFT_HAND_CARD);
            var gameObject = UIAll.newInstance(rightHandCardObject, parent);
            return gameObject;
        }
    }
}