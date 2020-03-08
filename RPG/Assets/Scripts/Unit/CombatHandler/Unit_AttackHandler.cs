using UnityEngine;

namespace Tamana
{
    public class Unit_AttackHandler : MonoBehaviour
    {
        [SerializeField] private TPC_CombatAnimDataContainer combatAnimDataContainer;
        [SerializeField] private TPC_CombatAnimDataContainer combatAnimDataContainer2H;

        private TPC_CombatAnimDataContainer currentCombatAnimDataContainer;

        private Unit_CombatHandler combatHandler;
        public Unit_CombatHandler CombatHandler => this.GetAndAssignComponent(ref combatHandler);

        public TPC_CombatAnimDataContainer CurrentlyPlayingCombatAnimDataContainer { set; get; }
        public TPC_CombatAnimData CurrentlyPlayingCombatAnimData { get; set; }

        public EventManager OnAttackAnimationStarted { get; } = new EventManager();
        public EventManager OnConsecutiveAttack { get; } = new EventManager();
        public EventManager OnAttackAnimationStopped { get; } = new EventManager();

        private void Awake()
        {
            if(CombatHandler.Unit.IsUnitPlayer)
            {
                CombatHandler.UnitAnimator.OnHitAnimationStarted.AddListener(MakePlayerUnableToAttack);
                CombatHandler.UnitAnimator.OnHitAnimationFinished.AddListener(MakePlayerAbleToAttackAgain);

                CombatHandler.Unit.Equipment.OnEquippedEvent.AddListener(OnEquippedWeapon);
            }

            CombatHandler.UnitAnimator.OnHitAnimationStarted.AddListener(SetAnimationStateToFalse);
        }

        private void OnEquippedWeapon(Item_Equipment oldWeapon, Item_Equipment newWeapon)
        {
            if(newWeapon is Item_Weapon == false)
            {
                return;
            }

            if((newWeapon as Item_Weapon).WeaponType == WeaponType.TwoHand)
            {
                currentCombatAnimDataContainer = combatAnimDataContainer2H;
            }
            else
            {
                currentCombatAnimDataContainer = combatAnimDataContainer;
            }
        }

        public void PlayAttackAnim()
        {
            if (currentCombatAnimDataContainer is null)
            {
                return;
            }

            if (currentCombatAnimDataContainer.StaminaCost > GameManager.PlayerStatus.ST.CurrentStamina)
            {
                return;
            }

            if (CurrentlyPlayingCombatAnimDataContainer == null || CurrentlyPlayingCombatAnimDataContainer == currentCombatAnimDataContainer)
            {
                if (currentCombatAnimDataContainer != null && CurrentlyPlayingCombatAnimData == null)
                {
                    CurrentlyPlayingCombatAnimDataContainer = currentCombatAnimDataContainer;
                    GameManager.PlayerStatus.ST.Attack(currentCombatAnimDataContainer.StaminaCost);
                    CombatHandler.UnitAnimator.Play(currentCombatAnimDataContainer.CombatDatas[0].MyAnimStateName);
                }

                else if (CurrentlyPlayingCombatAnimData != null)
                {
                    if (CurrentlyPlayingCombatAnimData.IsCurrentlyReceivingInput == true)
                    {
                        GameManager.PlayerStatus.ST.Attack(currentCombatAnimDataContainer.StaminaCost);
                        CurrentlyPlayingCombatAnimData.IsInputReceived = true;
                    }
                }
            }
        }

        private void MakePlayerUnableToAttack()
        {
            if(CombatHandler.UnitAnimator.Params.IsInCombatState == false)
            {
                return;
            }

            InputEvent.Instance.Event_DoAttackHeavy.RemoveListener(PlayAttackAnim);
        }

        private void MakePlayerAbleToAttackAgain()
        {
            CurrentlyPlayingCombatAnimDataContainer = null;
            CurrentlyPlayingCombatAnimData = null;

            if (CombatHandler.UnitAnimator.Params.IsInCombatState == false)
            {
                return;
            }

            InputEvent.Instance.Event_DoAttackHeavy.AddListener(PlayAttackAnim);
        }

        private void SetAnimationStateToFalse()
        {
            CombatHandler.UnitAnimator.Params.IsInAttackingState = false;
        }
    }
}
