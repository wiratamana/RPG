using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public abstract class Item_Base : ScriptableObject
    {
        [SerializeField] private string itemName;

        [GM_AttributeValueIsSetWithReflection(nameof(ItemName))]
        public string ItemName 
        {
            get
            {
                return itemName;
            }
            protected set
            {
                itemName = value;
            }
        }

        [SerializeField] private string itemDescription;

        [GM_AttributeValueIsSetWithReflection(nameof(ItemDescription))]
        public string ItemDescription
        {
            get
            {
                return itemDescription;
            }
            protected set
            {
                itemDescription = value;
            }
        }


        [SerializeField] private Transform prefab;
        public Transform Prefab
        {
            get
            {
                return prefab;
            }
            protected set
            {
                prefab = value;
            }
        }

        public abstract Item_ItemDetails ItemDetails { get; }

#if UNITY_EDITOR
        public void SetItemName(string value)
        {
            ItemName = value;
        }
        public void SetItemDescription(string value)
        {
            ItemDescription = value;
        }

        public void SetPrefab(Transform prefab)
        {
            Prefab = prefab;
        }
#endif
    }
}
