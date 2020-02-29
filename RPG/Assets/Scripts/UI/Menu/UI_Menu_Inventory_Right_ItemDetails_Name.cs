using UnityEngine;
using System.Collections;
using TMPro;

namespace Tamana
{
    public class UI_Menu_Inventory_Right_ItemDetails_Name : MonoBehaviour
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
                if (itemDetails == null)
                {
                    itemDetails = transform.parent.GetComponent<UI_Menu_Inventory_Right_ItemDetails>();
                }

                return itemDetails;
            }
        }

        public TextMeshProUGUI ItemName { private set; get; }

        private void Awake()
        {
            Init();

            UI_Menu_Inventory.OnMenuInventoryOpened.AddListener(Init);
            UI_Menu_Inventory.OnMenuInventoryClosed.AddListener(ReturnToPool);
        }

        private void Init()
        {
            ItemName = UI_Menu_Pool.Instance.GetText(RectTransform, 0, 0, "", nameof(ItemName));
            ItemName.rectTransform.localPosition = Vector3.zero;
            ItemName.rectTransform.anchorMax = Vector2.one;
            ItemName.rectTransform.anchorMin = Vector2.zero;
            ItemName.rectTransform.offsetMax = new Vector2(-10, 0);
            ItemName.rectTransform.offsetMin = new Vector2(10, 0);
            ItemName.alignment = TextAlignmentOptions.MidlineLeft;
        }

        public void ReturnToPool()
        {
            UI_Menu_Pool.Instance.RemoveText(ItemName);

            ItemName = null;
        }
    }
}
