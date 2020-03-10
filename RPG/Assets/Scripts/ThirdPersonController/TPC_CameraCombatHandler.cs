using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraCombatHandler : MonoBehaviour
    {
        public struct DataInfo
        {
            public readonly Vector3 CameraLookPosition;
            public readonly Vector3 CameraPosition;
            public readonly Quaternion CameraRotation;

            public DataInfo(Vector3 cameraLookPosition, Vector3 cameraPosition, Quaternion cameraRotation)
            {
                CameraLookPosition = cameraLookPosition;
                CameraPosition = cameraPosition;
                CameraRotation = cameraRotation;
            }
        }

        private TPC_CameraHandler cameraHandler;
        public TPC_CameraHandler CameraHandler => this.GetAndAssignComponent(ref cameraHandler);

        private Unit_AI_Hostile unitBase;
        private Transform playerRootTransform;
        private Transform playerSpineTransform;
        private Transform enemySpineTransform;
        private Transform mainCamera;
        private Transform cameraLookTransform;

        public DataInfo CameraCombatData { get; private set; }

        private void OnValidate()
        {
            playerRootTransform = CameraHandler.TPC.UnitPlayer.transform;
            playerSpineTransform = CameraHandler.TPC.UnitPlayer.BodyTransform.Spine;
            mainCamera = CameraHandler.MainCamera.transform;
            cameraLookTransform = CameraHandler.CameraLookPoint;

            Deactivate();
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
            var cameraLookPosition = playerSpineTransform.position + (dirToEnemy * halfDistance);
            var deltaTime = Time.deltaTime;

            var dirToCameraPos = (Quaternion.AngleAxis(20, Vector3.up) * -dirToEnemy).normalized * (halfDistance + 2.0f);
            dirToCameraPos.y = 0.0f;

            var camNextPos = cameraLookPosition + dirToCameraPos;
            camNextPos.y += 1.5f;
            var camCurrentPos = mainCamera.position;
            camCurrentPos = Vector3.Lerp(camCurrentPos, camNextPos, 5.0f * deltaTime);

            var cameraLookDir = (cameraLookPosition - camCurrentPos).normalized;
            var lookRotation = Quaternion.LookRotation(cameraLookDir, Vector3.up);
            var camCurrentRot = mainCamera.rotation;
            camCurrentRot = Quaternion.Slerp(camCurrentRot, lookRotation, 5.0f * deltaTime);

            cameraLookTransform.transform.position = playerRootTransform.position;
            cameraLookTransform.transform.rotation = camCurrentRot;

            CameraCombatData = new DataInfo(cameraLookPosition, camCurrentPos, camCurrentRot);
        }

        private void Activate(Unit_AI_Hostile unitBase)
        {
            this.unitBase = unitBase;
            enemySpineTransform = unitBase.BodyTransform.Spine;

            mainCamera.transform.SetParent(null);
        }

        private void Deactivate()
        {
            unitBase = null;
            enemySpineTransform = null;

            mainCamera.transform.SetParent(cameraLookTransform);
        }
    }
}
