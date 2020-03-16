using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIcon_Renderer : MonoBehaviour
    {
        private RawImage rawImage;
        public RawImage RawImage
        {
            get
            {
                if(rawImage == null)
                {
                    rawImage = GetComponent<RawImage>();
                }

                return rawImage;
            }
        }

        private UI_Menu_Inventory_Left_ItemIcon itemIcon;
        public UI_Menu_Inventory_Left_ItemIcon ItemIcon
        {
            get
            {
                if(itemIcon == null)
                {
                    itemIcon = transform.parent.GetComponent<UI_Menu_Inventory_Left_ItemIcon>();
                }

                return itemIcon;
            }
        }

        public Item_Preview ItemPreview { get; private set; }
        private bool isMousePointerAboveMe = false;
        private const float DEFAULT_PREVIEW_CAMERA_ORTHO_SIZE = 0.4f;

        private static readonly Vector2 NormalSize = new Vector2(100, 100);

        public EventManager OnMouseEnter { private set; get; } = new EventManager();
        public EventManager OnMouseExit { private set; get; } = new EventManager();
        public EventManager OnMouseLeftClick { private set; get; } = new EventManager();
        private EventTrigger eventTrigger;
        private EventTrigger EventTrigger
        {
            get
            {
                if(eventTrigger == null)
                {
                    eventTrigger = gameObject.GetOrAddComponent<EventTrigger>();
                }

                return eventTrigger;
            }
        }

        private void Awake()
        {
            EventTrigger.AddListener(EventTriggerType.PointerEnter, OnPointerEnter);
            EventTrigger.AddListener(EventTriggerType.PointerExit, OnPointerExit);
            EventTrigger.AddListener(EventTriggerType.PointerClick, OnPointerClick);
        }

        private void Update()
        {
            if(isMousePointerAboveMe == true)
            {
                if (ItemPreview.enabled == false)
                {
                    ItemPreview.enabled = true;
                    ResetCameraPositionAndRotation();
                }
            }
            else
            {
                if(RawImage.rectTransform.sizeDelta == NormalSize)
                {
                    if(ItemPreview.enabled == true)
                    {
                        ItemPreview.enabled = false;
                        ResetItemPreviewRotationToDefault();
                    }
                }
            }

            if(ItemPreview.enabled == true)
            {
                RenderCamera();
            }
        }

        private async void ResetItemPreviewRotationToDefault()
        {
            await AsyncManager.WaitForFrame(1);

            ItemPreview.ResetRotation();
            ResetCameraPositionAndRotation();
            RenderCamera();
        }

        public Item_Preview InstantiateItemPreview(Vector2 positionOffeset)
        {
            if(ItemPreview != null)
            {
                Debug.Log("ItemPreview is not null !!");
                return ItemPreview;
            }

            // ===============================================================================================
            // Instantiate item prefab and set its position.
            // ===============================================================================================
            ItemPreview = Item_Preview.InstantiateItemPrefab(ItemIcon, positionOffeset);

            return ItemPreview;
        }

        public void ResetCameraPositionAndRotation()
        {
            UI_ItemRenderer.SetOrthoSize(DEFAULT_PREVIEW_CAMERA_ORTHO_SIZE);
            UI_ItemRenderer.SetPosition(ItemPreview.transform.position - new Vector3(0, 0, 1));
            UI_ItemRenderer.SetRotation(Quaternion.identity);
            if (ItemPreview.ItemBase is Item_Weapon)
            {
                var weapon = ItemPreview.ItemBase as Item_Weapon;
                UI_ItemRenderer.SetOrthoSize(weapon.CustomOrthoSize);
                UI_ItemRenderer.SetPosition(UI_ItemRenderer.GetPosition() + weapon.MenuCameraOffset);
                UI_ItemRenderer.SetRotation(Quaternion.Euler(weapon.MenuDefaultCameraRotation));
            }
        }

        private void RenderCamera()
        {
            UI_ItemRenderer.SetTexture(RawImage.texture as RenderTexture);
            UI_ItemRenderer.Render();
        }

        private void OnPointerClick(BaseEventData eventData)
        {
            OnMouseLeftClick.Invoke();
        }

        private void OnPointerEnter(BaseEventData eventData)
        {
            isMousePointerAboveMe = true;
            UI_Menu.Instance.Inventory.Right.ItemDescription.Activate(ItemPreview.ItemBase);
            OnMouseEnter.Invoke();
        }

        private void OnPointerExit(BaseEventData eventData)
        {
            isMousePointerAboveMe = false;
            UI_Menu.Instance.Inventory.Right.ItemDescription.Deactivate();
            OnMouseExit.Invoke();
        }

        public void DestroyItemPreview()
        {
            Destroy(ItemPreview.gameObject);
            ItemPreview = null;
        }

        private void OnDestroy()
        {
            if(RawImage.texture != null)
            {
                DestroyImmediate(RawImage.texture);
            }

            DestroyItemPreview();
        }
    }
}
