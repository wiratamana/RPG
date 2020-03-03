using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimState_SetHitAnimationStatus : StateMachineBehaviour
    {
        public WeaponType weaponType;
        public AnimHit_1H animHit_1H;
        public AnimHit_2H animHit_2H;

        private Unit_Base unit;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(unit == null)
            {
                unit = animator.GetComponent<Unit_Base>();
            }

            Debug.Log($"OnStateExit - {weaponType}");
            switch (weaponType)
            {
                case WeaponType.OneHand:
                    unit.UnitAnimator.SetAnimationHitStatus(animHit_1H);
                    break;
                case WeaponType.TwoHand:
                    unit.UnitAnimator.SetAnimationHitStatus(animHit_2H);
                    break;
            }
        }
    }
}
