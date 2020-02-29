using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_CombatHandler : MonoBehaviour
    {
        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnDoDamage()
        {
            Debug.Log("Do Damage");

            var weapon = GameManager.Player.Equipment.EquippedWeapon;
            var weaponTransform = GameManager.Player.Equipment.WeaponTransform;
            var colliders = Physics.OverlapBox(weaponTransform.position + weapon.WeaponCollider.center, weapon.WeaponCollider.size * 0.5f, weaponTransform.rotation);

            if(colliders.Length == 0)
            {
                Debug.Log("Nothing hitted");
            }

            else
            {
                foreach(var c in colliders)
                {
                    var damageHandler = c.GetComponent<Status_DamageHandler>();
                    if(damageHandler != null)
                    {
                        SendDamage(damageHandler);
                    }
                }
            }
        }

        private void SendDamage(Status_DamageHandler damageHandler)
        {
            damageHandler.SendDamage(new Status_DamageData()
            {
                damagePoint = 100
            });
        }
    }
}
