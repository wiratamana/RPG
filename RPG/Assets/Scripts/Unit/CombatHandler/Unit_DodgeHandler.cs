using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_DodgeHandler : MonoBehaviour
    {
        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        public void Dodge()
        {
            PlayDodgeAnimation();
        }

        private void PlayDodgeAnimation()
        {
            var animationDodgeData = CombatHandler.UnitAnimator.AnimStatus.GetAnimationDodgeData();

            //CombatHandler.UnitAnimator.OnHitAnimationStarted.Invoke();
            //CombatHandler.UnitAnimator.Params.IsTakingDamage = true;
            CombatHandler.UnitAnimator.Params.AnimDodge = animationDodgeData.paramValue;
            CombatHandler.UnitAnimator.Play(animationDodgeData.stateName);
        }

        public void SetDodgeAnimationStatus<Enum>(Enum stateValue)
            where Enum : System.Enum
        {
            CombatHandler.UnitAnimator.AnimStatus.SetToFalse(stateValue);

            bool isTakingDamage = false;
            foreach (var hit in CombatHandler.UnitAnimator.AnimStatus.HitDic_1H)
            {
                if (hit.Value == true)
                {
                    isTakingDamage = true;
                    break;
                }
            }

            if (isTakingDamage == false)
            {
                CombatHandler.UnitAnimator.OnHitAnimationFinished.Invoke();
                CombatHandler.UnitAnimator.Params.AnimHit = 0;
            }

            CombatHandler.UnitAnimator.Params.IsTakingDamage = isTakingDamage;
        }
    }
}
