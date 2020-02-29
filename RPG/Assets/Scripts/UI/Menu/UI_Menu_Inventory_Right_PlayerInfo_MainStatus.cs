using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{   
    public class UI_Menu_Inventory_Right_PlayerInfo_MainStatus : MonoBehaviour
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

        public const float HEIGHT = 64;
        public const float SPACING = 5;
        public const float SPACING_BIG = 48;
        public const float WIDTH = HEIGHT + 200;

        public Image[] Backgrounds { private set; get; }
        public Image[] Icon { private set; get; }
        public UI_Menu_Inventory_Right_PlayerInfo_MainStatus_Child[] MainStatus { private set; get; }

        private void Awake()
        {
            var mainStatus = Status_Information.GetMainStatusFieldsName();
            MainStatus = new UI_Menu_Inventory_Right_PlayerInfo_MainStatus_Child[mainStatus.Length];
            var spacing = new float[] { SPACING, SPACING, SPACING };
            var index = 0;

            var startPos = new Vector2((RectTransform.sizeDelta.x * -0.5f) + (WIDTH * 0.5f),
               (RectTransform.sizeDelta.y * 0.5f) - (HEIGHT * 0.5f));
            var childSize = new Vector2(WIDTH, HEIGHT);

            foreach(var m in mainStatus)
            {
                var mainStatusGO = new GameObject(m);
                var mainStatusRT = mainStatusGO.AddComponent<RectTransform>();
                mainStatusRT.SetParent(RectTransform);
                mainStatusRT.sizeDelta = childSize;
                mainStatusRT.localPosition = startPos;
                MainStatus[index] = mainStatusGO.AddComponent<UI_Menu_Inventory_Right_PlayerInfo_MainStatus_Child>();

                if (index < spacing.Length)
                {
                    startPos -= new Vector2(0, spacing[index]);
                }
                startPos -= new Vector2(0, HEIGHT);
                index++;
            }

            // ===============================================================================================
            // Register OnEquipped and OnUnequipped callback to event
            // ===============================================================================================
            GameManager.Player.Equipment.OnEquippedEvent.AddListener(OnEquipped);
            GameManager.Player.Equipment.OnUnequippedEvent.AddListener(OnUnequipped);
        }

        private void UpdateValue()
        {
            foreach (var item in MainStatus)
            {
                item.Value.UpdateValue();
            }
        }

        private void OnUnequipped(Item_Equipment equipment)
        {
            Debug.Log($"Old : {equipment.ItemName}");
            UpdateValue();
        }

        private void OnEquipped(Item_Equipment oldEquipment, Item_Equipment newEquipment)
        {
            Debug.Log($"Old : {oldEquipment?.ItemName} | New : {newEquipment.ItemName}");
            UpdateValue();
        }
    }
}