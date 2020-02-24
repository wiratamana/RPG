using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public abstract class AI_Enemy_Base : MonoBehaviour
    {
        private Animator enemyAnimator;
        public Animator EnemyAnimator
        {
            get
            {
                if(enemyAnimator == null)
                {
                    enemyAnimator = GetComponent<Animator>();
                }

                return enemyAnimator;
            }
        }

        private Status_DamageHandler damageHandler;
        public Status_DamageHandler DamageHandler
        {
            get
            {
                if(damageHandler == null)
                {
                    damageHandler = GetComponent<Status_DamageHandler>();
                }

                if(damageHandler == null)
                {
                    damageHandler = gameObject.AddComponent<Status_DamageHandler>();
                }

                return damageHandler;
            }
        }

        private Status_Main statusMain;
        public Status_Main StatusMain
        {
            get
            {
                if(statusMain == null)
                {
                    statusMain = GetComponent<Status_Main>();
                }

                if(statusMain == null)
                {
                    statusMain = gameObject.AddComponent<Status_Main>();
                }

                return statusMain;
            }
        }
        

        protected virtual void Awake()
        {
            DamageHandler.OnReceivedDamageEvent.AddListener(OnReceivedDamage);
            StatusMain.OnDeadEvent.AddListener(OnDead);
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
