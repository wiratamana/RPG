using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [System.Serializable]
    public class Item_Product
    {
        public struct ValidationResult
        {
            public bool isAvailable;
            public bool isPlayerHasMoney;
        }

        [SerializeField] private Item_Base product;
        [SerializeField] private int price;
        [SerializeField] private int stock;

        public Item_Base Product => product;
        public int Price => price;
        public int Stock => stock;

        public Item_Product(Item_Base product, int price, int stock)
        {
            this.product = product;
            this.price = price;
            this.stock = stock;
        }

        public void Purchase()
        {
            PlayerData.Money -= Price;
            GameManager.Player.Inventory.AddItem(Object.Instantiate(Product));
            stock--;
        }

        public void Sell()
        {
            PlayerData.Money -= Price;
            GameManager.Player.Inventory.RemoveItem(product);
        }

        public void Validate(out ValidationResult result)
        {
            result.isAvailable = Stock > 0;
            result.isPlayerHasMoney = PlayerData.Money >= Price;
        }

        public static Item_Product CreateProduct(Item_Base itemBase)
        {
            return new Item_Product(itemBase, 50, 1);
        }
    }
}