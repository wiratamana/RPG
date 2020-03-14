using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct IsTakingDamage
    {
        public readonly bool Result;

        public IsTakingDamage(Data data)
        {
            Result = data.Params.IsTakingDamage;
        }

        public static implicit operator bool(IsTakingDamage result)
        {
            return result.Result;
        }
    }
}
