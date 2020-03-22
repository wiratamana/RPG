using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Right : MonoBehaviour
    {
        public const float START_Y = 200.0f;
        public const float START_X = 50.0f;
        public const float END_X = 100.0f;
        public const float SPACING = 15.0f;

        private UI_Shop shop;
        public UI_Shop Shop => this.GetAndAssignComponentInParent(ref shop);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Right_ItemDetails itemDetails;
        private UI_Shop_Right_Money money;
        public UI_Shop_Right_ItemDetails ItemDetails => this.GetAndAssignComponentInChildren(ref itemDetails);
        public UI_Shop_Right_Money Money => this.GetAndAssignComponentInChildren(ref money);

        public void Activate()
        {
            Resize();

            gameObject.SetActive(true);

            Money.Activate();
            ItemDetails.Activate();
        }

        public void Deactivate()
        {
            ItemDetails.Deactivate();
            gameObject.SetActive(false);
        }

        private void Resize()
        {
            var leftSize = Shop.Left.RectTransform.sizeDelta;
            var leftPos = Shop.Left.RectTransform.position;

            RectTransform.position = leftPos + new Vector3(leftSize.x, 0, 0);
        }
    }
}
