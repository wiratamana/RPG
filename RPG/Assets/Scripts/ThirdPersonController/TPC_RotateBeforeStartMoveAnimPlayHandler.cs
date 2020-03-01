using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_RotateBeforeStartMoveAnimPlayHandler : MonoBehaviour
    {
        private Unit_Base unit;
        public Unit_Base Unit => this.GetAndAssignComponent(ref unit);

        public EventManager OnRotateCompleted { private set; get; } = new EventManager();

        private void Update()
        {
            if(Unit.UnitAnimator.Params.IsRotateBeforeMove == false)
            {
                return;
            }

            var cameraForward = GameManager.MainCamera.transform.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;

            var lookRotation = Quaternion.LookRotation(cameraForward);
            GameManager.PlayerTransform.transform.rotation = Quaternion.RotateTowards(GameManager.PlayerTransform.transform.rotation, lookRotation, 1080 * Time.deltaTime);

            var camAngle = TPC_CameraMovementManager.Instance.CameraAngleFromPlayerForward;
            if (Mathf.Abs(camAngle) < 5.0f)
            {
                Unit.UnitAnimator.Params.IsRotateBeforeMove = false;
                OnRotateCompleted.Invoke();
            }
        }
    }
}
