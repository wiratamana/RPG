using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_DamageReceiveHandler : MonoBehaviour
    {
        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        public EventManager<Unit_Status_DamageData> OnReceivedDamageEvent { get; } = new EventManager<Unit_Status_DamageData>();

        public void DamageReceiver(Unit_Status_DamageData damage)
        {
            Debug.Log($"'{name}' received '{damage.damagePoint}' damage");

            var parryValue = CombatHandler.ParryHandler.ParryTiming > damage.parryTiming &&
                CombatHandler.ParryHandler.ParryTiming < damage.damageTiming;

            if (parryValue == true)
            {
                Debug.Log("Parry success");
                CombatHandler.UnitAnimator.Play("Sword1h_Parry_T");
            }
            else
            {
                Debug.Log("Parry failed");
                OnReceivedDamageEvent.Invoke(damage);

                if(CombatHandler.Unit.Status.IsDead == true)
                {
                    CombatHandler.UnitAnimator.Play("Sword1h_Death_1");
                }
                else
                {
                    PlayHitAnimation(damage.hitsAnimation);
                }                
            }

            CombatHandler.Unit.RotationHandler.RotateToward(damage.damageSenderPosition, 20.0f);
        }

        private void PlayHitAnimation(int[] statesName)
        {
            var animationHitData = CombatHandler.UnitAnimator.AnimStatus.GetAnimationHitData(statesName); ;

            if(CombatHandler.UnitAnimator.Params.IsTakingDamage == false)
            {
                CombatHandler.UnitAnimator.OnHitAnimationStarted.Invoke();
                CombatHandler.UnitAnimator.Params.IsTakingDamage = true;
            }            

            CombatHandler.UnitAnimator.Params.AnimHit = animationHitData.paramValue;
            CombatHandler.UnitAnimator.Play(animationHitData.stateName);
        }

        public void SetAnimationHitStatus<Enum>(Enum stateValue)
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
