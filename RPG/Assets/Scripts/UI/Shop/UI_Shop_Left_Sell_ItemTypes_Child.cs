using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tamana
{
    public class UI_Shop_Left_Sell_ItemTypes_Child : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Shop_Left_Sell_ItemTypes itemTypes;
        public UI_Shop_Left_Sell_ItemTypes ItemTypes => this.GetAndAssignComponentInParent(ref itemTypes);

        public EventManager<ItemType> OnItemTypeChanged { get; } = new EventManager<ItemType>();

        [SerializeField] private Image background;
        [SerializeField] private Image ring;
        [SerializeField] private Image icon;
        [SerializeField] private ItemType itemType;
        private EventTrigger eventTrigger;

        public Image Background => background;
        public Image Ring => ring;
        public Image Icon => icon;
        public ItemType ItemType => itemType;
        public EventTrigger EventTrigger
        {
            get
            {
                if(eventTrigger == null)
                {
                    eventTrigger = Background.GetComponent<EventTrigger>() ?? Background.gameObject.AddComponent<EventTrigger>();
                }

                return eventTrigger;
            }
        }

        private void Awake()
        {
            EventTrigger.AddListener(EventTriggerType.PointerClick, OnPointerClick);
        }

        public void Activate()
        {
            Background.color = Color.white;
            Ring.color = Color.black;
            Icon.color = Color.black;
        }

        public void Deactivate()
        {
            Background.color = Color.black;
            Ring.color = Color.white;
            Icon.color = Color.white;
        }

        private void OnPointerClick(BaseEventData eventData)
        {
            OnItemTypeChanged.Invoke(itemType);
        }
    }
}
