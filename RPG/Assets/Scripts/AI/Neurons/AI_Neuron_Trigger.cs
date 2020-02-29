using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public abstract class AI_Neuron_Trigger : AI_Neuron
    {
        public AI_Neuron_Trigger(AI_Brain brain) : base(brain)
        {

        }

        public abstract void Execute();
    }
}
