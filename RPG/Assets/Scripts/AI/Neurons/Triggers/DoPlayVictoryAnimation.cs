using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI.Neuron
{
    public readonly struct DoPlayVictoryAnimation
    {
        public DoPlayVictoryAnimation(Data data, Prop prop)
        {
            if(prop.isVictory == true)
            {
                if(prop.isPlayingVictoryAnimation == false)
                {
                    if(data.DistanceToIdlePosition > data.DistanceStop)
                    {
                        data.State = AIState.Return;
                    }
                    else
                    {
                        data.IsAlert = false;
                    }                    
                }

                return;
            }

            var enumValues = System.Enum.GetNames(typeof(VictoryAnimation));
            var randomNumber = Random.Range(0, enumValues.Length);
            data.Myself.UnitAnimator.Play(enumValues[randomNumber]);
        }
    }
}
