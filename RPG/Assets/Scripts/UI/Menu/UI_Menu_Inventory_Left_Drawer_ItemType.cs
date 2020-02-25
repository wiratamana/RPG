﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_Drawer_ItemType : SingletonMonobehaviour<UI_Menu_Inventory_Left_Drawer_ItemType>
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        private Dictionary<UI_Menu_Inventory_Left_ItemType, ItemType> itemTypeDic;
        public Dictionary<UI_Menu_Inventory_Left_ItemType, ItemType> ItemTypeDic
        {
            get
            {
                if(itemTypeDic == null || itemTypeDic.Count == 0)
                {
                    itemTypeDic = new Dictionary<UI_Menu_Inventory_Left_ItemType, ItemType>();

                    for(int i = 0; i < RectTransform.childCount; i++)
                    {
                        var comp = RectTransform.GetChild(i).GetComponent<UI_Menu_Inventory_Left_ItemType>();
                        var type = (ItemType)System.Enum.Parse(typeof(ItemType), RectTransform.GetChild(i).name);

                        if(comp != null)
                        {
                            itemTypeDic.Add(comp, type);
                        }
                    }
                }

                return itemTypeDic;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            var itemTypeCount = System.Enum.GetNames(typeof(ItemType)).Length;
            var iconSize = UI_Menu_Inventory_Left_ItemType.PARENT_SIZE;
            var spacing = 15.0f;

            var drawerHorizontalSize = spacing * (itemTypeCount - 1) + (iconSize * itemTypeCount);
            var position = new Vector3((drawerHorizontalSize * -0.5f) + (iconSize * 0.5f), 0.0f);

            for (int i = 0; i < itemTypeCount; i++)
            {
                var itemTypeGO = new GameObject(((ItemType)i).ToString());
                var itemTypeRT = itemTypeGO.AddComponent<RectTransform>();
                itemTypeRT.SetParent(RectTransform);
                itemTypeRT.sizeDelta = new Vector2(iconSize, iconSize);
                itemTypeRT.localPosition = position;

                itemTypeGO.AddComponent<UI_Menu_Inventory_Left_ItemType>();

                position += new Vector3(spacing + iconSize, 0.0f);
            }
        }
    }
}