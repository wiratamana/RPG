using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_AttackHandler : MonoBehaviour
    {
        [SerializeField] private TPC_CombatAnimDataContainer combatAnimDataContainer;

        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        public TPC_CombatAnimDataContainer CurrentlyPlayingCombatAnimDataContainer { set; get; }
        public TPC_CombatAnimData CurrentlyPlayingCombatAnimData { get; set; }

        private void Awake()
        {
            if(CombatHandler.Unit.IsUnitPlayer)
            {
                CombatHandler.UnitAnimator.OnHitAnimationStarted.AddListener(MakePlayerUnableToAttack);
                CombatHandler.UnitAnimator.OnHitAnimationFinished.AddListener(MakePlayerAbleToAttackAgain);
            }           
        }

        public void PlayAttackAnim()
        {
            if (combatAnimDataContainer is null)
            {
                return;
            }

            if (combatAnimDataContainer.StaminaCost > GameManager.PlayerStatus.ST.CurrentStamina)
            {
                return;
            }

            if (CurrentlyPlayingCombatAnimDataContainer == null || CurrentlyPlayingCombatAnimDataContainer == combatAnimDataContainer)
            {
                if (combatAnimDataContainer != null && CurrentlyPlayingCombatAnimData == null)
                {
                    CurrentlyPlayingCombatAnimDataContainer = combatAnimDataContainer;
                    GameManager.PlayerStatus.ST.Attack(combatAnimDataContainer.StaminaCost);
                    CombatHandler.UnitAnimator.Play(combatAnimDataContainer.CombatDatas[0].MyAnimStateName);
                }

                else if (CurrentlyPlayingCombatAnimData != null)
                {
                    if (CurrentlyPlayingCombatAnimData.IsCurrentlyReceivingInput == true)
                    {
                        GameManager.PlayerStatus.ST.Attack(combatAnimDataContainer.StaminaCost);
                        CurrentlyPlayingCombatAnimData.IsInputReceived = true;
                    }
                }
            }
        }

        private void MakePlayerUnableToAttack()
        {
            if(CombatHandler.UnitAnimator.Params.IsInCombatState == false)
            {
                return;
            }

            InputEvent.Instance.Event_DoAttackHeavy.RemoveListener(PlayAttackAnim);
        }

        private void MakePlayerAbleToAttackAgain()
        {
            CurrentlyPlayingCombatAnimDataContainer = null;
            CurrentlyPlayingCombatAnimData = null;

            if (CombatHandler.UnitAnimator.Params.IsInCombatState == false)
            {
                return;
            }

            InputEvent.Instance.Event_DoAttackHeavy.AddListener(PlayAttackAnim);
        }
    }
}
