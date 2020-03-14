using UnityEngine;

namespace Tamana
{
    public class Environment_ObjectMeshHolder : MonoBehaviour
    {
        [SerializeField] private Mesh mesh;
        public Mesh sharedMesh => mesh;

        private void OnValidate()
        {
            if(mesh == null)
            {
                mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
            }

            if(GetComponent<MeshCollider>() == null)
            {
                gameObject.AddComponent<MeshCollider>();
            }
        }
    }
}
