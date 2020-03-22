using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Left_Sell_ItemTypes : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Left_Sell sell;
        public UI_Shop_Left_Sell Sell => this.GetAndAssignComponentInParent(ref sell);

        private Dictionary<ItemType, UI_Shop_Left_Sell_ItemTypes_Child> types;
        public Dictionary<ItemType, UI_Shop_Left_Sell_ItemTypes_Child> Types
        {
            get
            {
                if(types == null || types.Count == 0)
                {
                    types = new Dictionary<ItemType, UI_Shop_Left_Sell_ItemTypes_Child>();

                    foreach(var i in GetComponentsInChildren<UI_Shop_Left_Sell_ItemTypes_Child>())
                    {
                        types.Add(i.ItemType, i);
                    }
                }

                return types;
            }
        }

        public ItemType ItemType { get; private set; }

        private void Awake()
        {
            ItemType = ItemType.Weapon;
            UI_Shop.Instance.OnClosed.AddListener(ResetItemTypeToWeapon);
        }

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            foreach(var i in Types)
            {
                i.Value.OnItemTypeChanged.AddListener(OnItemTypeChanged);

                if(i.Key == ItemType)
                {
                    i.Value.Activate();
                    continue;
                }

                i.Value.Deactivate();
            }
        }

        public void Deactivate()
        {
            foreach (var i in Types)
            {
                i.Value.OnItemTypeChanged.RemoveListener(OnItemTypeChanged);
            }
        }

        private void Resize()
        {
            var right = (Screen.width * 0.5f) - UI_Shop_Left.END_X;
            var left = UI_Shop_Left.START_X;
            var sizeX = Mathf.Abs(left - right);
            var startY = Screen.height - UI_Shop_Left.START_Y;
            var sizeY = Sell.Left.Buy.Price.sizeDelta.y;

            RectTransform.sizeDelta = new Vector2(sizeX, RectTransform.sizeDelta.y);
            RectTransform.position = new Vector3(left + (sizeX * 0.5f), startY - (sizeY * 0.5f) + (RectTransform.sizeDelta.y * 0.5f));
        }

        private void OnItemTypeChanged(ItemType itemType)
        {
            if(ItemType == itemType)
            {
                return;
            }

            Types[ItemType].Deactivate();
            Types[itemType].Activate();

            ItemType = itemType;

            Sell.ItemParent.Deactivate();
            Sell.ItemParent.Activate();
        }

        private void ResetItemTypeToWeapon()
        {
            ItemType = ItemType.Weapon;
        }
    }
}