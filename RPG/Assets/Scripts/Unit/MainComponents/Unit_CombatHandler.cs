using System.Collections;
using System.Collections.Generic;
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

        public Unit_Base Unit => this.GetAndAssignComponent(ref unit);
        public Unit_DamageReceiveHandler DamageReceiveHandler => this.GetOrAddAndAssignComponent(ref damageReceiveHandler);
        public Unit_DamageSendHandler DamageSendHandler => this.GetOrAddAndAssignComponent(ref damageSendReceiver);
        public Unit_ParryHandler ParryHandler => this.GetOrAddAndAssignComponent(ref parryHandler);
        public Unit_DodgeHandler DodgeHandler => this.GetOrAddAndAssignComponent(ref dodgeHandler);
        public Unit_AttackHandler AttackHandler => this.GetOrAddAndAssignComponent(ref attackHandler);
        public Unit_BodyTransform BodyTransform => Unit.BodyTransform;
        public Unit_Animator UnitAnimator =>Unit.UnitAnimator;

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
        }

        private void Awake()
        {
            if(Unit.IsUnitPlayer == true)
            {
                Unit.UnitAnimator.OnStateChangedToCombatState.AddListener(AddCombatEventsToListeners);
                Unit.UnitAnimator.OnStateChangedToIdleState.AddListener(RemoveCombatEventsFromListeners);

                UI_Menu.OnBeforeOpen.AddListener(TemporarilyDisableCombatEvents);
                UI_Menu.OnAfterClose.AddListener(ReenableCombatEvents);
            }
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnHolster()
        {
            Unit.Equipment.EquippedWeapon.SetWeaponTransformParent(false);
            OnHolsterEvent.Invoke();

            if (Unit.IsUnitPlayer == true)
            {
                InputEvent.Instance.Event_Holster.RemoveListener(Holster);
                InputEvent.Instance.Event_Equip.AddListener(Equip);
            }
        }

        [TPC_AnimClip_AttributeWillBeInvokeByAnimationEvent]
        private void OnEquip()
        {
            Unit.Equipment.EquippedWeapon.SetWeaponTransformParent(true);
            OnEquipEvent.Invoke();

            if (Unit.IsUnitPlayer == true)
            {
                InputEvent.Instance.Event_Equip.RemoveListener(Equip);
                InputEvent.Instance.Event_Holster.AddListener(Holster);
            }
        }

        private void AddCombatEventsToListeners()
        {
            Debug.Log("AddCombatEventsToListeners");

            InputEvent.Instance.Event_DoAttackHeavy.AddListener(AttackHandler.PlayAttackAnim);
            InputEvent.Instance.Event_Parry.AddListener(ParryHandler.Parry);
            InputEvent.Instance.Event_Dodge.AddListener(DodgeHandler.Dodge);
        }

        private void RemoveCombatEventsFromListeners()
        {
            Debug.Log("RemoveCombatEventsFromListeners");

            InputEvent.Instance.Event_DoAttackHeavy.RemoveListener(AttackHandler.PlayAttackAnim);
            InputEvent.Instance.Event_Parry.RemoveListener(ParryHandler.Parry);
            InputEvent.Instance.Event_Dodge.RemoveListener(DodgeHandler.Dodge);
        }

        private void TemporarilyDisableCombatEvents()
        {
            if(Unit.UnitAnimator.Params.IsInCombatState == true)
            {
                RemoveCombatEventsFromListeners();
                InputEvent.Instance.Event_Holster.RemoveListener(Holster);
            }
            else
            {
                InputEvent.Instance.Event_Equip.RemoveListener(Equip);
            }
        }

        private void ReenableCombatEvents()
        {
            if (Unit.UnitAnimator.Params.IsInCombatState == true)
            {
                AddCombatEventsToListeners();
                InputEvent.Instance.Event_Holster.AddListener(Holster);
            }
            else
            {
                InputEvent.Instance.Event_Equip.AddListener(Equip);
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
    }
}
