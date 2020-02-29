using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Tamana
{
    public class Unit_Inventory
    {     
        public Unit_Base Owner { private set; get; }
        private List<Item_Base> itemList = new List<Item_Base>();

        public int ItemCount
        {
            get
            {
                return itemList.Count;
            }
        }

        public Unit_Inventory(Unit_Base owner)
        {
            Owner = owner;
        }

        public ReadOnlyCollection<Item_Base> GetItemListAsReadOnly()
        {
            return GetItemListAsReadOnly(x => x is Item_Base);
        }

        public ReadOnlyCollection<Item_Base> GetItemListAsReadOnly(System.Func<Item_Base, bool> predicate)
        {
            return itemList.Where(predicate).ToList().AsReadOnly();
        }    

        public void AddItem(Item_Base item)
        {
            itemList.Add(item);
            item.SetItemOwner(this);

            Debug.Log($"Item added. Name : '{item.ItemName}'");
        }
    }
}
