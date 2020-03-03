using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_AI_Hostile : Unit_Base
    {
        private AI_Enemy_Base ai;
        public AI_Enemy_Base AI => this.GetOrAddAndAssignComponent(ref ai);

        [SerializeField] private Item_Weapon weapon;

        protected override void OnValidate()
        {
            this.LogErrorIfComponentIsNull(AI);
        }

        private void Awake()
        {
            Inventory.AddItem(Instantiate(weapon));
            Equipment.EquipWeapon(Inventory.GetItemListAsReadOnly(x => x is Item_Weapon)[0] as Item_Weapon);
        }
    }
}
