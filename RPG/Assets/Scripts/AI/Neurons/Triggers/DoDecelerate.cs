using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoDecelerate
    {
        public DoDecelerate(Unit_Animator_Params param, float velocity)
        {
            param.Movement = Mathf.Max(0.0f, param.Movement - velocity);
        }
    }
}
