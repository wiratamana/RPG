using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI
{
    public abstract class AI_Neuron_Condition : AI_Neuron
    {
        public abstract bool Result { get; }

        public AI_Neuron_Condition(AI_Brain brain) : base(brain)
        {

        }
    }

}
