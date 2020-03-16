using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Left_ItemParent : MonoBehaviour
    {
        private RectTransform rectTransform;
        private UI_Shop_Left left;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);
        public UI_Shop_Left Left => this.GetAndAssignComponentInParent(ref left);

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            
        }

        private void Resize()
        {
            var lpos = Left.ItemTypes.RectTransform.position;
            var lsize = Left.ItemTypes.RectTransform.sizeDelta;
            var rpos = Left.Price.position;
            var rsize = Left.Price.sizeDelta;
            var spacing = 15.0f;

            var left = lpos.x - (lsize.x * 0.5f);
            var right = rpos.x + (rsize.x * 0.5f);
            var bot = rpos.y - (rsize.y * 0.5f);

            var sizex = Mathf.Abs(left - right);
            var sizey = bot - spacing;
            RectTransform.sizeDelta = new Vector2(sizex, sizey);
            RectTransform.position = new Vector3(left + (sizex * 0.5f), sizey * 0.5f);
        }
    }
}