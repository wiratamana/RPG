using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIcon : MonoBehaviour
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

        private UI_Menu_Inventory_Left_ItemIcon_Background background;
        private UI_Menu_Inventory_Left_ItemIcon_Ring ring;
        private UI_Menu_Inventory_Left_ItemIcon_Renderer itemRenderer;

        private void Start()
        {
            itemRenderer.OnMouseEnter.AddListener(ring.OnMouseEnter);
            itemRenderer.OnMouseExit.AddListener(ring.OnMouseExit);

            itemRenderer.OnMouseEnter.AddListener(background.OnMouseEnter);
            itemRenderer.OnMouseExit.AddListener(background.OnMouseExit);

            itemRenderer.OnMouseLeftClick.AddListener(OpenItemOption);
        }

        public void SetValue(UI_Menu_Inventory_Left_ItemIcon_Background background,
            UI_Menu_Inventory_Left_ItemIcon_Ring ring,
            UI_Menu_Inventory_Left_ItemIcon_Renderer itemRenderer)
        {
            this.background = background;
            this.ring = ring;
            this.itemRenderer = itemRenderer;
        }

        public void ReturnToPool()
        {
            UI_Menu_Pool.Instance.RemoveImage(background.Background);
            UI_Menu_Pool.Instance.RemoveImage(ring.Ring);
            UI_Menu_Pool.Instance.RemoveRawImage(itemRenderer.RawImage);

            Destroy(background);
            Destroy(itemRenderer);
            Destroy(ring);
        }

        private void OpenItemOption()
        {
            UI_Menu_Inventory_ItemOption.Instance.ClearRegisteredGenericMenu();

            var equip = UI_Menu_Inventory_ItemOption_GenericMenu.CreateGenericMenu("Equip");
            var cancel = UI_Menu_Inventory_ItemOption_GenericMenu.CreateGenericMenu("Cancel");

            var modularPart = itemRenderer.ItemPreview.ItemBase as Item_ModularBodyPart;
            if(modularPart != null)
            {
                Inventory_EquipmentManager.Instance.ObjectPartPath = modularPart?.PartLocation;
                equip.OnMouseLeftClick.AddListener(Inventory_EquipmentManager.Instance.EquipModularPart);
            }
            equip.OnMouseLeftClick.AddListener(UI_Menu_Inventory_ItemOption.Instance.Close);
            equip.OnMouseLeftClick.AddListener(equip.OnMouseLeftClick.RemoveAllListener);

            cancel.OnMouseLeftClick.AddListener(UI_Menu_Inventory_ItemOption.Instance.Close);
            cancel.OnMouseLeftClick.AddListener(cancel.OnMouseLeftClick.RemoveAllListener);

            UI_Menu_Inventory_ItemOption.Instance.RegisterGenericMenu(equip);
            UI_Menu_Inventory_ItemOption.Instance.RegisterGenericMenu(cancel);

            UI_Menu_Inventory_ItemOption.Instance.Open(this);
        }
    }
}
