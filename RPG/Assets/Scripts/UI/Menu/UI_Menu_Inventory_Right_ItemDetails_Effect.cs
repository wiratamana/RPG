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

        public UI_Menu_Inventory_Right_ItemDetails_Effect_Child[] Effects { private set; get; }

        private void Start()
        {
            UI_Menu.Instance.Inventory.Right.ItemDescription.OnBecomeDeactiveEvent.AddListener(CleanUp, GetInstanceID());

            UI_Menu_Inventory.OnMenuInventoryClosed.AddListener(CleanUp);
        }

        public void SetEffects(Item_Equipment_Effect[] effects)
        {
            var index = 0;
            var xSpacing = 20.0f;
            var xOffset = xSpacing;
            Effects = new UI_Menu_Inventory_Right_ItemDetails_Effect_Child[effects.Length];

            foreach (var e in effects)
            {
                var effect = UI_Menu_Inventory_Right_ItemDetails_Effect_Child
                    .CreateInstance(RectTransform, e, xOffset, out float outWidth);
                Effects[index] = effect;

                xOffset += outWidth;
                xOffset += xSpacing;
                index++;
            }
        }

        private void CleanUp()
        {
            if(Effects == null || Effects.Length == 0)
            {
                return;
            }

            for(int i = 0; i < Effects.Length; i++)
            {
                Effects[i].ReturnToPool();
                Destroy(Effects[i].gameObject);
            }

            Effects = null;
        }
    }
}
