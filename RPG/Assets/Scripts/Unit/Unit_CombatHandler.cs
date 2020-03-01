using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_CombatHandler : MonoBehaviour
    {
        private Unit_Base unit;
        private Unit_DamageHandler damageHandler;
        private Unit_BodyTransform bodyTransform;

        public TPC_CombatAnimDataContainer lightAttack;
        public TPC_CombatAnimDataContainer heavyAttack;

        public Unit_Base Unit => this.GetAndAssignComponent(ref unit);
        public Unit_DamageHandler DamageHandler => this.GetOrAddAndAssignComponent(ref damageHandler);
        public Unit_BodyTransform BodyTransform => this.GetOrAddAndAssignComponent(ref bodyTransform);
        public Unit_Animator Animator =>Unit.UnitAnimator;

        public TPC_CombatAnimDataContainer CurrentlyPlayingCombatAnimDataContainer { set; get; }
        public TPC_CombatAnimData CurrentlyPlayingCombatAnimData { get; set; }

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(DamageHandler);
            this.LogErrorIfComponentIsNull(BodyTransform);
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnHoster()
        {
            GameManager.Player.Equipment.EquippedWeapon.SetWeaponTransformParent(false);

            if(Unit.IsUnitPlayer == true)
            {
                InputEvent.Instance.Event_Holster.RemoveListener(PlayHolsterAnimation);
                InputEvent.Instance.Event_Equip.AddListener(PlayEquipAnimation);

                InputEvent.Instance.Event_DoAttackHeavy.RemoveListener(PlayAttackAnim_Heavy);
                InputEvent.Instance.Event_DoAttackLight.RemoveListener(PlayAttackAnim_Light);
            }
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnEquip()
        {
            GameManager.Player.Equipment.EquippedWeapon.SetWeaponTransformParent(true);

            if (Unit.IsUnitPlayer == true)
            {
                InputEvent.Instance.Event_Equip.RemoveListener(PlayEquipAnimation);
                InputEvent.Instance.Event_Holster.AddListener(PlayHolsterAnimation);

                InputEvent.Instance.Event_DoAttackHeavy.AddListener(PlayAttackAnim_Heavy);
                InputEvent.Instance.Event_DoAttackLight.AddListener(PlayAttackAnim_Light);
            }
        }

        public void PlayEquipAnimation()
        {
            Animator.Play(TPC_Anim_SwordAnimsetPro.Sword1h_Equip);
        }
        public void PlayHolsterAnimation()
        {
            Animator.Play(TPC_Anim_SwordAnimsetPro.Sword1h_Holster);
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
                    Animator.Play(attackType.CombatDatas[0].MyAnimStateName);
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
    }
}
