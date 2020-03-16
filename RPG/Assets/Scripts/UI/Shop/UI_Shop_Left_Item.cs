﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tamana
{
    public class UI_Shop_Left_Item : MonoBehaviour
    {
        private RectTransform rectTransform;
        [SerializeField] private Image background;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI stock;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private RawImage itemRenderer;

        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);
        public Image Background => background;
        public TextMeshProUGUI ItemName => itemName;
        public TextMeshProUGUI Stock => stock;
        public TextMeshProUGUI Price => price;
        public RawImage ItemRenderer => itemRenderer;

        private UI_Shop_Left_ItemParent itemParent;

        public void Initialize(Item_Product product, UI_Shop_Left_ItemParent itemParent, Vector3 pos)
        {
            this.itemParent = itemParent;

            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                RectTransform.sizeDelta = new Vector2(this.itemParent.RectTransform.sizeDelta.x, RectTransform.sizeDelta.y);
            }

            ItemName.text = product.Product.ItemName;
            stock.text = product.Stock.ToString();

            RectTransform.position = pos;
            gameObject.SetActive(true);
        }
    }
}
