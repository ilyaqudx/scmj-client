using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace entity
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        public int value;
        public string code;
        public bool selected;

        public delegate void EventHandler(GameObject obj);

        /**
         * 当牌第一次被选中触发的事件(牌Y轴向上移动表示被选中,而之前被选中的牌则Y轴向下移动表示被取消)
         */
        public event EventHandler onSelected;

        /**
         * 当牌第二次被选中触发的事件(出牌)
         */
        public event EventHandler onOutCard;

        public void register()
        {
            onSelected += RoomScript.onSelected;
            onOutCard += RoomScript.onOutCard;
        }

        public void unregister()
        {
            onSelected -= RoomScript.onSelected;
            onOutCard -= RoomScript.onOutCard;
        }

        /*public void setOnClickListener()
        {
            var eventTrigger = gameObject.GetComponent<EventTrigger>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener(clickHandCard);
            eventTrigger.triggers.Add(entry);
        }

        public void clickHandCard(BaseEventData eventData)
        {
            Debug.Log(string.Format("{0} is clicked by clickHandCard", name));
        }*/

        public override string ToString()
        {
            return string.Format("{0}, Value: {1}, Name: {2}, Selected: {3}", base.ToString(), value, name, selected);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!selected)
            {
                selected = true;
                gameObject.transform.DOBlendableLocalMoveBy(new Vector3(0, +30, 0), 0.1f);
                onSelected(gameObject);
            }
            else
            {
                //gameObject.transform.DOBlendableLocalMoveBy(new Vector3(0, -30, 0), 0.1f);
                onOutCard(gameObject);
            }

            Debug.Log(string.Format("{0} is clicked by OnPointerClick", code));
        }

        public void cancelSelected()
        {
            this.selected = false;
            gameObject.transform.DOBlendableLocalMoveBy(new Vector3(0, -30, 0), 0.1f);
        }

        private void OnDestroy()
        {
            unregister();
        }
    }
}