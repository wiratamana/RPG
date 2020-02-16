using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Item_Inventory : SingletonMonobehaviour<Item_Inventory>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            var go = new GameObject(nameof(Item_Inventory));
            DontDestroyOnLoad(go);
            go.AddComponent<Item_Inventory>();
        }

        private List<Item_Base> item = new List<Item_Base>();

        public int ItemCount
        {
            get
            {
                return item.Count;
            }
        }

        public string GetItemNameAtIndex(int idx)
        {
            return item[idx].ItemName;
        }

        public Transform GetItemPrefabIndex(int idx)
        {
            return item[idx].Prefab;
        }

        public void AddItem(Item_Base item)
        {
            this.item.Add(item);
            Debug.Log($"Item added. Name : '{item.ItemName}'");
        }
    }
}
