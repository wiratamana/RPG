using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public class TPC_PlayerMovement : SingletonMonobehaviour<TPC_PlayerMovement>
    {
        private Unit_Player unit;
        public Unit_Player Unit => this.GetAndAssignComponent(unit);
        public TPC_RotateBeforeStartMoveAnimPlayHandler StartRotateAnimHandler { private set; get; }

        private bool isRun = false;

        protected override void Awake()
        {
            base.Awake();

            StartRotateAnimHandler = GameManager.PlayerTransform.gameObject.AddComponent<TPC_RotateBeforeStartMoveAnimPlayHandler>();
            StartRotateAnimHandler.OnRotateCompleted.AddListener(OnRotationCompleted);
        }

        private void Update()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            if(KeyboardController.IsForwardDown)
            {
                Unit.UnitAnimator.Params.Param_IsRotateBeforeMove = true;
            }     
            
            if(KeyboardController.IsForwardUp)
            {
                isRun = false;
            }

            if(isRun == true)
            {
                var val = Unit.UnitAnimator.Params.Params_Movement;
                Unit.UnitAnimator.Params.Params_Movement = Mathf.Min(1.0f, val + 5 * Time.deltaTime);

                var cameraForward = GameManager.MainCamera.transform.forward;
                cameraForward.y = 0;
                cameraForward = cameraForward.normalized;

                var lookRotation = Quaternion.LookRotation(cameraForward);
                GameManager.PlayerTransform.transform.rotation = Quaternion.Slerp(GameManager.PlayerTransform.transform.rotation, lookRotation, 5 * Time.deltaTime);
            }
            else
            {
                var val = Unit.UnitAnimator.Params.Params_Movement;
                Unit.UnitAnimator.Params.Params_Movement = Mathf.Max(0.0f, val - 5 * Time.deltaTime);
            }
        }

        private void OnRotationCompleted()
        {
            isRun = true;            
        }
        
        public string GetStartMoveAnimationName(float angle)
        {
            return null;
        }
    }
}
