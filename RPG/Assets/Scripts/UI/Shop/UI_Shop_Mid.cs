using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Mid : MonoBehaviour
    {
        private UI_Shop shop;
        public UI_Shop Shop => this.GetAndAssignComponentInParent(ref shop);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Mid_TradeMenu tradeMenu;
        public UI_Shop_Mid_TradeMenu TradeMenu => this.GetAndAssignComponentInChildren(ref tradeMenu);       

        public Item_Product ItemProduct { get; private set; }

        public void Activate(Item_Product itemProduct, in TradeType tradeType)
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            ItemProduct = itemProduct;
            gameObject.SetActive(true);
            TradeMenu.Activate();
        }

        public void Deactivate()
        {
            ItemProduct = null;

            gameObject.SetActive(false);
            TradeMenu.Deactivate();
        }

        private void Resize()
        {
            var xSize = Screen.width * 0.5f;
            var ySize = Screen.height;
            RectTransform.sizeDelta = new Vector2(xSize, ySize);
            RectTransform.position = new Vector3(xSize, ySize * 0.5f);
        }
    }
}
