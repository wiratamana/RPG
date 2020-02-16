using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIcon_Ring : MonoBehaviour
    {
        private Image ring;
        public Image Ring
        {
            get
            {
                if(ring == null)
                {
                    ring = GetComponent<Image>();
                }

                return ring;
            }
        }

        private static readonly Color OnMouseOver = Color.red;
        private static readonly Color NormalColor = new Color(200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 1.0f);

        private void Awake()
        {
            Ring.color = NormalColor;
        }

        public void OnMouseEnter()
        {
            Ring.color = OnMouseOver;
        }

        public void OnMouseExit()
        {
            Ring.color = NormalColor;
        }
    }
}

