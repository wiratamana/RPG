using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AI_Neuron_DebugLog : AI_Neuron_Trigger
    {
        private readonly string message;

        public AI_Neuron_DebugLog(AI_Brain brain, string message) : base(brain)
        {
            this.message = message;
        }

        public override void Execute()
        {
            Debug.Log(message);
        }
    }
}
