using UnityEngine;

namespace Tamana
{
    public class Unit_CombatHandler : MonoBehaviour
    {
        private Unit_Base unit;
        private Unit_DamageReceiveHandler damageReceiveHandler;
        private Unit_DamageSendHandler damageSendReceiver;
        private Unit_ParryHandler parryHandler;
        private Unit_DodgeHandler dodgeHandler;
        private Unit_AttackHandler attackHandler;
        private Unit_DeathHandler deathHandler;

        public Unit_Base Unit => this.GetAndAssignComponent(ref unit);
        public Unit_DamageReceiveHandler DamageReceiveHandler => this.GetOrAddAndAssignComponent(ref damageReceiveHandler);
        public Unit_DamageSendHandler DamageSendHandler => this.GetOrAddAndAssignComponent(ref damageSendReceiver);
        public Unit_ParryHandler ParryHandler => this.GetOrAddAndAssignComponent(ref parryHandler);
        public Unit_DodgeHandler DodgeHandler => this.GetOrAddAndAssignComponent(ref dodgeHandler);
        public Unit_AttackHandler AttackHandler => this.GetOrAddAndAssignComponent(ref attackHandler);
        public Unit_BodyTransform BodyTransform => Unit.BodyTransform;
        public Unit_Animator UnitAnimator =>Unit.UnitAnimator;
        public Unit_DeathHandler DeathHandler => this.GetOrAddAndAssignComponent(ref deathHandler);

        public EventManager OnHolsterEvent { get; } = new EventManager();
        public EventManager OnEquipEvent { get; } = new EventManager();

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(DamageReceiveHandler);
            this.LogErrorIfComponentIsNull(DamageSendHandler);
            this.LogErrorIfComponentIsNull(ParryHandler);
            this.LogErrorIfComponentIsNull(DodgeHandler);
            this.LogErrorIfComponentIsNull(AttackHandler);
            this.LogErrorIfComponentIsNull(BodyTransform);
            this.LogErrorIfComponentIsNull(DeathHandler);
        }

        private void Awake()
        {
            this.LogErrorIfComponentIsNull(DamageReceiveHandler);
            this.LogErrorIfComponentIsNull(DamageSendHandler);
            this.LogErrorIfComponentIsNull(ParryHandler);
            this.LogErrorIfComponentIsNull(DodgeHandler);
            this.LogErrorIfComponentIsNull(AttackHandler);
            this.LogErrorIfComponentIsNull(BodyTransform);
            this.LogErrorIfComponentIsNull(DeathHandler);

            if (Unit.IsUnitPlayer == true)
            {
                Unit.UnitAnimator.OnStateChangedToCombatState.AddListener(AddCombatEventsToListeners);
                Unit.UnitAnimator.OnStateChangedToIdleState.AddListener(RemoveCombatEventsFromListeners);

                UI_Menu.OnBeforeOpen.AddListener(TemporarilyDisableCombatEvents);
                UI_Menu.OnAfterClose.AddListener(ReenableCombatEvents);

                Unit.UnitAnimator.OnHolsteringAnimationStarted.AddListener(RemoveHolster);
                Unit.UnitAnimator.OnHolsteringAnimationFinished.AddListener(AddEquip);

                Unit.UnitAnimator.OnEquippingAnimationStarted.AddListener(RemoveEquip);
                Unit.UnitAnimator.OnEquippingAnimationFinished.AddListener(AddHolster);

                UI_Chat_Main.Instance.Dialogue.OnDialogueActivated.AddListener(TemporarilyDisableCombatEvents);
                UI_Chat_Main.Instance.Dialogue.OnDialogueDeactivated.AddListener(ReenableCombatEvents);
            }

            Unit.Equipment.OnEquippedEvent.AddListener(OnWeaponEquipped);
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnHolster()
        {
            Unit.Equipment.EquippedWeapon.SetWeaponTransformParent(false);
            OnHolsterEvent.Invoke();
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnEquip()
        {
            Unit.Equipment.EquippedWeapon.SetWeaponTransformParent(true);
            OnEquipEvent.Invoke();
        }

        private void AddCombatEventsToListeners()
        {
            InputEvent.Instance.Event_DoAttackHeavy.AddListener(AttackHandler.PlayAttackAnim);
            InputEvent.Instance.Event_Parry.AddListener(ParryHandler.Parry);
            InputEvent.Instance.Event_Dodge.AddListener(DodgeHandler.Dodge);
        }

        private void RemoveCombatEventsFromListeners()
        {
            InputEvent.Instance.Event_DoAttackHeavy.RemoveListener(AttackHandler.PlayAttackAnim);
            InputEvent.Instance.Event_Parry.RemoveListener(ParryHandler.Parry);
            InputEvent.Instance.Event_Dodge.RemoveListener(DodgeHandler.Dodge);
        }

        private void TemporarilyDisableCombatEvents()
        {
            if(Unit.UnitAnimator.Params.IsInCombatState == true)
            {
                RemoveCombatEventsFromListeners();
                RemoveHolster();
            }
            else
            {
                RemoveEquip();
            }
        }

        private void ReenableCombatEvents()
        {
            if (Unit.UnitAnimator.Params.IsInCombatState == true)
            {
                AddCombatEventsToListeners();
                AddHolster();
            }
            else
            {
                AddEquip();
            }
        }

        public void Equip()
        {
            if (Unit.Equipment.EquippedWeapon == null)
            {
                return;
            }

            if(UnitAnimator.Params.IsEquipping == true || UnitAnimator.Params.IsHolstering == true)
            {
                return;
            }

            UnitAnimator.Params.IsEquipping = true;
        }
        public void Holster()
        {
            if (UnitAnimator.Params.IsEquipping == true || UnitAnimator.Params.IsHolstering == true)
            {
                return;
            }

            UnitAnimator.Params.IsHolstering = true;
        } 

        private void RemoveHolster()
        {
            InputEvent.Instance.Event_Holster.RemoveListener(Holster);
        }

        private void RemoveEquip()
        {
            InputEvent.Instance.Event_Equip.RemoveListener(Equip);
        }

        private void AddHolster()
        {
            if (UI_Menu.Instance?.gameObject.activeInHierarchy == false)
            {
                InputEvent.Instance.Event_Holster.AddListener(Holster);
            }         
        }

        private void AddEquip()
        {
            if(UI_Menu.Instance?.gameObject.activeInHierarchy == false)
            {
                InputEvent.Instance.Event_Equip.AddListener(Equip);
            }            
        }

        private void OnWeaponEquipped(Item_Equipment oldWeapon, Item_Equipment newWeapon)
        {
            if(newWeapon is Item_Weapon == false)
            {
                return;
            }

            Unit.UnitAnimator.Params.Is2H = (newWeapon as Item_Weapon).WeaponType == WeaponType.TwoHand;
        }
    }
}
