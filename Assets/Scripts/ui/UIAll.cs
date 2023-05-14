using UnityEngine;

namespace DefaultNamespace.ui
{
    public class UIAll : MonoBehaviour
    {
        public static GameObject newInstance(Object clazz, Transform parent)
        {
            return Instantiate(clazz, parent) as GameObject;
        }
    }
}