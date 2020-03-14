using UnityEngine;
using System.Collections;

namespace Tamana.AI
{
    public class Neuron_Trigger_PlayAnimation : AI_Neuron_Trigger
    {
        private readonly string stateName;

        public Neuron_Trigger_PlayAnimation(AI_Brain brain, string stateName) : base(brain)
        {
            this.stateName = stateName;
        }

        public override void Execute()
        {
            brain.Unit.UnitAnimator.Play(stateName);
        }
    }
}
