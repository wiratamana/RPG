using UnityEngine;
using System.Collections.Generic;

namespace Tamana
{
    public static class UI_Menu_Inventory_Left_EquippedItemIcon
    {
        private static Dictionary<Item_Armor.ArmorPart, UI_Menu_Inventory_Left_ItemIcon> equippedArmor;
        private static Dictionary<Item_Attachment.AttachmentPart, UI_Menu_Inventory_Left_ItemIcon> equippedAttachment;
        private static UI_Menu_Inventory_Left_ItemIcon equippedWeapon;
             
        private static void InitEquippedArmorDic()
        {
            if (equippedArmor == null || equippedArmor.Count == 0)
            {
                equippedArmor = new Dictionary<Item_Armor.ArmorPart, UI_Menu_Inventory_Left_ItemIcon>();
                for (int i = 0; i < System.Enum.GetValues(typeof(Item_Armor.ArmorPart)).Length; i++)
                {
                    equippedArmor.Add((Item_Armor.ArmorPart)i, null);
                }
            }
        }

        private static void InitEquippedAttachment()
        {
            if (equippedAttachment == null || equippedAttachment.Count == 0)
            {
                equippedAttachment = new Dictionary<Item_Attachment.AttachmentPart, UI_Menu_Inventory_Left_ItemIcon>();
                for (int i = 0; i < System.Enum.GetValues(typeof(Item_Attachment.AttachmentPart)).Length; i++)
                {
                    equippedAttachment.Add((Item_Attachment.AttachmentPart)i, null);
                }
            }
        }

        public static bool IsCurrentlyEquipped(UI_Menu_Inventory_Left_ItemIcon itemIcon)
        {
            InitEquippedArmorDic();

            foreach (var a in equippedArmor)
            {
                if (itemIcon == a.Value)
                {
                    return true;
                }
            }

            foreach(var a in equippedAttachment)
            {
                if(itemIcon == a.Value)
                {
                    return true;
                }
            }

            if(itemIcon == equippedWeapon)
            {
                return true;
            }

            return false;
        }

        public static void SetToNull()
        {
            equippedArmor?.Clear();
            equippedAttachment?.Clear();
            equippedWeapon = null;
        }

        public static void MarkItemAsEquippedItem(UI_Menu_Inventory_Left_ItemIcon itemIcon)
        {
            var equipment = itemIcon.Item as Item_Equipment;
            if(equipment == null)
            {
                return;
            }

            if(GameManager.Player.Equipment.IsCurrentlyEquipped(equipment) == false)
            {
                return;
            }

            var weapon = equipment as Item_Weapon;
            if(weapon != null)
            {
                SetEquippedWeapon(itemIcon);
                return;
            }

            var armor = equipment as Item_Armor;
            if(armor != null)
            {
                SetArmor(armor.Type, itemIcon);
                return;
            }

            var attachment = equipment as Item_Attachment;
            if (attachment != null)
            {
                SetAttachment(attachment.Type, itemIcon);
                return;
            }
        }

        public static void SetArmor(Item_Armor.ArmorPart key, UI_Menu_Inventory_Left_ItemIcon itemIcon)
        {
            InitEquippedArmorDic();

            if (equippedArmor[key] != null)
            {
                equippedArmor[key].Background.UpdateColor();
                equippedArmor[key].Ring.UpdateColor();
            }

            equippedArmor[key] = itemIcon;
        }

        public static void SetAttachment(Item_Attachment.AttachmentPart key, UI_Menu_Inventory_Left_ItemIcon itemIcon)
        {
            InitEquippedAttachment();

            if(equippedAttachment[key] != null)
            {
                equippedAttachment[key].Background.UpdateColor();
                equippedAttachment[key].Ring.UpdateColor();
            }           

            equippedAttachment[key] = itemIcon;
        }

        public static void SetEquippedWeapon(UI_Menu_Inventory_Left_ItemIcon itemIcon)
        {
            if (equippedWeapon != null)
            {
                equippedWeapon.Background.UpdateColor();
                equippedWeapon.Ring.UpdateColor();
            }

            equippedWeapon = itemIcon;
        }
    }
}

