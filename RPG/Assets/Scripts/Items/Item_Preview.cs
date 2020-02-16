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

        private void Awake()
        {
            renderer = GetComponent<MeshRenderer>();
            material = renderer.material;
        }

        public void Update()
        {
            material.SetVector("_CamDir", cam == null ? GameManager.MainCamera.forward : cam.transform.forward);
            transform.Rotate(Vector3.up * 120 * Time.deltaTime);
        }

        public void SetValue(Camera cam, Item_Base itemBase)
        {
            this.cam = cam;
            ItemBase = itemBase;
        }

        public void ResetPosition()
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
