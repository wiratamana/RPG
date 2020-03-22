using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI
{
    public class Prop
    {
        public enum PropCode
        {
            isCounter,
            isPlayingVictoryAnimation,
            isVictory,
        }

        public bool isCounter;

        public bool isVictory;
        public bool isPlayingVictoryAnimation;

        public float attackDelay_val;
        public float attackDelay_cur;
    }
}
