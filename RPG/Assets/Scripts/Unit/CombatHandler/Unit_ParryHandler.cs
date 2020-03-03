using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_ParryHandler : MonoBehaviour
    {
        public float ParryTiming { private set; get; }
        public float ChanceToParryTiming { private set; get; }
        public bool IsAbleToParry => parryDelayCoroutine == null;

        private readonly WaitForSeconds secondsToWaitBeforeAbleToParryAgain = new WaitForSeconds(0.5f);
        private Coroutine parryDelayCoroutine;

        public void Parry()
        {
            if(IsAbleToParry == false)
            {
                return;
            }

            ParryTiming = Time.time;
            Debug.Log($"Parry!! ParyTiming : {ParryTiming}");

            parryDelayCoroutine = StartCoroutine(ParryDelay());
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void ChanceToParry()
        {
            ChanceToParryTiming = Time.time;
            Debug.Log("ChanceToParry");
        }

        private IEnumerator ParryDelay()
        {
            yield return secondsToWaitBeforeAbleToParryAgain;
            parryDelayCoroutine = null;
        }
    }
}
