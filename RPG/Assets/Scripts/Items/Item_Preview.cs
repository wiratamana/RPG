using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Item_Preview : MonoBehaviour
    {
        private MeshRenderer renderer;
        private Material material;

        public Item_Base ItemBase => ItemIcon.Item;
        public UI_Menu_Inventory_Left_ItemIcon ItemIcon { private set; get; }

        private void Awake()
        {
            renderer = GetComponent<MeshRenderer>();
            material = renderer.material;

            enabled = false;
        }

        private void Update()
        {
            UpdateMaterial();
            UpdateRotation();
        }

        public void UpdateMaterial()
        {
            var cam = UI_Menu.Instance.Inventory.Left.ItemIconDrawer.TextureRendererCamera;
            material.SetVector("_CamDir", cam.transform.forward);
        }

        public void UpdateRotation()
        {
            transform.Rotate(Vector3.up * 120 * Time.deltaTime);
        }

        public void SetItemIcon(UI_Menu_Inventory_Left_ItemIcon itemIcon)
        {
            ItemIcon = itemIcon;
        }

        public void ResetRotation()
        {
            if (ItemBase is Item_Weapon)
            {
                var weapon = ItemBase as Item_Weapon;
                transform.rotation = Quaternion.Euler(weapon.MenuDefaultItemRotation);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        public static Item_Preview InstantiateItemPrefab(UI_Menu_Inventory_Left_ItemIcon itemIcon, Vector2 positionOffset)
        {
            var itemBase = itemIcon.Item;
            var item = Instantiate(itemBase.Prefab);

            if (itemBase is Item_Armor || itemBase is Item_Attachment)
            {
                item.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            }

            item.gameObject.layer = LayerMask.NameToLayer(LayerManager.LAYER_ITEM_PROJECTION);
            item.GetComponent<MeshRenderer>().sharedMaterial = GameManager.ItemMaterial;
            item.gameObject.AddComponent<Item_Preview>().SetItemIcon(itemIcon);

            item.position = new Vector3(0, 1000, 1) + (Vector3)positionOffset;
            item.rotation = Quaternion.Euler(0, 180, 0);

            return item.GetComponent<Item_Preview>();
        }
    }
}
