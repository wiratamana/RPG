using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class DN_Main : SingletonMonobehaviour<DN_Main>
    {
        private DN_LightingManager lightingManager;

        public DN_LightingManager LightingManager => this.GetOrAddAndAssignComponent(ref lightingManager);

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(LightingManager);
        }
    }
}
