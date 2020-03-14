using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimState_SetIsAttackingState : StateMachineBehaviour
    {
        [SerializeField] private StateMachineBehaviourPosition position;
        [SerializeField] private bool value;
        private Unit_Base unit;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(unit == null)
            {
                unit = animator.GetComponent<Unit_Base>();
            }

            if(position == StateMachineBehaviourPosition.OnStateEnter)
            {
                unit.UnitAnimator.Params.IsInAttackingState = value;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (unit == null)
            {
                unit = animator.GetComponent<Unit_Base>();
            }

            if (position == StateMachineBehaviourPosition.OnStateExit)
            {
                unit.UnitAnimator.Params.IsInAttackingState = value;
            }
        }
    }
}
