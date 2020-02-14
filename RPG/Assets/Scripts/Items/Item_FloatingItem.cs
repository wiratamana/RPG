using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Item_FloatingItem : MonoBehaviour
    {
        [SerializeField] private Item_Base item;

        private Transform itemTransform;
        private Transform prefab;

        private void Start()
        {
            StartCoroutine(SphereCast());
        }

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
                if(item is Item_ModularBodyPart)
                {
                    itemTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                }                
            }

            if(itemTransform != null)
            {
                itemTransform.position = transform.position + new Vector3(0, Mathf.PingPong(Time.time * 0.1f, 0.1f), 0);
                itemTransform.Rotate(Vector3.up * 120 * Time.deltaTime);
            }            
        }

        [SerializeField] private float colliderRadius;
        private bool isColliding = false;

        private void OnDrawGizmos()
        {
            Gizmos.color = isColliding ? Color.red : Color.green;

            Gizmos.DrawWireSphere(transform.position, colliderRadius);
        }

        private IEnumerator SphereCast()
        {
            var halfSecond = new WaitForSeconds(0.5f);

            while(true)
            {
                var overlap = Physics.OverlapSphere(transform.position, colliderRadius);
                isColliding = overlap.Length > 0;
                if (overlap.Length > 0)
                {
                    foreach(var c in overlap)
                    {
                        //Debug.Log(c.gameObject.name);
                    }
                }
                else
                {
                    //Debug.Log("Nothing colliding with me");
                }

                yield return halfSecond;
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
        }
    }
}
