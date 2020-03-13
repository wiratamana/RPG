using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct IsDistanceIsFromMyPositionToIdlePositionIsGreaterThan
    {
        public readonly bool Result;

        public IsDistanceIsFromMyPositionToIdlePositionIsGreaterThan(Data data)
        {
            Result = data.DistanceToIdlePosition > data.MaximumDistanceToIdlePosition;
        }
    }
}
