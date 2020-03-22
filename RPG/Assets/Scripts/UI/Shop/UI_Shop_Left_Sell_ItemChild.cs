using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Shop_Left_Sell_ItemChild : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Left_Sell_ItemParent itemParent;
        public UI_Shop_Left_Sell_ItemParent ItemParent => this.GetAndAssignComponentInParent(ref itemParent);

        private Item_Product itemProduct;
        private UI_Selection uiSelection;
        private Item_Preview itemPreview;
        private RenderTexture renderTexture;

        [SerializeField] private Image background;
        [SerializeField] private Image ring;
        [SerializeField] private RawImage icon;
        private EventTrigger eventTrigger;

        public Image Background => background;
        public Image Ring => ring;
        public RawImage Icon => icon;
        public EventTrigger EventTrigger
        {
            get
            {
                if (eventTrigger == null)
                {
                    eventTrigger = Background.GetComponent<EventTrigger>() ?? Background.gameObject.AddComponent<EventTrigger>();
                }

                return eventTrigger;
            }
        }

        public static readonly Vector2 Size128x128 = new Vector2(128.0f, 128.0f);

        private void Awake()
        {
            EventTrigger.AddListener(EventTriggerType.PointerClick, OnPointerClick);
            EventTrigger.AddListener(EventTriggerType.PointerEnter, OnPointerEnter);
            EventTrigger.AddListener(EventTriggerType.PointerExit, OnPointerExit);
        }

        public void Initialize(Item_Product itemProduct, in Vector3 position)
        {
            this.itemProduct = itemProduct;
            RectTransform.sizeDelta = Size128x128;
            RectTransform.position = position;

            renderTexture = new RenderTexture((int)Size128x128.x, (int)Size128x128.y, 16, RenderTextureFormat.ARGBHalf);
            icon.texture = renderTexture;
            itemPreview = Item_Preview.InstantiateItemPrefab(itemProduct.Product, position);

            RenderItemForFirstTime();
        }

        private async void RenderItemForFirstTime()
        {
            await AsyncManager.WaitForFrame(1);

            UI_ItemRenderer.SetTexture(renderTexture);
            UI_ItemRenderer.ResetCameraPositionAndRotation(itemPreview.ItemBase, itemPreview.transform);
            UI_ItemRenderer.Render();
            UI_ItemRenderer.SetTexture(null);
        }

        private void OnPointerClick(BaseEventData eventData)
        {
            UI_Selection.DestroyInstance();
            ItemParent.OnSelectedItemChanged.Invoke(null);

            ItemParent.Sell.Left.Shop.OpenTradeMenu(itemProduct);
        }

        private void OnPointerEnter(BaseEventData eventData)
        {
            UI_Selection.CreateInstance(RectTransform, 24);
            ItemParent.OnSelectedItemChanged.Invoke(itemProduct);
        }

        private void OnPointerExit(BaseEventData eventData)
        {
            UI_Selection.DestroyInstance();
            ItemParent.OnSelectedItemChanged.Invoke(null);
        }

        private void OnDestroy()
        {
            if (renderTexture != null)
            {
                Destroy(renderTexture);
            }

            if(itemPreview != null)
            {
                Destroy(itemPreview);
            }

            Destroy(gameObject);
        }
    }
}