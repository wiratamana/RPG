using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Animator_Params
    {
        private readonly Unit_Animator unitAnimator;
        private readonly Animator animator;

        public Unit_Animator_Params(Unit_Animator unitAnimator)
        {
            this.unitAnimator = unitAnimator;
            animator = this.unitAnimator.Animator;
        }

        private const string IsInCombatState = "IsInCombatState";
        private const string Movement = "Movement";
        private const string IsTransitingToNextAttackAnimation = "IsTransitingToNextAttackAnimation";
        private const string IsRotateBeforeMove = "IsRotateBeforeMove";

        public bool Params_IsInCombatState => animator.GetBool(IsInCombatState);
        public float Params_Movement
        {
            get => animator.GetFloat(Movement);
            set => animator.SetFloat(Movement, value);
        }
        public bool Param_IsTransitingToNextAttackAnimation
        {
            get => animator.GetBool(IsTransitingToNextAttackAnimation);
            set => animator.SetBool(IsTransitingToNextAttackAnimation, value);
        }
        public bool Param_IsRotateBeforeMove
        {
            get => animator.GetBool(IsRotateBeforeMove);
            set => animator.SetBool(IsRotateBeforeMove, value);
        }
    }
}
