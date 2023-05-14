using System;
using System.Text;
using System.Threading;

namespace DefaultNamespace
{
    public class All
    {
        // 当前客户端版本
        public static readonly int CURRENT_VERSION = 100;

        // android客户端
        public static readonly int CLIEN_TYPE_ANDROID = 1;

        // ios客户端
        public static readonly int CLIEN_TYPE_IOS = 2;

        // 麻将玩法：血战到底
        public const string MODE_XZDD = "xzdd";

        // 麻将玩法：血流成河
        public const string MODE_XLCH = "xlch";

        public static byte[] getBytes(long value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return bytes;
        }

        public static byte[] getBytes(int value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return bytes;
        }

        public static byte[] getBytes(short value)
        {
            var bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return bytes;
        }

        public static long toLong(byte[] bytes)
        {
            Array.Reverse(bytes);
            return BitConverter.ToInt64(bytes, 0);
        }

        public static int toInt(byte[] bytes)
        {
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static short toShort(byte[] bytes)
        {
            Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }

        public static byte[] getBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public static string toString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static int currentThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }

        public static string currentThreadName()
        {
            return Thread.CurrentThread.Name;
        }

        public static string formatNow()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string getCardName(int value)
        {
            if (value < 10)
            {
                return string.Format("tong{0}", value);
            }
            else if (value < 20)
            {
                return string.Format("suo{0}", value - 10);
            }
            else if (value < 30)
            {
                return string.Format("wan{0}", value - 20);
            }

            throw new ArgumentException(string.Format("unknown card value:", value));
        }

        public static class Room
        {
            /**
             * 下家手牌区域X轴偏移量
             */
            public static float DOWN_HAND_CARD_X = -428;

            /**
             *  下家手牌区域Y轴偏移量
             */
            public static float DOWN_HAND_CARD_Y = -285;

            /**
             *下家手牌长度
            */
            public static float DOWN_HAND_CARD_LEN = 75;

            /*
             * 右家手牌区域X轴偏移量
             */
            public static float RIGHT_HAND_CARD_X = 0;

            /*
             * 右家手牌区域Y轴偏移量
             */
            public static float RIGHT_HAND_CARD_Y = 210;

            /*
             * 右家手牌长度
             */
            public static float RIGHT_HAND_CARD_LEN = 27;
        }
    }
}