using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AI_Enemy_Base : MonoBehaviour
    {
        private Status_Main statusMain;
        private AI_Enemy_CombatLogic combatLogic;
        private Unit_AI_Hostile unit;

        public Status_Main StatusMain => this.GetOrAddAndAssignComponent(ref statusMain);
        public AI_Enemy_CombatLogic CombatLogic => this.GetOrAddAndAssignComponent(ref combatLogic);        
        public Unit_AI_Hostile Unit => this.GetOrAddAndAssignComponent(ref unit);

        protected virtual void Awake()
        {
            Unit.CombatHandler.DamageHandler.OnReceivedDamageEvent.AddListener(OnReceivedDamage);
            StatusMain.OnDeadEvent.AddListener(OnDead);
        }

        private void Start()
        {
            var brain = ScriptableObject.CreateInstance<AI_Brain_Enemy_Dummy>();
            brain.name = nameof(AI_Brain_Enemy_Dummy);
            brain.Init(this);
            CombatLogic.InstallBrain(brain);
        }

        private void OnReceivedDamage(Status_DamageData receivedDamage)
        {
            StatusMain.HP.Damage(receivedDamage);

            if(StatusMain.IsDead == false)
            {
                //EnemyAnimator.Play("Sword1h_Hit_Torso_Front");
            }
        }

        private void OnDead()
        {
            //EnemyAnimator.Play("Sword1h_Death_Front");
        }
    }
}
