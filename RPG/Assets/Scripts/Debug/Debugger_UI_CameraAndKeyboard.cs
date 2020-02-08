using UnityEngine;
using System.Collections;
using TMPro;

namespace Tamana
{
    public class Debugger_UI_CameraAndKeyboard : Debugger_UI_WindowBase
    {
        [SerializeField] private TextMeshProUGUI MouseAxis;
        [SerializeField] private TextMeshProUGUI CameraEulerAngle;
        [SerializeField] private TextMeshProUGUI CameraAndPlayerAngle;

        [SerializeField] private RectTransform DotPlayer;
        [SerializeField] private RectTransform DotCamera;

        private readonly float MaxDistance = 45.0f;

        private Transform _mainCamera;
        private Transform MainCamera
        {
            get
            {
                if (_mainCamera == null)
                {
                    var gameObjectWithMainCameraTag = GameObject.FindGameObjectWithTag(TagManager.TAG_MAIN_CAMERA);
                    if (gameObjectWithMainCameraTag == null)
                    {
                        Debug.Log($"Cannot find gameObject with '{TagManager.TAG_MAIN_CAMERA}' tag.", Debug.LogType.ForceQuit);
                    }

                    _mainCamera = gameObjectWithMainCameraTag.transform;
                }

                return _mainCamera;
            }
        }

        private Transform _player;
        private Transform Player
        {
            get
            {
                if (_player == null)
                {
                    var gameObjectWithMainPlayerTag = GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER);
                    if (gameObjectWithMainPlayerTag == null)
                    {
                        Debug.Log($"Cannot find gameObject with '{TagManager.TAG_PLAYER}' tag.", Debug.LogType.ForceQuit);
                    }

                    _player = gameObjectWithMainPlayerTag.transform;
                }

                return _player;
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        // Update is called once per frame
        void Update()
        {
            var mouseX = string.Format("{0:0.00}", KeyboardController.MouseHorizontal);
            var mouseY = string.Format("{0:0.00}", KeyboardController.MouseVertical);
            MouseAxis.text = $"Mouse Axis X : {mouseX} | Y : {mouseY}";

            var eulerAngles = MainCamera.eulerAngles;
            var eulerX = string.Format("{0:0.00}", eulerAngles.x);
            var eulerY = string.Format("{0:0.00}", eulerAngles.y);
            var eulerZ = string.Format("{0:0.00}", eulerAngles.z);
            CameraEulerAngle.text = $"CameraEulerAngle X : {eulerX} | Y : {eulerY} | Z : {eulerZ}";

            var cameraForward = VectorHelper.GetForwardWithY0(MainCamera.transform.forward) * MaxDistance;
            DotCamera.localPosition = new Vector3(cameraForward.x, cameraForward.z);
            var playerForward = VectorHelper.GetForwardWithY0(Player.transform.forward) * MaxDistance;
            DotPlayer.localPosition = new Vector3(playerForward.x, playerForward.z);

            cameraForward = VectorHelper.GetForward2DWithZ0(MainCamera.transform.forward);
            playerForward = VectorHelper.GetForward2DWithZ0(Player.transform.forward);

            var cameraAngle = Vector2.SignedAngle(playerForward, cameraForward);
            var playerAngle = Vector2.SignedAngle(Vector2.up, playerForward);

            var cameraAngleString = string.Format("{0:0.00}", TPC_CameraMovementManager.Instance.CameraAngleFromPlayerForward);
            var playerAngleString = string.Format("{0:0.00}", playerAngle);
            var camAndPlayerAngle = $"Camera : {cameraAngleString} \n\nPlayer : {playerAngleString}";
            CameraAndPlayerAngle.text = camAndPlayerAngle;
        }
    }
}
