using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AI_Enemy_CombatLogic : MonoBehaviour
    {    
        private AI_Brain brain;
        public bool IsBrainInstalled => brain != null;

        public void InstallBrain(AI_Brain brain)
        {
            if (IsBrainInstalled == true)
            {
                Debug.Log("Brain was already installed!!");
                return;
            }

            Debug.Log($"Brain was installed!! Brain name is '{brain.name}'");
            this.brain = brain;
        }

        private void Update()
        {
            brain.Update();
        }
    }
}