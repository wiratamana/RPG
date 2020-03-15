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

            var camAngle = GetCameraAngleFromPlayerForward(direction);
            if (Mathf.Abs(camAngle) < 5.0f)
            {
                PlayerMovement.TPC.UnitPlayer.UnitAnimator.Params.IsRotateBeforeMove = false;
                OnRotateCompleted.Invoke();
                enabled = false;
            }
        }

        public void SetActive(Direction direction)
        {
            if(PlayerMovement.MovementType == MovementType.Strafe)
            {
                return;
            }

            this.direction = direction;
            enabled = true;
        }

        private float GetCameraAngleFromPlayerForward(Direction direction)
        {
            var playerForward = VectorHelper.GetForward2DWithZ0(transform.forward);
            var cameraForward = Vector2.zero;
            var mainCamera = GameManager.MainCameraTransform;

            switch (direction)
            {
                case Direction.Forward:
                    cameraForward = VectorHelper.GetForward2DWithZ0(mainCamera.forward);
                    break;
                case Direction.Backward:
                    cameraForward = VectorHelper.GetForward2DWithZ0(-mainCamera.forward);
                    break;
                case Direction.Left:
                    cameraForward = VectorHelper.GetForward2DWithZ0(-mainCamera.right);
                    break;
                case Direction.Right:
                    cameraForward = VectorHelper.GetForward2DWithZ0(mainCamera.right);
                    break;
            }

            var cameraAngle = Vector2.SignedAngle(playerForward, cameraForward);

            return cameraAngle;
        }
    }
}
