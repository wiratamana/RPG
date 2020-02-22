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

        protected virtual void Awake()
        {
            DamageHandler.OnReceivedDamageEvent.AddListener(OnReceivedDamage);
        }

        private void OnReceivedDamage(Status_DamageData receivedDamage)
        {
            Debug.Log($"Damage Received : {receivedDamage.damagePoint}");
            EnemyAnimator.Play("Sword1h_Hit_Torso_Front");
        }
    }
}
