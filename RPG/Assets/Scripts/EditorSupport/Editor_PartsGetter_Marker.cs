using UnityEngine;
using System.Collections;

namespace Tamana
{
#if UNITY_EDITOR
    public class Editor_PartsGetter_Marker : MonoBehaviour
    {
        [SerializeField] private bool isAttachment;
        [SerializeField] private Gender genderPart;
        [SerializeField] private Item_Armor.ArmorPart armorPart;
        [SerializeField] private Item_Attachment.AttachmentPart attachmentPart;

        public bool IsAttachment { get { return isAttachment; } }
        public Gender GenderPart { get { return genderPart; } }
        public Item_Armor.ArmorPart ArmorPart { get { return armorPart; } }
        public Item_Attachment.AttachmentPart AttachmentPart { get { return attachmentPart; } }
    }
#endif
}
