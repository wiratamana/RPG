using UnityEngine;
using System.Collections;

namespace Tamana.AI
{
    public abstract class AI_Neuron
    {
        public EventManager OnNeuronStopped { private set; get; } = new EventManager();
        public EventManager OnNeuronStarted { private set; get; } = new EventManager();

        protected AI_Brain brain;

        public AI_Neuron(AI_Brain brain)
        {
            this.brain = brain;
            brain.AddNeuron(this);
        }

        public void StopNeuron()
        {
            brain.RemoveNeuron(this);
            OnNeuronStopped.Invoke();
        }

        public void ResumeNeuron()
        {
            brain.AddNeuron(this);
            OnNeuronStarted.Invoke();
        }

        public string GetInstanceID()
        {
            var type = GetType();
            return type.FullName;
        }
    }
}
