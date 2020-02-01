using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class Debugger_PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Color notPressed;
        [SerializeField] private Color pressed;

        [SerializeField] private Image buttonW;
        [SerializeField] private Image buttonS;
        [SerializeField] private Image buttonA;
        [SerializeField] private Image buttonD;

        // Update is called once per frame
        void Update()
        {
            ChangeButtonColor(KeyboardController.IsForwardPressed, buttonW);
            ChangeButtonColor(KeyboardController.IsBackwardPressed, buttonS);
            ChangeButtonColor(KeyboardController.IsLeftPressed, buttonA);
            ChangeButtonColor(KeyboardController.IsRightPressed, buttonD);
        }

        private void ChangeButtonColor(bool buttonState, Image img)
        {
            if (buttonState == true)
            {
                img.color = pressed;
            }
            else
            {
                img.color = notPressed;
            }
        }
    }

}
