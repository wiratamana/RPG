using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Chat_Branch : MonoBehaviour
    {
        private UI_Chat_Main chatMain;
        public UI_Chat_Main ChatMain => this.GetAndAssignComponentInParent(ref chatMain);

        private List<UI_Chat_BranchChild> options;
        private List<UI_Chat_BranchChild> Options
        {
            get
            {
                if(options == null || options.Count == 0)
                {
                    options = new List<UI_Chat_BranchChild>();
                    options.AddRange(GetComponentsInChildren<UI_Chat_BranchChild>(true));
                }

                return options;
            }
        }

        public void Activate(Chat_Branch branch)
        {
            Debug.Log($"Activate Count : {branch.Dialogues.Count}");
            for(int i = 0; i < branch.Dialogues.Count; i++)
            {
                var dialogue = branch.Dialogues.ElementAt(i).Dialogue;
                Options[i].Activate(dialogue);
            }
        }

        public void Deactivate()
        {
            foreach(var i in Options)
            {
                if(i.gameObject.activeInHierarchy == false)
                {
                    continue;
                }

                i.Deactivate();
            }
        }
    }
}
