using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIcon : MonoBehaviour
    {
        private UI_Menu_Inventory_Left_ItemIcon_Background background;
        private UI_Menu_Inventory_Left_ItemIcon_Ring ring;
        private UI_Menu_Inventory_Left_ItemIcon_Renderer itemRenderer;

        private void Start()
        {
            itemRenderer.OnMouseEnter.AddListener(ring.OnMouseEnter);
            itemRenderer.OnMouseExit.AddListener(ring.OnMouseExit);

            itemRenderer.OnMouseEnter.AddListener(background.OnMouseEnter);
            itemRenderer.OnMouseExit.AddListener(background.OnMouseExit);

            itemRenderer.OnMouseLeftClick.AddListener(UI_Menu_Inventory_ItemOption.Instance.Open);
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
    }
}
