using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct IsWeaponEquipped
    {
        public readonly bool Result;

        public IsWeaponEquipped(Data data)
        {
            Result = data.Params.IsInCombatState;
        }

        public static implicit operator bool(IsWeaponEquipped result)
        {
            return result.Result;
        }
    }
}
