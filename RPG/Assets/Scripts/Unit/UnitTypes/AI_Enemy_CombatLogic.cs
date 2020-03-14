using UnityEngine;
using System.Collections;
using Tamana.AI;

namespace Tamana
{
    public class AI_Enemy_CombatLogic : MonoBehaviour
    {
        private Unit_AI_Hostile unitEnemy;
        public Unit_AI_Hostile UnitEnemy => this.GetAndAssignComponent(ref unitEnemy);

        private AI_Brain brain;
        public bool IsBrainInstalled => brain != null;

        private void OnValidate()
        {
            enabled = IsBrainInstalled;
        }

        private void Awake()
        {
            enabled = IsBrainInstalled;

            UnitEnemy.Status.HP.OnCurrentHealthUpdated.AddListener(OnCurrentHealthUpdated);            
        }

        private void Update()
        {
            brain.Update();
        }

        public void InstallBrain(AI_Brain brain)
        {
            if (IsBrainInstalled == true)
            {
                Debug.Log("Brain was already installed!!");
                return;
            }

            Debug.Log($"Brain was installed!! Brain name is '{brain.name}'");
            this.brain = brain;
            enabled = true;
        }

        private void OnCurrentHealthUpdated(float currentHP)
        {
            if(UnitEnemy.Status.IsDead == true)
            {
                enabled = false;
                Debug.Log("Brain was shut down!");
            }
        }
    }
}