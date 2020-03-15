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
    }
}
