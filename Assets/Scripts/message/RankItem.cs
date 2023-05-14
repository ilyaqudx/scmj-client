using System;

namespace DefaultNamespace.message
{
    [Serializable]
    public class RankItem
    {
        public string name;
        public long score;
        public string headUrl;

        public override string ToString()
        {
            return string.Format("Name: {0}, Score: {1}, HeadUrl: {2}", name, score, headUrl);
        }
    }
}