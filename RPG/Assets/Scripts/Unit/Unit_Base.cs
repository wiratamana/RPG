using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public abstract class Unit_Base : MonoBehaviour
    {
        private Unit_Inventory inventory;        
        private Unit_Equipment equipment;
        private Unit_BodyTransform bodyTransform;

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

        public Unit_BodyTransform BodyTransform => this.GetOrAddAndAssignComponent(bodyTransform);
    }
}
