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
    }
}
