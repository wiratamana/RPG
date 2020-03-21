using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Mid_TradeMenu : MonoBehaviour
    {
        private UI_Shop_Mid mid;
        public UI_Shop_Mid Mid => this.GetAndAssignComponentInParent(ref mid);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Mid_TradeMenu_Confirmation confirmation;
        private UI_Shop_Mid_TradeMenu_Details details;
        public UI_Shop_Mid_TradeMenu_Confirmation Confirmation => this.GetAndAssignComponentInChildren(ref confirmation);
        public UI_Shop_Mid_TradeMenu_Details Details => this.GetAndAssignComponentInChildren(ref details);

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            Confirmation.Activate();
            Details.Activate();
        }

        public void Deactivate()
        {
            Confirmation.Deactivate();
            Details.Deactivate();
        }

        private void Resize()
        {
            var top = Mid.Shop.Left.Buy.Stock.position.y + (Mid.Shop.Left.Buy.Stock.sizeDelta.y * 0.5f);
            var bot = UI_Chat_Main.Instance.Dialogue.RectTransform.position.y + (UI_Chat_Main.Instance.Dialogue.RectTransform.sizeDelta.y * 0.5f);
            var sizeY = Mathf.Abs(top - bot);
            var sizeX = Mid.RectTransform.sizeDelta.x;
            var spacing = 20.0f;

            RectTransform.sizeDelta = new Vector2(sizeX, sizeY - (spacing * 2));
            RectTransform.position = new Vector3(Screen.width * 0.5f, bot + (sizeY * 0.5f));
        }
    }
}
