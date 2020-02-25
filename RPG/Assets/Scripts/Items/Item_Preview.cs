using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Item_Preview : MonoBehaviour
    {
        private MeshRenderer renderer;
        private Material material;
        [SerializeField] private Camera cam;

        public Item_Base ItemBase { private set; get; }
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
            material.SetVector("_CamDir", cam == null ? GameManager.MainCamera.forward : cam.transform.forward);
        }

        public void UpdateRotation()
        {
            transform.Rotate(Vector3.up * 120 * Time.deltaTime);
        }

        public void SetValue(Camera cam, Item_Base itemBase, UI_Menu_Inventory_Left_ItemIcon itemIcon)
        {
            this.cam = cam;
            ItemIcon = itemIcon;
            ItemBase = itemBase;
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

        public static Item_Preview InstantiateItemPrefab(Item_Base itemBase, Vector2 positionOffset, 
            UI_Menu_Inventory_Left_ItemIcon itemIcon, Camera textureRendererCamera)
        {
            var item = Instantiate(itemBase.Prefab);

            if (itemBase is Item_Armor || itemBase is Item_Attachment)
            {
                item.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            }

            item.gameObject.layer = LayerMask.NameToLayer(LayerManager.LAYER_ITEM_PROJECTION);
            item.GetComponent<MeshRenderer>().sharedMaterial = GameManager.ItemMaterial;
            item.gameObject.AddComponent<Item_Preview>().SetValue(textureRendererCamera, itemBase, itemIcon);

            item.position = new Vector3(0, 1000, 1) + (Vector3)positionOffset;
            item.rotation = Quaternion.Euler(0, 180, 0);

            return item.GetComponent<Item_Preview>();
        }
    }
}
