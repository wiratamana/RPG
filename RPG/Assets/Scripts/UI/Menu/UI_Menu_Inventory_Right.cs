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

        public UI_Menu_Inventory_Right_PlayerPortrait PlayerPortrait { private set; get; }
        public UI_Menu_Inventory_Right_ItemDetails ItemDescription { private set; get; }

        protected override void Awake()
        {
            base.Awake();

            // ===============================================================================================
            // Player Portrait
            // ===============================================================================================
            var playerPortraitGO = new GameObject(nameof(UI_Menu_Inventory_Right_PlayerPortrait));
            var playerPortraitRT = playerPortraitGO.AddComponent<RectTransform>();
            playerPortraitRT.SetParent(transform);
            playerPortraitRT.sizeDelta = RectTransform.sizeDelta - new Vector2(RectTransform.sizeDelta.x * 0.2f, 0);
            playerPortraitRT.localPosition = Vector3.zero + new Vector3(RectTransform.sizeDelta.x * 0.2f, 0);
            PlayerPortrait = playerPortraitRT.gameObject.AddComponent<UI_Menu_Inventory_Right_PlayerPortrait>();

            // ===============================================================================================
            // Item Description
            // ===============================================================================================
            var itemDescriptionGO = new GameObject(nameof(UI_Menu_Inventory_Right_ItemDetails));
            var itemDescriptionRT = itemDescriptionGO.AddComponent<RectTransform>();
            itemDescriptionRT.SetParent(transform);
            itemDescriptionRT.sizeDelta = new Vector2(RectTransform.sizeDelta.x, UI_Menu_Inventory_Right_ItemDetails.HEIGHT);
            itemDescriptionRT.localPosition = new Vector3(0, (RectTransform.sizeDelta.y * -0.5f) + 
                (itemDescriptionRT.sizeDelta.y * 0.5f) + UI_Menu_Inventory_Right_ItemDetails.POSITION_Y_OFFSET);
            ItemDescription = itemDescriptionRT.gameObject.AddComponent<UI_Menu_Inventory_Right_ItemDetails>();
        }
    }
}