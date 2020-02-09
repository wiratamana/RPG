﻿using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class TPC_PlayerMovementCombat : SingletonMonobehaviour<TPC_PlayerMovementCombat>
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) == true)
            {
                if(TPC_AnimController.Instance.AnimStateDic[nameof(TPC_Anim_AttributeIdle)] == false)
                {
                    return;
                }

                if(TPC_AnimController.Instance.GetLayerWeight(TPC_Anim_SwordAnimsetPro.LAYER) == 0.0f)
                {
                    TPC_AnimController.Instance.SetLayerWeight(TPC_Anim_SwordAnimsetPro.LAYER, 1.0f);
                    TPC_AnimController.Instance.PlayAnim(TPC_Anim_SwordAnimsetPro.Sword1h_Equip);
                }
                else
                {
                    TPC_AnimController.Instance.PlayAnim(TPC_Anim_SwordAnimsetPro.Sword1h_Holster);
                }                
            }
        }

        public TPC_BodyTransform BodyTransform { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            BodyTransform = gameObject.AddComponent<TPC_BodyTransform>();
        }

        [TPC_AnimClip_AttributeEvent]
        private void OnEquip()
        {
            Debug.Log("OnEquip");
        }

        [TPC_AnimClip_AttributeEvent]
        private void OnHolster()
        {
            Debug.Log("OnHolster");
        }

        public string GetStartMoveAnimationName(float angle)
        {
            if (angle < 45 && angle > -45)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart;
            }
            else if (angle < 120 && angle >= 45)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart90_L;
            }
            else if (angle > -120 && angle <= -45)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart90_R;
            }
            else if (angle < 165 && angle >= 45)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart135_L;
            }
            else if (angle > -165 && angle <= -45)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart135_R;
            }
            else if (angle <= 179.99 && angle >= 165)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart180_L;
            }
            else if (angle >= -180 && angle <= -165)
            {
                return TPC_Anim_SwordAnimsetPro.Sword1h_WalkFwdStart180_R;
            }

            return null;
        }
    }
}