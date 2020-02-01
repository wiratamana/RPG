using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

namespace Tamana
{
    public class Debugger_AnimatorState : MonoBehaviour
    {
        [SerializeField] private Color notPlaying;
        [SerializeField] private Color playing;
        [SerializeField] private Color startMoving;

        [SerializeField] private Image RunFwdStart;
        [SerializeField] private Image RunFwdLoop;
        [SerializeField] private Image RunFwdStart90_L;
        [SerializeField] private Image RunFwdStart90_R;
        [SerializeField] private Image RunFwdStart180_R;
        [SerializeField] private Image RunFwdStart180_L;
        [SerializeField] private Image RunFwdStop_RU;
        [SerializeField] private Image RunFwdStop_LU;
        [SerializeField] private Image Idle;

        [SerializeField] private TextMeshProUGUI AnimStateText;

        private Dictionary<string, bool> AnimStateDic;

        private void Start()
        {
            AnimStateDic = (Dictionary<string, bool>)TPC_PlayerMovement.Instance.GetType().GetField(nameof(AnimStateDic),
                BindingFlags.NonPublic | BindingFlags.Instance).GetValue(TPC_PlayerMovement.Instance);
        }

        private void Update()
        {
            SetColor(RunFwdStart, nameof(RunFwdStart));
            SetColor(RunFwdLoop, nameof(RunFwdLoop));
            SetColor(RunFwdStart90_L, nameof(RunFwdStart90_L));
            SetColor(RunFwdStart90_R, nameof(RunFwdStart90_R));
            SetColor(RunFwdStart180_R, nameof(RunFwdStart180_R));
            SetColor(RunFwdStart180_L, nameof(RunFwdStart180_L));
            SetColor(RunFwdStop_RU, nameof(RunFwdStop_RU));
            SetColor(RunFwdStop_LU, nameof(RunFwdStop_LU));
            SetColor(Idle, nameof(Idle));

            SetState();
        }

        private void SetColor(Image img, string varName)
        {
            if (TPC_PlayerMovement.Instance.IsPlaying(varName) == true)
            {
                img.color = playing;
            }
            else
            {
                var anim = TPC_AnimController.Instance.GetStartMoveAnimationName();
                if (anim == TPC_Anim_RunAnimsetBasic.RunFwdStart && varName == anim)
                {
                    img.color = startMoving;
                }
                else if (anim == TPC_Anim_RunAnimsetBasic.RunFwdStart90_L && varName == anim)
                {
                    img.color = startMoving;
                }
                else if (anim == TPC_Anim_RunAnimsetBasic.RunFwdStart90_R && varName == anim)
                {
                    img.color = startMoving;
                }
                else if (anim == TPC_Anim_RunAnimsetBasic.RunFwdStart180_L && varName == anim)
                {
                    img.color = startMoving;
                }
                else if (anim == TPC_Anim_RunAnimsetBasic.RunFwdStart180_R && varName == anim)
                {
                    img.color = startMoving;
                }
                else
                {
                    img.color = notPlaying;
                }                
            }
        }

        private void SetState()
        {
            var sb = new StringBuilder();
            foreach(var state in AnimStateDic)
            {
                sb.AppendLine($"{state.Key} : {state.Value}");
            }

            AnimStateText.text = sb.ToString();
        }
    }
}
