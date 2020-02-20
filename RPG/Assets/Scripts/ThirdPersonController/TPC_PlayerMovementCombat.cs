using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_PlayerMovementCombat : SingletonMonobehaviour<TPC_PlayerMovementCombat>
    {
        public TPC_CombatAnimDataContainer lightAttack;
        public TPC_CombatAnimDataContainer heavyAttack;

        public TPC_CombatAnimDataContainer CurrentlyPlayingCombatAnimDataContainer { set; get; }
        public TPC_CombatAnimData CurrentlyPlayingCombatAnimData { get; set; }
        public TPC_BodyTransform BodyTransform { get; private set; }
        public TPC_CombatHandler CombatHandler { private set; get; }

        protected override void Awake()
        {
            base.Awake();

            BodyTransform = gameObject.AddComponent<TPC_BodyTransform>();
            CombatHandler = gameObject.AddComponent<TPC_CombatHandler>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) == true)
            {
                if(Inventory_EquipmentManager.Instance.IsEquippedWithWeapon() == false)
                {
                    return;
                }

                if(TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeIdle)] == false)
                {
                    return;
                }

                if(TPC_AnimController.Instance.GetLayerWeight(TPC_Anim_SwordAnimsetPro.LAYER) == 0.0f)
                {
                    TPC_AnimController.Instance.SetLayerWeight(TPC_Anim_SwordAnimsetPro.LAYER, 1.0f);
                    TPC_AnimController.Instance.PlayAnim(TPC_Anim_SwordAnimsetPro.Sword1h_Equip);
                }
                else
                {
                    TPC_AnimController.Instance.PlayAnim(TPC_Anim_SwordAnimsetPro.Sword1h_Holster);
                }                
            }

            if (TPC_AnimController.Instance.GetLayerWeight(TPC_Anim_SwordAnimsetPro.LAYER) == 0)
            { return; }

            PlayAttackAnim(KeyCode.Mouse0, lightAttack);
            PlayAttackAnim(KeyCode.Mouse1, heavyAttack);
        }        

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnEquip()
        {
            var weaponTransform = Inventory_EquipmentManager.Instance.WeaponTransform;
            var weaponItem = Inventory_EquipmentManager.Instance.EquippedWeapon;

            weaponTransform.SetParent(BodyTransform.HandR);
            weaponTransform.localPosition = weaponItem.EquipPostion;
            weaponTransform.localRotation = weaponItem.EquipRotation;
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnHolster()
        {
            var weaponTransform = Inventory_EquipmentManager.Instance.WeaponTransform;
            var weaponItem = Inventory_EquipmentManager.Instance.EquippedWeapon;

            weaponTransform.SetParent(BodyTransform.Hips);
            weaponTransform.localPosition = weaponItem.HolsterPosition;
            weaponTransform.localRotation = weaponItem.HolsterRotation;
        }

        private void PlayAttackAnim(KeyCode keyTrigger, TPC_CombatAnimDataContainer attackType)
        {
            if (Input.GetKeyDown(keyTrigger) == true && (CurrentlyPlayingCombatAnimDataContainer == null || CurrentlyPlayingCombatAnimDataContainer == attackType))
            {
                if (attackType != null && CurrentlyPlayingCombatAnimData == null)
                {
                    CurrentlyPlayingCombatAnimDataContainer = attackType;
                    TPC_AnimController.Instance.PlayAnim(attackType.CombatDatas[0].MyAnimStateName);
                }

                else if (CurrentlyPlayingCombatAnimData != null)
                {
                    if (CurrentlyPlayingCombatAnimData.IsCurrentlyReceivingInput == true)
                    {
                        CurrentlyPlayingCombatAnimData.IsInputReceived = true;
                    }
                }
            }
        }
    }
}
