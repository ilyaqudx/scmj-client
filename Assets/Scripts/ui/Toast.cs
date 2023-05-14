using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.ui
{
    public class Toast : MonoBehaviour
    {
        public static void show(Transform parent, string text, float duration)
        {
            var toastPrefab = Resources.Load("prefabs/toast");
            var toastGameObject = UIAll.newInstance(toastPrefab, parent);
            var imageComponent = toastGameObject.transform.Find("Image").GetComponent<Image>();
            var textComponent = toastGameObject.transform.Find("Text").GetComponent<Text>();
            textComponent.text = text;
            toastGameObject.SetActive(true);

            imageComponent.DOFade(100, duration).OnComplete(() => { Destroy(toastGameObject); });
        }
    }
}