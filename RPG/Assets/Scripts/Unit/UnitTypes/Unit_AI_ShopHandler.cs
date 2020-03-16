using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_AI_ShopHandler : MonoBehaviour
    {
        [SerializeField] private Item_ShopProducts products;

        public Item_ShopProducts Products => products;
    }
}
