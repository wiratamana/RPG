using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Tamana.AI;

namespace Tamana
{
    public class Unit_AI : Unit_Base
    {
        [SerializeField] private Item_Weapon weapon;
        [SerializeField] private AIBehaviour behaviour;
        [SerializeField] private AI_Brain brain;

        private Unit_AI_CombatLogic combatLogic;
        public Unit_AI_CombatLogic CombatLogic => this.GetOrAddAndAssignComponent(ref combatLogic);
        private PF_Unit pf;
        public PF_Unit PF => this.GetOrAddAndAssignComponent(ref pf);
        public AIBehaviour Behaviour => behaviour;

        protected override void OnValidate()
        {
            base.OnValidate();

            this.LogErrorIfComponentIsNull(CombatLogic);
        }

        protected override void Awake()
        {
            base.Awake();

            this.LogErrorIfComponentIsNull(CombatLogic);

            Inventory.AddItem(Instantiate(weapon));
            Equipment.EquipWeapon(Inventory.GetItemListAsReadOnly(x => x is Item_Weapon)[0] as Item_Weapon);

            var status = Resources.Load<Unit_Status_Information>("DummyStatus");
            Status.Initialize(Instantiate(status));
            Resources.UnloadAsset(status);

            if(brain != null)
            {
                var brain = Instantiate(this.brain);
                brain.Initialize(this);
                CombatLogic.InstallBrain(brain);
            }     
        }
    }
}
