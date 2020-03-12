using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoReturnToIdlePosition
    {
        private const AIState AI_STATE = AIState.Return;

        public DoReturnToIdlePosition(ref DataPacket packet, ref Quaternion myRotation
             , Vector3 myPosition, Vector3 idlePosition, float rotationSpeed
             , float movementVelocity, float distanceToIdlePosition, float distanceStop
             , PF_Node nodeParent, float distanceToNodeParent)
        {
            packet.State = AI_STATE;
            if(packet.destination == Vector3.zero)
            {
                packet.destination = nodeParent.transform.position;
            }

            var distanceToNextDestination = Vector3.Distance(packet.destination, myPosition);
            var distanceFromNodeParentToIdlePosition = Vector3.Distance(nodeParent.transform.position, idlePosition);

            if(distanceToNextDestination < 1.5f)
            {
                foreach (var n in nodeParent.neighbours)
                {
                    if (n == null)
                    {
                        continue;
                    }

                    var distanceFromNeighbourNodeToIdlePosition = Vector3.Distance(n.transform.position, idlePosition);
                    if (distanceFromNeighbourNodeToIdlePosition < distanceFromNodeParentToIdlePosition)
                    {
                        distanceFromNodeParentToIdlePosition = distanceFromNeighbourNodeToIdlePosition;
                        packet.destination = n.transform.position;
                    }
                }
            }

            if (distanceToIdlePosition < distanceToNextDestination)
            {
                packet.destination = idlePosition;
            }

            UnityEngine.Debug.DrawLine(myPosition, packet.destination, Color.cyan);
            

            //if(destination == nodeParent.transform.position && distanceToNodeParent < 0.5f)
            //{
            //    destination = idlePosition;
            //}

            var directionTowardDestination = Vector3.Normalize(packet.destination - myPosition);
            new DoRotateToward(ref myRotation, directionTowardDestination, rotationSpeed);

            if (distanceToIdlePosition > distanceStop)
            {
                new DoAccelerate(packet.Params, movementVelocity);
            }
            else
            {
                new DoDecelerate(packet.Params, movementVelocity);

                if (packet.Params.Movement == 0.0f)
                {
                    packet.IsAlert = false;
                    packet.State = AIState.Idle;
                }
            }
        }
    }
}
