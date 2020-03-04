using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_RotateBeforeStartMoveAnimPlayHandler : MonoBehaviour
    {
        private TPC_PlayerMovement playerMovement;
        public TPC_PlayerMovement PlayerMovement => this.GetAndAssignComponent(ref playerMovement);

        public EventManager OnRotateCompleted { private set; get; } = new EventManager();

        private void Update()
        {
            if(PlayerMovement.TPC.Unit.UnitAnimator.Params.IsRotateBeforeMove == false)
            {
                return;
            }

            var cameraForward = GameManager.MainCamera.transform.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;

            var lookRotation = Quaternion.LookRotation(cameraForward);
            GameManager.PlayerTransform.transform.rotation = Quaternion.RotateTowards(GameManager.PlayerTransform.transform.rotation, lookRotation, 1080 * Time.deltaTime);

            var camAngle = PlayerMovement.TPC.CameraHandler.CameraAngleFromPlayerForward;
            if (Mathf.Abs(camAngle) < 5.0f)
            {
                PlayerMovement.TPC.Unit.UnitAnimator.Params.IsRotateBeforeMove = false;
                OnRotateCompleted.Invoke();
            }
        }
    }
}
