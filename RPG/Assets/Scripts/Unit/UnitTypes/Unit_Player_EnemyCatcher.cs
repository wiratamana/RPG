using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Player_EnemyCatcher : MonoBehaviour
    {
        private Unit_Player unitPlayer;
        public Unit_Player UnitPlayer => this.GetAndAssignComponent(ref unitPlayer);

        public EventManager<Unit_AI_Hostile> OnEnemyCatched { get; } = new EventManager<Unit_AI_Hostile>();
        public EventManager OnCatchedEnemyReleased { get; } = new EventManager();

        public Unit_AI_Hostile UnitEnemy { get; private set; }

        private void Awake()
        {
            InputEvent.Instance.Event_CatchEnemy.AddListener(SphereCastEnemy);
        }

        private void Start()
        {
            StartCoroutine(PassiveEnemyCatcher());
        }

        private void SphereCastEnemy()
        {
            if(UnitEnemy != null)
            {
                UnitEnemy = null;
                OnCatchedEnemyReleased.Invoke();
                UnitPlayer.UnitAnimator.Params.IsStrafing = false;
                return;
            }

            var radius = 10.0f;
            var layer = LayerMask.GetMask(LayerManager.LAYER_ENEMY);

            var colliders = Physics.OverlapSphere(transform.position, radius, layer);
            if (colliders.Length == 0)
            {
                UnitPlayer.UnitAnimator.Params.IsStrafing = false;
                return;
            }

            UnitEnemy = null;
            var distance = float.MaxValue;
            var myPos = transform.position;
            foreach(var c in colliders)
            {
                var dist = Vector3.Distance(myPos, c.transform.position);
                if(dist < distance)
                {
                    distance = dist;
                    UnitEnemy = c.GetComponent<Unit_AI_Hostile>();
                }
            }

            if(UnitEnemy == null)
            {
                Debug.Log("Unable to catch any enemy!!", Debug.LogType.Error);
            }

            UnitPlayer.UnitAnimator.Params.IsStrafing = true;
            OnEnemyCatched.Invoke(UnitEnemy);
        }

        private IEnumerator PassiveEnemyCatcher()
        {
            var fourTimesPerSeconds = new WaitForSeconds(0.25f);
            var mainCamera = UnitPlayer.TPC.CameraHandler.MainCamera;   

            while(true)
            {
                var radius = 25.0f;
                var layer = LayerMask.GetMask(LayerManager.LAYER_ENEMY);

                var colliders = Physics.OverlapSphere(transform.position, radius, layer);
                foreach (var c in colliders)
                {
                    if(c.transform.IsInsideCameraFrustum(mainCamera) == false)
                    {
                        continue;
                    }

                    UI_Battle.Instance.TargetHP.RegisterEnemy(c.GetComponent<Unit_AI_Hostile>());
                }

                yield return fourTimesPerSeconds;
            }            
        }
    }
}
