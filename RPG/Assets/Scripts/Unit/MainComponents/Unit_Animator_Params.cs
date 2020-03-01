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

        public const float MAX_VELOCITY = 1.0f;
        public const float MIN_VELOCITY = 0.0f;

        private const string ParamName_IsInCombatState = "IsInCombatState";
        private const string ParamName_Movement = "Movement";
        private const string ParamName_IsMoving = "IsMoving";
        private const string ParamName_IsDecelerating = "IsDecelerating";
        private const string ParamName_IsAccelerating = "IsAccelerating";
        private const string ParamName_IsTransitingToNextAttackAnimation = "IsTransitingToNextAttackAnimation";
        private const string ParamName_IsRotateBeforeMove = "IsRotateBeforeMove";
        private const string ParamName_IsHolstering = "IsHolstering";
        private const string ParamName_IsEquipping = "IsEquipping";
        private const string ParamName_IsTakingDamage = "IsTakingDamage";
        private const string Paramname_IsInBattleState = "IsInBattleState";

        public bool IsInCombatState => animator.GetBool(ParamName_IsInCombatState);
        public float Params_Movement
        {
            get => animator.GetFloat(ParamName_Movement);
            set => animator.SetFloat(ParamName_Movement, value);
        }
        public bool IsTransitingToNextAttackAnimation
        {
            get => animator.GetBool(ParamName_IsTransitingToNextAttackAnimation);
            set => animator.SetBool(ParamName_IsTransitingToNextAttackAnimation, value);
        }
        public bool IsRotateBeforeMove
        {
            get => animator.GetBool(ParamName_IsRotateBeforeMove);
            set => animator.SetBool(ParamName_IsRotateBeforeMove, value);
        }
        public bool IsMoving
        {
            get => animator.GetBool(ParamName_IsMoving);
            set => animator.SetBool(ParamName_IsMoving, value);
        }
        public bool IsDeceleratin
        {
            get => animator.GetBool(ParamName_IsDecelerating);
            set => animator.SetBool(ParamName_IsDecelerating, value);
        }
        public bool IsAccelerating
        {
            get => animator.GetBool(ParamName_IsAccelerating);
            set => animator.SetBool(ParamName_IsAccelerating, value);
        }
        public bool IsHolstering
        {
            get => animator.GetBool(ParamName_IsHolstering);
            set => animator.SetBool(ParamName_IsHolstering, value);
        }
        public bool IsEquipping
        {
            get => animator.GetBool(ParamName_IsEquipping);
            set => animator.SetBool(ParamName_IsEquipping, value);
        }
        public bool IsTakingDamage
        {
            get => animator.GetBool(ParamName_IsTakingDamage);
            set => animator.SetBool(ParamName_IsTakingDamage, value);
        }
        public bool IsInBattleState
        {
            get => animator.GetBool(Paramname_IsInBattleState);
            set => animator.SetBool(Paramname_IsInBattleState, value);
        }
    }
}
