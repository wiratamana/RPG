using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Item_Preview : MonoBehaviour
    {
        private MeshRenderer renderer;
        private Material material;

        public Item_Base ItemBase { get; private set; }

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
            material.SetVector("_CamDir", UI_ItemRenderer.GetCameraTransform().forward);
        }

        public void UpdateRotation()
        {
            transform.Rotate(Vector3.up * 120 * Time.deltaTime);
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

        public static Item_Preview InstantiateItemPrefab(Item_Base itemBase, Vector2 positionOffset)
        {
            var item = Instantiate(itemBase.Prefab);

            if (itemBase is Item_Armor || itemBase is Item_Attachment)
            {
                item.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            }

            item.gameObject.layer = LayerMask.NameToLayer(LayerManager.LAYER_ITEM_PROJECTION);
            item.GetComponent<MeshRenderer>().sharedMaterial = GameManager.ItemMaterial;
            item.gameObject.AddComponent<Item_Preview>().ItemBase = itemBase;

            item.position = new Vector3(0, 1000, 1) + (Vector3)positionOffset;
            item.rotation = Quaternion.Euler(0, 180, 0);

            if (itemBase is Item_Weapon)
            {
                var weapon = itemBase as Item_Weapon;
                item.rotation = Quaternion.Euler(weapon.MenuDefaultItemRotation);
            }

            return item.GetComponent<Item_Preview>();
        }
    }
}
