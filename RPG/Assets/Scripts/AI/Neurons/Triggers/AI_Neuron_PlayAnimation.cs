using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AI_Neuron_PlayAnimation : AI_Neuron_Trigger
    {
        private readonly string stateName;

        public AI_Neuron_PlayAnimation(AI_Brain brain, string stateName) : base(brain)
        {
            this.stateName = stateName;
        }

        public override void Execute()
        {
            brain.Unit.UnitAnimator.Play(stateName);
        }
    }
}
