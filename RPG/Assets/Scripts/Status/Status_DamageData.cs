﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public struct Status_DamageData
    {
        public int damagePoint;
        public bool isIgnoreDefense;
        public WeaponType weaponType;
        public int[] hitsAnimation;
        public float parryTiming;
        public float damageTiming;
    }
}
