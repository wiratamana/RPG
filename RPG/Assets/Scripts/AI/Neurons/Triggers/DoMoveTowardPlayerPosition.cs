using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoMoveTowardPlayerPosition
    {
        private const AIState AI_STATE = AIState.Chase;

        public DoMoveTowardPlayerPosition(Data data)
        {
            data.State = AI_STATE;

            new DoRotateTowardPlayer(ref data.MyRotation, data.DirectionTowardPlayer, data.RotationSpeed);

            if(data.DistanceToPlayer > data.DistanceStop)
            {
                new DoAccelerate(data.Params, data.MovementVelocity);
            }
            else
            {
                new DoDecelerate(data.Params, data.MovementVelocity); 

                if(data.Params.Movement == 0.0f)
                {
                    data.State = AIState.Idle;
                }
            }
        }
    }
}
