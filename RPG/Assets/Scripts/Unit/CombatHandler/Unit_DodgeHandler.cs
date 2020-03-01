using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_DodgeHandler : MonoBehaviour
    {
        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftAlt) == true)
            {
                CombatHandler.UnitAnimator.Animator.SetBool("Wira", true);
            }
            
        }
    }
}
