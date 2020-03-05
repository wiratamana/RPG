using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Battle_EnemyLock : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Battle uiBattle;
        public UI_Battle UIBattle => this.GetAndAssignComponentInParent(ref uiBattle);

        private void OnValidate()
        {
            RectTransform.SetSizeDeltaToCanvasParentCanvasSize(UIBattle.Canvas);
        }
    }
}
