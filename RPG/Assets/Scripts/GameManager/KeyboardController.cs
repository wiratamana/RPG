using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public static class KeyboardController
    {
        public static float MouseVertical
        {
            get
            {
                return Input.GetAxis("Mouse Y");
            }
        }

        public static float MouseHorizontal
        {
            get
            {
                return Input.GetAxis("Mouse X");
            }
        }

        public static bool IsForwardDown => Input.GetKeyDown(KeyCode.W);
        public static bool IsForwardUp => Input.GetKeyUp(KeyCode.W);

        public static bool IsForwardPressed
        {
            get
            {
                return Input.GetKey(KeyCode.W);
            }
        }

        public static bool IsBackwardPressed
        {
            get
            {
                return Input.GetKey(KeyCode.S);
            }
        }

        public static bool IsLeftPressed
        {
            get
            {
                return Input.GetKey(KeyCode.A);
            }
        }

        public static bool IsRightPressed
        {
            get
            {
                return Input.GetKey(KeyCode.D);
            }
        }
    }
}
