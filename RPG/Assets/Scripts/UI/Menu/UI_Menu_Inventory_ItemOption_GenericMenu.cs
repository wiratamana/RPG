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

        public Image Ring { private set; get; }
        public TextMeshProUGUI Text { private set; get; }
        private EventTrigger eventTrigger;

        public EventManager OnMouseLeftClick { private set; get; } = new EventManager();
        public EventManager OnMouseEnter { private set; get; } = new EventManager();
        public EventManager OnMouseExit { private set; get; } = new EventManager();

        private void Start()
        {
            eventTrigger = Ring.gameObject.AddComponent<EventTrigger>();

            var pointerEnterEntry = new EventTrigger.Entry();
            var pointerExitEntry = new EventTrigger.Entry();
            var pointerClickEntry = new EventTrigger.Entry();

            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerExitEntry.eventID = EventTriggerType.PointerExit;
            pointerClickEntry.eventID = EventTriggerType.PointerClick;

            pointerEnterEntry.callback.AddListener(OnPointerEnter);
            pointerExitEntry.callback.AddListener(OnPointerExit);
            pointerClickEntry.callback.AddListener(OnPointerClick);

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

            genericMenu.Ring = UI_Menu_Pool.Instance.GetImage(rt, width, height, nameof(Ring));
            genericMenu.Ring.raycastTarget = true;
            genericMenu.Ring.sprite = UI_Menu.Instance.MenuResources.InventoryItemIconRing_Sprite;
            genericMenu.Ring.type = Image.Type.Sliced;
            genericMenu.Ring.rectTransform.anchorMin = Vector2.zero;
            genericMenu.Ring.rectTransform.anchorMax = Vector2.one;
            genericMenu.Ring.rectTransform.offsetMin = Vector2.zero;
            genericMenu.Ring.rectTransform.offsetMax = Vector2.zero;

            genericMenu.Text = UI_Menu_Pool.Instance.GetText(rt, width, height, name, nameof(Text));
            genericMenu.Text.fontSize = 28;
            genericMenu.Text.rectTransform.anchorMin = Vector2.zero;
            genericMenu.Text.rectTransform.anchorMax = Vector2.one;
            genericMenu.Text.rectTransform.offsetMin = Vector2.zero;
            genericMenu.Text.rectTransform.offsetMax = Vector2.zero;

            return genericMenu;
        }

        private void OnPointerExit(BaseEventData eventData)
        {
            OnMouseExit.Invoke();
            UI_Menu_Selection.DestroyInstance();
        }

        private void OnPointerEnter(BaseEventData eventData)
        {
            OnMouseEnter.Invoke();
            UI_Menu_Selection.CreateInstance(RectTransform, 24);
        }

        private void OnPointerClick(BaseEventData eventData)
        {
            OnMouseLeftClick.Invoke();
        }
    }
}
