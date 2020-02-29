﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Player : Unit_Base
    {
        private Status_Player status;
        public Status_Player Status => this.GetOrAddAndAssignComponent(status);
    }
}