using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct IsPlayerInsideAttackRange
    {
        public readonly bool Result;

        public IsPlayerInsideAttackRange(Data data, float range)
        {
            Result = data.DistanceToPlayer < range;
        }

        public static implicit operator bool(IsPlayerInsideAttackRange result)
        {
            return result.Result;
        }
    }
}