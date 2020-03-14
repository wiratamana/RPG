using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public struct DoDrawWeapon
    {
        public DoDrawWeapon(Data data)
        {
            if(data.Params.IsEquipping == true)
            {
                return;
            }

            data.Params.IsEquipping = true;
        }
    }
}
