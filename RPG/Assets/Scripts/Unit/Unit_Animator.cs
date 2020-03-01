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
        public Unit_Base Unit => this.GetAndAssignComponent(unit);
        public Unit_CombatHandler CombatHandler => Unit.CombatHandler;


        public void Play(string stateName)
        {
            Animator.Play(stateName);
        }
    }
}
