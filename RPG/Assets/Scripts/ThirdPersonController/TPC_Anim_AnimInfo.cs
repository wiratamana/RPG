using UnityEngine;
using System.Collections.Generic;

namespace Tamana
{
    public class TPC_Anim_AnimInfo<T> where T : struct
    {
        public T TValue { get; set; }
        public string Layer { get; private set; }
        public Dictionary<string, bool> StateDic { private set; get; }

        public TPC_Anim_AnimInfo(T value, string layer)
        {
            TValue = value;
            Layer = layer;
            StateDic = new Dictionary<string, bool>();

            foreach(var t in ClassManager.GetAttributes<TPC_Anim_AttributeBase>())
            {
                StateDic.Add(t.Name, false);
            }
        }
    }
}
