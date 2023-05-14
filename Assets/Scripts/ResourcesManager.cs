using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResourcesManager
    {
        private static ResourcesManager INSTANCE = new ResourcesManager();

        private Hashtable resources = new Hashtable();

        public const string DOWN_HAND_CARD = "downHandCard";
        public const string RIGHT_HAND_CARD = "rightHandCard";
        public const string UP_HAND_CARD = "upHandCard";
        public const string LEFT_HAND_CARD = "leftHandCard";

        public static ResourcesManager getInstance()
        {
            return INSTANCE;
        }

        public ResourcesManager()
        {
            // init resource
            resources.Add(DOWN_HAND_CARD, Resources.Load("prefabs/card/down_handcard"));
            resources.Add(RIGHT_HAND_CARD, Resources.Load("prefabs/card/right_handcard"));
            resources.Add(UP_HAND_CARD, Resources.Load("prefabs/card/up_handcard"));
            resources.Add(LEFT_HAND_CARD, Resources.Load("prefabs/card/left_handcard"));
            

            // init sprite
            for (var i = 1; i <= 9; i++)
            {
                var wan = string.Format("wan{0}", i);
                var tong = string.Format("tong{0}", i);
                var suo = string.Format("suo{0}", i);
                resources.Add(wan, Resources.Load(string.Format("ui/功能/麻将牌/牌面/{0}", wan), typeof(Sprite)));
                resources.Add(tong, Resources.Load(string.Format("ui/功能/麻将牌/牌面/{0}", tong), typeof(Sprite)));
                resources.Add(suo, Resources.Load(string.Format("ui/功能/麻将牌/牌面/{0}", suo), typeof(Sprite)));
            }
        }

        public T get<T>(string resourceName)
        {
            return (T)resources[resourceName];
        }
    }
}