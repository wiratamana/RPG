using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_RotateBeforeStartMoveAnimPlayHandler : MonoBehaviour
    {
        private TPC_Movement playerMovement;
        public TPC_Movement PlayerMovement => this.GetAndAssignComponent(ref playerMovement);

        public EventManager OnRotateCompleted { private set; get; } = new EventManager();

        private void Update()
        {
            if(PlayerMovement.TPC.UnitPlayer.UnitAnimator.Params.IsRotateBeforeMove == false)
            {
                return;
            }

            var cameraForward = GameManager.MainCameraTransform.transform.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;

            var lookRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 1080 * Time.deltaTime);

            var camAngle = PlayerMovement.TPC.CameraHandler.CameraAngleFromPlayerForward;
            if (Mathf.Abs(camAngle) < 5.0f)
            {
                PlayerMovement.TPC.UnitPlayer.UnitAnimator.Params.IsRotateBeforeMove = false;
                OnRotateCompleted.Invoke();
            }
        }
    }
}
