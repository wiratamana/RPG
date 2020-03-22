using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Menu_Header : SingletonMonobehaviour<UI_Menu_Header>
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if(_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        private RectTransform left;
        private RectTransform mid;
        private RectTransform right;

        public UI_Menu_Header_MenuName MenuName { private set; get; }
        public UI_Menu_Header_Money Money { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            SplitToThreeRegion();
        }

        private void SplitToThreeRegion()
        {
            var leftGO  = new GameObject(nameof(left));
            var midGO   = new GameObject(nameof(mid));
            var rightGO = new GameObject(nameof(right));

            leftGO.transform.SetParent(transform);
            midGO.transform.SetParent(transform);
            rightGO.transform.SetParent(transform);

            left    = leftGO.AddComponent<RectTransform>();
            mid     = midGO.AddComponent<RectTransform>();
            right   = rightGO.AddComponent<RectTransform>();

            MenuName = mid.gameObject.AddComponent<UI_Menu_Header_MenuName>();
            Money = right.gameObject.AddComponent<UI_Menu_Header_Money>();

            // =================================================================

            var headerWidth = Screen.width;
            var thirdWidth = headerWidth / 3.0f;

            left.sizeDelta  = new Vector2(thirdWidth, RectTransform.sizeDelta.y);
            mid.sizeDelta   = left.sizeDelta;
            right.sizeDelta = left.sizeDelta;

            mid.localPosition   = Vector2.zero + new Vector2(0.0f, RectTransform.sizeDelta.y / -2.0f);
            left.localPosition  = mid.localPosition - new Vector3(thirdWidth, 0);
            right.localPosition = mid.localPosition + new Vector3(thirdWidth, 0);
        }
    }
}
