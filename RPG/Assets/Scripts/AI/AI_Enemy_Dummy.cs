using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AI_Enemy_Dummy : AI_Enemy_Base
    {
        public EventManager<int> DamageEvent { private set; get; } = new EventManager<int>();
    }
}
