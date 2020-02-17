using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIcon : MonoBehaviour
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

        private UI_Menu_Inventory_Left_ItemIcon_Background background;
        private UI_Menu_Inventory_Left_ItemIcon_Ring ring;
        private UI_Menu_Inventory_Left_ItemIcon_Renderer itemRenderer;

        private void Start()
        {
            itemRenderer.OnMouseEnter.AddListener(ring.OnMouseEnter);
            itemRenderer.OnMouseExit.AddListener(ring.OnMouseExit);

            itemRenderer.OnMouseEnter.AddListener(background.OnMouseEnter);
            itemRenderer.OnMouseExit.AddListener(background.OnMouseExit);

            itemRenderer.OnMouseLeftClick.AddListener(OpenItemOption);
        }

        public void SetValue(UI_Menu_Inventory_Left_ItemIcon_Background background,
            UI_Menu_Inventory_Left_ItemIcon_Ring ring,
            UI_Menu_Inventory_Left_ItemIcon_Renderer itemRenderer)
        {
            this.background = background;
            this.ring = ring;
            this.itemRenderer = itemRenderer;
        }

        public void ReturnToPool()
        {
            UI_Menu_Pool.Instance.RemoveImage(background.Background);
            UI_Menu_Pool.Instance.RemoveImage(ring.Ring);
            UI_Menu_Pool.Instance.RemoveRawImage(itemRenderer.RawImage);

            Destroy(background);
            Destroy(itemRenderer);
            Destroy(ring);
        }

        private void OpenItemOption()
        {
            // ===============================================================================================
            // Clear current registerd generic menu.
            // ===============================================================================================
            UI_Menu_Inventory_ItemOption.Instance.ClearRegisteredGenericMenu();

            // ===============================================================================================
            // Create Equip and Cancel generic menu instance.
            // ===============================================================================================
            var equip = UI_Menu_Inventory_ItemOption_GenericMenu.CreateGenericMenu("Equip");
            var cancel = UI_Menu_Inventory_ItemOption_GenericMenu.CreateGenericMenu("Cancel");

            // ===============================================================================================
            // Register on equip callback
            // ===============================================================================================
            var equipment = itemRenderer.ItemPreview.ItemBase as Item_Equipment;
            var isEquipped = Inventory_EquipmentManager.Instance.IsCurrentlyEquipped(equipment);
            if (isEquipped == true)
            {
                equip.Text.text = "Unequip";
                equip.OnMouseLeftClick.AddListener(equipment.Unequip);
            }
            else
            {
                equip.OnMouseLeftClick.AddListener(equipment.Equip);
            }

            equip.OnMouseLeftClick.AddListener(UI_Menu_Inventory_ItemOption.Instance.Close);
            equip.OnMouseLeftClick.AddListener(equip.OnMouseLeftClick.RemoveAllListener);

            // ===============================================================================================
            // Register on cancel callback
            // ===============================================================================================
            cancel.OnMouseLeftClick.AddListener(UI_Menu_Inventory_ItemOption.Instance.Close);
            cancel.OnMouseLeftClick.AddListener(cancel.OnMouseLeftClick.RemoveAllListener);

            // ===============================================================================================
            // Register instantiated Equip and Cancel generic menu to ItemOption.
            // ===============================================================================================
            UI_Menu_Inventory_ItemOption.Instance.RegisterGenericMenu(equip);
            UI_Menu_Inventory_ItemOption.Instance.RegisterGenericMenu(cancel);

            // ===============================================================================================
            // Open the menu.
            // ===============================================================================================
            UI_Menu_Inventory_ItemOption.Instance.Open(this);
        }
    }
}
