using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tamana
{
    public class UI_Shop_Right_ItemDetails : MonoBehaviour
    {
        private UI_Shop_Right right;
        public UI_Shop_Right Right => this.GetAndAssignComponentInParent(ref right);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        [SerializeField] private Image background;
        [SerializeField] private Image ring;
        [SerializeField] private RawImage itemRenderer;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDesription;
        private UI_Shop_Right_ItemDetails_Effect effect;

        public UI_Shop_Right_ItemDetails_Effect Effect => this.GetAndAssignComponentInChildren(ref effect);

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            Right.Shop.Left.ItemParent.OnSelectedItemChanged.AddListener(OnSelectedItemChanged, GetInstanceID());
        }

        public void Deactivate()
        {
            Right.Shop.Left.ItemParent.OnSelectedItemChanged.RemoveListener(OnSelectedItemChanged, GetInstanceID());

            if(itemRenderer.texture != null)
            {
                Destroy(itemRenderer.texture);
            }

            Effect.Deactivate();
        }

        private void OnSelectedItemChanged(Item_Product itemProduct)
        {
            Debug.Log(itemProduct == null ? "null" : itemProduct.Product.ItemName);
        }

        private void Resize()
        {
            var itemParentRT = Right.Shop.Left.ItemParent.RectTransform;
            var size = itemParentRT.sizeDelta;
            var localPos = itemParentRT.localPosition;
            localPos.x = -localPos.x;

            RectTransform.sizeDelta = size;
            RectTransform.localPosition = localPos;

            var pos = RectTransform.position;
            var offset = 20.0f;
            var leftTop = new Vector3(pos.x - (size.x * 0.5f), pos.y + (size.y * 0.5f));
            var itemRendererSize = size.y * 0.5f - offset;
            itemRenderer.rectTransform.sizeDelta = new Vector2(itemRendererSize, itemRendererSize);
            itemRenderer.rectTransform.position = leftTop + new Vector3((itemRendererSize * 0.5f) + offset, (itemRendererSize * -0.5f) - offset);

            pos = new Vector3(RectTransform.position.x, itemRenderer.rectTransform.position.y - (itemRendererSize * 0.5f));
            var itemNameSize = new Vector2(size.x - (offset * 2), size.y * 0.10f);
            itemName.rectTransform.sizeDelta = itemNameSize;
            itemName.rectTransform.position = pos - new Vector3(0, itemNameSize.y * 0.5f);

            var effectSize = itemName.rectTransform.sizeDelta;
            effectSize.y *= 0.5f;
            Effect.RectTransform.sizeDelta = effectSize;
            Effect.RectTransform.position = new Vector3(itemName.rectTransform.position.x,
                itemName.rectTransform.position.y - (itemNameSize.y * 0.5f) - (effectSize.y * 0.5f));

            pos = new Vector3(Effect.RectTransform.position.x, Effect.RectTransform.position.y - (effectSize.y * 0.5f));
            var bot = RectTransform.position.y - (RectTransform.sizeDelta.y * 0.5f);
            var descSize = new Vector2(effectSize.x, Mathf.Abs(pos.y - bot) - offset);
            itemDesription.rectTransform.sizeDelta = descSize;
            itemDesription.rectTransform.position = pos - new Vector3(0, descSize.y * 0.5f);
        }
    }
}
