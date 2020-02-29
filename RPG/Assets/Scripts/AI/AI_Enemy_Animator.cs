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
                if(animator == null)
                {
                    animator = GetComponent<Animator>();
                }

                return animator;
            }
        }

        private readonly string Sword1h_Equip = "Sword1h_Equip";
        private readonly string Sword1h_Holster = "Sword1h_Holster";

        public void Play(string stateName)
        {
            Animator.Play(stateName);
        }

        public void PlayEquipAnimation()
        {
            Play(Sword1h_Equip);
        }

        public void PlayHolsterAnimation()
        {
            Play(Sword1h_Holster);
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnHolster() { }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnEquip() { }
    }
}
