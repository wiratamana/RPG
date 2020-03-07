using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimState_SetIsInCombatState_OnStateExit : StateMachineBehaviour
    {
        public bool value;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<Unit_Base>().UnitAnimator.Params.IsInCombatState = value;
        }
    }
}
