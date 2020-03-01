﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_DamageHandler : MonoBehaviour
    {
        private Unit_Base unit;
        public Unit_Base Unit => this.GetAndAssignComponent(ref unit);
        public bool IsUnitPlayer => Unit is Unit_Player;

        public EventManager<Status_DamageData> OnReceivedDamageEvent { get; } = new EventManager<Status_DamageData>();        

        public void DamageReceiver(Status_DamageData damage)
        {
            Debug.Log($"'{name}' received '{damage.damagePoint}' damage");
            OnReceivedDamageEvent.Invoke(damage);

            Unit.UnitAnimator.PlayHitAnimation(damage.hitsAnimation);
        }

        private Collider[] OverlapWeapon()
        {
            var boxCenter = Unit.Equipment.WeaponTransform.GetChild(0).position;
            var boxHalfExtents = Unit.Equipment.EquippedWeapon.WeaponCollider.size * 0.5f;
            int layer;

            if (IsUnitPlayer == true)
            {
                layer = LayerMask.GetMask(LayerManager.LAYER_ENEMY);
            }
            else
            {
                layer = LayerMask.GetMask(LayerManager.LAYER_PLAYER);
            }

            var colliders = Physics.OverlapBox(boxCenter, boxHalfExtents, Unit.Equipment.WeaponTransform.rotation, layer);

            return colliders;
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnDoDamage(Status_DamageObject damageObject)
        {
            Collider[] colliders = OverlapWeapon();

            if (colliders == null || colliders.Length == 0)
            {
                Debug.Log("Nothing hitted");
            }

            else
            {
                foreach (var c in colliders)
                {
                    var damageHandler = c.GetComponent<Unit_DamageHandler>();
                    if (damageHandler == null)
                    {
                        continue;
                    }

                    Debug.Log($"SendDamage form '{name}' to '{damageHandler.name}'");
                    damageHandler.DamageReceiver(new Status_DamageData()
                    {
                        damagePoint = 100,
                        hitsAnimation = damageObject.GetHitAnimations(AnimationState.Idle)
                    });
                }
            }
        }
    }
}
