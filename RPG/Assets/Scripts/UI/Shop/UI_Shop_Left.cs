using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public partial class UI_Shop_Left : MonoBehaviour
    {
        public const float START_Y = 200.0f;
        public const float START_X = 100.0f;
        public const float END_X = 50.0f;
        public const float SPACING = 15.0f;

        private UI_Shop shop;
        public UI_Shop Shop => this.GetAndAssignComponentInParent(ref shop);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Left_ItemTypes itemTypes;
        private UI_Shop_Left_ItemParent itemParent;
        [SerializeField] private RectTransform stock;
        [SerializeField] private RectTransform price;

        public UI_Shop_Left_ItemTypes ItemTypes => this.GetAndAssignComponentInChildren(ref itemTypes);
        public UI_Shop_Left_ItemParent ItemParent => this.GetAndAssignComponentInChildren(ref itemParent);
        public RectTransform Stock => stock;
        public RectTransform Price => price;

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }
           
            gameObject.SetActive(true);
            ItemParent.Activate();
        }

        public void Deactivate()
        {
            ItemParent.Deactivate();
            gameObject.SetActive(false);
        }

        private void Resize()
        {
            var priceWidth = Price.sizeDelta.x;
            var parentHeigth = RectTransform.sizeDelta.y;
            var parentWidth = RectTransform.sizeDelta.x;

            Price.localPosition = new Vector3((parentWidth * 0.5f) - (priceWidth * 0.5f) - END_X,
                (parentHeigth * 0.5f) - START_Y);

            Stock.localPosition = Price.localPosition - new Vector3(SPACING + Stock.sizeDelta.x, 0);

            var stockLeftPos = Stock.position.x - (Stock.sizeDelta.x * 0.5f);
            var itemTypeWidth = Mathf.Abs(START_X - stockLeftPos) - SPACING;
            ItemTypes.RectTransform.sizeDelta = new Vector2(itemTypeWidth, Price.sizeDelta.y);
            ItemTypes.RectTransform.position = new Vector3(START_X + (itemTypeWidth * 0.5f), Price.position.y);
        }
    }
}
