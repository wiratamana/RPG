using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public class TPC_PlayerMovement : SingletonMonobehaviour<TPC_PlayerMovement>
    {
        private Unit_Player unit;
        public Unit_Player Unit => this.GetAndAssignComponent(ref unit);
        public TPC_RotateBeforeStartMoveAnimPlayHandler StartRotateAnimHandler { private set; get; }

        private bool isButtonWPressed;

        protected override void Awake()
        {
            base.Awake();

            StartRotateAnimHandler = GameManager.PlayerTransform.gameObject.AddComponent<TPC_RotateBeforeStartMoveAnimPlayHandler>();
            StartRotateAnimHandler.OnRotateCompleted.AddListener(OnRotationCompleted);

            Unit.UnitAnimator.OnReachMaximumVelocity.AddListener(OnReachMaximumVelocity);
            Unit.UnitAnimator.OnReachZeroVelocity.AddListener(OnReachZeroVelocity);

            InputEvent.Instance.Event_Equip.AddListener(Unit.CombatHandler.Equip);

            InputEvent.Instance.Event_BeginMove.AddListener(OnBeginMove);
            InputEvent.Instance.Event_StopMove.AddListener(OnStopMove);
        }

        private void Update()
        {
            if(Unit.UnitAnimator.Params.IsAccelerating == true)
            {
                Unit.UnitAnimator.Accelerate();
            }

            if(Unit.UnitAnimator.Params.IsDeceleratin == true)
            {
                Unit.UnitAnimator.Decelerate();
            }

            if(Unit.UnitAnimator.Params.IsMoving == true)
            {
                var cameraForward = GameManager.MainCamera.transform.forward;
                cameraForward.y = 0;
                cameraForward = cameraForward.normalized;

                var lookRotation = Quaternion.LookRotation(cameraForward);
                Unit.transform.rotation = Quaternion.Slerp(Unit.transform.rotation, lookRotation, 5 * Time.deltaTime);
            }
        }

        private void OnBeginMove()
        {
            isButtonWPressed = true;

            Unit.UnitAnimator.Params.IsRotateBeforeMove = true;
        }

        private void OnStopMove()
        {
            isButtonWPressed = false;

            Unit.UnitAnimator.Params.IsAccelerating = false; 
            unit.UnitAnimator.Params.IsDeceleratin = true;
        }

        private void OnReachMaximumVelocity()
        {
            Unit.UnitAnimator.Params.IsAccelerating = false;
        }

        private void OnReachZeroVelocity()
        {
            Unit.UnitAnimator.Params.IsDeceleratin = false;
            Unit.UnitAnimator.Params.IsMoving = false;
        }

        private void OnRotationCompleted()
        {
            if(isButtonWPressed == true)
            {
                Unit.UnitAnimator.Params.IsDeceleratin = false;
                Unit.UnitAnimator.Params.IsMoving = true;
                Unit.UnitAnimator.Params.IsAccelerating = true;
            }            
        }            
    }
}
