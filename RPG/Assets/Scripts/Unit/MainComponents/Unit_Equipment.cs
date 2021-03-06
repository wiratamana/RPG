﻿using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Tamana
{
    public class Unit_Equipment
    {
        public Unit_Base Unit { private set; get; }
        public Unit_Inventory Inventory => Unit.Inventory;
        public Unit_BodyTransform BodyTransform => Unit.CombatHandler.BodyTransform;

        public Transform UnitTransform => Unit.transform;
        private Transform portraitTransform;

        private Dictionary<Item_Attachment.AttachmentPart, Item_Attachment> equippedAttachmentDic;
        private Dictionary<Item_Armor.ArmorPart, Item_Armor> equippedArmorDic;
        public Item_Weapon EquippedWeapon { private set; get; }
        public Transform WeaponTransform { private set; get; }

        public EventManager<Item_Equipment, Item_Equipment> OnEquippedEvent { private set; get; } = new EventManager<Item_Equipment, Item_Equipment>();
        public EventManager<Item_Equipment> OnUnequippedEvent { private set; get; } = new EventManager<Item_Equipment>();

        public Unit_Equipment(Unit_Base owner, Transform portraitTransform)
        {
            Unit = owner;
            this.portraitTransform = portraitTransform;

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
            // Get the equip part by searching it through child
            // ===============================================================================================
            var portraitPart = portraitTransform;
            var ownerPart = UnitTransform;
            while (split.Count > 0)
            {
                portraitPart = GetChildTransformWithName(portraitPart, split[0]);
                ownerPart = GetChildTransformWithName(ownerPart, split[0]);
                split.RemoveAt(0);
            }

            // ===============================================================================================
            // Deactivate all parts with same type as the equip item
            // ===============================================================================================
            var portaitPartParent = portraitPart?.parent;
            var ownerPartParent = ownerPart.parent;
            for(int i = 0; i < ownerPartParent.childCount; i++)
            {
                if(ownerPartParent.GetChild(i) == ownerPart)
                {
                    continue;
                }

                portaitPartParent?.GetChild(i).gameObject.SetActive(false);
                ownerPartParent.GetChild(i).gameObject.SetActive(false);
            }

            // ===============================================================================================
            // Finally, activate the equip
            // ===============================================================================================
            ownerPart.gameObject.SetActive(true);
            portraitPart?.gameObject.SetActive(true);

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
            // Get the equip part by searching it through child
            // ===============================================================================================
            var portraitPart = portraitTransform;
            var ownerPart = UnitTransform;

            var defaultPortaitPart = portraitTransform;
            var defaultOwnerPart = ownerPart;

            while (splitCurrentEquippedPart.Count > 0)
            {
                portraitPart = GetChildTransformWithName(portraitPart, splitCurrentEquippedPart[0]);
                ownerPart = GetChildTransformWithName(ownerPart, splitCurrentEquippedPart[0]);
                splitCurrentEquippedPart.RemoveAt(0);

                if(splitDefaultPart != null)
                {
                    defaultPortaitPart = GetChildTransformWithName(defaultPortaitPart, splitDefaultPart[0]);
                    defaultOwnerPart = GetChildTransformWithName(defaultOwnerPart, splitDefaultPart[0]);

                    splitDefaultPart.RemoveAt(0);
                }

            }            

            // ===============================================================================================
            // Finally, activate the equip
            // ===============================================================================================
            ownerPart.gameObject.SetActive(false);
            portraitPart?.gameObject.SetActive(false);
            if(splitDefaultPart != null)
            {
                defaultPortaitPart?.gameObject.SetActive(true);
                defaultOwnerPart.gameObject.SetActive(true);
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

        public void EquipWeapon(Item_Weapon equippedWeapon)
        {
            // ===============================================================================================
            // Destroy old weapon if exist
            // ===============================================================================================
            var oldUnitWeapon = EquippedWeapon;            
            if (oldUnitWeapon != null)
            {                
                var oldUserWeaponTransform = WeaponTransform;
                Object.Destroy(oldUserWeaponTransform.gameObject);
                WeaponTransform = null;

                if(Unit.IsUnitPlayer == true)
                {
                    var oldPortraitWeaponTransform = Inventory_Menu_PlayerPortrait.Instance.WeaponTransform;
                    Object.Destroy(oldPortraitWeaponTransform.gameObject);
                    Inventory_Menu_PlayerPortrait.Instance.WeaponTransform = null;
                }                
            }

            // ===============================================================================================
            // Instantiate weapon prefab
            // ===============================================================================================
            var weaponTransform = Object.Instantiate(equippedWeapon.Prefab, BodyTransform.Hips);
            weaponTransform.transform.localScale = new Vector3(100, 100, 100);
            weaponTransform.transform.localPosition = equippedWeapon.HolsterPosition;
            weaponTransform.transform.localRotation = equippedWeapon.HolsterRotation;

            if(portraitTransform != null)
            {
                var weaponPreview = Object.Instantiate(equippedWeapon.Prefab, Inventory_Menu_PlayerPortrait.Instance.Hips);
                weaponPreview.transform.localScale = new Vector3(100, 100, 100);
                weaponPreview.transform.localPosition = equippedWeapon.HolsterPosition;
                weaponPreview.transform.localRotation = equippedWeapon.HolsterRotation;

                Inventory_Menu_PlayerPortrait.Instance.WeaponTransform = weaponPreview;
            }

            EquippedWeapon = equippedWeapon;
            WeaponTransform = weaponTransform;

            // ===============================================================================================
            // Fire on equipped event (left hand is old, right hand is new)
            // ===============================================================================================
            OnEquippedEvent.Invoke(oldUnitWeapon, equippedWeapon);
        }

        public void UnequipWeapon()
        {
            Object.Destroy(WeaponTransform.gameObject);
            if(portraitTransform != null)
            {
                Inventory_Menu_PlayerPortrait.Instance.WeaponTransform = null;
            }

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
            if(parent == null)
            {
                return null;
            }

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
