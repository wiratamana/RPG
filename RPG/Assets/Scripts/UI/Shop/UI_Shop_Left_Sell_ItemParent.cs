using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Left_Sell_ItemParent : MonoBehaviour
    {
        private struct CellProperty
        {
            public readonly float n128;
            public readonly Vector3 StartPosition;
            public readonly int HorizontalTotal;
            public readonly float HorizontalSpace;
            public readonly int VerticalTotal;
            public readonly float VerticalSpace;

            public CellProperty(UI_Shop_Left_Sell_ItemParent itemParent)
            {
                n128 = UI_Shop_Left_Sell_ItemChild.Size128x128.x;

                HorizontalTotal = (int)(itemParent.RectTransform.sizeDelta.x / n128);
                var remainingSpace = itemParent.RectTransform.sizeDelta.x - (HorizontalTotal * n128);
                HorizontalSpace = remainingSpace / (HorizontalTotal - 1);

                VerticalTotal = (int)(itemParent.RectTransform.sizeDelta.y / n128);
                var remainingVer = itemParent.RectTransform.sizeDelta.y - (VerticalTotal * n128);
                VerticalSpace = remainingVer / (VerticalTotal - 1);

                var left = itemParent.RectTransform.position.x - (itemParent.RectTransform.sizeDelta.x * 0.5f);
                var top = itemParent.RectTransform.position.y + (itemParent.RectTransform.sizeDelta.y * 0.5f);

                StartPosition = new Vector3(left + (n128 * 0.5f), top - (n128 * 0.5f));
            }
        }

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Left_Sell sell;
        public UI_Shop_Left_Sell Sell => this.GetAndAssignComponentInParent(ref sell);

        public EventManager<Item_Product> OnSelectedItemChanged { get; } = new EventManager<Item_Product>();

        [SerializeField] private UI_Shop_Left_Sell_ItemChild prefab;

        private UI_Shop_Left_Sell_ItemChild[] items;

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

            InstantiateItem();
        }

        public void Deactivate()
        {
            if(items != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    Destroy(items[i]);
                }

                items = null;
            }
        }

        private void Resize()
        {
            RectTransform.sizeDelta = Sell.Left.Buy.ItemParent.RectTransform.sizeDelta;
            RectTransform.position = Sell.Left.Buy.ItemParent.RectTransform.position;
        }

        private void InstantiateItem()
        {                     
            var itemList = GameManager.Player.Inventory.GetItemListAsReadOnly(x => x.ItemType == Sell.ItemTypes.ItemType);
            var n = itemList.Count;
            items = new UI_Shop_Left_Sell_ItemChild[n];

            var cellProperty = new CellProperty(this);

            var currentPosition = cellProperty.StartPosition;
            var horizontalBuffer = cellProperty.HorizontalTotal;
            var verticalLimit = cellProperty.VerticalTotal;
            for (int i = 0; i < n; i++)
            {
                var product = Item_Product.CreateProduct(itemList[i]);
                var item = Instantiate(prefab, RectTransform);
                item.gameObject.SetActive(true);
                item.Initialize(product, currentPosition);
                items[i] = item;

                horizontalBuffer--;
                if(horizontalBuffer == 0)
                {
                    verticalLimit--;
                    if(verticalLimit == 0)
                    {
                        break;
                    }

                    horizontalBuffer = cellProperty.HorizontalTotal;
                    currentPosition.y -= cellProperty.n128 + cellProperty.VerticalSpace;
                    currentPosition.x = cellProperty.StartPosition.x;
                }
                else
                {
                    currentPosition.x += cellProperty.n128 + cellProperty.HorizontalSpace;
                }
            }
        }

        private void ResetSettings()
        {
            OnSelectedItemChanged.RemoveAllListener();
        }
    }
}
