using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;

namespace Tamana
{
    public class UI_Menu_Inventory_ItemOption_GenericMenu : MonoBehaviour
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

        private Image ring;
        private TextMeshProUGUI text;
        private EventTrigger eventTrigger;

        public EventManager OnClick { private set; get; } = new EventManager();

        private void Start()
        {
            AddEntryToEventTrigger();
        }

        private void AddEntryToEventTrigger()
        {
            eventTrigger = ring.gameObject.AddComponent<EventTrigger>();
            var pointerEnterEntry = new EventTrigger.Entry();
            var pointerExitEntry = new EventTrigger.Entry();
            var pointerClickEntry = new EventTrigger.Entry();

            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerExitEntry.eventID = EventTriggerType.PointerExit;
            pointerClickEntry.eventID = EventTriggerType.PointerClick;

            pointerEnterEntry.callback.AddListener(OnMouseEnter);
            pointerExitEntry.callback.AddListener(OnMouseExit);
            pointerClickEntry.callback.AddListener(OnMouseClick);

            eventTrigger.triggers.Add(pointerEnterEntry);
            eventTrigger.triggers.Add(pointerExitEntry);
            eventTrigger.triggers.Add(pointerClickEntry);
        }

        public static UI_Menu_Inventory_ItemOption_GenericMenu CreateGenericMenu(string name)
        {
            var width = UI_Menu_Inventory_ItemOption.GENERIC_WIDTH;
            var height = UI_Menu_Inventory_ItemOption.GENERIC_HEIGHT;

            var go = new GameObject($"{nameof(UI_Menu_Inventory_ItemOption_GenericMenu)}-{name}");
            var rt = go.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(width, height);
            var genericMenu = go.AddComponent<UI_Menu_Inventory_ItemOption_GenericMenu>();

            genericMenu.ring = UI_Menu_Pool.Instance.GetImage(rt, width, height, nameof(ring));
            genericMenu.ring.sprite = UI_Menu.Instance.MenuResources.InventoryItemIconRing_Sprite;
            genericMenu.ring.type = Image.Type.Sliced;

            genericMenu.text = UI_Menu_Pool.Instance.GetText(rt, width, height, name, nameof(text));
            genericMenu.text.fontSize = 28;

            return genericMenu;
        }

        public void OnMouseExit(BaseEventData eventData)
        {
            
        }

        public void OnMouseEnter(BaseEventData eventData)
        {
            
        }

        public void OnMouseClick(BaseEventData eventData)
        {
            
        }
    }
}
