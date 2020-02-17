using UnityEngine;
using System.Collections;

namespace Tamana
{
    public abstract class Item_Equipment : Item_Base
    {
        public abstract void Equip();
        public abstract void Unequip();
    }

}
