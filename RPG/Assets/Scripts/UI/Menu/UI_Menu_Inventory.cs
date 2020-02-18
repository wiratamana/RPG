using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Menu_Inventory : SingletonMonobehaviour<UI_Menu_Inventory>
    {
        public RectTransform RectTransform { private set; get; }
        public UI_Menu_Inventory_Left Left { private set; get; }
        public UI_Menu_Inventory_Right Right { private set; get; }
        public UI_Menu_Inventory_ItemOption ItemOption { private set; get; }

        public static EventManager OnMenuInventoryOpened { private set; get; } = new EventManager();
        public static EventManager OnMenuInventoryClosed { private set; get; } = new EventManager();

        public enum InventoryItemType
        {
            Weapon, Armor, Consumable
        }

        public InventoryItemType ItemType { private set; get; }

        public static UI_Menu_Inventory CreateMenuInventory(UI_Menu_Body body, UI_Menu_Header header, UI_Menu_Navigator navigator)
        {
            // ===============================================================================================
            // Create gameobject and set it parent to body
            // ===============================================================================================
            var go = new GameObject(nameof(UI_Menu_Inventory));
            go.transform.SetParent(body.transform);
            var rt = go.AddComponent<RectTransform>();

            // ===============================================================================================
            // Set position to center of the screen and set size to fit between header and navigator
            // ===============================================================================================
            rt.localPosition = Vector3.zero;    
            rt.sizeDelta = new Vector2(Screen.width, Screen.height - (header.RectTransform.sizeDelta.y + navigator.RectTransform.sizeDelta.y));

            // ===============================================================================================
            // Instantiate left
            // ===============================================================================================
            var leftGO = new GameObject(nameof(UI_Menu_Inventory_Left));
            leftGO.transform.SetParent(rt);
            var leftRT = leftGO.AddComponent<RectTransform>();
            leftRT.sizeDelta = new Vector2(rt.sizeDelta.x * 0.5f, rt.sizeDelta.y);
            leftRT.localPosition = new Vector3(-Screen.width * 0.25f, 0.0f);

            // ===============================================================================================
            // Instantiate right
            // ===============================================================================================
            var rightGO = new GameObject(nameof(UI_Menu_Inventory_Right));
            rightGO.transform.SetParent(rt);
            var rightRT = rightGO.AddComponent<RectTransform>();
            rightRT.sizeDelta = leftRT.sizeDelta;
            rightRT.localPosition = new Vector3(Screen.width * 0.25f, 0.0f);

            // ===============================================================================================
            // Instantiate - Item Option
            // ===============================================================================================
            var itemOptionGO = new GameObject(nameof(UI_Menu_Inventory_ItemOption));
            itemOptionGO.transform.SetParent(rt);
            var itemOptionRT = itemOptionGO.AddComponent<RectTransform>();
            itemOptionRT.localPosition = Vector3.zero;

            // ===============================================================================================
            // Jobs done !!
            // ===============================================================================================
            var inventory = go.AddComponent<UI_Menu_Inventory>();
            inventory.RectTransform = rt;
            inventory.Left = leftGO.AddComponent<UI_Menu_Inventory_Left>();
            inventory.Right = rightGO.AddComponent<UI_Menu_Inventory_Right>();
            inventory.ItemOption = itemOptionGO.AddComponent<UI_Menu_Inventory_ItemOption>();

            return inventory;
        }

        private void OnEnable()
        {
            OnMenuInventoryOpened.Invoke();
        }

        private void OnDisable()
        {
            OnMenuInventoryClosed.Invoke();
        }
    }
}