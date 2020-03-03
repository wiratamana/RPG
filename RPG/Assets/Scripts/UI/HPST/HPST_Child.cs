using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class HPST_Child : MonoBehaviour
    {
        [SerializeField] private MainStatus status;
        public MainStatus Status => status;

        [SerializeField] private RectTransform frame;
        [SerializeField] private RectTransform fill;

        public void SetWidth(float value)
        {
            var size = frame.sizeDelta;
            size.x = 250 + value;
            frame.sizeDelta = size;
        }

        public void IncreaseValue(float value)
        {
            var size = fill.sizeDelta;
            size.x = Mathf.Min(size.x + value, frame.sizeDelta.x - 2);
            fill.sizeDelta = size;
        }

        public void DecreaseValue(float value)
        {
            var size = fill.sizeDelta;
            size.x = Mathf.Max(size.x - value, 0.0f);
            fill.sizeDelta = size;
        }

        public void SetFillRate(float fillrate)
        {
            var size = fill.sizeDelta;
            size.x = (frame.sizeDelta.x - 2) * fillrate;
            fill.sizeDelta = size;
        }
    }
}
