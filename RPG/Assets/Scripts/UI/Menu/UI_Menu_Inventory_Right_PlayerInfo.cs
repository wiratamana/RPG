using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class UI_Menu_Inventory_Right_PlayerInfo : MonoBehaviour
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

        
        public UI_Menu_Inventory_Right_PlayerInfo_MainStatus Status { private set; get; }

        private void Awake()
        {
            var status = Unit_Status_Information.GetMainStatusFieldsName();
            var spacing = UI_Menu_Inventory_Right_PlayerInfo_MainStatus.SPACING;
            var spacingBIG = UI_Menu_Inventory_Right_PlayerInfo_MainStatus.SPACING_BIG;
            var height = UI_Menu_Inventory_Right_PlayerInfo_MainStatus.HEIGHT;

            var totalSpacing = (spacing * 3) + spacingBIG;
            var totalHeight = height * status.Length;
            var ySize = totalSpacing + totalHeight;

            var statusGO = new GameObject(nameof(UI_Menu_Inventory_Right_PlayerInfo_MainStatus));
            var statusRT = statusGO.AddComponent<RectTransform>();
            statusRT.SetParent(RectTransform);
            statusRT.sizeDelta = new Vector2(RectTransform.sizeDelta.x, ySize);
            statusRT.localPosition = Vector3.zero;
            Status = statusGO.AddComponent<UI_Menu_Inventory_Right_PlayerInfo_MainStatus>();
        }
    }
}