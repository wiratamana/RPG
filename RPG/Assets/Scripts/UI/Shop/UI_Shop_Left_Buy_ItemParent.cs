using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tamana
{
    public class UI_Shop_Left_Buy_ItemParent : MonoBehaviour
    {
        [SerializeField] private UI_Shop_Left_Buy_ItemChild itemPrefab;

        private RectTransform rectTransform;
        private UI_Shop_Left_Buy buy;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);
        public UI_Shop_Left_Buy Buy => this.GetAndAssignComponentInParent(ref buy);

        public EventManager<Item_Product> OnSelectedItemChanged { get; } = new EventManager<Item_Product>();

        private UI_Shop_Left_Buy_ItemChild[] items;

        private EventTrigger eventTrigger;
        public EventTrigger EventTrigger => this.GetAndAssignComponent(ref eventTrigger);

        private bool isScrollEventRegistered;
        private int maxVisibleItem = -1;
        private int startIndex = 0;

        private void Awake()
        {
            UI_Shop.Instance.OnClosed.AddListener(ResetSettings);
        }

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            if(maxVisibleItem == -1)
            {
                maxVisibleItem = 1 + (int)((RectTransform.sizeDelta.y - itemPrefab.RectTransform.sizeDelta.y) / 110);
            }            

            isScrollEventRegistered = false;

            EventTrigger.AddListener(EventTriggerType.PointerEnter, OnPointerEnter);
            EventTrigger.AddListener(EventTriggerType.PointerExit, OnPointerExit);
            
            InstantiateItem();
        }

        public void Deactivate()
        {
            if(isScrollEventRegistered == true)
            {
                InputEvent.Instance.Event_ScrollDown.RemoveListener(ScrollDown);
                InputEvent.Instance.Event_ScrollUp.RemoveListener(ScrollUp);

                isScrollEventRegistered = false;
            }

            EventTrigger.RemoveEntry(EventTriggerType.PointerEnter);
            EventTrigger.RemoveEntry(EventTriggerType.PointerExit);

            if(items != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    Destroy(items[i]);
                }

                items = null;
            }           
        }

        private async void InstantiateItem()
        {
            var products = Buy.Left.Shop.Products;
            var position = new Vector3(RectTransform.position.x, RectTransform.position.y + (RectTransform.sizeDelta.y * 0.5f));
            var spacingPerItem = 10.0f;
            position.y -= itemPrefab.RectTransform.sizeDelta.y * 0.5f;

            items = new UI_Shop_Left_Buy_ItemChild[maxVisibleItem];
            for (int i = startIndex; i < startIndex + maxVisibleItem; i++)
            {
                if(i > products.Count - 1)
                {
                    break;
                }

                var product = Instantiate(itemPrefab, RectTransform);
                product.Initialize(products.ElementAt(i), this, position);
                items[i - startIndex] = product;

                position.y -= itemPrefab.RectTransform.sizeDelta.y + spacingPerItem;
            }

            await AsyncManager.WaitForFrame(1);

            int index = startIndex;
            foreach (var item in items)
            {
                if (index > products.Count - 1)
                {
                    break;
                }

                UI_ItemRenderer.ResetCameraPositionAndRotation(products.ElementAt(index).Product, item.ItemPreview.transform);
                UI_ItemRenderer.SetTexture(item.ItemRenderer.texture as RenderTexture);
                UI_ItemRenderer.Render();

                index++;
            }

            UI_ItemRenderer.SetTexture(null);
        }

        private void Resize()
        {
            var lpos = Buy.ItemTypes.RectTransform.position;
            var lsize = Buy.ItemTypes.RectTransform.sizeDelta;
            var rpos = Buy.Price.position;
            var rsize = Buy.Price.sizeDelta;
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

        private void OnPointerEnter(BaseEventData eventData)
        {
            InputEvent.Instance.Event_ScrollDown.AddListener(ScrollDown);
            InputEvent.Instance.Event_ScrollUp.AddListener(ScrollUp);

            isScrollEventRegistered = true;
        }

        private void OnPointerExit(BaseEventData eventData)
        {
            InputEvent.Instance.Event_ScrollDown.RemoveListener(ScrollDown);
            InputEvent.Instance.Event_ScrollUp.RemoveListener(ScrollUp);

            isScrollEventRegistered = false;
        }

        private void ScrollUp()
        {
            if(startIndex == 0)
            {
                return;
            }

            startIndex--;

            for (int i = 0; i < items.Length; i++)
            {
                Destroy(items[i]);
            }

            InstantiateItem();
        }

        private void ScrollDown()
        {
            if(Buy.Left.Shop.Products.Count <= startIndex + maxVisibleItem)
            {
                return;
            }

            startIndex++;

            for (int i = 0; i < items.Length; i++)
            {
                Destroy(items[i]);
            }

            InstantiateItem();
        }

        private void ResetSettings()
        {
            startIndex = 0;
            OnSelectedItemChanged.RemoveAllListener();
        }
    }
}