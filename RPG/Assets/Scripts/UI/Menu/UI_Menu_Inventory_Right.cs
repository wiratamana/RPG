using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Right : SingletonMonobehaviour<UI_Menu_Inventory_Right>
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            var go = new GameObject(nameof(UI_Menu_Inventory_Right_PlayerPortrait));
            var rt = go.AddComponent<RectTransform>();
            rt.SetParent(transform);
            rt.sizeDelta = RectTransform.sizeDelta - new Vector2(RectTransform.sizeDelta.x * 0.2f, 0);
            rt.localPosition = Vector3.zero + new Vector3(RectTransform.sizeDelta.x * 0.2f, 0);
            rt.gameObject.AddComponent<UI_Menu_Inventory_Right_PlayerPortrait>();
        }
    }
}