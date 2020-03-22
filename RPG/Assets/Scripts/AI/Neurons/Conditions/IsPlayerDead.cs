using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public readonly struct IsPlayerDead
    {
        public readonly bool Result;

        public IsPlayerDead(Data data)
        {
            Result = data.Player.Status.IsDead;
        }

        public static implicit operator bool(IsPlayerDead result)
        {
            return result.Result;
        }
    }
}
