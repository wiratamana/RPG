using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimState_SetDodgeAnimationStatus : StateMachineBehaviour
    {
        public WeaponType weaponType;
        public AnimDodge_1H animDodge_1H;
        public AnimDodge_1H animDodge_2H;

        private Unit_Base unit;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (unit == null)
            {
                unit = animator.GetComponent<Unit_Base>();
            }

            switch (weaponType)
            {
                case WeaponType.OneHand:
                    unit.CombatHandler.DodgeHandler.SetDodgeAnimationStatus(animDodge_1H);
                    break;
                case WeaponType.TwoHand:
                    unit.CombatHandler.DodgeHandler.SetDodgeAnimationStatus(animDodge_2H);
                    break;
            }
        }
    }
}
