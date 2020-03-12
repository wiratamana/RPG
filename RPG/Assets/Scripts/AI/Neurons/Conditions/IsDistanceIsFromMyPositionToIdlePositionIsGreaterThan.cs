using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct IsDistanceIsFromMyPositionToIdlePositionIsGreaterThan
    {
        public readonly bool Result;

        public IsDistanceIsFromMyPositionToIdlePositionIsGreaterThan(ref DataMyself myData, float distanceLimit)
        {
            Result = myData.DistanceToIdlePosition > distanceLimit;
        }
    }
}
