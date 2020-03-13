using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoHolsterWeapon
    {
        public DoHolsterWeapon(Data data)
        {
            if(data.Params.IsHolstering == true)
            {
                return;
            }

            data.Params.IsHolstering = true;
        }
    }
}
