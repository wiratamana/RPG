using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Menu_Inventory_Left : SingletonMonobehaviour<UI_Menu_Inventory_Left>
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

        public UI_Menu_Inventory_Left_ItemType ItemType;
        public UI_Menu_Inventory_Left_ItemIconDrawer ItemIconDrawer;

        protected override void Awake()
        {
            base.Awake();
            InstantiateTopAndBot();
        }

        private void InstantiateTopAndBot()
        {
            var topSize = 100;
            var botSize = RectTransform.sizeDelta.y - topSize;
            var half = 0.5f;

            // ===============================================================================================
            // Top (Height : 20%)
            // ===============================================================================================
            var topGO = new GameObject(nameof(ItemType));
            topGO.transform.SetParent(transform);
            var topRT = topGO.AddComponent<RectTransform>();

            topRT.sizeDelta = new Vector2(RectTransform.sizeDelta.x, topSize);
            topRT.localPosition = new Vector3(0.0f, (RectTransform.sizeDelta.y * half) - (topRT.sizeDelta.y * half));

            ItemType = topGO.AddComponent<UI_Menu_Inventory_Left_ItemType>();

            // ===============================================================================================
            // Bot (Height : 80%)
            // ===============================================================================================
            var botGO = new GameObject(nameof(ItemIconDrawer));
            botGO.transform.SetParent(transform);
            var botRT = botGO.AddComponent<RectTransform>();

            botRT.sizeDelta = new Vector2(RectTransform.sizeDelta.x, botSize);
            botRT.localPosition = new Vector3(0.0f, (RectTransform.sizeDelta.y * -half) + (botRT.sizeDelta.y * half));

            ItemIconDrawer = botGO.AddComponent<UI_Menu_Inventory_Left_ItemIconDrawer>();
        }
    }
}
