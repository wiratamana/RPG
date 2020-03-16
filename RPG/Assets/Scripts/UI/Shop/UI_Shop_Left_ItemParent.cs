using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Left_ItemParent : MonoBehaviour
    {
        [SerializeField] private UI_Shop_Left_Item itemPrefab;

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
            
            InstantiateItem();
        }

        private void InstantiateItem()
        {
            var products = Left.Shop.Products;
            var spacing = 10;
            var position = new Vector3(RectTransform.position.x, RectTransform.position.y + (RectTransform.sizeDelta.y * 0.5f));
            position.y -= itemPrefab.RectTransform.sizeDelta.y * 0.5f;

            foreach (var i in products)
            {
                var product = Instantiate(itemPrefab, RectTransform);
                product.Initialize(i, this, position);

                position.y -= itemPrefab.RectTransform.sizeDelta.y + spacing;
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