using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Player : Unit_Base
    {
        private Unit_Status status;
        private TPC_Main tpc;
        private Unit_Player_EnemyCatcher enemyCatcher;

        public Unit_Status Status => this.GetOrAddAndAssignComponent(ref status);
        public TPC_Main TPC => this.GetOrAddAndAssignComponent(ref tpc);
        public Unit_Player_EnemyCatcher EnemyCatcher => this.GetOrAddAndAssignComponent(ref enemyCatcher);

        protected override void OnValidate()
        {
            base.OnValidate();

            this.LogErrorIfComponentIsNull(Status);
            this.LogErrorIfComponentIsNull(TPC);
            this.LogErrorIfComponentIsNull(EnemyCatcher);
        }
    }
}
