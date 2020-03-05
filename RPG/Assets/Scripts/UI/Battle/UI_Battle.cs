using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Battle : SingletonMonobehaviour<UI_Battle>
    {
        private Canvas canvas;
        public Canvas Canvas => this.GetAndAssignComponent(ref canvas);
    }
}
