using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_AttackHandler : MonoBehaviour
    {
        [SerializeField] private TPC_CombatAnimDataContainer lightAttack;
        [SerializeField] private TPC_CombatAnimDataContainer heavyAttack;

        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        public TPC_CombatAnimDataContainer CurrentlyPlayingCombatAnimDataContainer { set; get; }
        public TPC_CombatAnimData CurrentlyPlayingCombatAnimData { get; set; }

        private void Awake()
        {
            CombatHandler.UnitAnimator.OnHitAnimationStarted.AddListener(MakePlayerUnableToAttack);
            CombatHandler.UnitAnimator.OnHitAnimationFinished.AddListener(MakePlayerAbleToAttackAgain);
        }

        private void PlayAttackAnimation(TPC_CombatAnimDataContainer attackType)
        {
            if (attackType is null)
            {
                return;
            }

            if (attackType.StaminaCost > GameManager.PlayerStatus.ST.CurrentStamina)
            {
                return;
            }

            if (CurrentlyPlayingCombatAnimDataContainer == null || CurrentlyPlayingCombatAnimDataContainer == attackType)
            {
                if (attackType != null && CurrentlyPlayingCombatAnimData == null)
                {
                    CurrentlyPlayingCombatAnimDataContainer = attackType;
                    GameManager.PlayerStatus.ST.Attack(attackType.StaminaCost);
                    CombatHandler.UnitAnimator.Play(attackType.CombatDatas[0].MyAnimStateName);
                }

                else if (CurrentlyPlayingCombatAnimData != null)
                {
                    if (CurrentlyPlayingCombatAnimData.IsCurrentlyReceivingInput == true)
                    {
                        GameManager.PlayerStatus.ST.Attack(attackType.StaminaCost);
                        CurrentlyPlayingCombatAnimData.IsInputReceived = true;
                    }
                }
            }
        }
        public void PlayAttackAnim_Light()
        {
            PlayAttackAnimation(lightAttack);
        }
        public void PlayAttackAnim_Heavy()
        {
            PlayAttackAnimation(heavyAttack);
        }

        private void MakePlayerUnableToAttack()
        {
            if(CombatHandler.UnitAnimator.Params.IsInBattleState == false)
            {
                return;
            }

            InputEvent.Instance.Event_DoAttackLight.RemoveListener(PlayAttackAnim_Heavy);
            InputEvent.Instance.Event_DoAttackHeavy.RemoveListener(PlayAttackAnim_Light);
        }

        private void MakePlayerAbleToAttackAgain()
        {
            CurrentlyPlayingCombatAnimDataContainer = null;
            CurrentlyPlayingCombatAnimData = null;

            if (CombatHandler.UnitAnimator.Params.IsInBattleState == false)
            {
                return;
            }

            InputEvent.Instance.Event_DoAttackLight.AddListener(PlayAttackAnim_Heavy);
            InputEvent.Instance.Event_DoAttackHeavy.AddListener(PlayAttackAnim_Light);
        }
    }
}
