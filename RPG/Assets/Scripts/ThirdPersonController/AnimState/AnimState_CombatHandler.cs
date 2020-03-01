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

        private Unit_Base unit;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(unit == null)
            {
                unit = animator.GetComponent<Unit_Base>();
            }

            isCrossFading = false;
            animData.IsInputReceived = false;
            animData.IsCurrentlyReceivingInput = false;

            unit.UnitAnimator.Params.IsTransitingToNextAttackAnimation = false;
            GameManager.Player.CombatHandler.CurrentlyPlayingCombatAnimData = animData;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animData.IsLastAnimation == true)
            {
                return;
            }
            
            if (isCrossFading == true)
            {
                if(GameManager.Player.CombatHandler.CurrentlyPlayingCombatAnimData == animData)
                {
                    var nextAnimationIndex = index + 1;
                    if(nextAnimationIndex < combatContainer.CombatDatas.Length)
                    {
                        GameManager.Player.CombatHandler.CurrentlyPlayingCombatAnimData = combatContainer.CombatDatas[nextAnimationIndex];
                    }
                    else
                    {
                        GameManager.Player.CombatHandler.CurrentlyPlayingCombatAnimData = null;
                    }
                }
                
                return;
            }

            var currentPosition = stateInfo.length * stateInfo.normalizedTime;

            animData.IsCurrentlyReceivingInput = currentPosition > animData.ReceivingInputStart &&
                currentPosition < animData.ReceivingInputFinish;

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

                GameManager.Player.CombatHandler.CurrentlyPlayingCombatAnimData = null;
                GameManager.Player.CombatHandler.CurrentlyPlayingCombatAnimDataContainer = null;
                unit.UnitAnimator.Params.IsTransitingToNextAttackAnimation = false;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animData.IsLastAnimation == true)
            {
                unit.UnitAnimator.Params.IsTransitingToNextAttackAnimation = false;
                GameManager.Player.CombatHandler.CurrentlyPlayingCombatAnimDataContainer = null;
                GameManager.Player.CombatHandler.CurrentlyPlayingCombatAnimData = null;
            }

            animData.IsCurrentlyReceivingInput = false;
        }
    }
}
