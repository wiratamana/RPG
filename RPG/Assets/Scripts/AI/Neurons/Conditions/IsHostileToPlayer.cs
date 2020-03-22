using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public readonly struct IsHostileToPlayer
    {
        public readonly bool Result;

        public IsHostileToPlayer(Data data)
        {
            Result = data.Myself.Behaviour == AIBehaviour.Hostile;
        }

        public static implicit operator bool(IsHostileToPlayer result)
        {
            return result.Result;
        }
    }
}
