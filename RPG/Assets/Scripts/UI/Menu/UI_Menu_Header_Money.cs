using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tamana
{
    public class UI_Menu_Header_Money : MonoBehaviour
    {
        private RectTransform rectTranform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTranform);

        private Image icon;
        private TextMeshProUGUI amount;

        private async void Awake()
        {
            await AsyncManager.WaitForFrame(1);
            var mid = RectTransform.position;
            icon = UI_Pool.Instance.GetImage(RectTransform, 64, 64, nameof(icon));
            icon.sprite = GlobalAssetsReference.Money_Sprite;
            icon.rectTransform.position = mid + new Vector3(32, 0);

            var iconLeft = icon.rectTransform.position + new Vector3(32, 0);
            var leftLeft = mid + new Vector3(RectTransform.sizeDelta.x * 0.5f, 0);
            var amountSizeX = Mathf.Abs(iconLeft.x - leftLeft.x);
            amount = UI_Pool.Instance.GetText(RectTransform, amountSizeX, 48, PlayerData.MoneyString, nameof(amount));
            amount.rectTransform.position = iconLeft + new Vector3(amountSizeX * 0.5f, 0);
            var amountPos = amount.rectTransform.position;
            amountPos.y = icon.rectTransform.position.y - 32 + 24;
            amount.rectTransform.position = amountPos;
            amount.alignment = TextAlignmentOptions.MidlineLeft;

            UI_Menu.OnBeforeOpen.AddListener(UpdateMoney);
        }

        private void UpdateMoney()
        {
            amount.text = PlayerData.MoneyString;
        }
    }
}
