using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ReadOnlyCollection<Item_Base> GetItemListAsReadOnly()
        {
            return GetItemListAsReadOnly(x => x is Item_Base);
        }

        public ReadOnlyCollection<Item_Base> GetItemListAsReadOnly(System.Func<Item_Base, bool> predicate)
        {
            return item.Where(predicate).ToList().AsReadOnly();
        }    

        public void AddItem(Item_Base item)
        {
            this.item.Add(item);
            Debug.Log($"Item added. Name : '{item.ItemName}'");
        }
    }
}
