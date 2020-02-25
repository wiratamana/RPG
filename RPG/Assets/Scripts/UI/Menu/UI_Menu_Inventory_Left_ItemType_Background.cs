using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemType_Background : MonoBehaviour
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        private Image _background;
        public Image Background
        {
            get
            {
                if(_background == null)
                {
                    _background = GetComponent<Image>();
                }

                return _background;
            }
        }

        private EventTrigger _eventTrigger;
        private EventTrigger EventTrigger
        {
            get
            {
                if(_eventTrigger == null)
                {
                    _eventTrigger = gameObject.GetOrAddComponent<EventTrigger>();
                }

                return _eventTrigger;
            }
        }

        public EventManager OnPointerEnterEvent { private set; get; } = new EventManager();
        public EventManager OnPointerExitEvent { private set; get; } = new EventManager();
        public EventManager OnPointerClickEvent { private set; get; } = new EventManager();

        private void Awake()
        {
            Background.color = Color.black;
            Background.raycastTarget = true;
        }

        private void Start()
        {
            var pointerEnterEntry = new EventTrigger.Entry();
            var pointerExitEntry = new EventTrigger.Entry();
            var pointerClickEntry = new EventTrigger.Entry();

            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerExitEntry.eventID = EventTriggerType.PointerExit;
            pointerClickEntry.eventID = EventTriggerType.PointerClick;

            pointerEnterEntry.callback.AddListener(OnPointerEnter);
            pointerExitEntry.callback.AddListener(OnPointerExit);
            pointerClickEntry.callback.AddListener(OnPointerClick);

            EventTrigger.triggers.Add(pointerEnterEntry);
            EventTrigger.triggers.Add(pointerExitEntry);
            EventTrigger.triggers.Add(pointerClickEntry);
        }

        private void OnPointerEnter(BaseEventData eventData)
        {
            OnPointerEnterEvent.Invoke();
        }

        private void OnPointerExit(BaseEventData eventData)
        {
            OnPointerExitEvent.Invoke();
        }

        private void OnPointerClick(BaseEventData eventData)
        {
            OnPointerClickEvent.Invoke();
        }
    }
}

