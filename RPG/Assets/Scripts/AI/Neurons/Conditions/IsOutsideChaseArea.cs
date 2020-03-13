using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct IsOutsideChaseArea
    {
        public readonly bool Result;

        public IsOutsideChaseArea(Data data)
        {
            Result = data.DistanceToIdlePosition > data.MaximumDistanceToIdlePosition;
        }
    }
}
