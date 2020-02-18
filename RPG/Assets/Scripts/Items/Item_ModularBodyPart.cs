using UnityEngine;
using System.Collections;

namespace Tamana
{
    public abstract class Item_ModularBodyPart : Item_Equipment
    {
        [SerializeField] private string partLocation;
        [SerializeField] private Gender gender;

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

        public Gender Gender
        {
            get
            {
                return gender;
            }
        }

#if UNITY_EDITOR
        public void SetPartLocation(string value)
        {
            PartLocation = value;
        }

        public void SetGender(Gender gender)
        {
            this.gender = gender;
        }
#endif
    }
}
