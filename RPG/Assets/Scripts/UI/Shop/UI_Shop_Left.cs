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

        private UI_Shop_Left_Buy buy;
        private UI_Shop_Left_Sell sell;
        public UI_Shop_Left_Buy Buy => this.GetAndAssignComponentInChildren(ref buy);
        public UI_Shop_Left_Sell Sell => this.GetAndAssignComponentInChildren(ref sell);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        public void Activate(in TradeType tradeType = TradeType.Buy)
        {
            gameObject.SetActive(true);

            if (tradeType == TradeType.Buy)
            {
                Buy.Activate();
            }
            else
            {
                Sell.Activate();
            }
        }

        public void Deactivate()
        {
            if (UI_Shop.Instance.TradeType == TradeType.Buy)
            {
                Buy.Deactivate();
            }
            else
            {
                Sell.Deactivate();
            }

            gameObject.SetActive(false);
        }
    }
}
