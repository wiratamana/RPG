using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Editor_PartsGetter_ModularPartDefaultMarker : MonoBehaviour
    {
        [SerializeField] private Gender genderPart;
        [SerializeField] private Item_Armor.ArmorPart armorPart;

        public Gender GenderPart { get { return genderPart; } }
        public Item_Armor.ArmorPart ArmorPart { get { return armorPart; } }
    }
}
