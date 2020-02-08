using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimState_SetLayerWeight_OnStateExit : StateMachineBehaviour
    {
        [SerializeField] private string Layer;
        [SerializeField] [Range(0.0f, 1.0f)] private float Value;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var index = animator.GetLayerIndex(Layer);
            animator.SetLayerWeight(index, Value);
        }
    }
}
