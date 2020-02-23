using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class UI_Menu_Inventory_Right_ItemDetails_Effect : MonoBehaviour
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
    }
}
