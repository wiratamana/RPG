using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public class TPC_PlayerMovement : SingletonMonobehaviour<TPC_PlayerMovement>
    {
        private void Update()
        {
            if(TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeDisableMovement)] == true)
            {
                return;
            }

            if (KeyboardController.IsForwardPressed == true && 
                TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeMoving)] == false)
            {
                TPC_AnimController.Instance.PlayStartMoveAnimation();
            }

            else if (KeyboardController.IsForwardPressed == false && 
                     TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeMoving)] == true &&
                     TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeMoveStopping)] == false)
            {
                TPC_AnimController.Instance.PlayStopMoveAnimation();
            }

            if (TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeMoving)] == true &&
                TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeMoveStopping)] == false &&
                TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeMoveStarting)] == false)
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
            if (angle < 45 && angle > -45)
            {
                return TPC_Anim_RunAnimsetBasic.RunFwdStart;
            }
            else if (angle < 120 && angle >= 45)
            {
                return TPC_Anim_RunAnimsetBasic.RunFwdStart90_L;
            }
            else if (angle > -120 && angle <= -45)
            {
                return TPC_Anim_RunAnimsetBasic.RunFwdStart90_R;
            }
            else if (angle <= 179.99 && angle >= 120)
            {
                return TPC_Anim_RunAnimsetBasic.RunFwdStart180_L;
            }
            else if (angle >= -180 && angle <= -120)
            {
                return TPC_Anim_RunAnimsetBasic.RunFwdStart180_R;
            }

            return null;
        }
    }
}
