using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Shop_Right_ItemDetails_Effect : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Right_ItemDetails itemDetails;
        public UI_Shop_Right_ItemDetails ItemDetails => this.GetAndAssignComponentInParent(ref itemDetails);

        private UI_ItemEffect[] itemEffects;

        public void Activate(Item_Product product)
        {
            var item = product.Product;
            
            if(item is Item_Equipment)
            {
                var equip = item as Item_Equipment;
                itemEffects = new UI_ItemEffect[equip.ItemEffects.Length];
                var xOffset = 0.0f;
                var index = 0;
                foreach (var e in equip.ItemEffects)
                {
                    itemEffects[index] = UI_ItemEffect.CreateInstance(RectTransform, e, xOffset, out float width);
                    index++;
                    xOffset += width + 10;
                }
            }
        }

        public void Deactivate()
        {
            if(itemEffects == null)
            {
                return;
            }

            foreach(var i in itemEffects)
            {
                if(i == null)
                {
                    continue;
                }

                i.ReturnToPool();
                Destroy(i.gameObject);
            }

            itemEffects = null;
        }
    }
}
