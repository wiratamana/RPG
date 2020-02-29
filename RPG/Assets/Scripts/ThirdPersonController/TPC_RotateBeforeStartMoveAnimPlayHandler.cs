using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_RotateBeforeStartMoveAnimPlayHandler : MonoBehaviour
    {
        public EventManager OnRotateCompleted { private set; get; } = new EventManager();

        private void Update()
        {
            if(TPC_AnimController.Instance.AnimParams.IsRotateBeforeMove == false)
            {
                return;
            }

            var cameraForward = GameManager.MainCamera.transform.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;

            var lookRotation = Quaternion.LookRotation(cameraForward);
            //GameManager.Player.transform.rotation = Quaternion.Slerp(GameManager.Player.transform.rotation, lookRotation, 5 * Time.deltaTime);
            GameManager.PlayerTransform.transform.rotation = Quaternion.RotateTowards(GameManager.PlayerTransform.transform.rotation, lookRotation, 1080 * Time.deltaTime);

            var camAngle = TPC_CameraMovementManager.Instance.CameraAngleFromPlayerForward;
            if (Mathf.Abs(camAngle) < 5.0f)
            {
                TPC_AnimController.Instance.AnimParams.IsRotateBeforeMove = false;
                OnRotateCompleted.Invoke();
                OnRotateCompleted.RemoveAllListener();
            }
        }
    }
}
