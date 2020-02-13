using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Item_FloatingItem : MonoBehaviour
    {
        [SerializeField] private Item_ModularBodyPart item;

        private Transform itemTransform;
        private Transform prefab;

        // Update is called once per frame
        void Update()
        {
            if(item != null && prefab != item.Prefab)
            {
                if(prefab != null)
                {
                    Destroy(itemTransform.gameObject);
                }

                prefab = item.Prefab;
                itemTransform = Instantiate(item.Prefab);
                itemTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            }

            if(itemTransform != null)
            {
                itemTransform.position = transform.position + new Vector3(0, Mathf.PingPong(Time.time * 0.1f, 0.1f), 0);
                itemTransform.Rotate(Vector3.up * 120 * Time.deltaTime);
            }
        }
    }
}
