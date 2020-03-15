using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Chat_Main : SingletonMonobehaviour<UI_Chat_Main>
    {
        private UI_Chat_Bubble bubble;
        public UI_Chat_Bubble Bubble => this.GetAndAssignComponentInChildren(ref bubble);
    }
}
