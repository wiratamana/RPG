using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIcon_Background : MonoBehaviour
    {
        private Image background;
        public Image Background
        {
            get
            {
                if (background == null)
                {
                    background = GetComponent<Image>();
                }

                return background;
            }
        }

        private UI_Menu_Inventory_Left_ItemIcon itemIcon;
        public UI_Menu_Inventory_Left_ItemIcon ItemIcon
        {
            get
            {
                if(itemIcon == null)
                {
                    itemIcon = transform.parent.GetComponent<UI_Menu_Inventory_Left_ItemIcon>();
                }

                return itemIcon;
            }
        }

        private static readonly Color NormalColor = new Color(40.0f / 255.0f, 40.0f / 255.0f, 40.0f / 255.0f, 200.0f / 255.0f);
        private static readonly Color OnMouseOver = Color.blue;

        private void Awake()
        {
            Background.color = NormalColor;
        }

        public void OnMouseEnter()
        {
            Background.color = OnMouseOver;
        }

        public void OnMouseExit()
        {
            Background.color = NormalColor;
        }
    }
}
