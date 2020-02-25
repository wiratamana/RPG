using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemType : MonoBehaviour
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

        public const int PARENT_SIZE = 64;
        public const int BACKGROUND_SIZE = 64;
        public const int RING_SIZE = 60;
        public const int ICON_SIZE = 48;

        public UI_Menu_Inventory_Left_ItemType_Background Background { private set; get; }
        public UI_Menu_Inventory_Left_ItemType_Ring Ring { private set; get; }
        public UI_Menu_Inventory_Left_ItemType_Icon Icon { private set; get; }

        public ItemType ItemType
        {
            get
            {
                return (ItemType)System.Enum.Parse(typeof(ItemType), name);
            }
        }

        private void Awake()
        {
            var background = UI_Menu_Pool.Instance.GetImage(RectTransform, BACKGROUND_SIZE, BACKGROUND_SIZE, nameof(Background));
            background.rectTransform.localPosition = Vector2.zero;

            var ring = UI_Menu_Pool.Instance.GetImage(RectTransform, RING_SIZE, RING_SIZE, nameof(Ring));
            ring.rectTransform.localPosition = Vector2.zero;

            var icon = UI_Menu_Pool.Instance.GetImage(RectTransform, ICON_SIZE, ICON_SIZE, nameof(Icon));
            icon.rectTransform.localPosition = Vector2.zero;

            Background = background.gameObject.AddComponent<UI_Menu_Inventory_Left_ItemType_Background>();
            Ring = ring.gameObject.AddComponent<UI_Menu_Inventory_Left_ItemType_Ring>();
            Icon = icon.gameObject.AddComponent<UI_Menu_Inventory_Left_ItemType_Icon>();
        }
    }
}
