using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct IsPlayerInsideHostileArea
    {
        public readonly bool Result;

        public IsPlayerInsideHostileArea(Data data)
        {
            Result = data.DistanceFromPlayerToIdlePosition < data.HostileAreaRadius;
        }
    }
}