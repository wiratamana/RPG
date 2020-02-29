using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemType_Icon : MonoBehaviour
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

        private Image _icon;
        public Image Icon
        {
            get
            {
                if (_icon == null)
                {
                    _icon = GetComponent<Image>();
                }

                return _icon;
            }
        }

        public ItemType ItemType
        {
            get
            {
                return (ItemType)System.Enum.Parse(typeof(ItemType), transform.parent.name);
            }
        }

        private void Awake()
        {
            Icon.color = Color.white;
            Icon.sprite = UI_Menu.Instance.MenuResources.GetItemTypeSprites(ItemType);
        }
    }
}
