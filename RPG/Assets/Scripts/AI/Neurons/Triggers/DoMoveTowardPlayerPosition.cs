using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoMoveTowardPlayerPosition
    {
        private const AIState AI_STATE = AIState.Chase;

        public DoMoveTowardPlayerPosition(ref DataPacket packet, ref Quaternion myRotation
            , Vector3 directionTowardPlayer, float rotationSpeed, float movementVelocity, float distanceToPlayer
            , float distanceStop)
        {
            packet.State = AI_STATE;

            new DoRotateTowardPlayer(ref myRotation, directionTowardPlayer, rotationSpeed);

            if(distanceToPlayer > distanceStop)
            {
                new DoAccelerate(packet.Params, movementVelocity);
            }
            else
            {
                new DoDecelerate(packet.Params, movementVelocity); 

                if(packet.Params.Movement == 0.0f)
                {
                    packet.State = AIState.Idle;
                }
            }
        }
    }
}
