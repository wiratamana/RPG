using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public class UI_Menu_Resources : SingletonMonobehaviour<UI_Menu_Resources>
    {
        [SerializeField] private Sprite headerSmallCircle;

        [SerializeField] 
        [UI_Menu_AttributeSpriteItemType(UI_Menu_Inventory.InventoryItemType.Weapon)]
        private Sprite inventoryItemTypeWeapon;

        [SerializeField]
        [UI_Menu_AttributeSpriteItemType(UI_Menu_Inventory.InventoryItemType.Armor)]
        private Sprite inventoryItemTypeArmor;

        [SerializeField]
        [UI_Menu_AttributeSpriteItemType(UI_Menu_Inventory.InventoryItemType.Consumable)]
        private Sprite inventoryItemTypeConsumable;

        [SerializeField]
        private Sprite inventoryItemIconRing_Sprite;

        public Sprite HeaderSmallCircle_Sprite { get { return headerSmallCircle; } }

        private Dictionary<UI_Menu_Inventory.InventoryItemType, Sprite> itemTypeSpritesDic;

        public Sprite InventoryItemIconRing_Sprite { get { return inventoryItemIconRing_Sprite; } }

        protected override void Awake()
        {
            base.Awake();
            name = nameof(UI_Menu_Resources);

            GetItemTypeSprites();
        }

        public Sprite GetItemTypeSprites(UI_Menu_Inventory.InventoryItemType key)
        {
            if(itemTypeSpritesDic.ContainsKey(key) == false)
            {
                Debug.Log($"'{nameof(itemTypeSpritesDic)}' dictionary doesn't have '{key}' key", Debug.LogType.Error);
                return null;
            }

            return itemTypeSpritesDic[key];
        }

        private void GetItemTypeSprites()
        {
            itemTypeSpritesDic = new Dictionary<UI_Menu_Inventory.InventoryItemType, Sprite>();

            var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var f in fields)
            {
                if(f.IsDefined(typeof(UI_Menu_AttributeSpriteItemType)) == false)
                {
                    continue;
                }

                var itemType = f.GetCustomAttribute<UI_Menu_AttributeSpriteItemType>().ItemType;
                itemTypeSpritesDic.Add(itemType, f.GetValue(this) as Sprite);
            }
        }
    }
}
