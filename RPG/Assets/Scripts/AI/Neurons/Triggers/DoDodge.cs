using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoDodge
    {
        public DoDodge(Data data)
        {
            data.Myself.CombatHandler.DodgeHandler.Dodge();
        }
    }
}
