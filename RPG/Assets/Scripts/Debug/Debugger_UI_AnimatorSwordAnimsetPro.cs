using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Debugger_UI_AnimatorSwordAnimsetPro : Debugger_UI_WindowBase
    {
        [SerializeField] private Color notPlaying;
        public Color ColorNotPlaying { get { return notPlaying; } }
        [SerializeField] private Color playing;
        public Color ColorPlaying { get { return playing; } }
        [SerializeField] private Color startMoving;
        public Color ColorStartMoving { get { return startMoving; } }

        private Debugger_AnimatorSwordAnimSetPro_Movement movement;
        public Debugger_AnimatorSwordAnimSetPro_Movement Movement
        {
            get
            {
                if(movement == null)
                {
                    movement = GetComponentInChildren<Debugger_AnimatorSwordAnimSetPro_Movement>();

                    if(movement == null)
                    {
                        Debug.Log($"Couldn't find '{nameof(Debugger_AnimatorSwordAnimSetPro_Movement)}' component from child", Debug.LogType.Error);
                    }
                }

                return movement;
            }
        }
        private Debugger_AnimatorSwordAnimSetPro_StrafeLocomotion strafeLocomotion;
        public Debugger_AnimatorSwordAnimSetPro_StrafeLocomotion StrafeLocomotion
        {
            get
            {
                if (strafeLocomotion == null)
                {
                    strafeLocomotion = GetComponentInChildren<Debugger_AnimatorSwordAnimSetPro_StrafeLocomotion>();

                    if (strafeLocomotion == null)
                    {
                        Debug.Log($"Couldn't find '{nameof(Debugger_AnimatorSwordAnimSetPro_StrafeLocomotion)}' component from child", Debug.LogType.Error);
                    }
                }

                return strafeLocomotion;
            }
        }
        private Debugger_AnimatorSwordAnimSetPro_Combat combat;
        public Debugger_AnimatorSwordAnimSetPro_Combat Combat
        {
            get
            {
                if (combat == null)
                {
                    combat = GetComponentInChildren<Debugger_AnimatorSwordAnimSetPro_Combat>();

                    if (combat == null)
                    {
                        Debug.Log($"Couldn't find '{nameof(Debugger_AnimatorSwordAnimSetPro_Combat)}' component from child", Debug.LogType.Error);
                    }
                }

                return combat;
            }
        }
    }
}
