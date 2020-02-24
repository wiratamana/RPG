using UnityEngine;
using System.Collections;

namespace Tamana
{
    public abstract class Item_Equipment : Item_Base
    {
        [SerializeField]
        private Item_Equipment_Effect[] effects;
        public Item_Equipment_Effect[] ItemEffects => effects;

        public abstract void Equip();
        public abstract void Unequip();
    }

}
