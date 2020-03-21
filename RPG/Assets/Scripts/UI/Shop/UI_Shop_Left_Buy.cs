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

            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            if (UI_Shop.Instance.TradeType == TradeType.Sell)
            {
                return;
            }

            ItemParent.Deactivate();

            gameObject.SetActive(false);
        }


        private void Resize()
        {
            var top = Screen.height - UI_Shop_Left.START_Y;
            var bot = UI_Chat_Main.Instance.Dialogue.RectTransform.position.y + (UI_Chat_Main.Instance.Dialogue.RectTransform.sizeDelta.y * 0.5f) + UI_Shop_Left.SPACING;
            var sizeY = Mathf.Abs(top - bot);

            var left = UI_Shop_Left.START_X;
            var right = (Screen.width * 0.5f) - UI_Shop_Left.END_X;
            var sizeX = right - left;

            var posX = left + (sizeX * 0.5f);
            var posY = top - (sizeY * 0.5f);

            var priceSize = Price.sizeDelta;
            var pricePosX = right - (priceSize.x * 0.5f);
            var pricePosY = top;

            var stockSize = Stock.sizeDelta;
            var stockPosX = pricePosX - (priceSize.x * 0.5f) - UI_Shop_Left.SPACING - (stockSize.x * 0.5f);
            var stockPosY = pricePosY;

            var itSizeX = stockPosX - (stockSize.x * 0.5f) - UI_Shop_Left.SPACING - left;
            var itSizeY = stockSize.y;
            var itPosX = stockPosX - (stockSize.x * 0.5f) - UI_Shop_Left.SPACING - (itSizeX * 0.5f);
            var itPosY = pricePosY;

            RectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            RectTransform.position = new Vector3(posX, posY);

            Price.position = new Vector3(pricePosX, pricePosY);
            Stock.position = new Vector3(stockPosX, stockPosY);
            ItemTypes.RectTransform.sizeDelta = new Vector2(itSizeX, itSizeY);
            ItemTypes.RectTransform.position = new Vector3(itPosX, itPosY);
        }
    }
}
