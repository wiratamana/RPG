using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Player : Unit_Base
    {
        private Status_Player status;
        private TPC_Main tpc;

        public Status_Player Status => this.GetOrAddAndAssignComponent(ref status);
        public TPC_Main TPC => this.GetOrAddAndAssignComponent(ref tpc);

        protected override void OnValidate()
        {
            base.OnValidate();

            this.LogErrorIfComponentIsNull(Status);
            this.LogErrorIfComponentIsNull(TPC);
        }
    }
}
