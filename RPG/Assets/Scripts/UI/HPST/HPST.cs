using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class HPST : SingletonMonobehaviour<HPST>
    {
        private HPST_Child hp;
        public HPST_Child HP
        {
            get
            {
                if(hp == null)
                {
                    var components = GetComponentsInChildren<HPST_Child>();
                    hp = System.Array.Find(components, x => x.Status == MainStatus.HP);
                }

                return hp;
            }
        }

        private HPST_Child st;
        public HPST_Child ST
        {
            get
            {
                if(st is null)
                {
                    var components = GetComponentsInChildren<HPST_Child>();
                    st = System.Array.Find(components, x => x.Status == MainStatus.ST);
                }

                return st;
            }
        }

        private void Start()
        {
            var playerStatus = GameManager.PlayerStatus;
            var bonus = playerStatus.HP.MaxHealth * 0.5f;
            HP.SetWidth(bonus);

            GameManager.PlayerStatus.ST.OnStaminaReducedBecauseAttackingEvent
                .AddListener(OnStaminaReducedBecauseAttackingEvent);
            int a = 0x000;
            a += 0;
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
