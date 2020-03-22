using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoAttack
    {
        public DoAttack(Data data, Prop prop, string stateName)
        {
            if(data.Params.IsInAttackingState)
            {
                return;
            }

            data.Myself.UnitAnimator.Play(stateName);
            prop.attackDelay_cur = prop.attackDelay_val;
        }
    }
}

