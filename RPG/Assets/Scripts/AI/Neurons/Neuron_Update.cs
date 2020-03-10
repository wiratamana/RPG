using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI
{
    public abstract class AI_Neuron_Update : AI_Neuron
    {
        public bool IsNeuronUpdating => brain.IsNeuronUpdating(this);
        public AI_Neuron_Update(AI_Brain brain) : base(brain)
        {

        }

        public abstract void Update();
    }
}
