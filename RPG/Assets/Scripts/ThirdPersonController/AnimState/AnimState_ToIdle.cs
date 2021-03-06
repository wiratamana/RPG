﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimState_ToIdle : StateMachineBehaviour
    {
        [SerializeField] [Range(0.01f, 1.0f)] private float transitionTimingMultiplier = 0.3f;
        [SerializeField] [Range(0.01f, 0.2f)] private float normalizedTransitionDuration = 0.1f;

        private Unit_Base unit;
        private WeaponType? currentWeapon;
        private float transitionTiming;
        private bool isTransition2IdleMotion;
        private bool isInCombatState;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            unit = animator.GetComponent<Unit_Base>();
            currentWeapon = unit.Equipment.EquippedWeapon?.WeaponType;
            transitionTiming = (stateInfo.length * transitionTimingMultiplier) - normalizedTransitionDuration;
            isInCombatState = unit.UnitAnimator.Params.IsInCombatState;

            isTransition2IdleMotion = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(isTransition2IdleMotion == true)
            {
                return;
            }

            var currentPosition = stateInfo.length * stateInfo.normalizedTime;

            if(currentPosition > transitionTiming)
            {
                isTransition2IdleMotion = true;

                if(currentWeapon.HasValue == true && isInCombatState == true)
                {
                    var stateName = currentWeapon.Value == WeaponType.OneHand ? "Movement_1H" : "Movement_2H";
                    animator.CrossFade(stateName, normalizedTransitionDuration);
                }
                else
                {
                    animator.CrossFade("Movement_Basic", normalizedTransitionDuration);
                }
            }
            
        }
    }
}
