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

        private const string ParamName_StrafeHorizontal = nameof(StrafeHorizontal);
        private const string ParamName_StrafeVertical = nameof(StrafeVertical);
        private const string ParamName_IsStrafing = nameof(IsStrafing);
        private const string ParamName_Movement = nameof(Movement);
        private const string ParamName_IsMoving = nameof(IsMoving);
        private const string ParamName_IsDecelerating = nameof(IsDecelerating);
        private const string ParamName_IsAccelerating = nameof(IsAccelerating);
        private const string ParamName_IsTransitingToNextAttackAnimation = nameof(IsTransitingToNextAttackAnimation);
        private const string ParamName_IsRotateBeforeMove = nameof(IsRotateBeforeMove);
        private const string ParamName_IsHolstering = nameof(IsHolstering);
        private const string ParamName_IsEquipping = nameof(IsEquipping);
        private const string ParamName_IsTakingDamage = nameof(IsTakingDamage);
        private const string Paramname_IsInCombatState = nameof(IsInCombatState);
        private const string ParamName_IsInAttackingState = nameof(IsInAttackingState);
        private const string Paramname_AnimDodge = nameof(AnimDodge);
        private const string Paramname_AnimHit = nameof(AnimHit);
        private const string ParamName_Is2H = nameof(Is2H);

        public float StrafeHorizontal
        {
            get => animator.GetFloat(ParamName_StrafeHorizontal);
            set => animator.SetFloat(ParamName_StrafeHorizontal, value);
        }
        public float StrafeVertical
        {
            get => animator.GetFloat(ParamName_StrafeVertical);
            set => animator.SetFloat(ParamName_StrafeVertical, value);
        }
        public bool IsStrafing
        {
            get => animator.GetBool(ParamName_IsStrafing);
            set => animator.SetBool(ParamName_IsStrafing, value);
        }
        public float Movement
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
        public bool IsDecelerating
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
            set
            {
                if(IsHolstering == value)
                {
                    return;
                }

                if(value == true)
                {
                    unitAnimator.OnHolsteringAnimationStarted.Invoke();
                }
                else
                {
                    unitAnimator.OnHolsteringAnimationFinished.Invoke();
                }

                animator.SetBool(ParamName_IsHolstering, value);
            }
        }
        public bool IsEquipping
        {
            get => animator.GetBool(ParamName_IsEquipping);
            set
            {
                if(IsEquipping == value)
                {
                    return;
                }

                if(value == true)
                {
                    unitAnimator.OnEquippingAnimationStarted.Invoke();
                }
                else
                {
                    unitAnimator.OnEquippingAnimationFinished.Invoke();
                }

                animator.SetBool(ParamName_IsEquipping, value);
            }
        }
        public bool IsTakingDamage
        {
            get => animator.GetBool(ParamName_IsTakingDamage);
            set => animator.SetBool(ParamName_IsTakingDamage, value);
        }
        public bool IsInCombatState
        {
            get => animator.GetBool(Paramname_IsInCombatState);
            set
            {
                if(IsInCombatState == value)
                {
                    return;
                }

                if(value == true)
                {
                    unitAnimator.OnStateChangedToCombatState.Invoke();
                }
                else
                {
                    unitAnimator.OnStateChangedToIdleState.Invoke();
                }

                animator.SetBool(Paramname_IsInCombatState, value);
            }
        }
        public int AnimDodge
        {
            get => animator.GetInteger(Paramname_AnimDodge);
            set => animator.SetInteger(Paramname_AnimDodge, value);
        }
        public int AnimHit
        {
            get => animator.GetInteger(Paramname_AnimHit);
            set => animator.SetInteger(Paramname_AnimHit, value);
        }
        public bool IsInDodgingState => AnimDodge > 0;
        public bool IsInAttackingState
        {
            get => animator.GetBool(ParamName_IsInAttackingState);
            set => animator.SetBool(ParamName_IsInAttackingState, value);
        }
        public bool Is2H
        {
            get => animator.GetBool(ParamName_Is2H);
            set => animator.SetBool(ParamName_Is2H, value);
        }
    }
}
