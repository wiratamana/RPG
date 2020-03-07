using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public static class RectTransformExtension
    {
        public static Canvas GetAndAssignCanvasFromParent(this RectTransform rt, ref Canvas canvas)
        {
            if(canvas == null)
            {
                canvas = rt.GetComponentInParent<Canvas>();
            }

            return canvas;
        }

        public static void SetSizeDeltaToCanvasParentCanvasSize(this RectTransform rt, Canvas canvas)
        {
            var halfVector = new Vector2(0.5f, 0.5f);
            rt.anchorMin = halfVector;
            rt.anchorMax = halfVector;
            rt.pivot = halfVector;

            var screenSize = new Vector2(Screen.width, Screen.height) * canvas.scaleFactor;
            rt.sizeDelta = screenSize;
        }
    }
}
