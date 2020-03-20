using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Left_Buy : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Left left;
        public UI_Shop_Left Left => this.GetAndAssignComponentInParent(ref left);

        private UI_Shop_Left_Buy_ItemTypes itemTypes;
        private UI_Shop_Left_Buy_ItemParent itemParent;
        [SerializeField] private RectTransform stock;
        [SerializeField] private RectTransform price;

        public UI_Shop_Left_Buy_ItemTypes ItemTypes => this.GetAndAssignComponentInChildren(ref itemTypes);
        public UI_Shop_Left_Buy_ItemParent ItemParent => this.GetAndAssignComponentInChildren(ref itemParent);
        public RectTransform Stock => stock;
        public RectTransform Price => price;

        public void Activate()
        {
            if (GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            ItemParent.Activate();
        }

        public void Deactivate()
        {

        }


        private void Resize()
        {
            var priceWidth = Price.sizeDelta.x;
            var parentHeigth = RectTransform.sizeDelta.y;
            var parentWidth = RectTransform.sizeDelta.x;

            Price.localPosition = new Vector3((parentWidth * 0.5f) - (priceWidth * 0.5f) - UI_Shop_Left.END_X,
                (parentHeigth * 0.5f) - UI_Shop_Left.START_Y);

            Stock.localPosition = Price.localPosition - new Vector3(UI_Shop_Left.SPACING + Stock.sizeDelta.x, 0);

            var stockLeftPos = Stock.position.x - (Stock.sizeDelta.x * 0.5f);
            var itemTypeWidth = Mathf.Abs(UI_Shop_Left.START_X - stockLeftPos) - UI_Shop_Left.SPACING;
            ItemTypes.RectTransform.sizeDelta = new Vector2(itemTypeWidth, Price.sizeDelta.y);
            ItemTypes.RectTransform.position = new Vector3(UI_Shop_Left.START_X + (itemTypeWidth * 0.5f), Price.position.y);
        }
    }
}
