using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PocketFighters
{
    public class PocketIA
    {

        public PocketIA() { }

        public List<JokenPo> GetJoKenPo()
        {
            List<JokenPo> list = new List<JokenPo>(3);

            JokenPo temPo = (JokenPo)Random.Range(0, 3);

            list.Add(temPo);

            return list;
        }
    }
}
