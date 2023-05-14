using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace.component
{
    public class GlobalComponent : MonoBehaviour
    {
        private static GlobalComponent instance;
        private static object _lock = new object();

        private Transform parentTransform;

        private void Awake()
        {
            Debug.Log(string.Format("GlobalComponent awake method is called"));
            getInstance();
        }

        public static GlobalComponent getInstance()
        {
            Debug.Log(string.Format("GlobalComponent.getInstance step 1"));
            if (null == instance)
            {
                instance = FindObjectOfType(typeof(GlobalComponent)) as GlobalComponent;
                Debug.Log(string.Format("GlobalComponent.getInstance step 2"));
                lock (_lock)
                {
                    Debug.Log(string.Format("GlobalComponent.getInstance step 3"));
                    if (instance == null)
                    {
                        Debug.Log(string.Format("GlobalComponent.getInstance step 4"));
                        var gameObject = new GameObject("GlobalComponent");
                        instance = gameObject.AddComponent<GlobalComponent>();
                    }

                    DontDestroyOnLoad(instance);
                }
            }

            Debug.Log(string.Format("GlobalComponent.getInstance step 5"));
            return instance;
        }

        private Queue<Action<Transform>> actions = new Queue<Action<Transform>>();

        public void changeTransform(Transform transform)
        {
            parentTransform = transform;
        }

        public void addAction(Action<Transform> action)
        {
            if (null != action)
            {
                actions.Enqueue(action);
                Debug.Log(string.Format("{0}-{1} addAction: {2} , current action size: {3}", All.currentThreadId(),
                    All.formatNow(), action, actions.Count));
            }
        }

        private void FixedUpdate()
        {
            if (actions.Count > 0)
            {
                var action = actions.Dequeue();
                Debug.Log(string.Format("{0}-{1} runAction: {2} {3}", All.currentThreadId(), All.formatNow(), action,
                    actions.Count));
                action(parentTransform);
            }
        }
    }
}