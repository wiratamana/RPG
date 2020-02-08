using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AnimState_SetLayerWeight_OnStateEnter : StateMachineBehaviour
    {
        [SerializeField] private string Layer;
        [SerializeField] [Range(0.0f, 1.0f)] private float Value;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var index = animator.GetLayerIndex(Layer);
            animator.SetLayerWeight(index, Value);
        }
    }
}
