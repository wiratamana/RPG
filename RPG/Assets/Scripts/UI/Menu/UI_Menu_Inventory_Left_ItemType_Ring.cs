using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemType_Ring : MonoBehaviour
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

        private Image _ring;
        public Image Ring
        {
            get
            {
                if (_ring == null)
                {
                    _ring = GetComponent<Image>();
                }

                return _ring;
            }
        }


        private void Awake()
        {
            Ring.color = Color.white;
            Ring.sprite = UI_Menu.Instance.MenuResources.InventoryItemIconRing_Sprite;
        }
    }
}
