using UnityEngine;
using System.Collections;

namespace Tamana
{
    public abstract class AI_Neuron
    {
        public abstract void Update();
        protected AI_Brain brain;

        public AI_Neuron(AI_Brain brain)
        {
            this.brain = brain;
            brain.AddNeuron(this);
        }

        public void StopNeuron()
        {
            brain.RemoveNeuron(this);
        }

        public void ResumeNeuron()
        {
            brain.AddNeuron(this);
        }
    }
}
