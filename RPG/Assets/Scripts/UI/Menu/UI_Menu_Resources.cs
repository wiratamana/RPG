using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public class UI_Menu_Resources : SingletonMonobehaviour<UI_Menu_Resources>
    {
        [SerializeField] private Sprite headerSmallCircle;

        [SerializeField] 
        [UI_Menu_AttributeSpriteItemType(ItemType.Weapon)]
        private Sprite inventoryItemTypeWeapon;

        [SerializeField]
        [UI_Menu_AttributeSpriteItemType(ItemType.Armor)]
        private Sprite inventoryItemTypeArmor;

        [SerializeField]
        [UI_Menu_AttributeSpriteItemType(ItemType.Attachment)]
        private Sprite inventoryItemTypeAttachment;

        [SerializeField]
        [UI_Menu_AttributeSpriteItemType(ItemType.Consumable)]
        private Sprite inventoryItemTypeConsumable;

        [SerializeField]
        private Sprite inventoryItemIconRing_Sprite;

        [SerializeField]
        private Sprite inventoryItemBackgroundEquipped_Sprite;

        [SerializeField]
        private Sprite inventoryItemOnPointerOver_Sprite;

        [SerializeField] 
        [Status_Attribute_MainStatus(MainStatus.HP)]
        private Sprite mainStatus_HP_Sprite;

        [SerializeField]
        [Status_Attribute_MainStatus(MainStatus.ST)]
        private Sprite mainStatus_ST_Sprite;

        [SerializeField]
        [Status_Attribute_MainStatus(MainStatus.AT)]
        private Sprite mainStatus_AT_Sprite;

        [SerializeField]
        [Status_Attribute_MainStatus(MainStatus.DF)]
        private Sprite mainStatus_DF_Sprite;


        public Sprite HeaderSmallCircle_Sprite { get { return headerSmallCircle; } }
        public Sprite InventoryItemIconRing_Sprite { get { return inventoryItemIconRing_Sprite; } }
        public Sprite InventoryItemBackgroundEquipped_Sprite { get { return inventoryItemBackgroundEquipped_Sprite; } }
        public Sprite InventoryItemOnPointerOver_Sprite { get { return inventoryItemOnPointerOver_Sprite; } }

        private Dictionary<ItemType, Sprite> itemTypeSpritesDic;
        public Dictionary<ItemType, Sprite> ItemTypeSpritesDic
        {
            get
            {
                if(itemTypeSpritesDic == null || itemTypeSpritesDic.Count == 0)
                {
                    itemTypeSpritesDic = GetItemTypeSprites<ItemType, Sprite, UI_Menu_AttributeSpriteItemType>();
                }

                return itemTypeSpritesDic;
            }
        }

        private Dictionary<MainStatus, Sprite> mainStatusSpritesDic;
        public Dictionary<MainStatus, Sprite> MainStatusSpritesDic
        {
            get
            {
                if(mainStatusSpritesDic == null || mainStatusSpritesDic .Count == 0)
                {
                    mainStatusSpritesDic = GetItemTypeSprites<MainStatus, Sprite, Status_Attribute_MainStatus>();
                }

                return mainStatusSpritesDic;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            name = nameof(UI_Menu_Resources);
        }

        public Sprite GetItemTypeSprites(ItemType key)
        {
            if(ItemTypeSpritesDic.ContainsKey(key) == false)
            {
                Debug.Log($"'{nameof(ItemTypeSpritesDic)}' dictionary doesn't have '{key}' key", Debug.LogType.Error);
                return null;
            }

            return ItemTypeSpritesDic[key];
        }

        public Sprite GetMainStatusSprites(MainStatus key)
        {
            if (MainStatusSpritesDic.ContainsKey(key) == false)
            {
                Debug.Log($"'{nameof(MainStatusSpritesDic)}' dictionary doesn't have '{key}' key", Debug.LogType.Error);
                return null;
            }

            return MainStatusSpritesDic[key];
        }

        private Dictionary<DicKey, DicValue> GetItemTypeSprites<DicKey, DicValue, Attribute>()
            where Attribute : System.Attribute
        {
            var retVal = new Dictionary<DicKey, DicValue>();

            var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var f in fields)
            {
                if(f.IsDefined(typeof(Attribute)) == false)
                {
                    continue;
                }

                var itemType = f.GetCustomAttribute<Attribute>();
                var properties = itemType.GetType().GetProperties();

                retVal.Add((DicKey)properties[0].GetValue(itemType), (DicValue)f.GetValue(this));
            }

            return retVal;
        }
    }
}
