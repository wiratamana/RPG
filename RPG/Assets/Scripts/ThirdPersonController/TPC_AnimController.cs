using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_AnimController : SingletonMonobehaviour<TPC_AnimController>
    {
        private Animator _characterAnimator;
        private Animator CharacterAnimator
        {
            get
            {
                if(_characterAnimator == null)
                {
                    _characterAnimator = GetComponent<Animator>();

                    if(_characterAnimator == null)
                    {
                        Debug.Log("Couldn't find 'Animator' component.");
                        return null;
                    }
                }

                return _characterAnimator;
            }
        }

        public void PlayAnim(string animName)
        {
            CharacterAnimator.Play(animName);
        }

        public void PlayStartMoveAnimation()
        {
            var animName = GetStartMoveAnimationName();
            if(string.IsNullOrEmpty(animName) == true)
            {
                Debug.Log("TPC_AnimController.GetStartMoveAnimationName() returned empty string.", 
                    Debug.LogType.Warning);
                return;
            }

            PlayAnim(animName);
        }

        public string GetStartMoveAnimationName()
        {
            var angle = TPC_CameraMovementManager.Instance.CameraAngleFromPlayerForward;

            if (angle < 45 && angle > -45)
            {
                return TPC_Anim_RunAnimsetBasic.RunFwdStart;
            }
            else if(angle < 120 && angle >= 45)
            {
                return TPC_Anim_RunAnimsetBasic.RunFwdStart90_L;
            }
            else if (angle > -120 && angle <= -45)
            {
                return TPC_Anim_RunAnimsetBasic.RunFwdStart90_R;
            }
            else if (angle <= 179.99 && angle >= 100)
            {
                return TPC_Anim_RunAnimsetBasic.RunFwdStart180_L;
            }
            else if (angle >= -180 && angle <= -100)
            {
                return TPC_Anim_RunAnimsetBasic.RunFwdStart180_R;
            }

            return "";
        }
    }
}
