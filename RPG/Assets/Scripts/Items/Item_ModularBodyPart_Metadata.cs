using UnityEngine;
using System.Collections;

namespace Tamana
{
    [System.Serializable]
    public class Item_ModularBodyPart_Metadata
    {
        [SerializeField] private string partLocation;
        [SerializeField] private int armorType;
        [SerializeField] private int attachmentType;
        [SerializeField] private int gender;
        [SerializeField] private bool isDefault;

        public string PartLocation
        {
            get
            {
                return partLocation;
            }
#if UNITY_EDITOR
            set
            {
                partLocation = value;
            }
#endif
        }

        public Item_Armor.ArmorPart ArmorType
        {
            get
            {
                return (Item_Armor.ArmorPart)armorType;
            }
#if UNITY_EDITOR
            set
            {
                armorType = (int)value;
            }
#endif
        }

        public Item_Attachment.AttachmentPart AttachmentType
        {
            get
            {
                return (Item_Attachment.AttachmentPart)attachmentType;
            }
#if UNITY_EDITOR
            set
            {
                attachmentType = (int)value;
            }
#endif
        }

        public bool IsAttachmenet
        {
            get
            {
                return Gender == Gender.All;
            }
        }

        public Gender Gender
        {
            get
            {
                return (Gender)gender;
            }

#if UNITY_EDITOR
            set
            {
                gender = (int)value;
            }
#endif
        }

        public bool IsDefault
        {
            get
            {
                return isDefault;
            }
#if UNITY_EDITOR
            set
            {
                isDefault = value;
            }
#endif
        }
    }
}
