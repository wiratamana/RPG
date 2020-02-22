using UnityEngine;


namespace Tamana
{
    public class TPC_AnimParams : MonoBehaviour
    {
        private Animator animator
        {
            get
            {
                return TPC_AnimController.Instance?.CharacterAnimator;
            }
        }

        private readonly string isMoving = "IsMoving";
        public bool IsMoving
        {
            get
            {
                return animator.GetBool(isMoving);
            }

            set
            {
                animator.SetBool(isMoving, value);
            }
        }

        private readonly string isRotateBeforeMove = "IsRotateBeforeMove";
        public bool IsRotateBeforeMove
        {
            get
            {
                return animator.GetBool(isRotateBeforeMove);
            }

            set
            {
                animator.SetBool(isRotateBeforeMove, value);
            }
        }

        private readonly string isTransitingToNextAttackAnimation = "IsTransitingToNextAttackAnimation";
        public bool IsTransitingToNextAttackAnimation
        {
            get
            {
                return animator.GetBool(isTransitingToNextAttackAnimation);
            }

            set
            {
                animator.SetBool(isTransitingToNextAttackAnimation, value);
            }
        }
    }
}