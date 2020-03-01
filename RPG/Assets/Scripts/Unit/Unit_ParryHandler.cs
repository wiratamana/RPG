using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_ParryHandler : MonoBehaviour
    {
        public float ParryTiming { private set; get; }
        public float ChanceToParryTiming { private set; get; }

        public void Parry()
        {
            ParryTiming = Time.time;
            Debug.Log($"Parry!! ParyTiming : {ParryTiming}");
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void ChanceToParry()
        {
            ChanceToParryTiming = Time.time;
            Debug.Log("ChanceToParry");
        }
    }
}
