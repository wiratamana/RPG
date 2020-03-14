using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AnimState_CombatHandler : StateMachineBehaviour
    {
        [SerializeField] private TPC_CombatAnimDataContainer combatContainer;
        [SerializeField] private int index;

        private TPC_CombatAnimData animData => combatContainer.CombatDatas[index];
        private bool isCrossFading = false;
        private bool isAnimationStopping = false;

        private Unit_Base unit;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(unit == null)
            {
                unit = animator.GetComponent<Unit_Base>();
            }

            if(index == 0)
            {
                unit.UnitAnimator.Params.IsInAttackingState = true;
                unit.CombatHandler.AttackHandler.OnAttackAnimationStarted.Invoke();
            }
            else
            {
                unit.CombatHandler.AttackHandler.OnConsecutiveAttack.Invoke();
            }

            isAnimationStopping = false;
            isCrossFading = false;
            animData.IsInputReceived = false;
            animData.IsCurrentlyReceivingInput = false;

            unit.UnitAnimator.Params.IsTransitingToNextAttackAnimation = false;
            unit.CombatHandler.AttackHandler.CurrentlyPlayingCombatAnimData = animData;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animData.IsLastAnimation == true)
            {
                return;
            }
            
            if (isCrossFading == true)
            {
                if(unit.CombatHandler.AttackHandler.CurrentlyPlayingCombatAnimData == animData)
                {
                    var nextAnimationIndex = index + 1;
                    if(nextAnimationIndex < combatContainer.CombatDatas.Length)
                    {
                        unit.CombatHandler.AttackHandler.CurrentlyPlayingCombatAnimData = combatContainer.CombatDatas[nextAnimationIndex];
                    }
                    else
                    {
                        unit.CombatHandler.AttackHandler.CurrentlyPlayingCombatAnimData = null;
                    }
                }
                
                return;
            }

            var currentPosition = stateInfo.length * stateInfo.normalizedTime;

            animData.IsCurrentlyReceivingInput = currentPosition > animData.ReceivingInputStart &&
                currentPosition < animData.ReceivingInputFinish;

            //Debug.Log($"StateName = {combatContainer.CombatDatas[index].MyAnimStateName} | IsCurrentlyReceivingInput = {animData.IsCurrentlyReceivingInput}");

            // ===============================================================================================
            // Transiting to next attack animation
            // ===============================================================================================
            if (animData.IsInputReceived == true && isCrossFading == false)
            {
                unit.UnitAnimator.Params.IsTransitingToNextAttackAnimation = true;
                isCrossFading = true;
                return;
            }

            // ===============================================================================================
            // Transition to idle animation
            // ===============================================================================================
            if (currentPosition > animData.TransitionToIdleAnim && isCrossFading == false)
            {                
                animator.CrossFade(animData.IdleAnimStateName, animData.TransitionTimeIdle);
                isCrossFading = true;
                isAnimationStopping = true;

                unit.CombatHandler.AttackHandler.CurrentlyPlayingCombatAnimData = null;
                unit.CombatHandler.AttackHandler.CurrentlyPlayingCombatAnimDataContainer = null;
                unit.UnitAnimator.Params.IsTransitingToNextAttackAnimation = false;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animData.IsLastAnimation == true)
            {
                unit.UnitAnimator.Params.IsTransitingToNextAttackAnimation = false;
                unit.CombatHandler.AttackHandler.CurrentlyPlayingCombatAnimDataContainer = null;
                unit.CombatHandler.AttackHandler.CurrentlyPlayingCombatAnimData = null;

                unit.UnitAnimator.Params.IsInAttackingState = false;
                unit.CombatHandler.AttackHandler.OnAttackAnimationStopped.Invoke();
            }

            if(isAnimationStopping == true && animData.IsLastAnimation == false)
            {
                unit.UnitAnimator.Params.IsInAttackingState = false;
                unit.CombatHandler.AttackHandler.OnAttackAnimationStopped.Invoke();
            }

            animData.IsCurrentlyReceivingInput = false;
        }
    }
}
