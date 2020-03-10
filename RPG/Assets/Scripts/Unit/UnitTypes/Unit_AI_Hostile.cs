using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tamana.AI;

namespace Tamana
{
    public class Unit_AI_Hostile : Unit_Base
    {
        [SerializeField] private Item_Weapon weapon;
        private AI_Enemy_CombatLogic combatLogic;
        public AI_Enemy_CombatLogic CombatLogic => this.GetOrAddAndAssignComponent(ref combatLogic);

        protected override void OnValidate()
        {
            base.OnValidate();

            this.LogErrorIfComponentIsNull(CombatLogic);
        }

        private void Awake()
        {
            Inventory.AddItem(Instantiate(weapon));
            Equipment.EquipWeapon(Inventory.GetItemListAsReadOnly(x => x is Item_Weapon)[0] as Item_Weapon);

            var status = Resources.Load<Unit_Status_Information>("DummyStatus");
            Status.Initialize(Instantiate(status));
            Resources.UnloadAsset(status);

            if(weapon.WeaponType == WeaponType.OneHand)
            {
                var brain = ScriptableObject.CreateInstance<AI_Brain_Enemy_Dummy>();
                brain.Initialize(this);
                CombatLogic.InstallBrain(brain);
            }
            else
            {
                var brain = ScriptableObject.CreateInstance<AI_Brain_Enemy_Dummy_2H>();
                brain.Initialize(this);
                CombatLogic.InstallBrain(brain);
            }            
        }
    }
}
