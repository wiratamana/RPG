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

        public EventManager<Item_Equipment, Item_Equipment> OnEquippedEvent { private set; get; } = new EventManager<Item_Equipment, Item_Equipment>();
        public EventManager<Item_Equipment> OnUnequippedEvent { private set; get; } = new EventManager<Item_Equipment>();

        public List<Item_Equipment_Effect> GetEquippedItemEffects()
        {
            var effects = new List<Item_Equipment_Effect>();

            foreach (var item in equippedAttachmentDic)
            {
                if(item.Value == null || item.Value.ItemEffects == null || item.Value.ItemEffects.Length == 0)
                {
                    continue;
                }

                effects.AddRange(item.Value.ItemEffects);
            }

            foreach (var item in equippedArmorDic)
            {
                if (item.Value == null || item.Value.ItemEffects == null || item.Value.ItemEffects.Length == 0)
                {
                    continue;
                }

                effects.AddRange(item.Value.ItemEffects);
            }

            if (EquippedWeapon != null && EquippedWeapon.ItemEffects != null && EquippedWeapon.ItemEffects.Length > 0)
            {
                effects.AddRange(EquippedWeapon.ItemEffects);
            }

            return effects;
        }

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
            Item_Equipment oldEquipment = null;
            if (modularPart is Item_Armor)
            {
                var itemArmor = modularPart as Item_Armor;
                oldEquipment = equippedArmorDic[itemArmor.Type];
                equippedArmorDic[itemArmor.Type] = itemArmor;
            }
            else if (modularPart is Item_Attachment)
            {
                var itemAttachment = modularPart as Item_Attachment;
                oldEquipment = equippedAttachmentDic[itemAttachment.Type];
                equippedAttachmentDic[itemAttachment.Type] = itemAttachment;
            }

            // ===============================================================================================
            // Fire on equipped event (left hand is old, right hand is new)
            // ===============================================================================================
            OnEquippedEvent.Invoke(oldEquipment, modularPart);
        }

        public void UnequipModularPart(Item_ModularBodyPart modularBodyPart)
        {
            // ===============================================================================================
            // Get default part
            // ===============================================================================================
            var modularMetadata = ResourcesLoader.Instance.LoadModularBodyMetadata();
            var defaults = modularMetadata.objs
                .Where(x => x.Gender == modularBodyPart.Gender &&
                            x.IsDefault == true);

            Item_ModularBodyPart_Metadata? defaultPart = null;
            Item_Armor armor = null;
            Item_Attachment attachment = null;

            if (modularBodyPart is Item_Armor)
            {
                armor = modularBodyPart as Item_Armor;
                defaultPart = defaults.FirstOrDefault(x => x.ArmorType == armor.Type);
            }
            else
            {
                attachment = modularBodyPart as Item_Attachment;
                defaultPart = defaults.FirstOrDefault(x => x.AttachmentType == attachment.Type);
            }

            if(defaultPart == null || defaultPart.HasValue == false)
            {
                string type = armor == null ? armor.Type.ToString() : attachment.Type.ToString();
                Debug.Log($"Faile to unequip. Default part was not found!! ItemType : {modularBodyPart.ItemType} | Type : {type}");
                return;
            }

            // ===============================================================================================
            // Split the path to the Transform part
            // ===============================================================================================
            var splitCurrentEquippedPart = modularBodyPart.PartLocation.Split('/').ToList();
            var splitDefaultPart = string.IsNullOrEmpty(defaultPart.Value.PartLocation) ? 
                null : defaultPart.Value.PartLocation.Split('/').ToList();
            splitCurrentEquippedPart.RemoveAt(0);
            splitDefaultPart?.RemoveAt(0);

            // ===============================================================================================
            // Get the Transform renference from player and the portrait on inventory menu.
            // ===============================================================================================
            var portraitTransform = Inventory_Menu_PlayerPortrait.Instance.transform;
            var playerTransform = GameManager.Player;

            // ===============================================================================================
            // Get the equip part by searching it through child
            // ===============================================================================================
            var portraitPart = portraitTransform;
            var playerPart = playerTransform;
            var defaultPortaitPart = portraitTransform;
            var defaultPlayerPart = playerPart;

            while (splitCurrentEquippedPart.Count > 0)
            {
                portraitPart = GetChildTransformWithName(portraitPart, splitCurrentEquippedPart[0]);
                playerPart = GetChildTransformWithName(playerPart, splitCurrentEquippedPart[0]);
                splitCurrentEquippedPart.RemoveAt(0);

                if(splitDefaultPart != null)
                {
                    defaultPortaitPart = GetChildTransformWithName(defaultPortaitPart, splitDefaultPart[0]);
                    defaultPlayerPart = GetChildTransformWithName(defaultPlayerPart, splitDefaultPart[0]);

                    splitDefaultPart.RemoveAt(0);
                }

            }            

            // ===============================================================================================
            // Finally, activate the equip
            // ===============================================================================================
            playerPart.gameObject.SetActive(false);
            portraitPart.gameObject.SetActive(false);
            if(splitDefaultPart != null)
            {
                defaultPortaitPart.gameObject.SetActive(true);
                defaultPlayerPart.gameObject.SetActive(true);
            }            

            // ===============================================================================================
            // Register equip item to dictionary
            // ===============================================================================================
            Item_Equipment oldEquipment = null;
            if (modularBodyPart is Item_Attachment)
            {
                var itemAttachment = modularBodyPart as Item_Attachment;
                oldEquipment = itemAttachment;
                equippedAttachmentDic[itemAttachment.Type] = null;
            }
            else if(modularBodyPart is Item_Armor)
            {
                var itemArmor = modularBodyPart as Item_Armor;
                oldEquipment = itemArmor;
                equippedArmorDic[itemArmor.Type] = null;
            }

            // ===============================================================================================
            // Fire on unequipped event
            // ===============================================================================================
            OnUnequippedEvent.Invoke(oldEquipment);
        }

        public void EquipWeapon(Item_Weapon equippedWeapon, Transform weaponTransform)
        {
            var oldWeapon = EquippedWeapon;
            EquippedWeapon = equippedWeapon;
            WeaponTransform = weaponTransform;

            // ===============================================================================================
            // Fire on equipped event (left hand is old, right hand is new)
            // ===============================================================================================
            OnEquippedEvent.Invoke(oldWeapon, equippedWeapon);
        }

        public void UnequipWeapon()
        {
            var oldWeapon = EquippedWeapon;
            EquippedWeapon = null;
            WeaponTransform = null;

            // ===============================================================================================
            // Fire on unequipped event
            // ===============================================================================================
            OnUnequippedEvent.Invoke(oldWeapon);
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
