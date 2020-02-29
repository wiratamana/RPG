using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Player : Unit_Base
    {
        private Status_Player status;
        private Unit_CombatHandler combatHandler;

        public Status_Player Status => this.GetOrAddAndAssignComponent(status);
        public Unit_CombatHandler CombatHandler => this.GetOrAddAndAssignComponent(combatHandler);

        protected override void OnValidate()
        {
            base.OnValidate();

            this.LogErrorIfComponentIsNull(Status);
            this.LogErrorIfComponentIsNull(CombatHandler);
        }
    }
}
