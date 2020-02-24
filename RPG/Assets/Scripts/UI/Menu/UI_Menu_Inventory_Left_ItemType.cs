using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemType : SingletonMonobehaviour<UI_Menu_Inventory_Left_ItemType>
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

        protected override void Awake()
        {
            base.Awake();

            InstantiateItemTypeIcons();
        }

        private void InstantiateItemTypeIcons()
        {
            var itemTypeCount = System.Enum.GetNames(typeof(UI_Menu_Inventory.InventoryItemType)).Length;
            var iconSize = 64;
            var spacing = 15.0f;

            var drawerHorizontalSize = spacing * (itemTypeCount - 1) + (iconSize * itemTypeCount);
            var position = new Vector3((drawerHorizontalSize * -0.5f) + (iconSize * 0.5f), 0.0f);

            for(int i = 0; i < itemTypeCount; i++)
            {
                Image parent;
                Image ring;
                Image img;

                parent = UI_Menu_Pool.Instance.GetImage(RectTransform, iconSize, iconSize, ((UI_Menu_Inventory.InventoryItemType)i).ToString());
                parent.rectTransform.localPosition = position;
                parent.color = Color.black;

                ring = UI_Menu_Pool.Instance.GetImage(parent.rectTransform, iconSize - 4, iconSize - 4, nameof(ring));
                ring.rectTransform.localPosition = Vector3.zero;
                ring.sprite = UI_Menu.Instance.MenuResources.InventoryItemIconRing_Sprite;
                ring.type = Image.Type.Sliced;
                ring.color = Color.white;

                img = UI_Menu_Pool.Instance.GetImage(parent.rectTransform, iconSize - 16, iconSize - 16, nameof(img));
                img.rectTransform.localPosition = Vector3.zero;
                img.sprite = UI_Menu.Instance.MenuResources.GetItemTypeSprites((UI_Menu_Inventory.InventoryItemType)i);

                position += new Vector3(spacing + iconSize, 0.0f);
            }
        }
    }
}