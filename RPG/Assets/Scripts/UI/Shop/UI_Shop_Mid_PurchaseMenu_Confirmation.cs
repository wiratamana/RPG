using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Mid_PurchaseMenu_Confirmation : MonoBehaviour
    {
        private UI_Shop_Mid_PurchaseMenu purchaseMenu;
        public UI_Shop_Mid_PurchaseMenu PurchaseMenu => this.GetAndAssignComponentInParent(ref purchaseMenu);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        public const float HEIGHT = 250.0f;

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }
        }

        public void Deactivate()
        {

        }

        private void Resize()
        {
            var bot = UI_Chat_Main.Instance.Dialogue.RectTransform.position.y + (UI_Chat_Main.Instance.Dialogue.RectTransform.sizeDelta.y * 0.5f);
            var offset = 20.0f;
            var posY = bot + (HEIGHT * 0.5f) + offset;
            var sizeX = PurchaseMenu.Mid.RectTransform.sizeDelta.x;

            RectTransform.sizeDelta = new Vector2(sizeX, HEIGHT);
            rectTransform.position = new Vector3(Screen.width * 0.5f, posY);
        }
    }
}
