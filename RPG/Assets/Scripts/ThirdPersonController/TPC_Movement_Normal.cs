using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_Movement_Normal : MonoBehaviour
    {
        private TPC_Movement movement;
        public TPC_Movement Movement => this.GetAndAssignComponent(ref movement);

        private TPC_RotateBeforeStartMoveAnimPlayHandler startRotateAnimHandler;
        public TPC_RotateBeforeStartMoveAnimPlayHandler StartRotateAnimHandler => this.GetOrAddAndAssignComponent(ref startRotateAnimHandler);

        private bool[] pressedMovementButton = new bool[] { false, false, false, false };
        private bool isMoveButtonPressed
        {
            get
            {
                for (int i = 0; i < pressedMovementButton.Length; i++)
                {
                    if (pressedMovementButton[i] == true)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(StartRotateAnimHandler);
        }

        private void Awake()
        {
            StartRotateAnimHandler.OnRotateCompleted.AddListener(OnRotationCompleted);

            Movement.TPC.UnitPlayer.UnitAnimator.OnReachMaximumVelocity.AddListener(OnReachMaximumVelocity);
            Movement.TPC.UnitPlayer.UnitAnimator.OnReachZeroVelocity.AddListener(OnReachZeroVelocity);

            InputEvent.Instance.Event_Equip.AddListener(Movement.TPC.UnitPlayer.CombatHandler.Equip);

            InputEvent.Instance.Event_BeginMove.AddListener(OnBeginMove);
            InputEvent.Instance.Event_StopMove.AddListener(OnStopMove);

            Movement.TPC.UnitPlayer.CombatHandler.AttackHandler.OnAttackAnimationStarted.AddListener(DisableMovement);
            Movement.TPC.UnitPlayer.CombatHandler.AttackHandler.OnAttackAnimationStopped.AddListener(EnableMovement);

            Movement.TPC.UnitPlayer.CombatHandler.UnitAnimator.OnHitAnimationStarted.AddListener(DisableMovement);
            Movement.TPC.UnitPlayer.CombatHandler.UnitAnimator.OnHitAnimationFinished.AddListener(EnableMovement);

            Movement.TPC.UnitPlayer.CombatHandler.UnitAnimator.OnDodgeAnimationStarted.AddListener(DisableMovement);
            Movement.TPC.UnitPlayer.CombatHandler.UnitAnimator.OnDodgeAnimationFinished.AddListener(EnableMovement);
        }

        private void Update()
        {
            if (Movement.TPC.UnitPlayer.UnitAnimator.Params.IsAccelerating == true)
            {
                Movement.TPC.UnitPlayer.UnitAnimator.Accelerate();
            }

            if (Movement.TPC.UnitPlayer.UnitAnimator.Params.IsDecelerating == true)
            {
                Movement.TPC.UnitPlayer.UnitAnimator.Decelerate();
            }

            if (Movement.TPC.UnitPlayer.UnitAnimator.Params.IsMoving == true)
            {
                var cameraForward = pressedMovementButton[(int)Direction.Forward] ? GameManager.MainCameraTransform.forward:
                    pressedMovementButton[(int)Direction.Backward] ? -GameManager.MainCameraTransform.forward : Vector3.zero;

                if(pressedMovementButton[(int)Direction.Right])
                {
                    if(cameraForward == Vector3.zero)
                    {
                        cameraForward = GameManager.MainCameraTransform.right;
                    }
                    else
                    {
                        if(pressedMovementButton[(int)Direction.Forward])
                        {
                            cameraForward = Quaternion.AngleAxis(45, Vector3.up) * cameraForward;
                        }
                        else if (pressedMovementButton[(int)Direction.Backward])
                        {
                            cameraForward = Quaternion.AngleAxis(-45, Vector3.up) * cameraForward;
                        }
                    }                    
                }
                else if(pressedMovementButton[(int)Direction.Left])
                {
                    if(cameraForward == Vector3.zero)
                    {
                        cameraForward = -GameManager.MainCameraTransform.right;
                    }
                    else
                    {
                        if(pressedMovementButton[(int)Direction.Forward])
                        {
                            cameraForward = Quaternion.AngleAxis(-45, Vector3.up) * cameraForward;
                        }
                        else if(pressedMovementButton[(int)Direction.Backward])
                        {
                            cameraForward = Quaternion.AngleAxis(45, Vector3.up) * cameraForward;
                        }
                    }                    
                }

                if (cameraForward == Vector3.zero)
                {
                    cameraForward = GameManager.MainCameraTransform.forward;
                    cameraForward.y = 0;
                }

                cameraForward = cameraForward.normalized;

                var lookRotation = Quaternion.LookRotation(cameraForward);
                Movement.TPC.UnitPlayer.transform.rotation = Quaternion.Slerp(Movement.TPC.UnitPlayer.transform.rotation, lookRotation, 5 * Time.deltaTime);
            }
        }

        private void DisableMovement()
        {
            InputEvent.Instance.Event_BeginMove.RemoveListener(OnBeginMove);
            InputEvent.Instance.Event_StopMove.RemoveListener(OnStopMove);

            Movement.TPC.UnitPlayer.UnitAnimator.SetMovementToZero();
        }

        private void EnableMovement()
        {
            InputEvent.Instance.Event_BeginMove.AddListener(OnBeginMove);
            InputEvent.Instance.Event_StopMove.AddListener(OnStopMove);

            if (KeyboardController.IsForwardPressed == true)
            {
                Movement.TPC.UnitPlayer.UnitAnimator.Params.IsMoving = true;
                Movement.TPC.UnitPlayer.UnitAnimator.Params.IsAccelerating = true;
            }
        }

        private void OnBeginMove(Direction direction)
        {
            if(pressedMovementButton[(int)direction] == true)
            {
                return;
            }

            if(isMoveButtonPressed == false)
            {
                StartRotateAnimHandler.SetActive(direction);
                Movement.TPC.UnitPlayer.UnitAnimator.Params.IsRotateBeforeMove = true;
            }

            pressedMovementButton[(int)direction] = true;
        }

        private void OnStopMove(Direction direction)
        {
            pressedMovementButton[(int)direction] = false;

            if (isMoveButtonPressed == false)
            {
                Movement.TPC.UnitPlayer.UnitAnimator.Params.IsAccelerating = false;
                Movement.TPC.UnitPlayer.UnitAnimator.Params.IsDecelerating = true;
            }            
        }

        private void OnReachMaximumVelocity()
        {
            Movement.TPC.UnitPlayer.UnitAnimator.Params.IsAccelerating = false;
        }

        private void OnReachZeroVelocity()
        {
            Movement.TPC.UnitPlayer.UnitAnimator.Params.IsDecelerating = false;
            Movement.TPC.UnitPlayer.UnitAnimator.Params.IsMoving = false;
        }

        private void OnRotationCompleted()
        {  
            if (isMoveButtonPressed == true)
            {
                Movement.TPC.UnitPlayer.UnitAnimator.Params.IsDecelerating = false;
                Movement.TPC.UnitPlayer.UnitAnimator.Params.IsMoving = true;
                Movement.TPC.UnitPlayer.UnitAnimator.Params.IsAccelerating = true;
            }
        }
    }
}
