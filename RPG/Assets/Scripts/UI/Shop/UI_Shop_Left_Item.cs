using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Tamana
{
    public class UI_Shop_Left_Item : MonoBehaviour
    {
        private RectTransform rectTransform;
        [SerializeField] private Image background;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI stock;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private RawImage itemRenderer;
        private EventTrigger eventTrigger;

        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);
        public Image Background => background;
        public TextMeshProUGUI ItemName => itemName;
        public TextMeshProUGUI Stock => stock;
        public TextMeshProUGUI Price => price;
        public RawImage ItemRenderer => itemRenderer;
        public EventTrigger EventTrigger => Background.GetOrAddAndAssignComponent(ref eventTrigger);

        public Item_Preview ItemPreview { get; private set; }
        private UI_Shop_Left_ItemParent itemParent;
        private Item_Product itemProduct;

        public void Initialize(Item_Product itemProduct, UI_Shop_Left_ItemParent itemParent, in Vector3 pos)
        {
            this.itemParent = itemParent;
            this.itemProduct = itemProduct;
            var texSize = ItemRenderer.rectTransform.sizeDelta;
            ItemRenderer.texture = new RenderTexture((int)texSize.x, (int)texSize.y, 16, RenderTextureFormat.ARGBHalf);

            ItemPreview = Item_Preview.InstantiateItemPrefab(itemProduct.Product, new Vector2(transform.GetSiblingIndex(), 1000));

            ItemName.enabled = false;
            Stock.enabled = false;
            Price.enabled = false;

            if (GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            ItemName.text = itemProduct.Product.ItemName;
            Stock.text = itemProduct.Stock.ToString();
            Price.text = itemProduct.Price.ToString();
            Price.color = PlayerData.Money < itemProduct.Price ? Color.red : Color.white;

            RectTransform.position = pos;
            gameObject.SetActive(true);

            EventTrigger.AddListener(EventTriggerType.PointerEnter, OnPointerEnter);
            EventTrigger.AddListener(EventTriggerType.PointerExit, OnPointerExit);
            EventTrigger.AddListener(EventTriggerType.PointerClick, OnPointerClick);
        }

        private void OnDestroy()
        {
            if(itemRenderer.texture != null)
            {
                Destroy(ItemRenderer.texture);
            }

            itemParent.OnSelectedItemChanged.Invoke(null);
            Destroy(ItemPreview);
            ItemPreview = null;
            Destroy(gameObject);
        }

        private void OnPointerEnter(BaseEventData eventData)
        {
            UI_Selection.CreateInstance(RectTransform, 24);
            itemParent.OnSelectedItemChanged.Invoke(itemProduct);
        }

        private void OnPointerExit(BaseEventData eventData)
        {
            UI_Selection.DestroyInstance();
            itemParent.OnSelectedItemChanged.Invoke(null);
        }

        private void OnPointerClick(BaseEventData eventData)
        {
            if(PlayerData.Money < itemProduct.Price)
            {
                Debug.Log("Not enough money to purchase item");
                return;
            }

            UI_Selection.DestroyInstance();
            itemParent.OnSelectedItemChanged.Invoke(null);
            itemParent.Left.Shop.OpenPurchaseConfirmation(itemProduct);
        }

        private async void Resize()
        {
            RectTransform.sizeDelta = new Vector2(itemParent.RectTransform.sizeDelta.x, RectTransform.sizeDelta.y);

            await AsyncManager.WaitForFrame(1);

            var nameLeft = ItemRenderer.rectTransform.position.x + (ItemRenderer.rectTransform.sizeDelta.x * 0.5f);
            var nameRight = itemParent.Left.ItemTypes.RectTransform.position.x + (itemParent.Left.ItemTypes.RectTransform.sizeDelta.x * 0.5f);
            var width = Mathf.Abs(nameLeft - nameRight);

            ItemName.rectTransform.sizeDelta = new Vector2(width, ItemName.rectTransform.sizeDelta.y);
            ItemName.rectTransform.position = new Vector3(nameLeft + (width * 0.5f), ItemName.rectTransform.position.y);

            var stockPos = itemParent.Left.Stock.position.x;
            var stockWidth = itemParent.Left.Stock.sizeDelta.x;
            Stock.rectTransform.sizeDelta = new Vector2(stockWidth, Stock.rectTransform.sizeDelta.y);
            Stock.rectTransform.position = new Vector3(stockPos, Stock.rectTransform.position.y);

            var pricePos = itemParent.Left.Price.position.x;
            var priceWidth = itemParent.Left.Price.sizeDelta.x;
            Price.rectTransform.sizeDelta = new Vector2(priceWidth, Stock.rectTransform.sizeDelta.y);
            Price.rectTransform.position = new Vector3(pricePos, Stock.rectTransform.position.y);

            ItemName.enabled = true;
            Stock.enabled = true;
            Price.enabled = true;
        }
    }
}
