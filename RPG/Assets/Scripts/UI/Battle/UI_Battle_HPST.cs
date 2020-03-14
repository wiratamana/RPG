using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Battle_HPST : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Battle uiBattle;
        public UI_Battle UIBattle => this.GetAndAssignComponentInParent(ref uiBattle);

        private UI_Battle_HPST_Child hp;
        public UI_Battle_HPST_Child HP => this.GetFindAndAssignComponentFromChildren(ref hp, x => x.Status == MainStatus.HP);

        private UI_Battle_HPST_Child st;
        public UI_Battle_HPST_Child ST => this.GetFindAndAssignComponentFromChildren(ref st, x => x.Status == MainStatus.ST);

        private void OnValidate()
        {
            RectTransform.SetSizeDeltaToCanvasParentCanvasSize(UIBattle.Canvas);
        }

        private void Start()
        {
            RectTransform.SetSizeDeltaToCanvasParentCanvasSize(UIBattle.Canvas);

            var playerStatus = GameManager.PlayerStatus;

            GameManager.PlayerStatus.ST.OnCurrentStaminaUpdated.AddListener(OnPlayerCurrentStaminaUpdated);
            GameManager.PlayerStatus.HP.OnCurrentHealthUpdated.AddListener(OnPlayerCurrentHealthUpdated);
        }

        private void OnPlayerCurrentHealthUpdated(float currentHPRate)
        {
            HP.SetFillRate(currentHPRate);
        }

        private void OnPlayerCurrentStaminaUpdated(float currentHPRate)
        {
            ST.SetFillRate(currentHPRate);
        }
    }
}
