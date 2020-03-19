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

        private UI_Shop_Mid_PurchaseMenu purchaseConfirmation;
        public UI_Shop_Mid_PurchaseMenu PurchaseConfirmation => this.GetAndAssignComponentInChildren(ref purchaseConfirmation);       

        public Item_Product ItemProduct { get; private set; }

        public void Activate(Item_Product itemProduct)
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            ItemProduct = itemProduct;
            gameObject.SetActive(true);
            PurchaseConfirmation.Activate();
        }

        public void Deactivate()
        {
            ItemProduct = null;

            gameObject.SetActive(false);
            PurchaseConfirmation.Deactivate();
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
