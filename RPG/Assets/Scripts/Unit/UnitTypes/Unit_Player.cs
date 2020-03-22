using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Player : Unit_Base
    {
        private TPC_Main tpc;
        private Unit_Player_EnemyCatcher enemyCatcher;
        private Unit_Player_AICatcher aiCatcher;
        private Unit_Player_NeutralCatcher neutralCatcher;

        public TPC_Main TPC => this.GetOrAddAndAssignComponent(ref tpc);
        public Unit_Player_EnemyCatcher EnemyCatcher => this.GetOrAddAndAssignComponent(ref enemyCatcher);
        public Unit_Player_AICatcher AICatcher => this.GetOrAddAndAssignComponent(ref aiCatcher);
        public Unit_Player_NeutralCatcher NeutralCathcer => this.GetOrAddAndAssignComponent(ref neutralCatcher);

        protected override void OnValidate()
        {
            base.OnValidate();

            this.LogErrorIfComponentIsNull(TPC);
            this.LogErrorIfComponentIsNull(EnemyCatcher);
            this.LogErrorIfComponentIsNull(AICatcher);         
        }

        protected override void Awake()
        {
            base.Awake();

            LoadStatus();
        }

        private void LoadStatus()
        {
            if (Status.IsInitialized == false)
            {
                var statusInfo = Resources.Load<Unit_Status_Information>("PlayerBaseStatus");
                Status.Initialize(Instantiate(statusInfo));
                Resources.UnloadAsset(statusInfo);
            }
        }
    }
}
