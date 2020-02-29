using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AI_Enemy_Base : MonoBehaviour
    {
        private Status_Main statusMain;
        public Status_Main StatusMain
        {
            get
            {
                if (statusMain == null)
                {
                    statusMain = gameObject.GetOrAddComponent<Status_Main>();
                }
                
                return statusMain;
            }
        }

        private Status_DamageHandler damageHandler;
        public Status_DamageHandler DamageHandler
        {
            get
            {
                if (damageHandler == null)
                {
                    damageHandler = gameObject.GetOrAddComponent<Status_DamageHandler>();
                }

                return damageHandler;
            }
        }

        private AI_Enemy_Animator enemyAnimator;
        public AI_Enemy_Animator EnemyAnimator
        {
            get
            {
                if(enemyAnimator == null)
                {
                    enemyAnimator = gameObject.GetOrAddComponent<AI_Enemy_Animator>();
                }

                return enemyAnimator;
            }
        }

        private AI_Enemy_CombatLogic combatLogic;
        public AI_Enemy_CombatLogic CombatLogic
        {
            get
            {
                if (combatLogic == null)
                {
                    combatLogic = gameObject.GetOrAddComponent<AI_Enemy_CombatLogic>();
                }

                return combatLogic;
            }
        }

        protected virtual void Awake()
        {
            DamageHandler.OnReceivedDamageEvent.AddListener(OnReceivedDamage);
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
                EnemyAnimator.Play("Sword1h_Hit_Torso_Front");
            }
        }

        private void OnDead()
        {
            EnemyAnimator.Play("Sword1h_Death_Front");
        }
    }
}
