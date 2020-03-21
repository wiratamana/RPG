using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Left_Sell : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Left left;
        public UI_Shop_Left Left => this.GetAndAssignComponentInParent(ref left);

        private UI_Shop_Left_Sell_ItemTypes itemTypes;
        private UI_Shop_Left_Sell_ItemParent itemParent;
        public UI_Shop_Left_Sell_ItemTypes ItemTypes => this.GetAndAssignComponentInChildren(ref itemTypes);
        public UI_Shop_Left_Sell_ItemParent ItemParent => this.GetAndAssignComponentInChildren(ref itemParent);

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            gameObject.SetActive(true);

            ItemTypes.Activate();
            ItemParent.Activate();
        }

        public void Deactivate()
        {
            if (UI_Shop.Instance.TradeType == TradeType.Buy)
            {
                return;
            }

            ItemTypes.Deactivate();
            ItemParent.Deactivate();

            gameObject.SetActive(false);
        }

        private void Resize()
        {
            RectTransform.sizeDelta = Left.RectTransform.sizeDelta;
            RectTransform.position = Left.RectTransform.position;
        }
    }
}
