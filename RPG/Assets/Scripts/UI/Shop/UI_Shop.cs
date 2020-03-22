using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Shop : MonoBehaviour
    {
        private static UI_Shop instance;
        public static UI_Shop Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<UI_Shop>();
                }

                return instance;
            }
        }

        private UI_Shop_Left left;
        private UI_Shop_Right right;
        private UI_Shop_Mid mid;

        public UI_Shop_Left Left => this.GetAndAssignComponentInChildren(ref left);
        public UI_Shop_Right Right => this.GetAndAssignComponentInChildren(ref right);
        public UI_Shop_Mid Mid => this.GetAndAssignComponentInChildren(ref mid);

        public IReadOnlyCollection<Item_Product> Products { get; private set; }

        private Canvas canvas;
        public Canvas Canvas => this.GetAndAssignComponent(ref canvas);

        public EventManager OnOpened { get; } = new EventManager();
        public EventManager OnClosed { get; } = new EventManager();

        public EventManager OnSwitchedMenuToBuy { get; } = new EventManager();
        public EventManager OnSwitchedMenuToSell { get; } = new EventManager();

        private UI_Navigator uinav_escapeToCloseShop;
        private UI_Navigator uinav_tabSellOrBuy;
        private Chat_ReturnTo dialogueAfterShopClosed;

        public TradeType TradeType { get; private set; }

        public void Open(IReadOnlyCollection<Item_Product> products, Chat_ReturnTo dialogueAfterShopClosed)
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD == false)
            {
                var canvasScaler = GetComponent<CanvasScaler>();
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = new Vector2(GameManager.FULLHD_WIDTH, GameManager.FULLHD_HEIGHT);
            }
            else
            {
                var screenSize = new Vector2(Screen.width, Screen.height);

                Left.RectTransform.sizeDelta = new Vector2(screenSize.x * 0.5f, screenSize.y);
                Left.RectTransform.localPosition = new Vector3(screenSize.x * -0.25f, 0.0f);

                Right.RectTransform.sizeDelta = Left.RectTransform.sizeDelta;
                Right.RectTransform.localPosition = new Vector3(screenSize.x * 0.25f, 0.0f);
            }

            TradeType = TradeType.Buy;

            Products = products;
            Left.Activate();
            Right.Activate();

            UI_NavigatorManager.Instance.Add(ref uinav_escapeToCloseShop, "Back", InputEvent.ACTION_CLOSE_SHOP_MENU);
            UI_NavigatorManager.Instance.Add(ref uinav_tabSellOrBuy, "Sell", InputEvent.ACTION_SWITCH_SHOP_MENU);
            this.dialogueAfterShopClosed = dialogueAfterShopClosed;
            InputEvent.Instance.Event_CloseShop.AddListener(Close);
            InputEvent.Instance.Event_SwitchShop.AddListener(SwitchMenuToSell);

            OnOpened.Invoke();
        }

        public void Close()
        {
            if(TradeType == TradeType.Buy)
            {
                InputEvent.Instance.Event_SwitchShop.RemoveListener(SwitchMenuToSell);
            }
            else
            {
                InputEvent.Instance.Event_SwitchShop.RemoveListener(SwitchMenuToBuy);
            }

            Left.Deactivate();
            Right.Deactivate();
            Products = null;

            UI_NavigatorManager.Instance.Remove(ref uinav_escapeToCloseShop);
            UI_NavigatorManager.Instance.Remove(ref uinav_tabSellOrBuy);
            InputEvent.Instance.Event_CloseShop.RemoveAllListener();

            UI_Chat_Main.Instance.Dialogue.UpdateDialogue(dialogueAfterShopClosed.ReturnToObject.Dialogue
                , dialogueAfterShopClosed.ReturnToIndex);

            dialogueAfterShopClosed = null;

            OnClosed.Invoke();
        }

        public void OpenTradeMenu(Item_Product itemProduct)
        {
            Left.Deactivate();
            Right.Deactivate();
            Mid.Activate(itemProduct, TradeType);

            InputEvent.Instance.Event_CloseShop.RemoveListener(Close);
            InputEvent.Instance.Event_CloseShop.AddListener(CloseTradeMenu);
        }

        public void CloseTradeMenu()
        {
            Left.Activate(TradeType);
            Right.Activate();
            Mid.Deactivate();

            InputEvent.Instance.Event_CloseShop.RemoveListener(CloseTradeMenu);
            InputEvent.Instance.Event_CloseShop.AddListener(Close);
        }

        private void SwitchMenuToSell()
        {
            InputEvent.Instance.Event_SwitchShop.RemoveListener(SwitchMenuToSell);
            InputEvent.Instance.Event_SwitchShop.AddListener(SwitchMenuToBuy);

            uinav_tabSellOrBuy.SetValue("Buy", InputEvent.ACTION_SWITCH_SHOP_MENU);

            Left.Buy.Deactivate();

            TradeType = TradeType.Sell;
            Left.Sell.Activate();

            OnSwitchedMenuToSell.Invoke();
        }

        private void SwitchMenuToBuy()
        {
            InputEvent.Instance.Event_SwitchShop.RemoveListener(SwitchMenuToBuy);
            InputEvent.Instance.Event_SwitchShop.AddListener(SwitchMenuToSell);

            uinav_tabSellOrBuy.SetValue("Sell", InputEvent.ACTION_SWITCH_SHOP_MENU);

            Left.Sell.Deactivate();

            TradeType = TradeType.Buy;
            Left.Buy.Activate();

            OnSwitchedMenuToBuy.Invoke();
        }
    }
}
