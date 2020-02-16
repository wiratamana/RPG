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

            ItemPreview.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            RenderCamera();
        }

        private void RenderCamera()
        {
            UI_Menu_Inventory_Left_ItemIconDrawer.Instance.TextureRendererCamera.transform.position = ItemPreview.transform.position - new Vector3(0, 0, 1);
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
            OnMouseEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMousePointerAboveMe = false;
            OnMouseExit.Invoke();
        }

        private void OnDestroy()
        {
            Destroy(ItemPreview.gameObject);
        }
    }
}
