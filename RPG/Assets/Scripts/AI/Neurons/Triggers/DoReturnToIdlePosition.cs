using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoReturnToIdlePosition
    {
        private const AIState AI_STATE = AIState.Return;

        public DoReturnToIdlePosition(Data data)
        {
            data.State = AI_STATE;
            if(data.NextDestination == Vector3.zero)
            {
                data.NextDestination = data.NodeParentPosition;
            }

            var isIdlePositionFounded = data.NextDestination == data.IdlePosition;

            var distanceToNextDestination = Vector3.Distance(data.NextDestination, data.MyPosition);
            var distanceFromNodeParentToIdlePosition = Vector3.Distance(data.NodeParentPosition, data.IdlePosition);

            if(isIdlePositionFounded == false && distanceToNextDestination < 1.5f)
            {
                bool isNearestNeighbourNodeFound = false;

                foreach (var n in data.NeighbourNodes)
                {
                    if (n == null)
                    {
                        continue;
                    }

                    var distanceFromNeighbourNodeToIdlePosition = Vector3.Distance(n.transform.position, data.IdlePosition);
                    if (distanceFromNeighbourNodeToIdlePosition < distanceFromNodeParentToIdlePosition)
                    {
                        isNearestNeighbourNodeFound = true;

                        distanceFromNodeParentToIdlePosition = distanceFromNeighbourNodeToIdlePosition;
                        data.NextDestination = n.transform.position;
                    }
                }

                if(isNearestNeighbourNodeFound == false)
                {
                    data.NextDestination = data.IdlePosition;
                }
            }

            UnityEngine.Debug.DrawLine(data.MyPosition, data.NextDestination, Color.cyan);
            
            var directionTowardDestination = Vector3.Normalize(data.NextDestination - data.MyPosition);
            new DoRotateToward(ref data.MyRotation, directionTowardDestination, data.RotationSpeed);

            if (data.DistanceToIdlePosition > data.DistanceStop)
            {
                new DoAccelerate(data.Params, data.MovementVelocity);
            }
            else
            {
                new DoDecelerate(data.Params, data.MovementVelocity);

                if (data.Params.Movement == 0.0f)
                {
                    data.IsAlert = false;
                    data.State = AIState.Idle;
                }
            }
        }
    }
}
