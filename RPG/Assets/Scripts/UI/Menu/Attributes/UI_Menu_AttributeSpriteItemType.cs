using System;

namespace Tamana
{
    [AttributeUsage(AttributeTargets.Field)]
    public class UI_Menu_AttributeSpriteItemType : Attribute
    {
        public UI_Menu_Inventory.InventoryItemType ItemType { private set; get; }

        public UI_Menu_AttributeSpriteItemType(UI_Menu_Inventory.InventoryItemType itemType)
        {
            ItemType = itemType;
        }
    }
}
