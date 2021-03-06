﻿using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Item_Armor : Item_ModularBodyPart
    {
        public enum ArmorPart
        {
            Helmet,
            Torso,
            UpperArmR,
            UpperArmL,
            LowerArmR,
            LowerArmL,
            HandR,
            HandL,
            Hip,
            LegR,
            LegL
        }

        [SerializeField] private ArmorPart type;
        [SerializeField] private bool isDefault;

        [GM_AttributeValueIsSetWithReflection(nameof(Type))]
        public ArmorPart Type
        {
            get
            {
                return type;
            }
            protected set
            {
                type = value;
            }
        }

        public bool IsDefault
        {
            get
            {
                return isDefault;
            }
        }


        public override void Equip()
        {
            inventoryOwner.Unit.Equipment.EquipModularPart(this);
        }

        public override void Unequip()
        {
            inventoryOwner.Unit.Equipment.UnequipModularPart(this);
        }

        public override Item_ItemDetails ItemDetails
        {
            get
            {
                return new Item_ItemDetails()
                {
                    ItemName = ItemName,
                    ItemDescription = ItemDescription,
                    ItemEffects = ItemEffects
                };
            }
        }

        public override ItemType ItemType => ItemType.Armor;

#if UNITY_EDITOR
        public void SetType(ArmorPart value)
        {
            Type = value;
        }

        public void SetIsDefault(bool isDefault)
        {
            this.isDefault = isDefault;
        }
#endif
    }
}