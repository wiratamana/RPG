using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Item_FloatingItem : MonoBehaviour
    {
        [SerializeField] private Item_Base item;

        private static readonly WaitForSeconds waitForHalfSecond = new WaitForSeconds(0.2f);
        private static Transform floatingItemParentTransform;

        private Transform itemTransform;
        private UI_Navigator navigator;
        private MeshRenderer meshRenderer;

        private Coroutine coroutine;

        private void OnValidate()
        {
            if(item != null)
            {
                gameObject.name = $"{item.ItemType} - {item.ItemName}";
            }

            if(floatingItemParentTransform == null)
            {
                floatingItemParentTransform = GameObject.FindWithTag(TagManager.TAG_FLOATING_ITEM_PARENT).transform;
            }

            if (transform.parent != floatingItemParentTransform)
            {
                transform.SetParent(floatingItemParentTransform);
            }
        }

        private void Start()
        {
            UI_Menu.OnBeforeOpen.AddListener(StopSphereCastCoroutine, GetInstanceID());
            UI_Menu.OnAfterClose.AddListener(StartSphereCastCoroutine, GetInstanceID());

            if (item != null)
            {
                itemTransform = Instantiate(item.Prefab);
                meshRenderer = itemTransform.gameObject.GetComponent<MeshRenderer>();
                meshRenderer.sharedMaterial = GameManager.ItemMaterial;

                if (item is Item_ModularBodyPart)
                {
                    itemTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                }
            }

            StartSphereCastCoroutine();
        }

        // Update is called once per frame
        void Update()
        {
            itemTransform.position = transform.position + new Vector3(0, Mathf.PingPong(Time.time * 0.1f, 0.1f), 0);
            itemTransform.Rotate(Vector3.up * 120 * Time.deltaTime);

            meshRenderer.sharedMaterial.SetVector("_CamDir", GameManager.MainCameraTransform.forward);
            meshRenderer.sharedMaterial.SetFloat("_Intensity", 2.0f);
        }

        private void AddEventPickUpItem()
        {
            if (navigator == null)
            {
                navigator = UI_NavigatorManager.Instance.Add(item.ItemName, InputEvent.ACTION_PICK_UP_ITEM);
                InputEvent.Instance.Event_PickUpItem.AddListener(PickUpItem, GetInstanceID());
            }
        }

        private void RemoveEventPickUpItem()
        {
            if (navigator != null)
            {
                UI_NavigatorManager.Instance.Remove(navigator);
                navigator = null;

                InputEvent.Instance.Event_PickUpItem.RemoveListener(PickUpItem, GetInstanceID());
            }
        }

        private void StartSphereCastCoroutine()
        {
            SphereCast();

            if(coroutine == null)
            {
                coroutine = StartCoroutine(SphereCastCoroutine());
            }            
        }

        private void StopSphereCastCoroutine()
        {
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            
            RemoveEventPickUpItem();
        }

        private IEnumerator SphereCastCoroutine()
        {
            while(true)
            {
                yield return waitForHalfSecond;

                SphereCast();
            }
        }

        private void SphereCast()
        {
            var distanceToPlayer = Vector3.Distance(GameManager.PlayerTransform.position, itemTransform.position);

            if (distanceToPlayer < 1.0f)
            {                
                AddEventPickUpItem();
            }
            else
            {
                RemoveEventPickUpItem();                
            }
        }


        public void PickUpItem()
        {
            GameManager.Player.Inventory.AddItem(item);

            RemoveEventPickUpItem();

            UI_Menu.OnBeforeOpen.RemoveListener(StopSphereCastCoroutine, GetInstanceID());
            UI_Menu.OnAfterClose.RemoveListener(StartSphereCastCoroutine, GetInstanceID());

            Destroy(itemTransform.gameObject);
            Destroy(gameObject);
        }
    }
}
