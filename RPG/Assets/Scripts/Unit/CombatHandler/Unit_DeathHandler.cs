using UnityEngine;

namespace Tamana
{
    public class Unit_DeathHandler : MonoBehaviour
    {
        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        public EventManager OnDeath { get; } = new EventManager();

        private void Awake()
        {
            CombatHandler.DamageReceiveHandler.OnPostReceivedDamageEvent.AddListener(OnReceiveDamageEvent);
        }

        private void OnReceiveDamageEvent()
        {
            if(CombatHandler.Unit.Status.IsDead == true)
            {
                PlayDeathAnimation();
                OnDeath.Invoke();
            }
        }

        private void PlayDeathAnimation()
        {
            var equippedWeapon = CombatHandler.Unit.Equipment.EquippedWeapon;
            if (equippedWeapon == null)
            {
                var random = Random.Range(1, 3);
                if (random == 1)
                {
                    PlayDeathAnimation_1H();
                }
                else
                {
                    PlayDeathAnimation_2H();
                }
            }
            else
            {
                switch (equippedWeapon.WeaponType)
                {
                    case WeaponType.OneHand:
                        PlayDeathAnimation_1H();
                        break;
                    case WeaponType.TwoHand:
                        PlayDeathAnimation_2H();
                        break;
                }
            }
        }

        private void PlayDeathAnimation_1H()
        {
            var enumValues = System.Enum.GetNames(typeof(AnimDeath_1H));
            var randomNumber = Random.Range(0, enumValues.Length);
            CombatHandler.Unit.UnitAnimator.Play(enumValues[randomNumber]);
        }

        private void PlayDeathAnimation_2H()
        {
            var enumValues = System.Enum.GetNames(typeof(AnimDeath_2H));
            var randomNumber = Random.Range(0, enumValues.Length);
            CombatHandler.Unit.UnitAnimator.Play(enumValues[randomNumber]);
        }
    }
}