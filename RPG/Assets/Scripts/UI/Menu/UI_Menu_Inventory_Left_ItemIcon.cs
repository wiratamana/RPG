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

        public UI_Menu_Inventory_Left_ItemIcon_Background Background { private set; get; }
        public UI_Menu_Inventory_Left_ItemIcon_Ring Ring { private set; get; }
        public UI_Menu_Inventory_Left_ItemIcon_Renderer ItemRenderer { private set; get; }
        public Item_Base Item { get { return ItemRenderer.ItemPreview.ItemBase; } }

        private void Start()
        {
            ItemRenderer.OnMouseEnter.AddListener(OnMouseEnter);
            ItemRenderer.OnMouseExit.AddListener(OnMouseExit);
            
            ItemRenderer.OnMouseLeftClick.AddListener(OpenItemOption);
        }

        private void OnMouseEnter()
        {
            UI_Menu_Selection.CreateInstance(RectTransform, 32);
        }

        private void OnMouseExit()
        {
            UI_Menu_Selection.DestroyInstance();
        }

        public void SetValue(UI_Menu_Inventory_Left_ItemIcon_Background background,
            UI_Menu_Inventory_Left_ItemIcon_Ring ring,
            UI_Menu_Inventory_Left_ItemIcon_Renderer itemRenderer)
        {
            Background = background;
            Ring = ring;
            ItemRenderer = itemRenderer;
        }

        public void ReturnToPool()
        {
            UI_Menu_Pool.Instance.RemoveImage(Background.Background);
            UI_Menu_Pool.Instance.RemoveImage(Ring.Ring);
            UI_Menu_Pool.Instance.RemoveRawImage(ItemRenderer.RawImage);

            Destroy(Background);
            Destroy(ItemRenderer);
            Destroy(Ring);
            Destroy(gameObject);
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
            var equipment = ItemRenderer.ItemPreview.ItemBase as Item_Equipment;
            var isEquipped = Inventory_EquipmentManager.Instance.IsCurrentlyEquipped(equipment);
            if (isEquipped == true)
            {
                equip.Text.text = "Unequip";
                equip.OnMouseLeftClick.AddListener(equipment.Unequip);
                equip.OnMouseLeftClick.AddListener(UnregisterThisItemIcon);
            }
            else
            {
                equip.OnMouseLeftClick.AddListener(equipment.Equip);
                equip.OnMouseLeftClick.AddListener(RegisterThisItemIcon);
            }

            equip.OnMouseLeftClick.AddListener(Ring.UpdateColor);
            equip.OnMouseLeftClick.AddListener(Background.UpdateColor);
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

        private void RegisterThisItemIcon()
        {
            if(Item is Item_Armor)
            {
                var armor = Item as Item_Armor;
                UI_Menu_Inventory_Left_EquippedItemIcon.SetArmor(armor.Type, this);
            }
            else if (Item is Item_Attachment)
            {
                var attachment = Item as Item_Attachment;
                UI_Menu_Inventory_Left_EquippedItemIcon.SetAttachment(attachment.Type, this);
            }
            else if (Item is Item_Weapon)
            {
                UI_Menu_Inventory_Left_EquippedItemIcon.SetEquippedWeapon(this);
            }
        }

        private void UnregisterThisItemIcon()
        {
            if (Item is Item_Armor)
            {
                var armor = Item as Item_Armor;
                UI_Menu_Inventory_Left_EquippedItemIcon.SetArmor(armor.Type, null);
            }
            else if (Item is Item_Attachment)
            {
                var attachment = Item as Item_Attachment;
                UI_Menu_Inventory_Left_EquippedItemIcon.SetAttachment(attachment.Type, null);
            }
            else if (Item is Item_Weapon)
            {
                UI_Menu_Inventory_Left_EquippedItemIcon.SetEquippedWeapon(null);
            }
        }
    }
}
