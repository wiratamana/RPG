using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct IsPlayerAttackingMe
    {
        public readonly bool Result;

        public IsPlayerAttackingMe(Data data, float minimumDistance = 3.0f, float dotValue = -0.667f)
        {
            Result = false;

            if(data.DistanceToPlayer > minimumDistance)
            {                
                return;
            }

            if(data.DotProductTowardPlayer > dotValue)
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
