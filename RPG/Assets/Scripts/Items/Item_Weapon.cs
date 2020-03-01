using UnityEngine;
using System.Collections;

namespace Tamana
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon", order = 1)]
    public class Item_Weapon : Item_Equipment
    {
        [SerializeField] private Vector3 holsterPosition;
        [SerializeField] private Vector3 holsterRotation;
        [SerializeField] private Vector3 equipPosition;
        [SerializeField] private Vector3 equipRotation;
        [SerializeField] private Vector3 menuDefaultItemRotation;
        [SerializeField] private Vector3 menuDefaultCameraRotation;
        [SerializeField] private Vector3 menuCameraOffset;
        [SerializeField] private float customOrthoSize;
        [SerializeField] private WeaponOverlapBox weaponCollider;

        public Vector3 HolsterPosition { get { return holsterPosition; } }
        public Quaternion HolsterRotation { get { return Quaternion.Euler(holsterRotation); } }
        public Vector3 EquipPostion { get { return equipPosition; } }
        public Quaternion EquipRotation { get { return Quaternion.Euler(equipRotation); } }
        public Vector3 MenuDefaultItemRotation { get { return menuDefaultItemRotation; } }
        public Vector3 MenuDefaultCameraRotation { get { return menuDefaultCameraRotation; } }
        public Vector3 MenuCameraOffset { get { return menuCameraOffset; } }
        public float CustomOrthoSize { get { return customOrthoSize; } }
        public WeaponOverlapBox WeaponCollider { get { return weaponCollider; } }
        public override ItemType ItemType => ItemType.Weapon;

        public override void Equip()
        {
            Debug.Log("Weapon - Equip");

            inventoryOwner.Unit.Equipment.EquipWeapon(this);
        }

        public override void Unequip()
        {
            Debug.Log("Unequip");

            inventoryOwner.Unit.Equipment.UnequipWeapon();
        }

        public override Item_ItemDetails ItemDetails
        {
            get
            {
                return new Item_ItemDetails()
                {
                    ItemName = ItemName,
                    ItemDescription = ItemDescription,
                    ItemEffects = ItemEffects
                };
            }
        }

        public void SetWeaponTransformParent(bool isEquip)
        {
            var equipment = inventoryOwner.Unit.Equipment;
            var bodyTransform = inventoryOwner.Unit.CombatHandler.BodyTransform;

            var weaponTransform = equipment.WeaponTransform;
            var weaponItem = equipment.EquippedWeapon;

            var parentTransform = isEquip ? bodyTransform.HandR : bodyTransform.Hips;
            var position = isEquip ? weaponItem.EquipPostion : weaponItem.HolsterPosition;
            var rotation = isEquip ? weaponItem.EquipRotation : weaponItem.HolsterRotation;    

            weaponTransform.SetParent(parentTransform);
            weaponTransform.localPosition = position;
            weaponTransform.localRotation = rotation;
        }

        [System.Serializable]
        public struct WeaponOverlapBox
        {
            public Vector3 center;
            public Vector3 size;
        }
    }
}
