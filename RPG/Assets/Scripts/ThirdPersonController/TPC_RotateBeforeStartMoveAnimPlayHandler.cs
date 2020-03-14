using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_RotateBeforeStartMoveAnimPlayHandler : MonoBehaviour
    {
        private TPC_Movement playerMovement;
        public TPC_Movement PlayerMovement => this.GetAndAssignComponent(ref playerMovement);

        public EventManager OnRotateCompleted { private set; get; } = new EventManager();

        private Direction direction;

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            var moveDirection = Vector3.zero;

            switch (direction)
            {
                case Direction.Forward:
                    moveDirection = GameManager.MainCameraTransform.forward;
                    break;
                case Direction.Backward:
                    moveDirection = -GameManager.MainCameraTransform.forward;
                    break;
                case Direction.Left:
                    moveDirection = -GameManager.MainCameraTransform.right;
                    break;
                case Direction.Right:
                    moveDirection = GameManager.MainCameraTransform.right;
                    break;
            }

            moveDirection.y = 0;
            moveDirection = moveDirection.normalized;

            var lookRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 1080 * Time.deltaTime);

            var camAngle = PlayerMovement.TPC.CameraHandler.GetCameraAngleFromPlayerForward(direction);
            if (Mathf.Abs(camAngle) < 5.0f)
            {
                PlayerMovement.TPC.UnitPlayer.UnitAnimator.Params.IsRotateBeforeMove = false;
                OnRotateCompleted.Invoke();
                enabled = false;
            }
        }

        public void SetActive(Direction direction)
        {
            this.direction = direction;
            enabled = true;
        }
    }
}
