using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimState_SetIsEquipping : StateMachineBehaviour
    {
        [SerializeField] private StateMachineBehaviourPosition pos;
        [SerializeField] bool value;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (pos == StateMachineBehaviourPosition.OnStateEnter)
            {
                animator.GetComponent<Unit_Base>().UnitAnimator.Params.IsEquipping = value;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (pos == StateMachineBehaviourPosition.OnStateExit)
            {
                animator.GetComponent<Unit_Base>().UnitAnimator.Params.IsEquipping = value;
            }
        }
    }
}
