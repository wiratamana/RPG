using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoSwitchToHostile
    {
        public DoSwitchToHostile(Data data)
        {
            data.Myself.Behaviour = AIBehaviour.Hostile;
        }
    }
}
