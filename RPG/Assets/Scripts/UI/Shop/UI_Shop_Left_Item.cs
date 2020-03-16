using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tamana
{
    public class UI_Shop_Left_Item : MonoBehaviour
    {
        private RectTransform rectTransform;
        [SerializeField] private Image background;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI stock;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private RawImage itemRenderer;

        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);
        public Image Background => background;
        public TextMeshProUGUI ItemName => itemName;
        public TextMeshProUGUI Stock => stock;
        public TextMeshProUGUI Price => price;
        public RawImage ItemRenderer => itemRenderer;

        public void Initialize(Item_Base item, int stock, int price, float width)
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD == false)
            {
                return;
            }

            RectTransform.sizeDelta = new Vector2(width, RectTransform.sizeDelta.y);
            RectTransform.position = new Vector3(100 + (RectTransform.sizeDelta.x * 0.5f), RectTransform.position.y);
        }
    }
}
