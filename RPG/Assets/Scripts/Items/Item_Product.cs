using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [System.Serializable]
    public class Item_Product
    {
        [SerializeField] private Item_Base product;
        [SerializeField] private int price;
        [SerializeField] private int stock;

        public Item_Base Product => product;
        public int Price => price;
        public int Stock => stock;
    }
}