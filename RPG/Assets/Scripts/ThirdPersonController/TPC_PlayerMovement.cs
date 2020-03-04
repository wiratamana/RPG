using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public class TPC_PlayerMovement : MonoBehaviour
    {
        private TPC_Main tpc;
        public TPC_Main TPC => this.GetAndAssignComponent(ref tpc);

        private TPC_RotateBeforeStartMoveAnimPlayHandler startRotateAnimHandler;
        public TPC_RotateBeforeStartMoveAnimPlayHandler StartRotateAnimHandler => this.GetOrAddAndAssignComponent(ref startRotateAnimHandler);

        private bool isButtonWPressed;

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(StartRotateAnimHandler);
        }

        private void Awake()
        {
            StartRotateAnimHandler.OnRotateCompleted.AddListener(OnRotationCompleted);

            TPC.Unit.UnitAnimator.OnReachMaximumVelocity.AddListener(OnReachMaximumVelocity);
            TPC.Unit.UnitAnimator.OnReachZeroVelocity.AddListener(OnReachZeroVelocity);

            InputEvent.Instance.Event_Equip.AddListener(TPC.Unit.CombatHandler.Equip);

            InputEvent.Instance.Event_BeginMove.AddListener(OnBeginMove);
            InputEvent.Instance.Event_StopMove.AddListener(OnStopMove);
        }

        private void Update()
        {
            if(TPC.Unit.UnitAnimator.Params.IsAccelerating == true)
            {
                TPC.Unit.UnitAnimator.Accelerate();
            }

            if(TPC.Unit.UnitAnimator.Params.IsDecelerating == true)
            {
                TPC.Unit.UnitAnimator.Decelerate();
            }

            if(TPC.Unit.UnitAnimator.Params.IsMoving == true)
            {
                var cameraForward = GameManager.MainCamera.transform.forward;
                cameraForward.y = 0;
                cameraForward = cameraForward.normalized;

                var lookRotation = Quaternion.LookRotation(cameraForward);
                TPC.Unit.transform.rotation = Quaternion.Slerp(TPC.Unit.transform.rotation, lookRotation, 5 * Time.deltaTime);
            }
        }

        private void OnBeginMove()
        {
            isButtonWPressed = true;

            TPC.Unit.UnitAnimator.Params.IsRotateBeforeMove = true;
        }

        private void OnStopMove()
        {
            isButtonWPressed = false;

            TPC.Unit.UnitAnimator.Params.IsAccelerating = false;
            TPC.Unit.UnitAnimator.Params.IsDecelerating = true;
        }

        private void OnReachMaximumVelocity()
        {
            TPC.Unit.UnitAnimator.Params.IsAccelerating = false;
        }

        private void OnReachZeroVelocity()
        {
            TPC.Unit.UnitAnimator.Params.IsDecelerating = false;
            TPC.Unit.UnitAnimator.Params.IsMoving = false;
        }

        private void OnRotationCompleted()
        {
            if(isButtonWPressed == true)
            {
                TPC.Unit.UnitAnimator.Params.IsDecelerating = false;
                TPC.Unit.UnitAnimator.Params.IsMoving = true;
                TPC.Unit.UnitAnimator.Params.IsAccelerating = true;
            }            
        }            
    }
}
