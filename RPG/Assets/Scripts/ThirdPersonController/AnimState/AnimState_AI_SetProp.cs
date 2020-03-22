using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tamana.AI;

namespace Tamana
{
    public class AnimState_AI_SetProp : StateMachineBehaviour
    {
        [SerializeField] private StateMachineBehaviourPosition position;
        [SerializeField] private Prop.PropCode prop;
        [SerializeField] private bool value;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (position == StateMachineBehaviourPosition.OnStateEnter)
            {
                SetProp(animator);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (position == StateMachineBehaviourPosition.OnStateExit)
            {
                SetProp(animator);
            }
        }

        private void SetProp(Animator animator)
        {
            switch (prop)
            {                
                case Prop.PropCode.isCounter:
                    animator.GetComponent<Unit_AI>().CombatLogic.GetBrainProp().isCounter = value;
                    break;
                case Prop.PropCode.isPlayingVictoryAnimation:
                    animator.GetComponent<Unit_AI>().CombatLogic.GetBrainProp().isPlayingVictoryAnimation = value;
                    break;
                case Prop.PropCode.isVictory:
                    animator.GetComponent<Unit_AI>().CombatLogic.GetBrainProp().isVictory = value;
                    break;
            }
        }
    }
}
