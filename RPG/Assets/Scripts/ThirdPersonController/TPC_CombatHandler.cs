﻿using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_CombatHandler : MonoBehaviour
    {
        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnDoDamage()
        {
            Debug.Log("Do Damage");

            var weapon = Inventory_EquipmentManager.Instance.EquippedWeapon;
            var weaponTransform = Inventory_EquipmentManager.Instance.WeaponTransform;
            var colliders = Physics.OverlapBox(weaponTransform.position + weapon.WeaponCollider.center, weapon.WeaponCollider.size * 0.5f, weaponTransform.rotation);

            if(colliders.Length == 0)
            {
                Debug.Log("Nothing hitted");
            }

            else
            {
                foreach(var c in colliders)
                {
                    Debug.Log(c.name);
                }
            }
        }
    }
}