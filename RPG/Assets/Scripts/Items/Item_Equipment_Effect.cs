using UnityEngine;
using System.Collections;

namespace Tamana
{
    [System.Serializable]
    public struct Item_Equipment_Effect
    {
        public MainStatus type;
        public int value;

        public override string ToString()
        {
            if(value < 0)
            {
                return $"{type} - {value}";
            }
            return $"{type} + {value}";
        }
    }
}
