using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Tamana
{
    public class UI_Chat_BranchChild : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image ring;
        [SerializeField] private TextMeshProUGUI text;

        private IReadOnlyCollection<Chat_Base> dialogue;

        private EventTrigger eventTrigger;
        public EventTrigger EventTrigger => background.GetOrAddAndAssignComponent(ref eventTrigger);

        public EventManager OnClickEvent { get; } = new EventManager();

        private void OnValidate()
        {
            background.LogErrorIfComponentIsNull(EventTrigger);
        }

        public void Activate(IReadOnlyCollection<Chat_Base> dialogue)
        {
            this.dialogue = dialogue;
            text.text = (dialogue.ElementAt(0) as Chat_Dialogue).Dialogue;
            gameObject.SetActive(true);

            EventTrigger.AddListener(EventTriggerType.PointerEnter, OnPointerEnter);
            EventTrigger.AddListener(EventTriggerType.PointerExit, OnPointerExit);
            EventTrigger.AddListener(EventTriggerType.PointerClick, OnPointerClick);

            if (dialogue.ElementAt(1) is Chat_Exit)
            {
                OnClickEvent.AddListener(UI_Chat_Main.Instance.Dialogue.Deactiavate);
                OnClickEvent.AddListener(UI_Chat_Main.Instance.Branching.Deactivate);
            }

            else if(dialogue.ElementAt(1) is Chat_Dialogue)
            {
                OnClickEvent.AddListener(UpdateDialogue);
                OnClickEvent.AddListener(UI_Chat_Main.Instance.Branching.Deactivate);
            }
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            ring.gameObject.SetActive(false);

            dialogue = null;

            EventTrigger.RemoveListener(EventTriggerType.PointerEnter, OnPointerEnter);
            EventTrigger.RemoveListener(EventTriggerType.PointerExit, OnPointerExit);
            EventTrigger.RemoveListener(EventTriggerType.PointerClick, OnPointerClick);

            OnClickEvent.RemoveAllListener();
        }

        private void OnPointerClick(BaseEventData eventData)
        {
            OnClickEvent.Invoke();
            OnClickEvent.RemoveAllListener();
        }

        private void OnPointerEnter(BaseEventData eventData)
        {
            ring.gameObject.SetActive(true);
        }

        private void OnPointerExit(BaseEventData eventData)
        {
            ring.gameObject.SetActive(false);
        }

        private void UpdateDialogue()
        {
            UI_Chat_Main.Instance.Dialogue.UpdateDialogue(dialogue, 1);
        }
    }
}
