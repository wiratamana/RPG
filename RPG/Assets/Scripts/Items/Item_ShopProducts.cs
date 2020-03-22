using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [CreateAssetMenu(fileName = "ShopProducts", menuName = "Create/Shop Products")]
    public class Item_ShopProducts : ScriptableObject
    {
        [SerializeField] private List<Item_Product> products;

        public IReadOnlyCollection<Item_Product> Products => products;
    }
}
