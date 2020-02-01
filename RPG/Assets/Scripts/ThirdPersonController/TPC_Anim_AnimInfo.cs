using UnityEngine;
using System.Collections.Generic;

namespace Tamana
{
    public class TPC_Anim_AnimInfo<T> where T : struct
    {
        public T value { set; get; }
        public Dictionary<string, bool> StateDic { private set; get; }

        public TPC_Anim_AnimInfo(T value)
        {
            this.value = value;
            StateDic = new Dictionary<string, bool>();

            foreach(var t in ClassManager.AnimAttributes)
            {
                StateDic.Add(t.Name, false);
            }
        }
    }
}
