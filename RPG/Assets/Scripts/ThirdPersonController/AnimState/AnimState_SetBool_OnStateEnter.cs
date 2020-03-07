using UnityEngine;
using System.Collections;
using System.Reflection;

namespace Tamana
{
    public class AnimState_SetBool_OnStateEnter : StateMachineBehaviour
    {
        [SerializeField] private string parameterName;
        [SerializeField] private bool value;

        PropertyInfo param;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(parameterName, value);
            param.SetValue(animator.GetComponent<Unit_Base>().UnitAnimator.Params, value);
        }
    }
}
