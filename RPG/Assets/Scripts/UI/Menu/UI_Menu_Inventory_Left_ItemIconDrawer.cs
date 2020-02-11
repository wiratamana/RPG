using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIconDrawer : SingletonMonobehaviour<UI_Menu_Inventory_Left_ItemIconDrawer>
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

            InstantiateItemIconBackground();
        }

        private void InstantiateItemIconBackground()
        {
            var itemCount = 25;
            var spacing = 10.0f;
            var iconSize = 128;
            var ringSize = 120;
            var horizontalMaxSize = RectTransform.sizeDelta.x;
            var horizontalShowLimit = 1;

            var horizontalSize = (iconSize * horizontalShowLimit) + ((horizontalShowLimit - 1) * spacing);
            while(horizontalSize < (horizontalMaxSize - (spacing * 2)))
            {
                horizontalShowLimit++;
                horizontalSize = (iconSize * horizontalShowLimit) + ((horizontalShowLimit - 1) * spacing);
            }

            horizontalShowLimit--;
            horizontalSize = (iconSize * horizontalShowLimit) + ((horizontalShowLimit - 1) * spacing);

            var horizontalRemainingSlots = horizontalShowLimit;
            var position = new Vector3((horizontalSize * -0.5f) + (iconSize * 0.5f), 
                (RectTransform.sizeDelta.y * 0.5f) - (iconSize * 0.5f) - spacing);

            var backgroundColor = new Color(40.0f / 255.0f, 40.0f / 255.0f, 40.0f / 255.0f, 200.0f / 255.0f);
            var ringColor = new Color(200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 1.0f);

            for(int i = 0; i < itemCount; i++)
            {
                var go = new GameObject("ItemIcon");
                go.transform.SetParent(RectTransform);
                var rt = go.AddComponent<RectTransform>();
                rt.sizeDelta = new Vector2(iconSize, iconSize);
                rt.localPosition = position;

                var backgroundImg = UIManager.CreateImage(rt, iconSize, iconSize, "ItemIcon-Background");
                backgroundImg.rectTransform.localPosition = Vector2.zero;
                backgroundImg.color = backgroundColor;
                backgroundImg.raycastTarget = false;

                var ringImg = UIManager.CreateImage(rt, ringSize, ringSize, "ItemIcon-Ring");
                ringImg.rectTransform.localPosition = Vector2.zero;
                ringImg.color = ringColor;
                ringImg.sprite = UI_Menu.Instance.MenuResources.InventoryItemIconRing_Sprite;
                ringImg.raycastTarget = false;

                horizontalRemainingSlots--;

                if(horizontalRemainingSlots == 0)
                {
                    horizontalRemainingSlots = horizontalShowLimit;
                    position.y -= spacing + iconSize;
                    position.x = (horizontalSize * -0.5f) + (iconSize * 0.5f);
                }
                else
                {
                    position.x += spacing + iconSize;
                }
            }
        }
    }
}
