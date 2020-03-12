using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DataMyself
    {
        public readonly Vector3 IdlePosition;
        public readonly Vector3 CurrentPosition;
        public readonly Vector3 DirectionTowardPlayer;
        public readonly float DistanceFromPlayerPositionToIdlePosition;
        public readonly float DistanceToIdlePosition;

        public DataMyself(Unit_Base ai, Vector3 playerPosition, Vector3 idlePosition)
        {
            IdlePosition = idlePosition;
            CurrentPosition = ai.transform.position;
            DistanceToIdlePosition = Vector3.Distance(IdlePosition, CurrentPosition);
            DistanceFromPlayerPositionToIdlePosition = Vector3.Distance(idlePosition, playerPosition);
            DirectionTowardPlayer = Vector3.Normalize(playerPosition - CurrentPosition);
        }
    }
}