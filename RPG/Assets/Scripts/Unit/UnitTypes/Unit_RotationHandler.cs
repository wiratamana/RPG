using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_RotationHandler : MonoBehaviour
    {
        private Unit_Base unit;
        public Unit_Base Unit => this.GetAndAssignComponent(ref unit);

        private Vector3 targetPosition;
        private float rotationSpeed;

        public EventManager OnRotatingProcessFinished { get; } = new EventManager();
        public EventManager OnRotatingProcessStarted { get; } = new EventManager();
        public EventManager OnRotatingProcessRestarted { get; } = new EventManager();

        private void Awake()
        {
            if (Unit.IsUnitPlayer)
            {
                Unit.CombatHandler.AttackHandler.OnAttackAnimationStarted.AddListener(RotateTowardNearestEnemyEventListener);
                Unit.CombatHandler.AttackHandler.OnConsecutiveAttack.AddListener(RotateTowardNearestEnemyEventListener);
            }

            enabled = false;
        }

        private void Update()
        {
            var directionToTarget = targetPosition - transform.position;
            directionToTarget.y = 0;
            directionToTarget = directionToTarget.normalized;
            var lookRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
            var playerRotation = transform.rotation;
            var rotationValue = Quaternion.Slerp(playerRotation, lookRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = rotationValue;

            var playerForward = VectorHelper.GetForward2DWithZ0(transform.forward);
            var dirToTarget = VectorHelper.GetForward2DWithZ0(directionToTarget);
            var cameraAngle = Vector2.SignedAngle(playerForward, dirToTarget);

            if(cameraAngle < 5.0f)
            {
                enabled = false;
                OnRotatingProcessFinished.Invoke();
            }
        }

        public void RotateToward(Vector3 targetPosition, float rotationSpeed)
        {
            this.targetPosition = targetPosition;
            this.rotationSpeed = rotationSpeed;

            if (enabled == false)
            {
                enabled = true;
                OnRotatingProcessStarted.Invoke();
            }
            else
            {
                OnRotatingProcessRestarted.Invoke();
            }
        }

        public void RotateTowardNearestEnemy(float rotationSpeed)
        {
            this.rotationSpeed = rotationSpeed;
            var enemyPos = Vector3.zero;
            var myPos = transform.position;
            var distance = float.MaxValue; 

            var layer = Unit.IsUnitPlayer ? LayerMask.GetMask(LayerManager.LAYER_ENEMY) :
                LayerMask.GetMask(LayerManager.LAYER_PLAYER);
            var radius = 5.0f;
            var overlap = Physics.OverlapSphere(transform.position, radius, layer);

            if(overlap.Length == 0)
            {
                return;
            }
            
            foreach(var t in overlap)
            {
                var tPos = t.transform.position;
                var curDist = Vector3.Distance(myPos, tPos);
                if(curDist < distance)
                {
                    enemyPos = tPos;
                }
            }

            RotateToward(enemyPos, rotationSpeed);
        }

        private void RotateTowardNearestEnemyEventListener()
        {
            RotateTowardNearestEnemy(5.0f);
        }
    }
}
