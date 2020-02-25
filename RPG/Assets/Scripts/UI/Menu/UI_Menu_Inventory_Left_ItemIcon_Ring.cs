using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIcon_Ring : MonoBehaviour
    {
        private Image ring;
        public Image Ring
        {
            get
            {
                if (ring == null)
                {
                    ring = GetComponent<Image>();
                }

                return ring;
            }
        }

        private static readonly Color OnMouseOver = Color.red;
        private static readonly Color NormalColor = new Color(200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 1.0f);

        private UI_Menu_Inventory_Left_ItemIcon itemIcon;
        public UI_Menu_Inventory_Left_ItemIcon ItemIcon
        {
            get
            {
                if (itemIcon == null)
                {
                    itemIcon = transform.parent.GetComponent<UI_Menu_Inventory_Left_ItemIcon>();
                }

                return itemIcon;
            }
        }

        private void Start()
        {
            UpdateColor();

            UI_Menu.Instance.Inventory.Left.ItemTypeDrawer.OnActiveItemTypeValueChanged.AddListener(UpdateColor, GetInstanceID());
        }

        public void UpdateColor(ItemType type)
        {
            var equipment = ItemIcon.Item as Item_Equipment;
            if (equipment != null && equipment.ItemType == type && 
                Inventory_EquipmentManager.Instance.IsCurrentlyEquipped(equipment) == true)
            {
                Ring.color = Color.white;
            }
            else
            {
                Ring.color = NormalColor;
            }
        }

        public void UpdateColor()
        {
            var equipment = ItemIcon.Item as Item_Equipment;
            if (equipment != null && Inventory_EquipmentManager.Instance.IsCurrentlyEquipped(equipment) == true)
            {
                Ring.color = Color.white;
            }
            else
            {
                Ring.color = NormalColor;
            }
        }

        private void OnDestroy()
        {
            UI_Menu.Instance.Inventory.Left.ItemTypeDrawer.OnActiveItemTypeValueChanged.RemoveListener(UpdateColor, GetInstanceID());
        }
    }
}

