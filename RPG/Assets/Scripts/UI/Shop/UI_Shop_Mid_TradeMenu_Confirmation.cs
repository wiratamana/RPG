﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Tamana
{
    public class UI_Shop_Mid_TradeMenu_Confirmation : MonoBehaviour
    {
        [SerializeField] private RectTransform yes;
        [SerializeField] private RectTransform no;
        [SerializeField] private TextMeshProUGUI confirmationText;

        private UI_Shop_Mid_TradeMenu tradeMenu;
        public UI_Shop_Mid_TradeMenu TradeMenu => this.GetAndAssignComponentInParent(ref tradeMenu);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private EventTrigger eventTrigger_yes;
        private EventTrigger eventTrigger_no;

        public const float HEIGHT = 250.0f;

        private static readonly string confirmationMessage_buy = $"Are you sure you want to{System.Environment.NewLine}buy this item?";
        private static readonly string confirmationMessage_sell = $"Are you sure you want to{System.Environment.NewLine}sell this item?";

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            confirmationText.text = UI_Shop.Instance.TradeType == TradeType.Buy ? confirmationMessage_buy : confirmationMessage_sell;

            if (eventTrigger_yes == null)
            {
                eventTrigger_yes = yes.GetComponent<EventTrigger>() ?? yes.gameObject.AddComponent<EventTrigger>();
            }

            if (eventTrigger_no == null)
            {
                eventTrigger_no = no.GetComponent<EventTrigger>() ?? no.gameObject.AddComponent<EventTrigger>();
            }

            eventTrigger_yes.AddListener(EventTriggerType.PointerEnter, OnPointerEnter_Yes);
            eventTrigger_yes.AddListener(EventTriggerType.PointerExit, OnPointerExit_Yes);
            eventTrigger_yes.AddListener(EventTriggerType.PointerClick, OnPointerClick_Yes);

            eventTrigger_no.AddListener(EventTriggerType.PointerEnter, OnPointerEnter_No);
            eventTrigger_no.AddListener(EventTriggerType.PointerExit, OnPointerExit_No);
            eventTrigger_no.AddListener(EventTriggerType.PointerClick, OnPointerClick_No);
        }

        public void Deactivate()
        {
            eventTrigger_yes.RemoveEntry(EventTriggerType.PointerEnter);
            eventTrigger_yes.RemoveEntry(EventTriggerType.PointerExit);
            eventTrigger_yes.RemoveEntry(EventTriggerType.PointerClick);

            eventTrigger_no.RemoveEntry(EventTriggerType.PointerEnter);
            eventTrigger_no.RemoveEntry(EventTriggerType.PointerExit);
            eventTrigger_no.RemoveEntry(EventTriggerType.PointerClick);
        }

        private void Resize()
        {
            var bot = UI_Chat_Main.Instance.Dialogue.RectTransform.position.y + (UI_Chat_Main.Instance.Dialogue.RectTransform.sizeDelta.y * 0.5f);
            var offset = 20.0f;
            var posY = bot + (HEIGHT * 0.5f) + offset;
            var sizeX = TradeMenu.Mid.RectTransform.sizeDelta.x;

            RectTransform.sizeDelta = new Vector2(sizeX, HEIGHT);
            rectTransform.position = new Vector3(Screen.width * 0.5f, posY);
        }

        private void OnPointerEnter_Yes(BaseEventData eventData)
        {
            UI_Selection.CreateInstance(yes, 24.0f);
        }

        private void OnPointerExit_Yes(BaseEventData eventData)
        {
            UI_Selection.DestroyInstance();
        }

        private void OnPointerClick_Yes(BaseEventData eventData)
        {    
            if (UI_Shop.Instance.TradeType == TradeType.Buy)
            {
                TradeMenu.Mid.ItemProduct.Purchase();
            }
            else
            {
                TradeMenu.Mid.ItemProduct.Sell();
            }
            

            UI_Selection.DestroyInstance();
            UI_Shop.Instance.CloseTradeMenu();
        }

        private void OnPointerEnter_No(BaseEventData eventData)
        {
            UI_Selection.CreateInstance(no, 24.0f);
        }

        private void OnPointerExit_No(BaseEventData eventData)
        {
            UI_Selection.DestroyInstance();
        }

        private void OnPointerClick_No(BaseEventData eventData)
        {
            UI_Selection.DestroyInstance();
            UI_Shop.Instance.CloseTradeMenu();
        }
    }
}
