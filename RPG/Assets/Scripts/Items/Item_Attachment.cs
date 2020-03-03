using UnityEngine;
using System.Collections;

namespace Tamana
{
    [CreateAssetMenu(fileName = "New Attachment", menuName = "Item/Weapon", order = 1)]
    public class Item_Attachment : Item_ModularBodyPart
    {
        public enum AttachmentPart
        {
            Helmet,
            Back,
            ShoulderR,
            ShoulderL,
            ElbowR,
            ElbowL,
            Hip,
            KneeR,
            KneeL
        }

        [SerializeField] private AttachmentPart type; 

        [GM_AttributeValueIsSetWithReflection(nameof(Type))]
        public AttachmentPart Type
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

        public override ItemType ItemType => ItemType.Attachment;

#if UNITY_EDITOR
        public void SetType(AttachmentPart value)
        {
            Type = value;
        }
#endif
    }
}
