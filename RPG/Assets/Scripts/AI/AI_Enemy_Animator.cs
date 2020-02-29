using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AI_Enemy_Animator : MonoBehaviour
    {
        private Animator animator;
        private Animator Animator
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

        public const string Sword1h_Equip = "Sword1h_Equip";
        public const string Sword1h_Holster = "Sword1h_Holster";


        private const string IsInCombatState = "IsInCombatState";
        public bool Params_IsInCombatState => Animator.GetBool(IsInCombatState);

        public void Play(string stateName)
        {
            Animator.Play(stateName);
        }
    }
}
