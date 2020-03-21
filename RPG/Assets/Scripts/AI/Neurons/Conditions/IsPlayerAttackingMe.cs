using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct IsPlayerAttackingMe
    {
        public readonly bool Result;

        public IsPlayerAttackingMe(Data data)
        {
            Result = false;

            if(data.DistanceToPlayer > 3.0f)
            {                
                return;
            }

            if(data.DotProductTowardPlayer > -0.667f)
            {
                return;
            }

            Result = data.IsPlayerOnAttackAnimationStarted;
        }

        public static implicit operator bool(IsPlayerAttackingMe result)
        {
            return result.Result;
        }
    }
}
