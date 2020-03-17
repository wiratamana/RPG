using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public EventManager<Item_Product> OnSelectedItemChanged { get; } = new EventManager<Item_Product>();

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }
            
            InstantiateItem();
        }

        private async void InstantiateItem()
        {
            var products = Left.Shop.Products;
            var spacing = 10;
            var position = new Vector3(RectTransform.position.x, RectTransform.position.y + (RectTransform.sizeDelta.y * 0.5f));
            position.y -= itemPrefab.RectTransform.sizeDelta.y * 0.5f;

            var items = new List<UI_Shop_Left_Item>(products.Count);

            foreach (var i in products)
            {
                var product = Instantiate(itemPrefab, RectTransform);
                product.Initialize(i, this, position);
                items.Add(product);

                position.y -= itemPrefab.RectTransform.sizeDelta.y + spacing;
            }

            await AsyncManager.WaitForFrame(1);

            int index = 0;
            foreach (var item in items)
            {
                UI_ItemRenderer.ResetCameraPositionAndRotation(products.ElementAt(index).Product, item.ItemPreview.transform);
                UI_ItemRenderer.SetTexture(item.ItemRenderer.texture as RenderTexture);
                UI_ItemRenderer.Render();

                index++;
            }

            UI_ItemRenderer.SetTexture(null);
        }

        private void Resize()
        {
            var lpos = Left.ItemTypes.RectTransform.position;
            var lsize = Left.ItemTypes.RectTransform.sizeDelta;
            var rpos = Left.Price.position;
            var rsize = Left.Price.sizeDelta;
            var bpos = UI_Chat_Main.Instance.Dialogue.RectTransform.position;
            var bsize = UI_Chat_Main.Instance.Dialogue.RectTransform.sizeDelta;
            var spacing = 15.0f;

            var left = lpos.x - (lsize.x * 0.5f);
            var right = rpos.x + (rsize.x * 0.5f);
            var up = rpos.y - (rsize.y * 0.5f);
            var bot = bpos.y + (bsize.y * 0.5f);

            var sizex = Mathf.Abs(left - right);
            var sizey = Mathf.Abs(up - bot) - (spacing * 2);
            RectTransform.sizeDelta = new Vector2(sizex, sizey);
            RectTransform.position = new Vector3(left + (sizex * 0.5f), up - (sizey * 0.5f) - spacing);
        }
    }
}