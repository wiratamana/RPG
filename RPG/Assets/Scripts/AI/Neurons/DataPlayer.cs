using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DataPlayer
    {
        public readonly Vector3 Position;
        public readonly Vector2 LookDirection;
        public readonly float Distance;

        public DataPlayer(Unit_Base ai, Unit_Base player)
        {
            Position = player.transform.position;
            LookDirection = player.transform.forward;

            Distance = Vector3.Distance(ai.transform.position, Position);
        }
    }
}
