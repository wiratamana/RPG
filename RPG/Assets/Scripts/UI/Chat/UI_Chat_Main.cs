using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Chat_Main : MonoBehaviour
    {
        private static UI_Chat_Main instance;
        public static UI_Chat_Main Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<UI_Chat_Main>();
                }

                return instance;
            }
        }

        private UI_Chat_Bubble bubble;
        private UI_Chat_Dialogue dialogue;
        private UI_Chat_Branch branching;
        public UI_Chat_Bubble Bubble => this.GetAndAssignComponentInChildren(ref bubble);
        public UI_Chat_Dialogue Dialogue => this.GetAndAssignComponentInChildren(ref dialogue);
        public UI_Chat_Branch Branching => this.GetAndAssignComponentInChildren(ref branching);
    }
}
