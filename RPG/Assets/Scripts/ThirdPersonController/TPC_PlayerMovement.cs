using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public class TPC_PlayerMovement : SingletonMonobehaviour<TPC_PlayerMovement>
    {
        public TPC_RotateBeforeStartMoveAnimPlayHandler StartRotateAnimHandler { private set; get; }

        protected override void Awake()
        {
            base.Awake();

            StartRotateAnimHandler = GameManager.Player.gameObject.AddComponent<TPC_RotateBeforeStartMoveAnimPlayHandler>();
        }

        private void Update()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            if (TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeDisableMovement)] == true)
            {
                return;
            }

            if (KeyboardController.IsForwardPressed == true &&
                TPC_AnimController.Instance.AnimParams.IsMoving == false &&
                TPC_AnimController.Instance.AnimParams.IsRotateBeforeMove == false)
            {
                StartRotateAnimHandler.OnRotateCompleted.AddListener(TPC_AnimController.Instance.PlayStartMoveAnimation);
                TPC_AnimController.Instance.AnimParams.IsRotateBeforeMove = true;
            }

            else if (KeyboardController.IsForwardPressed == false)
            {
                TPC_AnimController.Instance.PlayStopMoveAnimation();
                TPC_AnimController.Instance.AnimParams.IsMoving = false;
            }

            if (TPC_AnimController.Instance.AnimParams.IsMoving == true)
            {
                var cameraForward = GameManager.MainCamera.transform.forward;
                cameraForward.y = 0;
                cameraForward = cameraForward.normalized;

                var lookRotation = Quaternion.LookRotation(cameraForward);
                GameManager.Player.transform.rotation = Quaternion.Slerp(GameManager.Player.transform.rotation, lookRotation, 5 * Time.deltaTime);
            }
        }
        
        public string GetStartMoveAnimationName(float angle)
        {
            return TPC_Anim_RunAnimsetBasic.RunFwdLoop;
        }
    }
}
