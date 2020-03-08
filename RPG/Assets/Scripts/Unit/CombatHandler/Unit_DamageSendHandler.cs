using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_DamageSendHandler : MonoBehaviour
    {
        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        private Collider[] OverlapWeapon()
        {
            var boxCenter = CombatHandler.Unit.Equipment.WeaponTransform.GetChild(0).position;
            var boxHalfExtents = CombatHandler.Unit.Equipment.EquippedWeapon.WeaponCollider.size * 0.5f;
            int layer;

            if (CombatHandler.Unit.IsUnitPlayer == true)
            {
                layer = LayerMask.GetMask(LayerManager.LAYER_ENEMY);
            }
            else
            {
                layer = LayerMask.GetMask(LayerManager.LAYER_PLAYER);
            }

            var colliders = Physics.OverlapBox(boxCenter, boxHalfExtents, CombatHandler.Unit.Equipment.WeaponTransform.rotation, layer);

            return colliders;
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnDoDamage(Unit_Status_DamageObject damageObject)
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
                    var damageHandler = c.GetComponent<Unit_DamageReceiveHandler>();
                    if (damageHandler == null)
                    {
                        continue;
                    }

                    WeaponType? receiverWeapon = damageHandler.CombatHandler.Unit.Equipment.EquippedWeapon?.WeaponType;
                    WeaponType senderWeapon = CombatHandler.Unit.Equipment.EquippedWeapon.WeaponType;

                    Debug.Log($"SendDamage form '{name}' to '{damageHandler.name}'");
                    damageHandler.DamageReceiver(new Unit_Status_DamageData()
                    {
                        damagePoint = CombatHandler.Unit.Status.GetStatus(MainStatus.AT),
                        parryTiming = CombatHandler.ParryHandler.ChanceToParryTiming,
                        damageTiming = Time.time,
                        weaponType = senderWeapon,
                        hitsAnimation = damageObject.GetHitAnimations(receiverWeapon),
                        damageSenderPosition = transform.position
                    });
                }
            }
        }
    }
}
