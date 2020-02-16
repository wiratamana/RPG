using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Item_FloatingItem : MonoBehaviour
    {
        [SerializeField] private Item_Base item;
        [SerializeField] private float colliderRadius;

        private Transform itemTransform;
        private Transform prefab;
        private UI_Navigator navigator;
        private MeshRenderer meshRenderer;

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
                meshRenderer = itemTransform.gameObject.GetComponent<MeshRenderer>();
                meshRenderer.sharedMaterial = GameManager.ItemMaterial;

                if(item is Item_ModularBodyPart)
                {
                    itemTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                }                
            }

            if(itemTransform != null)
            {
                itemTransform.position = transform.position + new Vector3(0, Mathf.PingPong(Time.time * 0.1f, 0.1f), 0);
                itemTransform.Rotate(Vector3.up * 120 * Time.deltaTime);

                meshRenderer.sharedMaterial.SetVector("_CamDir", GameManager.MainCamera.forward);
                meshRenderer.sharedMaterial.SetFloat("_Intensity", 2.0f);
            }            
        }

        private IEnumerator SphereCast()
        {
            var halfSecond = new WaitForSeconds(0.5f);

            while(true)
            {
                var overlap = Physics.OverlapSphere(transform.position, colliderRadius);
                if (overlap.Length > 0)
                {
                    if(navigator == null)
                    {
                        navigator = UI_NavigatorManager.Instance.Add(item.ItemName, InputEvent.ACTION_PICK_UP_ITEM);

                        InputEvent.Instance.Event_PickUpItem.AddListener(PickUpItem);
                    }                    
                }
                else
                {
                    if(navigator != null)
                    {
                        UI_NavigatorManager.Instance.Remove(navigator);
                        navigator = null;

                        InputEvent.Instance.Event_PickUpItem.RemoveListener(PickUpItem);
                    }                    
                }

                yield return halfSecond;
            }
        }

        public void PickUpItem()
        {
            Item_Inventory.Instance.AddItem(item);
            if (itemTransform != null)
            {
                Destroy(itemTransform.gameObject);
            }

            Destroy(gameObject);

            UI_NavigatorManager.Instance.Remove(navigator);
            navigator = null;

            InputEvent.Instance.Event_PickUpItem.RemoveListener(PickUpItem);
        }
    }
}
