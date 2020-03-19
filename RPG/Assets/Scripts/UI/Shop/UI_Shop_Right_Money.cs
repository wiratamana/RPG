using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tamana
{
    public class UI_Shop_Right_Money : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI amount;

        private RectTransform rectTransform;
        private UI_Shop_Right right;

        public Image Background => background;
        public Image Icon => icon;
        public TextMeshProUGUI Amount => amount;

        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);
        public UI_Shop_Right Right => this.GetAndAssignComponentInParent(ref right);

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            Amount.text = PlayerData.Money.ToString();
        }

        public void Resize()
        {
            RectTransform.sizeDelta = new Vector2(Screen.width * 0.5f, RectTransform.sizeDelta.y);
            RectTransform.position = new Vector3(Screen.width - (RectTransform.sizeDelta.x * 0.5f),
                Screen.height - UI_Shop_Right.START_Y);

            Background.rectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x * 0.33f, RectTransform.sizeDelta.y);
            Background.rectTransform.position = new Vector3(Screen.width - (Background.rectTransform.sizeDelta.x * 0.5f),
                Screen.height - UI_Shop_Right.START_Y);

            var offsetLeft = (Icon.rectTransform.sizeDelta.x * 0.5f) + 15.0f;
            var offsetBot = (Icon.rectTransform.sizeDelta.y * 0.5f) + 5.0f;
            var startX = Background.rectTransform.position.x - (Background.rectTransform.sizeDelta.x * 0.5f);
            var startY = Background.rectTransform.position.y - (Background.rectTransform.sizeDelta.y * 0.5f);
            Icon.rectTransform.position = new Vector3(startX + offsetLeft, startY + offsetBot);

            var leftPos = startX + offsetLeft + (Icon.rectTransform.sizeDelta.x * 0.5f);
            var sizeX = Mathf.Abs(leftPos - Screen.width);
            leftPos += sizeX * 0.5f;
            Amount.rectTransform.sizeDelta = new Vector2(sizeX, Amount.rectTransform.sizeDelta.y);
            Amount.rectTransform.position = new Vector3(leftPos, Screen.height - UI_Shop_Right.START_Y);
        }
    }
}
