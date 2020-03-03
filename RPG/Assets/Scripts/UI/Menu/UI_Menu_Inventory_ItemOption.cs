using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_ItemOption : SingletonMonobehaviour<UI_Menu_Inventory_ItemOption>
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

        private List<UI_Menu_Inventory_ItemOption_GenericMenu> genericMenusList = new List<UI_Menu_Inventory_ItemOption_GenericMenu>();

        private Image background;
        private Image ring;

        public static readonly Color BackgroundNormalColor = new Color(40.0f / 255.0f, 40.0f / 255.0f, 40.0f / 255.0f, 250.0f / 255.0f);
        public static readonly Color RingNormalColor = new Color(200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 1.0f);

        public const int WIDTH = 300;
        public const int GENERIC_HEIGHT = 50;
        public const int GENERIC_WIDTH = WIDTH - 50;

        public bool IsActive => gameObject.activeInHierarchy;

        protected override void Awake()
        {
            base.Awake();

            RectTransform.sizeDelta = new Vector2(WIDTH, 100);
            background = UI_Menu_Pool.Instance.GetImage(RectTransform, (int)RectTransform.sizeDelta.x, (int)RectTransform.sizeDelta.y, nameof(background));
            background.rectTransform.localPosition = Vector2.zero;
            background.rectTransform.anchorMin = Vector2.zero;
            background.rectTransform.anchorMax = Vector2.one;
            background.rectTransform.offsetMin = Vector2.zero;
            background.rectTransform.offsetMax = Vector2.zero;
            background.color = BackgroundNormalColor;

            ring = UI_Menu_Pool.Instance.GetImage(RectTransform, (int)RectTransform.sizeDelta.x, (int)RectTransform.sizeDelta.y, nameof(ring));
            ring.rectTransform.localPosition = Vector2.zero;
            ring.rectTransform.anchorMin = Vector2.zero;
            ring.rectTransform.anchorMax = Vector2.one;
            ring.rectTransform.offsetMin = Vector2.zero;
            ring.rectTransform.offsetMax = Vector2.zero;
            ring.sprite = UI_Menu.Instance.MenuResources.InventoryItemIconRing_Sprite;
            ring.type = Image.Type.Sliced;
            ring.rectTransform.sizeDelta -= new Vector2(8, 8);
            ring.color = RingNormalColor;

            gameObject.SetActive(false);

            UI_Menu_Inventory.OnMenuInventoryClosed.AddListener(Close);
        }

        public void Open(UI_Menu_Inventory_Left_ItemIcon itemIcon)
        {
            var verticalSpacing = 15;

            RectTransform.sizeDelta = new Vector2(WIDTH,
                (verticalSpacing * 2) + (GENERIC_HEIGHT * genericMenusList.Count) + (genericMenusList.Count - 1) * verticalSpacing);

            var defaultPosition = new Vector3(0,
                (RectTransform.sizeDelta.y * 0.5f) - verticalSpacing - (GENERIC_HEIGHT * 0.5f));

            for(int i = 0; i < genericMenusList.Count; i++)
            {
                genericMenusList[i].RectTransform.SetParent(RectTransform);
                genericMenusList[i].RectTransform.localPosition = defaultPosition;

                defaultPosition -= new Vector3(0, verticalSpacing + GENERIC_HEIGHT);
            }

            gameObject.SetActive(true);

            RectTransform.position = itemIcon.RectTransform.position + 
                new Vector3(RectTransform.sizeDelta.x * 0.5f, RectTransform.sizeDelta.y * -0.5f);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void RegisterGenericMenu(UI_Menu_Inventory_ItemOption_GenericMenu genericMenu)
        {
            genericMenusList.Add(genericMenu);
        }

        public void ClearRegisteredGenericMenu()
        {
            foreach(var g in genericMenusList)
            {
                UI_Menu_Pool.Instance.RemoveImage(g.Ring);
                UI_Menu_Pool.Instance.RemoveText(g.Text);

                Destroy(g.gameObject);
            }

            genericMenusList.Clear();
        }       
    }
}
