using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public struct DoAccelerate
    {
        public DoAccelerate(Unit_Animator_Params param, float velocity)
        {
            param.Movement = Mathf.Min(1.0f, param.Movement + velocity);
        }
    }
}
