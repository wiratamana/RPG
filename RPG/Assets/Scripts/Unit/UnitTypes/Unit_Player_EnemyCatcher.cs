using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Player_EnemyCatcher : MonoBehaviour
    {
        private Unit_Player unitPlayer;
        public Unit_Player UnitPlayer => this.GetAndAssignComponent(ref unitPlayer);

        public EventManager<Unit_AI> OnEnemyCatched { get; } = new EventManager<Unit_AI>();
        public EventManager OnCatchedEnemyReleased { get; } = new EventManager();
        public EventManager OnCatchedNothing { get; } = new EventManager();

        public Unit_AI UnitEnemy { get; private set; }

        private void Awake()
        {
            InputEvent.Instance.Event_CatchEnemy.AddListener(SphereCastEnemy);
        }

        private void SphereCastEnemy()
        {
            if(UnitEnemy != null)
            {
                UnitEnemy.Status.HP.OnCurrentHealthUpdated.RemoveListener(OnEnemyDead);

                UnitEnemy = null;
                OnCatchedEnemyReleased.Invoke();
                return;
            }

            var radius = 10.0f;
            var layer = LayerMask.GetMask(LayerManager.LAYER_AI);

            var colliders = Physics.OverlapSphere(transform.position, radius, layer);
            if (colliders.Length == 0)
            {
                OnCatchedNothing.Invoke();
                return;
            }

            var mainCamera = GameManager.MainCamera;

            UnitEnemy = null;
            var distance = float.MaxValue;
            var myPos = transform.position;
            foreach(var c in colliders)
            {
                if(c.transform.IsInsideCameraFrustum(mainCamera) == false)
                {
                    continue;
                }

                var unitEnemy = c.GetComponent<Unit_AI>();
                if(unitEnemy.Status.IsDead == true || unitEnemy.Behaviour == AIBehaviour.Neutral)
                {
                    continue;
                }

                var dist = Vector3.Distance(myPos, c.transform.position);
                if(dist < distance)
                {
                    distance = dist;
                    UnitEnemy = unitEnemy;
                }
            }

            if(UnitEnemy == null)
            {
                Debug.Log("Unable to catch any enemy!!", Debug.LogType.Error);
                OnCatchedNothing.Invoke();
                return;
            }

            UnitEnemy.Status.HP.OnCurrentHealthUpdated.AddListener(OnEnemyDead);
            OnEnemyCatched.Invoke(UnitEnemy);
        }    
        
        public void Evaluate(IReadOnlyCollection<Unit_AI> hostileList)
        {
            var mainCamera = GameManager.MainCamera;

            foreach (var i in hostileList)
            {
                var camForward = mainCamera.transform.forward;
                var dirToEnemy = (i.transform.position - mainCamera.transform.position).normalized;
                var dotProduct = Vector3.Dot(camForward, dirToEnemy);

                if (dotProduct < 0.9f || i.transform.IsInsideCameraFrustum(mainCamera) == false)
                {
                    continue;
                }

                UI_Battle.Instance.TargetHP.RegisterEnemy(i);
            }
        }

        private void OnEnemyDead(float enemyCurrentHealthRate)
        {
            if(UnitEnemy.Status.IsDead == true)
            {
                UnitEnemy = null;
                OnCatchedEnemyReleased.Invoke();
            }
        }
    }
}
