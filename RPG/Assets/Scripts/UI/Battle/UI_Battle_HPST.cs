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
            var playerStatus = GameManager.PlayerStatus;
            var bonus = playerStatus.HP.MaxHealth * 0.5f;
            HP.SetWidth(bonus);

            GameManager.PlayerStatus.ST.OnStaminaReducedBecauseAttackingEvent
                .AddListener(OnStaminaReducedBecauseAttackingEvent);

            GameManager.PlayerStatus.ST.OnStaminaRegeneratingEvent
                .AddListener(OnStaminaRegeneratingEvent);

            GameManager.PlayerStatus.ST.OnStaminaFullyRegeneratedEvent
                .AddListener(OnStaminaFullyRegenerated);
        }

        private void OnStaminaReducedBecauseAttackingEvent(int staminaUsage)
        {
            ST.SetFillRate(GameManager.PlayerStatus.ST.StaminaFillRate);
        }

        private void OnStaminaRegeneratingEvent(int regenerationRate)
        {
            ST.SetFillRate(GameManager.PlayerStatus.ST.StaminaFillRate);
        }

        private void OnStaminaFullyRegenerated()
        {
            ST.SetFillRate(GameManager.PlayerStatus.ST.StaminaFillRate);
        }
    }
}
