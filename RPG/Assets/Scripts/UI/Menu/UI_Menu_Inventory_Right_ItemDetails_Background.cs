using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Right_ItemDetails_Background : MonoBehaviour
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

        private UI_Menu_Inventory_Right_ItemDetails itemDetails;
        public UI_Menu_Inventory_Right_ItemDetails ItemDetails
        {
            get
            {
                if(itemDetails == null)
                {
                    itemDetails = transform.parent.GetComponent<UI_Menu_Inventory_Right_ItemDetails>();
                }

                return itemDetails;
            }
        }

        public static readonly Color BackgroundColor = new Color(0, 0, 0, 200.0f / 255.0f);

        public Image Background { private set; get; }
        public Image Ring { private set; get; }

        private void Awake()
        {
            Init();

            UI_Menu_Inventory.OnMenuInventoryOpened.AddListener(Init);
            UI_Menu_Inventory.OnMenuInventoryClosed.AddListener(ReturnToPool);
        }

        private void Init()
        {
            Background = UI_Pool.Instance.GetImage(RectTransform, 0, 0, nameof(Background));
            Background.rectTransform.localPosition = Vector3.zero;
            Background.rectTransform.anchorMax = Vector2.one;
            Background.rectTransform.anchorMin = Vector2.zero;
            Background.rectTransform.offsetMax = new Vector2(-5, -5);
            Background.rectTransform.offsetMin = new Vector2(5, 5);
            Background.color = BackgroundColor;

            Ring = UI_Pool.Instance.GetImage(RectTransform, 0, 0, nameof(Ring));
            Ring.rectTransform.localPosition = Vector3.zero;
            Ring.rectTransform.anchorMax = Vector2.one;
            Ring.rectTransform.anchorMin = Vector2.zero;
            Ring.rectTransform.offsetMax = new Vector2(-10, -10);
            Ring.rectTransform.offsetMin = new Vector2(10, 10);
            Ring.color = Color.white;
            Ring.type = Image.Type.Sliced;
            Ring.sprite = UI_Menu.Instance.MenuResources.InventoryItemIconRing_Sprite;
        }

        public void ReturnToPool()
        {
            UI_Pool.Instance.RemoveImage(Ring);
            UI_Pool.Instance.RemoveImage(Background);

            Background = null;
            Ring = null;
        }
    }
}
