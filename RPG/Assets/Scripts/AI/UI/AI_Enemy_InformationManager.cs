using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AI_Enemy_InformationManager : SingletonMonobehaviour<AI_Enemy_InformationManager>
    {
        private AI_Enemy_HostilitySign hostilitySign;
        public AI_Enemy_HostilitySign HostilitySign
        {
            get
            {
                if(hostilitySign == null)
                {
                    hostilitySign = GetComponentInChildren<AI_Enemy_HostilitySign>();
                }

                return hostilitySign;
            }
        }
    }
}
