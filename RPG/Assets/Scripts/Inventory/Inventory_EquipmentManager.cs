using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Tamana
{
    public class Inventory_EquipmentManager : SingletonMonobehaviour<Inventory_EquipmentManager>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            var go = new GameObject(nameof(Inventory_EquipmentManager));
            DontDestroyOnLoad(go);
            go.AddComponent<Inventory_EquipmentManager>();
        }

        private Dictionary<Item_Attachment.AttachmentPart, Item_Attachment> equippedAttachmentDic;
        private Dictionary<Item_Armor.ArmorPart, Item_Armor> equippedArmorDic;
        public Item_Weapon EquippedWeapon { private set; get; }
        public Transform WeaponTransform { private set; get; }

        protected override void Awake()
        {
            base.Awake();

            equippedAttachmentDic = new Dictionary<Item_Attachment.AttachmentPart, Item_Attachment>();
            for (int i = 0; i < System.Enum.GetValues(typeof(Item_Attachment.AttachmentPart)).Length; i++)
            {
                equippedAttachmentDic.Add((Item_Attachment.AttachmentPart)i, null);
            }

            equippedArmorDic = new Dictionary<Item_Armor.ArmorPart, Item_Armor>();
            for (int i = 0; i < System.Enum.GetValues(typeof(Item_Armor.ArmorPart)).Length; i++)
            {
                equippedArmorDic.Add((Item_Armor.ArmorPart)i, null);
            }
        }

        public bool IsCurrentlyEquipped(Item_Equipment equipment)
        {
            foreach(var a in equippedAttachmentDic)
            {
                if(a.Value == equipment)
                {
                    return true;
                }
            }

            foreach (var a in equippedArmorDic)
            {
                if (a.Value == equipment)
                {
                    return true;
                }
            }

            if(equipment is Item_Weapon)
            {
                var weapon = equipment as Item_Weapon;
                if(EquippedWeapon == weapon)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsEquippedWithWeapon()
        {
            return EquippedWeapon != null;
        }

        public void EquipModularPart(Item_ModularBodyPart modularPart)
        {
            // ===============================================================================================
            // Split the path to the Transform part
            // ===============================================================================================
            var split = modularPart.PartLocation.Split('/').ToList();
            split.RemoveAt(0);

            // ===============================================================================================
            // Get the Transform renference from player and the portrait on inventory menu.
            // ===============================================================================================
            var PortraitTransform = Inventory_Menu_PlayerPortrait.Instance.transform;
            var playerTransform = GameManager.Player;

            // ===============================================================================================
            // Get the equip part by searching it through child
            // ===============================================================================================
            var PortraitPart = PortraitTransform;
            var playerPart = playerTransform;
            while (split.Count > 0)
            {
                PortraitPart = GetChildTransformWithName(PortraitPart, split[0]);
                playerPart = GetChildTransformWithName(playerPart, split[0]);
                split.RemoveAt(0);
            }

            // ===============================================================================================
            // Deactivate all parts with same type as the equip item
            // ===============================================================================================
            var portaitPartParent = PortraitPart.parent;
            var playerPartParent = playerPart.parent;
            for(int i = 0; i < portaitPartParent.childCount; i++)
            {
                if(portaitPartParent.GetChild(i) == PortraitPart)
                {
                    continue;
                }

                portaitPartParent.GetChild(i).gameObject.SetActive(false);
                playerPartParent.GetChild(i).gameObject.SetActive(false);
            }

            // ===============================================================================================
            // Finally, activate the equip
            // ===============================================================================================
            playerPart.gameObject.SetActive(true);
            PortraitPart.gameObject.SetActive(true);

            // ===============================================================================================
            // Register equip item to dictionary
            // ===============================================================================================
            if (modularPart is Item_Armor)
            {
                var itemArmor = modularPart as Item_Armor;
                equippedArmorDic[itemArmor.Type] = itemArmor;
            }
            else if (modularPart is Item_Attachment)
            {
                var itemAttachment = modularPart as Item_Attachment;
                equippedAttachmentDic[itemAttachment.Type] = itemAttachment;
            }
        }

        public void UnequipModularPart(Item_ModularBodyPart_Metadata modularMetadata)
        {
            // ===============================================================================================
            // Split the path to the Transform part
            // ===============================================================================================
            var split = modularMetadata.PartLocation.Split('/').ToList();
            split.RemoveAt(0);

            // ===============================================================================================
            // Get the Transform renference from player and the portrait on inventory menu.
            // ===============================================================================================
            var PortraitTransform = Inventory_Menu_PlayerPortrait.Instance.transform;
            var playerTransform = GameManager.Player;

            // ===============================================================================================
            // Get the equip part by searching it through child
            // ===============================================================================================
            var PortraitPart = PortraitTransform;
            var playerPart = playerTransform;
            while (split.Count > 0)
            {
                PortraitPart = GetChildTransformWithName(PortraitPart, split[0]);
                playerPart = GetChildTransformWithName(playerPart, split[0]);
                split.RemoveAt(0);
            }

            // ===============================================================================================
            // Deactivate all parts with same type as the equip item
            // ===============================================================================================
            var portaitPartParent = PortraitPart.parent;
            var playerPartParent = playerPart.parent;
            for (int i = 0; i < portaitPartParent.childCount; i++)
            {
                if (portaitPartParent.GetChild(i) == PortraitPart)
                {
                    continue;
                }

                portaitPartParent.GetChild(i).gameObject.SetActive(false);
                playerPartParent.GetChild(i).gameObject.SetActive(false);
            }

            // ===============================================================================================
            // Finally, activate the equip
            // ===============================================================================================
            playerPart.gameObject.SetActive(true);
            PortraitPart.gameObject.SetActive(true);

            // ===============================================================================================
            // Register equip item to dictionary
            // ===============================================================================================
            if (modularMetadata.IsAttachmenet == true)
            {
                equippedAttachmentDic[modularMetadata.AttachmentType] = null;
            }
            else
            {
                equippedArmorDic[modularMetadata.ArmorType] = null;
            }
        }

        public void EquipWeapon(Item_Weapon equippedWeapon, Transform weaponTransform)
        {
            EquippedWeapon = equippedWeapon;
            WeaponTransform = weaponTransform;
        }

        public void UnequipWeapon()
        {
            EquippedWeapon = null;
            WeaponTransform = null;
        }

        private Transform GetChildTransformWithName(Transform parent, string name)
        {
            for(int i = 0; i < parent.childCount; i++)
            {
                if(parent.GetChild(i).name == name)
                {
                    return parent.GetChild(i);
                }
            }

            return null;
        }
    }
}
