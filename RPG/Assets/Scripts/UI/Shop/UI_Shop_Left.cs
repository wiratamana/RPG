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
        public UI_Shop_Left_Buy Buy => this.GetAndAssignComponentInChildren(ref buy);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        public void Activate()
        {       
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            gameObject.SetActive(true);
            Buy.Activate();
        }

        public void Deactivate()
        {
            Buy.Deactivate();
            gameObject.SetActive(false);
        }

        private void Resize()
        {
            RectTransform.sizeDelta = Buy.RectTransform.sizeDelta;
            RectTransform.position = Buy.RectTransform.position;
        }
    }
}
