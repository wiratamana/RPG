using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Item_FloatingItem : MonoBehaviour
    {
        [SerializeField] private Item_Base item;
        [SerializeField] private float colliderRadius;

        private static readonly WaitForSeconds waitForHalfSecond = new WaitForSeconds(0.5f);

        private Transform itemTransform;
        private Transform prefab;
        private UI_Navigator navigator;
        private MeshRenderer meshRenderer;
        private bool isOverlapping;

        private void OnValidate()
        {
            if(item != null)
            {
                gameObject.name = $"{item.ItemType} - {item.ItemName}";
            }
        }

        private void Start()
        {
            UI_Menu.OnBeforeOpen.AddListener(RemoveEventPickUpItem, GetInstanceID());
            UI_Menu.OnBeforeOpen.AddListener(StopSphereCastCoroutine, GetInstanceID());

            UI_Menu.OnAfterClose.AddListener(AddEventPickUpItem, GetInstanceID());
            UI_Menu.OnAfterClose.AddListener(StartSphereCastCoroutine, GetInstanceID());

            StartSphereCastCoroutine();
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

                meshRenderer.sharedMaterial.SetVector("_CamDir", GameManager.MainCameraTransform.forward);
                meshRenderer.sharedMaterial.SetFloat("_Intensity", 2.0f);
            }            
        }

        private void AddEventPickUpItem()
        {
            if(isOverlapping == false)
            {
                return;
            }

            InputEvent.Instance.Event_PickUpItem.AddListener(PickUpItem, GetInstanceID());
        }

        private void RemoveEventPickUpItem()
        {
            if (isOverlapping == false)
            {
                return;
            }

            InputEvent.Instance.Event_PickUpItem.RemoveListener(PickUpItem, GetInstanceID());
        }

        private void StartSphereCastCoroutine()
        {
            StartCoroutine(SphereCast());
        }

        private void StopSphereCastCoroutine()
        {
            StopCoroutine(SphereCast());
        }

        private IEnumerator SphereCast()
        {
            while(true)
            {
                var playerMask = LayerMask.GetMask(LayerManager.LAYER_PLAYER);
                var overlap = Physics.OverlapSphere(transform.position, colliderRadius, playerMask);
                isOverlapping = overlap.Length > 0;

                if (isOverlapping == true)
                {
                    if(navigator == null)
                    {
                        navigator = UI_NavigatorManager.Instance.Add(item.ItemName, InputEvent.ACTION_PICK_UP_ITEM);

                        AddEventPickUpItem();
                    }

                }
                else
                {
                    if(navigator != null)
                    {
                        UI_NavigatorManager.Instance.Remove(navigator);
                        navigator = null;

                        RemoveEventPickUpItem();
                    }
                }

                yield return waitForHalfSecond;
            }
        }

        public void PickUpItem()
        {
            GameManager.Player.Inventory.AddItem(item);
            if (itemTransform != null)
            {
                Destroy(itemTransform.gameObject);
            }

            UI_NavigatorManager.Instance.Remove(navigator);
            navigator = null;

            RemoveEventPickUpItem();

            UI_Menu.OnBeforeOpen.RemoveListener(RemoveEventPickUpItem, GetInstanceID());
            UI_Menu.OnBeforeOpen.RemoveListener(StopSphereCastCoroutine, GetInstanceID());

            UI_Menu.OnAfterClose.RemoveListener(AddEventPickUpItem, GetInstanceID());
            UI_Menu.OnAfterClose.RemoveListener(StartSphereCastCoroutine, GetInstanceID());

            Destroy(gameObject);
        }
    }
}
