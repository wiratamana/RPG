using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraCombatHandler : MonoBehaviour
    {
        private TPC_CameraHandler cameraHandler;
        public TPC_CameraHandler CameraHandler => this.GetAndAssignComponent(ref cameraHandler);

        private Unit_AI_Hostile unitBase;
        private Transform playerSpineTransform;
        private Transform enemySpineTransform;
        private Transform mainCamera;

        private void OnValidate()
        {
            Deactivate();

            playerSpineTransform = CameraHandler.TPC.UnitPlayer.BodyTransform.Spine;
            mainCamera = CameraHandler.MainCamera.transform;
        }

        private void Awake()
        {
            CameraHandler.TPC.UnitPlayer.EnemyCatcher.OnEnemyCatched.AddListener(Activate);
            CameraHandler.TPC.UnitPlayer.EnemyCatcher.OnCatchedEnemyReleased.AddListener(Deactivate);
        }

        private void Update()
        {
            var playerPos = playerSpineTransform.position;
            var enemyPos = enemySpineTransform.position;
            var halfDistance = Vector3.Distance(playerPos, enemyPos) * 0.5f;
            var dirToEnemy = (enemyPos - playerPos).normalized;
            var cameraFacePoint = playerSpineTransform.position + (dirToEnemy * halfDistance);
            var deltaTime = Time.deltaTime;

            var dirToCameraPos = (Quaternion.AngleAxis(20, Vector3.up) * -dirToEnemy).normalized * (halfDistance + 2.0f);
            dirToCameraPos.y = 0.0f;

            var camNextPos = cameraFacePoint + dirToCameraPos;
            camNextPos.y += 1.5f;
            var camCurrentPos = mainCamera.position;
            camCurrentPos = Vector3.Lerp(camCurrentPos, camNextPos, 5.0f * deltaTime);

            var lookRotation = Quaternion.LookRotation((cameraFacePoint - camCurrentPos).normalized, Vector3.up);
            var camCurrentRot = mainCamera.rotation;
            camCurrentRot = Quaternion.Slerp(camCurrentRot, lookRotation, 5.0f * deltaTime);

            mainCamera.transform.position = camCurrentPos;
            mainCamera.transform.rotation = camCurrentRot;
        }

        private void Activate(Unit_AI_Hostile unitBase)
        {
            this.unitBase = unitBase;
            enemySpineTransform = unitBase.BodyTransform.Spine;
        }

        private void Deactivate()
        {
            unitBase = null;
            enemySpineTransform = null;
        }
    }
}
