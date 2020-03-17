using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Right : MonoBehaviour
    {
        private UI_Shop shop;
        public UI_Shop Shop => this.GetAndAssignComponentInParent(ref shop);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Right_ItemDetails itemDetails;
        public UI_Shop_Right_ItemDetails ItemDetails => this.GetAndAssignComponentInChildren(ref itemDetails);

        public void Activate()
        {
            Resize();

            gameObject.SetActive(true);

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
