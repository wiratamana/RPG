using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Left : MonoBehaviour
    {
        private UI_Shop shop;
        public UI_Shop Shop => this.GetAndAssignComponentInParent(ref shop);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Left_ItemTypes itemTypes;
        [SerializeField] private RectTransform stock;
        [SerializeField] private RectTransform price;

        public UI_Shop_Left_ItemTypes ItemTypes => this.GetAndAssignComponentInChildren(ref itemTypes);
        public RectTransform Stock => stock;
        public RectTransform Price => price;

        public void Activate()
        {
            var startY = 200.0f;
            var startX = 100.0f;
            var endX = 50.0f;
            var spacing = 15.0f;

            var priceWidth = Price.sizeDelta.x;
            var parentHeigth = RectTransform.sizeDelta.y;
            var parentWidth = RectTransform.sizeDelta.x;

            Price.localPosition = new Vector3((parentWidth * 0.5f) - (priceWidth * 0.5f) - endX, 
                (parentHeigth * 0.5f) - startY);

            Stock.localPosition = Price.localPosition - new Vector3(spacing + Stock.sizeDelta.x, 0);

            var stockLeftPos = Stock.position.x - (Stock.sizeDelta.x * 0.5f);
            var itemTypeWidth = Mathf.Abs(startX - stockLeftPos) - spacing;
            ItemTypes.RectTransform.sizeDelta = new Vector2(itemTypeWidth, Price.sizeDelta.y);
            ItemTypes.RectTransform.position = new Vector3(startX + (itemTypeWidth * 0.5f), Price.position.y);
        }
    }
}
