using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tamana
{
    public class UI_Shop_Mid_PurchaseMenu_Confirmation : MonoBehaviour
    {
        [SerializeField] private RectTransform yes;
        [SerializeField] private RectTransform no;

        private UI_Shop_Mid_PurchaseMenu purchaseMenu;
        public UI_Shop_Mid_PurchaseMenu PurchaseMenu => this.GetAndAssignComponentInParent(ref purchaseMenu);

        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private EventTrigger eventTrigger_yes;
        private EventTrigger eventTrigger_no;

        public const float HEIGHT = 250.0f;

        public void Activate()
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD)
            {
                Resize();
            }

            if(eventTrigger_yes == null)
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
            var sizeX = PurchaseMenu.Mid.RectTransform.sizeDelta.x;

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
            UI_Shop.Instance.ClosePurchaseConfirmation();
        }
    }
}
