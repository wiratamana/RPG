using UnityEngine;
using System.Collections;

namespace Tamana
{
    public abstract class Item_ModularBodyPart : Item_Base
    {
        [SerializeField] private string partLocation;

        [GM_AttributeValueIsSetWithReflection(nameof(PartLocation))]
        public string PartLocation
        {
            get
            {
                return partLocation;
            }
            protected set
            {
                partLocation = value;
            }
        }

#if UNITY_EDITOR
        public void SetPartLocation(string value)
        {
            PartLocation = value;
        }
#endif
    }
}
