using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_ParryHandler : MonoBehaviour
    {
        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        public float ParryTiming { private set; get; }
        public float ChanceToParryTiming { private set; get; }
        public bool IsAbleToParry => parryDelayCoroutine == null;

        private readonly WaitForSeconds secondsToWaitBeforeAbleToParryAgain = new WaitForSeconds(0.5f);
        private Coroutine parryDelayCoroutine;

        private void Awake()
        {
            CombatHandler.UnitAnimator.OnHitAnimationStarted.AddListener(MakePlayerUnableToParry);
            CombatHandler.UnitAnimator.OnHitAnimationFinished.AddListener(MakePlayerAbleToParryAgain);
        }

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

        private void MakePlayerUnableToParry()
        {
            if (CombatHandler.UnitAnimator.Params.IsInCombatState == false)
            {
                return;
            }

            InputEvent.Instance.Event_Parry.RemoveListener(Parry);
        }

        private void MakePlayerAbleToParryAgain()
        {
            if (CombatHandler.UnitAnimator.Params.IsInCombatState == false)
            {
                return;
            }

            InputEvent.Instance.Event_Parry.AddListener(Parry);
        }
    }
}
