using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Item_Preview : MonoBehaviour
    {
        private MeshRenderer renderer;
        private Material material;
        [SerializeField] private Camera cam;

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

        public void SetCameraRender(Camera cam)
        {
            this.cam = cam;
        }

        public void ResetPosition()
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
