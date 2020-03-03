using System;

namespace Tamana
{
    [AttributeUsage(AttributeTargets.Field)]
    public class UI_Menu_AttributeSpriteItemType : Attribute
    {
        public ItemType ItemType { private set; get; }

        public UI_Menu_AttributeSpriteItemType(ItemType itemType)
        {
            ItemType = itemType;
        }
    }
}
