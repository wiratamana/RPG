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
        [SerializeField] private bool useCustomePrice;
        [SerializeField] private int customPrice;
        [SerializeField] private int stock;

        public Item_Base Product => product;
        public int PriceBuy => useCustomePrice ? customPrice : product.Price;
        public int PriceSell => Mathf.FloorToInt(PriceBuy * 0.5f);
        public string PriceBuyString => PriceBuy.ToString();
        public string PriceSellString => PriceSell.ToString();
        public int Stock => stock;

        public Item_Product(Item_Base product, int customPrice, int stock)
        {
            this.product = product;
            this.customPrice = customPrice;
            this.stock = stock;
        }

        public void Purchase()
        {
            PlayerData.Money -= PriceBuy;
            GameManager.Player.Inventory.AddItem(Object.Instantiate(Product));
            stock--;
        }

        public void Sell()
        {
            PlayerData.Money += PriceSell;
            GameManager.Player.Inventory.RemoveItem(product);
        }

        public void Validate(out ValidationResult result)
        {
            result.isAvailable = Stock > 0;
            result.isPlayerHasMoney = PlayerData.Money >= PriceBuy;
        }

        public static Item_Product CreateProduct(Item_Base itemBase)
        {
            return new Item_Product(itemBase, 50, 1);
        }
    }
}