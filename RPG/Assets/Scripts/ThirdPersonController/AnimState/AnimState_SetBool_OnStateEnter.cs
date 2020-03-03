using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AnimState_SetBool_OnStateEnter : StateMachineBehaviour
    {
        [SerializeField] private string parameterName;
        [SerializeField] private bool value;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(parameterName, value);
        }
    }
}
