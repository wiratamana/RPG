using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraMovementManager : SingletonMonobehaviour<TPC_CameraMovementManager>
    {
        [SerializeField] private float _offsetY;
        [SerializeField] private float _offsetZ;
        [SerializeField] private float _cameraLookHeight;

        public Camera Camera { private set; get; }
        public Transform CameraLookPoint { private set; get; }
        public TPC_AnimController AnimController { private set; get; }
        public TPC_PlayerMovement PlayerMovement { private set; get; }

        public float CameraAngleFromPlayerForward
        {
            get
            {
                var cameraForward = VectorHelper.GetForward2DWithZ0(Camera.transform.forward);
                var playerForward = VectorHelper.GetForward2DWithZ0(transform.forward);

                var cameraAngle = Vector2.SignedAngle(playerForward, cameraForward);

                return cameraAngle;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            CreateThirdPersonCamera();
            InstantiateAnimControllerOnPlayer();
            InstantiatePlayerMovementOnPlayer();
        }

        private void Update()
        {
            CameraLookPoint.transform.position = Vector3.Lerp(CameraLookPoint.transform.position, transform.position, 5 * Time.deltaTime);

            RotateCamera();                
        }

        private void RotateCamera()
        {
            var eulerAngle = CameraLookPoint.eulerAngles;
            eulerAngle.x += KeyboardController.MouseVertical;
            eulerAngle.y += KeyboardController.MouseHorizontal;

            CameraLookPoint.transform.rotation = Quaternion.Euler(eulerAngle.x, eulerAngle.y, 0);
        }

        private void CreateThirdPersonCamera()
        {
            if (gameObject.tag != TagManager.TAG_PLAYER)
            {
                Debug.Log($"This gameObject doesn't have '{TagManager.TAG_PLAYER}' tag.", Debug.LogType.ForceQuit);
                return;
            }

            CameraLookPoint = new GameObject(nameof(CameraLookPoint)).transform;
            CameraLookPoint.localPosition = new Vector3(0, _cameraLookHeight, 0);
            CameraLookPoint.gameObject.tag = TagManager.TAG_PLAYER_CAMERA_LOOK_POINT;

            var lookDirection = (transform.position + Vector3.forward - transform.position).normalized;
            lookDirection.y = 0;
            CameraLookPoint.rotation = Quaternion.LookRotation(lookDirection);

            Camera = new GameObject(nameof(Camera)).AddComponent<Camera>();
            Camera.transform.SetParent(CameraLookPoint);
            Camera.transform.localPosition = new Vector3(0, _offsetY, _offsetZ);
            Camera.gameObject.AddComponent<TPC_CameraLookPlayer>();
            Camera.gameObject.tag = TagManager.TAG_MAIN_CAMERA;
        }

        private void InstantiateAnimControllerOnPlayer()
        {
            AnimController = gameObject.AddComponent<TPC_AnimController>();
        }

        private void InstantiatePlayerMovementOnPlayer()
        {
            PlayerMovement = gameObject.AddComponent<TPC_PlayerMovement>();
        }
    }
}
