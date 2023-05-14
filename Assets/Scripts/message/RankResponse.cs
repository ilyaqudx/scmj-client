using System;
using System.Collections.Generic;

namespace DefaultNamespace.message
{
    [Serializable]
    public class RankResponse
    {
        public List<RankItem> rankItemList;

        public RankResponse()
        {
        }

        public override string ToString()
        {
            return string.Format("RankItems: {0}", rankItemList.Count);
        }
    }
}