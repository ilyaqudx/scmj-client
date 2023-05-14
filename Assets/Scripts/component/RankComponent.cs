using System.Collections.Generic;
using DefaultNamespace.message;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.component
{
    public class RankComponent : MonoBehaviour
    {
        // public ScrollRect friendRankNode; 通过unity编辑器注入进来,从房间退出到大厅该组件friendRankNode.content为空了,现在采用代码形式获取看能不能解决,原因找到了没有在OnDestory里面注销事件回调
        // public ScrollRect scrollRect;
        public RectTransform content;

        private void Awake()
        {
            Debug.Log(string.Format("RankComponent awake is running: {0}-{1} {2}", All.currentThreadId(),
                All.formatNow(), content));
        }

        public void setRankList(List<RankItem> rankDataList)
        {
            var itemCount = rankDataList.Count;
            // 获取scrollView content
            content.sizeDelta = new Vector2(content.sizeDelta.x, itemCount * 76);
            // 获取prefab
            var rankListItemPrefab = Resources.Load("prefabs/rank_list_item");
            for (int i = 0; i < itemCount; i++)
            {
                // 实例化prefab,并将content设为父组件
                var rankListItem = Instantiate(rankListItemPrefab, content.transform) as GameObject;

                // 设置item位置
                var rankListItemPosition = rankListItem.transform.localPosition;
                rankListItem.transform.localPosition = new Vector3(rankListItemPosition.x,
                    138 - i * 76
                    , rankListItemPosition.z);

                // 设置item head

                // 设置item name
                var nameText = rankListItem.transform.Find("name").GetComponent<Text>();
                nameText.text = rankDataList[i].name;
                // 设置item score
                var goldNumText = rankListItem.transform.Find("gold_node/num").GetComponent<Text>();
                goldNumText.text = rankDataList[i].score.ToString();
            }
        }

        private void OnDestroy()
        {
            Debug.Log(string.Format("RankComponent is detstory"));
        }
    }
}