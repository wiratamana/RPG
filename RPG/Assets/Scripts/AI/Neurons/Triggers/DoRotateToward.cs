using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoRotateToward
    {
        public DoRotateToward(ref Quaternion myRotation, Vector3 direction, float rotationSpeed)
        {
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            myRotation = Quaternion.Slerp(myRotation, lookRotation, rotationSpeed);
        }
    }
}
