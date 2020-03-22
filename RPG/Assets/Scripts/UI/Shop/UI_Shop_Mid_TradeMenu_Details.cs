using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tamana
{
    public class UI_Shop_Mid_TradeMenu_Details : MonoBehaviour
    {
        [SerializeField] private RawImage itemRenderer;
        [SerializeField] private RectTransform costRT;
        [SerializeField] private TextMeshProUGUI costText;
        public RawImage ItemRenderer => itemRenderer;
        public RectTransform CostRT => costRT;
        public TextMeshProUGUI CostText => costText;

        private UI_Shop_Mid_TradeMenu tradeMenu;
        public UI_Shop_Mid_TradeMenu TradeMenu => this.GetAndAssignComponentInParent(ref tradeMenu);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private RenderTexture renderTexture;
        private Item_Preview itemPreview;

        public void Activate()
        {
            if (GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            if (renderTexture != null)
            {
                Debug.Log("Render texture not null!!", Debug.LogType.Warning);
            }
            else
            {
                renderTexture = new RenderTexture((int)itemRenderer.rectTransform.sizeDelta.x, 
                    (int)itemRenderer.rectTransform.sizeDelta.y, 16, RenderTextureFormat.ARGBHalf);
            }

            var product = TradeMenu.Mid.ItemProduct;

            costText.text = UI_Shop.Instance.TradeType == TradeType.Buy ? product.PriceBuyString : product.PriceSellString;

            itemRenderer.texture = renderTexture;
            itemPreview = Item_Preview.InstantiateItemPrefab(product.Product, Vector2.zero);
            itemPreview.enabled = true;
            UI_ItemRenderer.ResetCameraPositionAndRotation(product.Product, itemPreview.transform);
            UI_ItemRenderer.SetTexture(renderTexture);

            UpdateAsync();
        }

        public void Deactivate()
        {
            if(renderTexture != null)
            {
                Destroy(renderTexture);
                renderTexture = null;
            }

            if(itemPreview != null)
            {
                Destroy(itemPreview);
                itemPreview = null;
            }

            itemRenderer.texture = null;
            UI_ItemRenderer.SetTexture(null);
        }

        private async void UpdateAsync()
        {
            while (renderTexture != null)
            {
                await AsyncManager.WaitForFrame(1);

                UI_ItemRenderer.Render();
            }
        }

        private void Resize()
        {
            var top = TradeMenu.Mid.Shop.Left.Buy.Stock.position.y + (TradeMenu.Mid.Shop.Left.Buy.Stock.sizeDelta.y * 0.5f);
            var bot = UI_Chat_Main.Instance.Dialogue.RectTransform.position.y + (UI_Chat_Main.Instance.Dialogue.RectTransform.sizeDelta.y * 0.5f);
            var offset = 20.0f;
            var posY = bot + UI_Shop_Mid_TradeMenu_Confirmation.HEIGHT + offset;
            var sizeX = TradeMenu.Mid.RectTransform.sizeDelta.x;
            var sizeY = Mathf.Abs(top - posY) - offset;
            posY += sizeY * 0.5f;

            RectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            RectTransform.position = new Vector3(Screen.width * 0.5f, posY);

            var rendSizeY = sizeY - (offset * 2);
            var rendPos = new Vector3((Screen.width * 0.25f) + (rendSizeY * 0.5f) + offset, posY);
            itemRenderer.rectTransform.sizeDelta = new Vector2(rendSizeY, rendSizeY);
            itemRenderer.rectTransform.position = rendPos;

            var costOffsetX = 65;
            var costOffsetY = 30;
            var costPosX = rendPos.x + (rendSizeY * 0.5f) + (costRT.sizeDelta.x * 0.5f) + costOffsetX;
            var costPosY = bot + (offset * 2) + UI_Shop_Mid_TradeMenu_Confirmation.HEIGHT + costOffsetY;

            costRT.position = new Vector3(costPosX, costPosY);
        }
    }
}

