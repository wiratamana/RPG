using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public class DoMoveTowardPosition : MonoBehaviour
    {
        public DoMoveTowardPosition(Unit_Animator_Params param, ref Quaternion myRotation
             , Vector3 myPosition, Vector3 destination, float rotationSpeed
             , float movementVelocity, float distanceToDestination, float distanceStop)
        {
            var directionTowardDestination = Vector3.Normalize(destination - myPosition);
            new DoRotateTowardPlayer(ref myRotation, directionTowardDestination, rotationSpeed);

            if (distanceToDestination > distanceStop)
            {
                new DoAccelerate(param, movementVelocity);
            }
            else
            {
                new DoDecelerate(param, movementVelocity);
            }
        }
    }
}
