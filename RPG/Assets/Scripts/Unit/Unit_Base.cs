﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public abstract class Unit_Base : MonoBehaviour
    {
        private Unit_Inventory inventory;        
        private Unit_Equipment equipment;
        private Unit_CombatHandler combatHandler;
        private Unit_Animator animator;
        private Unit_BodyTransform bodyTransform;
        private Unit_Status status;
        private Unit_RotationHandler rotationHandler;

        public Unit_Inventory Inventory
        {
            get
            {
                if(inventory == null)
                {
                    inventory = new Unit_Inventory(this);
                }

                return inventory;
            }
        }
        public Unit_Equipment Equipment
        {
            get
            {
                if (equipment == null)
                {
                    if(this is Unit_Player)
                    {
                        equipment = new Unit_Equipment(this, Inventory_Menu_PlayerPortrait.Instance.transform);
                    }
                    else
                    {
                        equipment = new Unit_Equipment(this, null);
                    }
                }

                return equipment;
            }
        }
        public Unit_CombatHandler CombatHandler => this.GetOrAddAndAssignComponent(ref combatHandler);
        public Unit_Animator UnitAnimator => this.GetOrAddAndAssignComponent(ref animator);
        public Unit_BodyTransform BodyTransform => this.GetOrAddAndAssignComponent(ref bodyTransform);
        public Unit_Status Status => this.GetOrAddAndAssignComponent(ref status);
        public Unit_RotationHandler RotationHandler => this.GetOrAddAndAssignComponent(ref rotationHandler);

        public bool IsUnitPlayer => this is Unit_Player;

        protected virtual void OnValidate()
        {
            this.LogErrorIfComponentIsNull(CombatHandler);
            this.LogErrorIfComponentIsNull(UnitAnimator);
            this.LogErrorIfComponentIsNull(BodyTransform);
            this.LogErrorIfComponentIsNull(Status);
            this.LogErrorIfComponentIsNull(RotationHandler);
        }

        protected virtual void Awake()
        {
            this.LogErrorIfComponentIsNull(CombatHandler);
            this.LogErrorIfComponentIsNull(UnitAnimator);
            this.LogErrorIfComponentIsNull(BodyTransform);
            this.LogErrorIfComponentIsNull(Status);
            this.LogErrorIfComponentIsNull(RotationHandler);
        }
    }
}
