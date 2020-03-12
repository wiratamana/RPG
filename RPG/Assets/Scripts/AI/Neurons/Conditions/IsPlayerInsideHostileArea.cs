using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct IsPlayerInsideHostileArea
    {
        public readonly bool Result;

        public IsPlayerInsideHostileArea(float distanceToPlayer, float radius)
        {
            Result = distanceToPlayer < radius;
        }
    }
}