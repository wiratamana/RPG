using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_DamageReceiveHandler : MonoBehaviour
    {
        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        public EventManager<Status_DamageData> OnReceivedDamageEvent { get; } = new EventManager<Status_DamageData>();

        public void DamageReceiver(Status_DamageData damage)
        {
            Debug.Log($"'{name}' received '{damage.damagePoint}' damage");
            OnReceivedDamageEvent.Invoke(damage);

            var parryValue = CombatHandler.ParryHandler.ParryTiming > damage.parryTiming &&
                CombatHandler.ParryHandler.ParryTiming < damage.damageTiming;
            if(parryValue == true)
            {
                Debug.Log("Parry success");
                CombatHandler.UnitAnimator.Play("Sword1h_Parry_T");
            }
            else
            {
                Debug.Log("Parry failed");
                CombatHandler.UnitAnimator.PlayHitAnimation(damage.hitsAnimation);
            }
        }
    }
}
