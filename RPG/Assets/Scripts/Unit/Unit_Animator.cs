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
                if(_params == null)
                {
                    _params = new Unit_Animator_Params(this);
                }

                return _params;
            }
        }

        private Unit_Base unit;
        public Unit_Base Unit => this.GetAndAssignComponent(ref unit);
        public Unit_CombatHandler CombatHandler => Unit.CombatHandler;

        public float MoveAcceleration => 5 * Time.deltaTime;
        public EventManager OnReachMaximumVelocity { get; } = new EventManager();
        public EventManager OnReachZeroVelocity { get; } = new EventManager();

        public void Play(string stateName)
        {
            Animator.Play(stateName);
        }

        public void Accelerate()
        {
            var movementValue = Params.Params_Movement;
            var maxVelocity = Unit_Animator_Params.MAX_VELOCITY;
            Params.Params_Movement = Mathf.Min(maxVelocity, movementValue + MoveAcceleration);

            if(movementValue != maxVelocity && Params.Params_Movement == maxVelocity)
            {
                OnReachMaximumVelocity.Invoke();
            }
        }

        public void Decelerate()
        {
            var movementValue = Params.Params_Movement;
            var minVelocity = Unit_Animator_Params.MIN_VELOCITY;
            Params.Params_Movement = Mathf.Max(minVelocity, movementValue - MoveAcceleration);

            if (movementValue != minVelocity && Params.Params_Movement == minVelocity)
            {
                OnReachZeroVelocity.Invoke();
            }
        }
    }
}
