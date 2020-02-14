using UnityEngine;
using System.Collections;

namespace Tamana
{
    [CreateAssetMenu(fileName = "New Armor", menuName = "Item/Weapon", order = 1)]
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

#if UNITY_EDITOR
        public void SetType(ArmorPart value)
        {
            Type = value;
        }
#endif
    }
}