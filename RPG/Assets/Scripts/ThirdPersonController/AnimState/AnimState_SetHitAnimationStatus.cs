using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimState_SetHitAnimationStatus : StateMachineBehaviour
    {
        [SerializeField] private string stateName;
        [SerializeField] private bool value;
        private Unit_Base unit;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(unit == null)
            {
                unit = animator.GetComponent<Unit_Base>();
            }

            unit.UnitAnimator.SetAnimationHitStatus(stateName, value);
        }
    }
}
