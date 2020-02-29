using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class UI_Menu_Inventory_Right_PlayerInfo_MainStatus_Child : MonoBehaviour
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

        public UI_Menu_Inventory_Right_PlayerInfo_MainStatus_Child_Icon Icon { private set; get; }
        public UI_Menu_Inventory_Right_PlayerInfo_MainStatus_Child_Value Value { private set; get; }

        private void Awake()
        {
            var iconSize = new Vector2(UI_Menu_Inventory_Right_PlayerInfo_MainStatus.HEIGHT,
                UI_Menu_Inventory_Right_PlayerInfo_MainStatus.HEIGHT);
            var valueSize = new Vector2(RectTransform.sizeDelta.x - iconSize.x, iconSize.y);

            var iconGO = new GameObject(nameof(Icon));
            var iconRT = iconGO.AddComponent<RectTransform>();
            iconRT.SetParent(RectTransform);
            iconRT.sizeDelta = iconSize;
            iconRT.localPosition = new Vector3((RectTransform.sizeDelta.x * -0.5f) + (iconSize.x * 0.5f), 0);
            Icon = iconGO.AddComponent<UI_Menu_Inventory_Right_PlayerInfo_MainStatus_Child_Icon>();

            var valueGO = new GameObject(nameof(Value));
            var valueRT = valueGO.AddComponent<RectTransform>();
            valueRT.SetParent(RectTransform);
            valueRT.sizeDelta = valueSize;
            valueRT.localPosition = iconRT.localPosition + new Vector3((valueSize.x * 0.5f) + (iconSize.x * 0.5f), 0);
            Value = valueGO.AddComponent<UI_Menu_Inventory_Right_PlayerInfo_MainStatus_Child_Value>();
        }
    }
}
