using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI
{
    public class Neuron_Trigger_DebugLog : AI_Neuron_Trigger
    {
        private readonly string message;

        public Neuron_Trigger_DebugLog(AI_Brain brain, string message) : base(brain)
        {
            this.message = message;
        }

        public override void Execute()
        {
            Debug.Log(message);
        }
    }
}
