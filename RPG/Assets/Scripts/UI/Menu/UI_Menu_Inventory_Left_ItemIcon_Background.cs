using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIcon_Background : MonoBehaviour
    {
        private Image background;
        public Image Background
        {
            get
            {
                if (background == null)
                {
                    background = GetComponent<Image>();
                }

                return background;
            }
        }

        private UI_Menu_Inventory_Left_ItemIcon itemIcon;
        public UI_Menu_Inventory_Left_ItemIcon ItemIcon
        {
            get
            {
                if(itemIcon == null)
                {
                    itemIcon = transform.parent.GetComponent<UI_Menu_Inventory_Left_ItemIcon>();
                }

                return itemIcon;
            }
        }

        private static readonly Color NormalColor = new Color(40.0f / 255.0f, 40.0f / 255.0f, 40.0f / 255.0f, 200.0f / 255.0f);
        private static readonly Color OnMouseOver = Color.blue;

        private void Start()
        {
            UpdateColor();

            UI_Menu.Instance.Inventory.Left.ItemTypeDrawer.OnActiveItemTypeValueChanged.AddListener(UpdateColor, GetInstanceID());
        }

        private void UpdateColor(ItemType type)
        {
            var equipment = ItemIcon.Item as Item_Equipment;
            if (equipment != null && equipment.ItemType == type &&
                GameManager.Player.Equipment.IsCurrentlyEquipped(equipment) == true)
            {
                Background.sprite = UI_Menu.Instance.MenuResources.InventoryItemBackgroundEquipped_Sprite;
                Background.color = Color.white;
            }
            else
            {
                Background.color = NormalColor;
            }
        }

        public void UpdateColor()
        {
            var equipment = ItemIcon.Item as Item_Equipment;
            if (equipment != null && GameManager.Player.Equipment.IsCurrentlyEquipped(equipment) == true)
            {
                Background.sprite = UI_Menu.Instance.MenuResources.InventoryItemBackgroundEquipped_Sprite;
                Background.color = Color.white;
            }
            else
            {
                Background.color = NormalColor;
            }
        }

        private void OnDestroy()
        {
            UI_Menu.Instance.Inventory.Left.ItemTypeDrawer.OnActiveItemTypeValueChanged.RemoveListener(UpdateColor, GetInstanceID());
        }
    }
}
