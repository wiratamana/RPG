using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIcon_Renderer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

        public Item_Preview ItemPreview { get; set; }
        private bool isMousePointerAboveMe = false;
        private const float DEFAULT_PREVIEW_CAMERA_ORTHO_SIZE = 0.4f;

        private static readonly Vector2 NormalSize = new Vector2(100, 100);

        public EventManager OnMouseEnter { private set; get; } = new EventManager();
        public EventManager OnMouseExit { private set; get; } = new EventManager();
        public EventManager OnMouseLeftClick { private set; get; } = new EventManager();


        private void Update()
        {
            if(isMousePointerAboveMe == true)
            {
                if (ItemPreview.enabled == false)
                {
                    ItemPreview.enabled = true;
                }
            }
            else
            {
                if(RawImage.rectTransform.sizeDelta == NormalSize)
                {
                    if(ItemPreview.enabled == true)
                    {
                        ItemPreview.enabled = false;
                        StartCoroutine(ResetItemPreviewRotationToDefault());
                    }
                }
            }

            if(ItemPreview.enabled == true)
            {
                RenderCamera();
            }
        }

        private IEnumerator ResetItemPreviewRotationToDefault()
        {
            yield return new WaitForEndOfFrame();

            ItemPreview.ResetPosition();
            RenderCamera();
        }

        private void RenderCamera()
        {
            UI_Menu_Inventory_Left_ItemIconDrawer.Instance.TextureRendererCamera.orthographicSize = DEFAULT_PREVIEW_CAMERA_ORTHO_SIZE;
            UI_Menu_Inventory_Left_ItemIconDrawer.Instance.TextureRendererCamera.transform.position = ItemPreview.transform.position - new Vector3(0, 0, 1);
            UI_Menu_Inventory_Left_ItemIconDrawer.Instance.TextureRendererCamera.transform.rotation = Quaternion.identity;
            if(ItemPreview.ItemBase is Item_Weapon)
            {
                var weapon = ItemPreview.ItemBase as Item_Weapon;
                UI_Menu_Inventory_Left_ItemIconDrawer.Instance.TextureRendererCamera.orthographicSize = weapon.CustomOrthoSize;
                UI_Menu_Inventory_Left_ItemIconDrawer.Instance.TextureRendererCamera.transform.position += weapon.MenuCameraOffset;
                UI_Menu_Inventory_Left_ItemIconDrawer.Instance.TextureRendererCamera.transform.rotation = Quaternion.Euler(weapon.MenuDefaultCameraRotation);
            }
            UI_Menu_Inventory_Left_ItemIconDrawer.Instance.TextureRendererCamera.targetTexture = RawImage.texture as RenderTexture;
            UI_Menu_Inventory_Left_ItemIconDrawer.Instance.TextureRendererCamera.Render();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnMouseLeftClick.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isMousePointerAboveMe = true;
            UI_Menu.Instance.Inventory.Right.ItemDescription.Activate(ItemPreview.ItemBase);
            OnMouseEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMousePointerAboveMe = false;
            UI_Menu.Instance.Inventory.Right.ItemDescription.Deactivate();
            OnMouseExit.Invoke();
        }

        private void OnDestroy()
        {
            Destroy(ItemPreview.gameObject);
        }
    }
}
