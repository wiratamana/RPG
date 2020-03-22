using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public readonly struct DoCounter
    {
        public DoCounter(Data data, Prop prop, string stateName)
        {
            if(prop.isCounter == true)
            {
                return;
            }

            prop.isCounter = true;
            prop.attackDelay_cur = prop.attackDelay_val;
            data.Myself.UnitAnimator.Play(stateName);
        }
    }
}
