using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_Animator : MonoBehaviour
    {
        private Animator animator;
        public Animator Animator
        {
            get
            {
                if (animator == null)
                {
                    animator = GetComponent<Animator>();
                }

                return animator;
            }
        }
        private Unit_Animator_Params _params;
        public Unit_Animator_Params Params
        {
            get
            {
                if (_params == null)
                {
                    _params = new Unit_Animator_Params(this);
                }

                return _params;
            }
        }

        private Unit_Animator_Status animStatus;
        public Unit_Animator_Status AnimStatus
        {
            get
            {
                if (animStatus == null)
                {
                    animStatus = new Unit_Animator_Status(this);
                }

                return animStatus;
            }
        }

        private Unit_Base unit;
        public Unit_Base Unit => this.GetAndAssignComponent(ref unit);
        public Unit_CombatHandler CombatHandler => Unit.CombatHandler;

        public float MoveAcceleration => 5 * Time.deltaTime;
        public EventManager OnReachMaximumVelocity { get; } = new EventManager();
        public EventManager OnReachZeroVelocity { get; } = new EventManager();
        public EventManager OnAccelerating { get; } = new EventManager();
        public EventManager OnDecelerating { get; } = new EventManager();

        public EventManager OnHitAnimationStarted { get; } = new EventManager();
        public EventManager OnHitAnimationFinished { get; } = new EventManager();

        public EventManager OnDodgeAnimationStarted { get; } = new EventManager();
        public EventManager OnDodgeAnimationFinished { get; } = new EventManager();

        public EventManager OnStateChangedToCombatState { get; } = new EventManager();
        public EventManager OnStateChangedToIdleState { get; } = new EventManager();

        public void Play(string stateName)
        {
            Animator.Play(stateName);
        }        

        public void Accelerate()
        {
            var movementValue = Params.Movement;
            var maxVelocity = Unit_Animator_Params.MAX_VELOCITY;
            Params.Movement = Mathf.Min(maxVelocity, movementValue + MoveAcceleration);
            OnAccelerating.Invoke();

            if (movementValue != maxVelocity && Params.Movement == maxVelocity)
            {
                OnReachMaximumVelocity.Invoke();
            }
        }

        public void Decelerate()
        {
            var movementValue = Params.Movement;
            var minVelocity = Unit_Animator_Params.MIN_VELOCITY;
            Params.Movement = Mathf.Max(minVelocity, movementValue - MoveAcceleration);
            OnDecelerating.Invoke();

            if (movementValue != minVelocity && Params.Movement == minVelocity)
            {
                OnReachZeroVelocity.Invoke();
            }
        }
    }
}
