using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoRotateTowardPlayer
    {
        public DoRotateTowardPlayer(ref Quaternion myRotation, Vector3 directionTowardPlayer, float rotationSpeed)
        {
            new DoRotateToward(ref myRotation, directionTowardPlayer, rotationSpeed);
        }
    }
}
