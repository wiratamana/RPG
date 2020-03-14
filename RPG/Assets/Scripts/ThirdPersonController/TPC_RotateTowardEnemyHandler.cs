using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_RotateTowardEnemyHandler : MonoBehaviour
    {
        private Unit_AI_Hostile enemy;
        private TPC_Movement movement;
        public TPC_Movement Movement => this.GetOrAddAndAssignComponent(ref movement);

        private void OnValidate()
        {
            enabled = false;
        }

        private void Awake()
        {
            enabled = false;

            GameManager.Player.EnemyCatcher.OnEnemyCatched.AddListener(OnEnemyCatched);
            GameManager.Player.EnemyCatcher.OnCatchedEnemyReleased.AddListener(OnCatchedEnemyReleased);
        }

        private void Update()
        {
            if(Movement.TPC.UnitPlayer.UnitAnimator.Params.IsInDodgingState == true)
            {
                return;
            }

            var myRot = transform.rotation;
            var myPos = transform.position;
            var enemyPos = enemy.transform.position;
            var dirToEnemy = (enemyPos - myPos).normalized;
            var lookRotation = Quaternion.LookRotation(dirToEnemy, Vector3.up);
            myRot = Quaternion.Slerp(myRot, lookRotation, 5.0f * Time.deltaTime);

            transform.rotation = myRot;
        }

        private void OnCatchedEnemyReleased()
        {
            enemy = null;
            enabled = false;
        }

        private void OnEnemyCatched(Unit_AI_Hostile enemy)
        {
            this.enemy = enemy;
            enabled = true;
        }
    }
}
