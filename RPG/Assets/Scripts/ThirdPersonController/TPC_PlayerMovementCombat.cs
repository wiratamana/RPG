using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_PlayerMovementCombat : SingletonMonobehaviour<TPC_PlayerMovementCombat>
    {
        public  TPC_CombatAnimDataContainer lightAttack;
        public TPC_CombatAnimData CurrentlyPlayingCombatAnimData { get; set; }

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

            if (Input.GetKeyDown(KeyCode.Mouse0) == true)
            {
                if(lightAttack != null && CurrentlyPlayingCombatAnimData == null)
                {
                    TPC_AnimController.Instance.PlayAnim(lightAttack.CombatDatas[0].MyAnimStateName);
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

        public TPC_BodyTransform BodyTransform { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            BodyTransform = gameObject.AddComponent<TPC_BodyTransform>();
        }

        [TPC_AnimClip_AttributeEvent]
        private void OnEquip()
        {
            var weaponTransform = Inventory_EquipmentManager.Instance.WeaponTransform;
            var weaponItem = Inventory_EquipmentManager.Instance.EquippedWeapon;

            weaponTransform.SetParent(BodyTransform.HandR);
            weaponTransform.localPosition = weaponItem.EquipPostion;
            weaponTransform.localRotation = weaponItem.EquipRotation;
        }

        [TPC_AnimClip_AttributeEvent]
        private void OnHolster()
        {
            var weaponTransform = Inventory_EquipmentManager.Instance.WeaponTransform;
            var weaponItem = Inventory_EquipmentManager.Instance.EquippedWeapon;

            weaponTransform.SetParent(BodyTransform.Hips);
            weaponTransform.localPosition = weaponItem.HolsterPosition;
            weaponTransform.localRotation = weaponItem.HolsterRotation;
        }

        public string GetStartMoveAnimationName(float angle)
        {
            if (angle < 45 && angle > -45)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart;
            }
            else if (angle < 120 && angle >= 45)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart90_L;
            }
            else if (angle > -120 && angle <= -45)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart90_R;
            }
            else if (angle < 165 && angle >= 45)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart135_L;
            }
            else if (angle > -165 && angle <= -45)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart135_R;
            }
            else if (angle <= 179.99 && angle >= 165)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart180_L;
            }
            else if (angle >= -180 && angle <= -165)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart180_R;
            }

            return null;
        }
    }
}
