using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Right_ItemDetails_Effect : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Right_ItemDetails itemDetails;
        public UI_Shop_Right_ItemDetails ItemDetails => this.GetAndAssignComponentInParent(ref itemDetails);
    }
}
