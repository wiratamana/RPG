using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AnimState_SetBool_OnStateExit : StateMachineBehaviour
    {
        [SerializeField] private string parameterName;
        [SerializeField] private bool value;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(parameterName, value);
        }
    }

}
