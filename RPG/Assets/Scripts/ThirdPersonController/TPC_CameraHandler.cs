using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraHandler : MonoBehaviour
    {
        private TPC_Main tpc;
        public TPC_Main TPC => this.GetAndAssignComponent(ref tpc);

        private TPC_CameraLookPlayer cameraLookPlayer;
        private TPC_CameraMovementManager cameraMovement;
        private TPC_CameraCombatHandler cameraCombatHandler;

        public TPC_CameraLookPlayer CameraLookPlayer => this.GetOrAddAndAssignComponent(ref cameraLookPlayer);
        public TPC_CameraMovementManager CameraMovement => this.GetOrAddAndAssignComponent(ref cameraMovement);
        public TPC_CameraCombatHandler CameraCombatHandler => this.GetOrAddAndAssignComponent(ref cameraCombatHandler);

        [SerializeField] private float _offsetY;
        [SerializeField] private float _offsetZ;
        [SerializeField] private float _cameraLookHeight;

        public float OffsetY => _offsetY;
        public float OffsetZ => _offsetZ;
        public float CameraLookHeight => _cameraLookHeight;

        public float CameraAngleFromPlayerForward
        {
            get
            {
                var cameraForward = VectorHelper.GetForward2DWithZ0(MainCamera.transform.forward);
                var playerForward = VectorHelper.GetForward2DWithZ0(transform.forward);

                var cameraAngle = Vector2.SignedAngle(playerForward, cameraForward);

                return cameraAngle;
            }
        }

        private Transform cameraLookPointTransform;
        private Camera mainCamera;

        public Camera MainCamera
        {
            get
            {
                if(mainCamera == null)
                {
                    var go = GameObject.FindWithTag(TagManager.TAG_MAIN_CAMERA);
                    if (go != null)
                    {
                        mainCamera = go.GetComponent<Camera>();
                        return mainCamera;
                    }

                    mainCamera = new GameObject(nameof(MainCamera)).AddComponent<Camera>();
                    mainCamera.transform.SetParent(CameraLookPoint.transform);
                    mainCamera.transform.localPosition = new Vector3(0, 0, OffsetZ);
                    mainCamera.gameObject.tag = TagManager.TAG_MAIN_CAMERA;
                }

                return mainCamera;
            }
        }
        public Transform CameraLookPoint
        {
            get
            {
                if(cameraLookPointTransform == null)
                { 
                    var go = GameObject.FindWithTag(TagManager.TAG_PLAYER_CAMERA_LOOK_POINT);
                    if (go != null)
                    {
                        cameraLookPointTransform = go.transform;
                        return cameraLookPointTransform;
                    }

                    if (gameObject.tag != TagManager.TAG_PLAYER)
                    {
                        Debug.Log($"This gameObject doesn't have '{TagManager.TAG_PLAYER}' tag.", Debug.LogType.ForceQuit);
                        return null;
                    }

                    cameraLookPointTransform = new GameObject(nameof(CameraLookPoint)).transform;
                    cameraLookPointTransform.localPosition = new Vector3(0, CameraLookHeight, 0);
                    cameraLookPointTransform.gameObject.tag = TagManager.TAG_PLAYER_CAMERA_LOOK_POINT;

                    var playerForwardPosition = transform.position + Vector3.forward - transform.position;
                    playerForwardPosition.y = 0;

                    var defaultCameraPosition = transform.position + new Vector3(0, OffsetY, OffsetZ);
                    var defaultCameraLookDirection = transform.position - defaultCameraPosition;

                    cameraLookPointTransform.rotation = Quaternion.LookRotation(defaultCameraLookDirection.normalized);
                }

                return cameraLookPointTransform;
            }
        }

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(CameraLookPlayer);
            this.LogErrorIfComponentIsNull(CameraMovement);
            this.LogErrorIfComponentIsNull(CameraCombatHandler);
        }

        private void Awake()
        {
            TPC.UnitPlayer.EnemyCatcher.OnEnemyCatched.AddListener(SetActiveCameraCombatHandler);
            TPC.UnitPlayer.EnemyCatcher.OnCatchedEnemyReleased.AddListener(SetActiveCameraNormal);
        }

        private void SetActiveCameraCombatHandler(Unit_AI_Hostile enemy)
        {
            CameraLookPlayer.enabled = false;
            CameraMovement.enabled = false;
            CameraCombatHandler.enabled = true;
        }

        private void SetActiveCameraNormal()
        {

            CameraLookPlayer.enabled = true;
            CameraMovement.enabled = true;
            CameraCombatHandler.enabled = false;

            CameraLookPlayer.SetCameraLocalPositionToZero();
        }
    }
}