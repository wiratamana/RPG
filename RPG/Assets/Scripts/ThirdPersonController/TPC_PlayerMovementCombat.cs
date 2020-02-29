using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_PlayerMovementCombat : SingletonMonobehaviour<TPC_PlayerMovementCombat>
    {
        public TPC_CombatAnimDataContainer lightAttack;
        public TPC_CombatAnimDataContainer heavyAttack;
        private TPC_CombatHandler combatHandler;

        public TPC_CombatAnimDataContainer CurrentlyPlayingCombatAnimDataContainer { set; get; }
        public TPC_CombatAnimData CurrentlyPlayingCombatAnimData { get; set; }
        public TPC_CombatHandler CombatHandler => this.GetOrAddAndAssignComponent(combatHandler);

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(CombatHandler);
        }

        protected override void Awake()
        {
            base.Awake();         
        }

        private void Start()
        {
            InputEvent.Instance.Event_Equip.AddListener(Equip);
        }

        private void Equip()
        {
            if (GameManager.Player.Equipment.IsEquippedWithWeapon() == false)
            {
                return;
            }

            if (TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeIdle)] == false)
            {
                return;
            }

            TPC_AnimController.Instance.SetLayerWeight(TPC_Anim_SwordAnimsetPro.LAYER, 1.0f);
            TPC_AnimController.Instance.PlayAnim(TPC_Anim_SwordAnimsetPro.Sword1h_Equip);
        }

        private void Holster()
        {
            TPC_AnimController.Instance.PlayAnim(TPC_Anim_SwordAnimsetPro.Sword1h_Holster);
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnEquip()
        {
            GameManager.Player.Equipment.EquippedWeapon.SetWeaponTransformParent(true);

            InputEvent.Instance.Event_Equip.RemoveListener(Equip);
            InputEvent.Instance.Event_Holster.AddListener(Holster);

            InputEvent.Instance.Event_DoAttackHeavy.AddListener(PlayAttackAnim_Heavy);
            InputEvent.Instance.Event_DoAttackLight.AddListener(PlayAttackAnim_Light);
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnHolster()
        {
            GameManager.Player.Equipment.EquippedWeapon.SetWeaponTransformParent(false);

            InputEvent.Instance.Event_Holster.RemoveListener(Holster);
            InputEvent.Instance.Event_Equip.AddListener(Equip);

            InputEvent.Instance.Event_DoAttackHeavy.RemoveListener(PlayAttackAnim_Heavy);
            InputEvent.Instance.Event_DoAttackLight.RemoveListener(PlayAttackAnim_Light);
        }

        private void PlayAttackAnim(TPC_CombatAnimDataContainer attackType)
        {
            if(attackType is null)
            {
                return;
            }

            if(attackType.StaminaCost > GameManager.PlayerStatus.ST.CurrentStamina)
            {
                return;
            }

            if (CurrentlyPlayingCombatAnimDataContainer == null || CurrentlyPlayingCombatAnimDataContainer == attackType)
            {
                if (attackType != null && CurrentlyPlayingCombatAnimData == null)
                {
                    CurrentlyPlayingCombatAnimDataContainer = attackType;
                    GameManager.PlayerStatus.ST.Attack(attackType.StaminaCost);
                    TPC_AnimController.Instance.PlayAnim(attackType.CombatDatas[0].MyAnimStateName);
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

        private void PlayAttackAnim_Light()
        {
            PlayAttackAnim(lightAttack);
        }
        private void PlayAttackAnim_Heavy()
        {
            PlayAttackAnim(heavyAttack);
        }
    }
}
