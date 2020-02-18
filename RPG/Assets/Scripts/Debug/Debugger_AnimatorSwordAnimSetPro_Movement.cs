using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    public class Debugger_AnimatorSwordAnimSetPro_Movement : MonoBehaviour
    {
        private Debugger_UI_AnimatorSwordAnimsetPro swordAnimsetPro;
        public Debugger_UI_AnimatorSwordAnimsetPro SwordAnimsetPro
        {
            get
            {
                if (swordAnimsetPro == null)
                {
                    swordAnimsetPro = GetComponentInParent<Debugger_UI_AnimatorSwordAnimsetPro>();

                    if (swordAnimsetPro == null)
                    {
                        Debug.Log($"Couldn't find '{nameof(Debugger_UI_AnimatorSwordAnimsetPro)}' component from parent", Debug.LogType.Error);
                    }
                }

                return swordAnimsetPro;
            }
        }

        [SerializeField] private Image Sword1h_Idle;
        [SerializeField] private Image Sword1h_WalkFwdStart;
        [SerializeField] private Image Sword1h_WalkFwdLoop;
        [SerializeField] private Image Sword1h_WalkFwdStart90_R;
        [SerializeField] private Image Sword1h_WalkFwdStart135_R;
        [SerializeField] private Image Sword1h_WalkFwdStart180_R;
        [SerializeField] private Image Sword1h_WalkFwdStart90_L;
        [SerializeField] private Image Sword1h_WalkFwdStart135_L;
        [SerializeField] private Image Sword1h_WalkFwdStart180_L;
        [SerializeField] private Image Sword1h_Equip;
        [SerializeField] private Image Sword1h_Holter;
        [SerializeField] private Image Sword1h_StrafeLtStop_LU;
        [SerializeField] private Image Sword1h_StrafeLtStop_RU;
        [SerializeField] private Image Sword1h_StrafeRtStop_LU;
        [SerializeField] private Image Sword1h_StrafeRtStop_RU;
        [SerializeField] private Image StrafeAnimBlendTree;

        private void Update()
        {
            SetColor(Sword1h_Idle, nameof(Sword1h_Idle));
            SetColor(Sword1h_WalkFwdStart, nameof(Sword1h_WalkFwdStart));
            SetColor(Sword1h_WalkFwdLoop, nameof(Sword1h_WalkFwdLoop));
            SetColor(Sword1h_WalkFwdStart90_R, nameof(Sword1h_WalkFwdStart90_R));
            SetColor(Sword1h_WalkFwdStart135_R, nameof(Sword1h_WalkFwdStart135_R));
            SetColor(Sword1h_WalkFwdStart180_R, nameof(Sword1h_WalkFwdStart180_R));
            SetColor(Sword1h_WalkFwdStart90_L, nameof(Sword1h_WalkFwdStart90_L));
            SetColor(Sword1h_WalkFwdStart135_L, nameof(Sword1h_WalkFwdStart135_L));
            SetColor(Sword1h_WalkFwdStart180_L, nameof(Sword1h_WalkFwdStart180_L));
            SetColor(Sword1h_Holter, nameof(Sword1h_Holter));
            SetColor(Sword1h_StrafeLtStop_LU, nameof(Sword1h_StrafeLtStop_LU));
            SetColor(Sword1h_StrafeLtStop_RU, nameof(Sword1h_StrafeLtStop_RU));
            SetColor(Sword1h_StrafeRtStop_LU, nameof(Sword1h_StrafeRtStop_LU));
            SetColor(Sword1h_StrafeRtStop_RU, nameof(Sword1h_StrafeRtStop_RU));
            SetColor(StrafeAnimBlendTree, nameof(StrafeAnimBlendTree));
        }

        private void SetColor(Image img, string varName)
        {
            img.color = SwordAnimsetPro.ColorNotPlaying;

            if (TPC_AnimController.Instance.IsPlaying(varName) == true)
            {
                img.color = SwordAnimsetPro.ColorPlaying;
            }   
        }
    }
}
