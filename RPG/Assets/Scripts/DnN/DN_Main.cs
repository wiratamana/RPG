using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class DN_Main : SingletonMonobehaviour<DN_Main>
    {
        private DN_LightingManager lightingManager;
        private DN_UI_Clock clock;

        public DN_LightingManager LightingManager => this.GetOrAddAndAssignComponent(ref lightingManager);
        public DN_UI_Clock Clock => this.GetOrAddAndAssignComponent(ref clock);

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(LightingManager);
            this.LogErrorIfComponentIsNull(Clock);
        }
    }
}
