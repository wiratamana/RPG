using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Tamana
{
    public class GlobalAssetsReference : MonoBehaviour
    {
        private static GlobalAssetsReference instance;
        private static GlobalAssetsReference Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<GlobalAssetsReference>();
                }

                return instance;
            }
        }
        [SerializeField] private Sprite headerSmallCircle;
        [SerializeField] private Sprite money;

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


        public static Sprite HeaderSmallCircle_Sprite => Instance.headerSmallCircle;
        public static Sprite InventoryItemIconRing_Sprite => Instance.inventoryItemIconRing_Sprite;
        public static Sprite InventoryItemBackgroundEquipped_Sprite => Instance.inventoryItemBackgroundEquipped_Sprite;
        public static Sprite InventoryItemOnPointerOver_Sprite => Instance.inventoryItemOnPointerOver_Sprite;
        public static Sprite Money_Sprite => Instance.money;

        private static Dictionary<ItemType, Sprite> itemTypeSpritesDic;
        public static Dictionary<ItemType, Sprite> ItemTypeSpritesDic
        {
            get
            {
                if (itemTypeSpritesDic == null || itemTypeSpritesDic.Count == 0)
                {
                    itemTypeSpritesDic = GetItemTypeSprites<ItemType, Sprite, UI_Menu_AttributeSpriteItemType>();
                }

                return itemTypeSpritesDic;
            }
        }

        private static Dictionary<MainStatus, Sprite> mainStatusSpritesDic;
        public static Dictionary<MainStatus, Sprite> MainStatusSpritesDic
        {
            get
            {
                if (mainStatusSpritesDic == null || mainStatusSpritesDic.Count == 0)
                {
                    mainStatusSpritesDic = GetItemTypeSprites<MainStatus, Sprite, Status_Attribute_MainStatus>();
                }

                return mainStatusSpritesDic;
            }
        }

        public static Sprite GetItemTypeSprites(ItemType key)
        {
            if (ItemTypeSpritesDic.ContainsKey(key) == false)
            {
                Debug.Log($"'{nameof(ItemTypeSpritesDic)}' dictionary doesn't have '{key}' key", Debug.LogType.Error);
                return null;
            }

            return ItemTypeSpritesDic[key];
        }

        public static Sprite GetMainStatusSprites(MainStatus key)
        {
            if (MainStatusSpritesDic.ContainsKey(key) == false)
            {
                Debug.Log($"'{nameof(MainStatusSpritesDic)}' dictionary doesn't have '{key}' key", Debug.LogType.Error);
                return null;
            }

            return MainStatusSpritesDic[key];
        }

        private static Dictionary<DicKey, DicValue> GetItemTypeSprites<DicKey, DicValue, Attribute>()
            where Attribute : System.Attribute
        {
            var retVal = new Dictionary<DicKey, DicValue>();

            var fields = Instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var f in fields)
            {
                if (f.IsDefined(typeof(Attribute)) == false)
                {
                    continue;
                }

                var itemType = f.GetCustomAttribute<Attribute>();
                var properties = itemType.GetType().GetProperties();

                retVal.Add((DicKey)properties[0].GetValue(itemType), (DicValue)f.GetValue(Instance));
            }

            return retVal;
        }
    }
}
