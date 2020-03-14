using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Player : Unit_Base
    {
        private TPC_Main tpc;
        private Unit_Player_EnemyCatcher enemyCatcher;

        public TPC_Main TPC => this.GetOrAddAndAssignComponent(ref tpc);
        public Unit_Player_EnemyCatcher EnemyCatcher => this.GetOrAddAndAssignComponent(ref enemyCatcher);

        protected override void OnValidate()
        {
            base.OnValidate();

            this.LogErrorIfComponentIsNull(TPC);
            this.LogErrorIfComponentIsNull(EnemyCatcher);

            var statusInfo = Resources.Load<Unit_Status_Information>("PlayerBaseStatus");
            Status.Initialize(Instantiate(statusInfo));
            Resources.UnloadAsset(statusInfo);
        }

        protected override void Awake()
        {
            base.Awake();

            this.LogErrorIfComponentIsNull(TPC);
            this.LogErrorIfComponentIsNull(EnemyCatcher);

            var statusInfo = Resources.Load<Unit_Status_Information>("PlayerBaseStatus");
            Status.Initialize(Instantiate(statusInfo));
            Resources.UnloadAsset(statusInfo);
        }
    }
}
