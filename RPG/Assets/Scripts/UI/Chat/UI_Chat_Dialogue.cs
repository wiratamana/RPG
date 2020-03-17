using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

namespace Tamana
{
    public class UI_Chat_Dialogue : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private TextMeshProUGUI dialogueText;

        private RectTransform rectTransfrom;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransfrom);

        private UI_Chat_Main chatMain;
        public UI_Chat_Main ChatMain => this.GetAndAssignComponentInParent(ref chatMain);

        private IReadOnlyCollection<Chat_Base> dialogues;
        private int index = 0;

        public EventManager OnDialogueActivated { get; } = new EventManager();
        public EventManager OnDialogueDeactivated { get; } = new EventManager();

        public void Activate()
        {
            gameObject.SetActive(true);
            OnDialogueActivated.Invoke();
            InputEvent.Instance.SetCursorToVisible();
        }

        public void Deactiavate()
        {
            gameObject.SetActive(false);
            OnDialogueDeactivated.Invoke();
            dialogues = null;

            InputEvent.Instance.SetCursorToInvisible();
            InputEvent.Instance.Event_NextDialogue.RemoveAllListener();
        }

        public async void SetValue(string characterName, IReadOnlyCollection<Chat_Base> dialogues)
        {
            Debug.Log("SetValue");
            this.characterName.text = characterName;
            this.dialogues = dialogues;
            index = 0;

            dialogueText.text = (dialogues.ElementAt(index) as Chat_Dialogue).Dialogue;
            index++;

            await AsyncManager.WaitForFrame(1);

            InputEvent.Instance.Event_NextDialogue.AddListener(GoToNextDialogue);
        }

        public void UpdateDialogue(IReadOnlyCollection<Chat_Base> dialogues, int startIndex)
        {
            this.dialogues = dialogues;
            index = startIndex;

            dialogueText.text = (dialogues.ElementAt(index) as Chat_Dialogue).Dialogue;
            index++;
   
            InputEvent.Instance.Event_NextDialogue.AddListener(GoToNextDialogue);
        }

        private void GoToNextDialogue()
        {
            var chat = dialogues.ElementAt(index);
            if (chat is Chat_Dialogue)
            {
                dialogueText.text = (chat as Chat_Dialogue).Dialogue;
                index++;
            }

            else if (chat is Chat_Branch)
            {
                ChatMain.Branching.Activate(chat as Chat_Branch);
                InputEvent.Instance.Event_NextDialogue.RemoveListener(GoToNextDialogue);
                index++;
            }

            else if (chat is Chat_Exit)
            {
                InputEvent.Instance.Event_NextDialogue.RemoveAllListener();
                Deactiavate();
            }

            else if (chat is Chat_Event)
            {
                var e = chat as Chat_Event;
                switch (e.Event)
                {
                    case ChatEvent.Shop:
                        ChatMain.Branching.Deactivate();
                        var products = e.GetEventObject<Item_ShopProducts>();
                        UI_Shop.Instance.Open(products.Products);
                        break;
                }
            }
        }
    }
}
