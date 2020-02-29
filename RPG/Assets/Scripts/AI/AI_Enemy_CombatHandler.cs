using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AI_Enemy_CombatHandler : MonoBehaviour
    {
        private AI_Enemy_Base ai;
        public AI_Enemy_Base AI => this.GetAndAssignComponent(ai);

        public bool IsInCombatStance => AI.EnemyAnimator.Params_IsInCombatState;

        public void PlayEquipAnimation()
        {
            AI.EnemyAnimator.Play(AI_Enemy_Animator.Sword1h_Equip);
        }
        public void PlayHolsterAnimation()
        {
            AI.EnemyAnimator.Play(AI_Enemy_Animator.Sword1h_Holster);
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnHolster()
        {
            Debug.Log("OnHolster - Enemy");
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnEquip() 
        {
            Debug.Log("OnEquip - Enemy");
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnDoDamage()
        {
            Debug.Log("OnDoDamage - Enemy");
        }
    }
}
