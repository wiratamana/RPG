using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimState_SetIsInCombatState_OnStateEnter : StateMachineBehaviour
    {
        public bool value;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<Unit_Base>().UnitAnimator.Params.IsInCombatState = value;
        }
    }
}
