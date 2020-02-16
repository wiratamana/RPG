using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIconDrawer : SingletonMonobehaviour<UI_Menu_Inventory_Left_ItemIconDrawer>
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        private Camera _textureRendererCamera;
        public Camera TextureRendererCamera
        {
            get
            {
                if(_textureRendererCamera == null)
                {
                    var go = new GameObject(nameof(UI_Menu_Inventory_Left_ItemIconDrawer.CreateItemRendererCamera));
                    var cam = go.AddComponent<Camera>();
                    cam.orthographic = true;
                    cam.orthographicSize = 0.4f;
                    cam.clearFlags = CameraClearFlags.SolidColor;
                    cam.backgroundColor = Color.clear;
                    cam.cullingMask = 1 << LayerMask.NameToLayer(LayerManager.LAYER_ITEM_PROJECTION);

                    go.SetActive(false);

                    _textureRendererCamera = cam;
                }

                return _textureRendererCamera;
            }
        }

        private Dictionary<Transform, RenderTexture> listTransform = new Dictionary<Transform, RenderTexture>();
        private List<UI_Menu_Inventory_Left_ItemIcon> itemIconsList = new List<UI_Menu_Inventory_Left_ItemIcon>();

        protected override void Awake()
        {
            base.Awake();    
        }

        private void OnEnable()
        {
            InstantiateItemIconBackground();
        }

        private void OnDisable()
        {
            foreach(var itemIcon in itemIconsList)
            {
                itemIcon.ReturnToPool();
            }

            listTransform.Clear();
            itemIconsList.Clear();
        }

        private void InstantiateItemIconBackground()
        {
            var itemCount = Item_Inventory.Instance.ItemCount;
            var spacing = 10.0f;
            var iconSize = 128;
            var ringSize = 120;
            var horizontalMaxSize = RectTransform.sizeDelta.x;
            var horizontalShowLimit = 1;

            var horizontalSize = (iconSize * horizontalShowLimit) + ((horizontalShowLimit - 1) * spacing);
            while(horizontalSize < (horizontalMaxSize - (spacing * 2)))
            {
                horizontalShowLimit++;
                horizontalSize = (iconSize * horizontalShowLimit) + ((horizontalShowLimit - 1) * spacing);
            }

            horizontalShowLimit--;
            horizontalSize = (iconSize * horizontalShowLimit) + ((horizontalShowLimit - 1) * spacing);

            var horizontalRemainingSlots = horizontalShowLimit;
            var position = new Vector3((horizontalSize * -0.5f) + (iconSize * 0.5f), 
                (RectTransform.sizeDelta.y * 0.5f) - (iconSize * 0.5f) - spacing);

            float offsetY = 0;

            for(int i = 0; i < itemCount; i++)
            {
                // ===============================================================================================
                // Create Parent
                // ===============================================================================================
                var go = new GameObject("ItemIcon");
                go.transform.SetParent(RectTransform);
                var itemIcon = go.AddComponent<UI_Menu_Inventory_Left_ItemIcon>();
                var rt = go.AddComponent<RectTransform>();
                rt.sizeDelta = new Vector2(iconSize, iconSize);
                rt.localPosition = position;

                // ===============================================================================================
                // Create Background
                // ===============================================================================================
                var backgroundImg = UI_Menu_Pool.Instance.GetImage(rt, iconSize, iconSize, "ItemIcon-Background");
                backgroundImg.rectTransform.localPosition = Vector2.zero;
                backgroundImg.raycastTarget = false;
                var background = backgroundImg.gameObject.AddComponent<UI_Menu_Inventory_Left_ItemIcon_Background>();

                // ===============================================================================================
                // Create Ring
                // ===============================================================================================
                var ringImg = UI_Menu_Pool.Instance.GetImage(rt, ringSize, ringSize, "ItemIcon-Ring");                
                ringImg.rectTransform.localPosition = Vector2.zero;
                ringImg.sprite = UI_Menu.Instance.MenuResources.InventoryItemIconRing_Sprite;
                ringImg.raycastTarget = false;
                var ring = ringImg.gameObject.AddComponent<UI_Menu_Inventory_Left_ItemIcon_Ring>();

                // ===============================================================================================
                // Create RawTexture to render the item
                // ===============================================================================================
                var obj = CreateItemRendererCamera(Item_Inventory.Instance.GetItemPrefabIndex(i), new Vector2(i, offsetY));
                var itemImage = UI_Menu_Pool.Instance.GetRawImage(rt, 100, 100, Item_Inventory.Instance.GetItemNameAtIndex(i));
                itemImage.rectTransform.localPosition = Vector2.zero;
                itemImage.color = Color.white;
                itemImage.raycastTarget = true;
                itemImage.texture = new RenderTexture(175, 175, 16, RenderTextureFormat.ARGBHalf);
                var itemIconRenderer = itemImage.gameObject.AddComponent<UI_Menu_Inventory_Left_ItemIcon_Renderer>();
                itemIconRenderer.ItemPreview = obj.GetComponent<Item_Preview>();

                listTransform.Add(obj, itemImage.texture as RenderTexture);

                // ===============================================================================================
                // Register itemiconlist
                // ===============================================================================================
                itemIcon.SetValue(background, ring, itemIconRenderer);
                itemIconsList.Add(itemIcon);

                // ===============================================================================================
                // Update position
                // ===============================================================================================
                horizontalRemainingSlots--;

                if(horizontalRemainingSlots == 0)
                {
                    horizontalRemainingSlots = horizontalShowLimit;
                    position.y -= spacing + iconSize;
                    position.x = (horizontalSize * -0.5f) + (iconSize * 0.5f);

                    offsetY += 1.0f;
                }
                else
                {
                    position.x += spacing + iconSize;
                }
            }

            // ===============================================================================================
            // Render texture for the first time after instantiation finished
            // ===============================================================================================
            StartCoroutine(RenderCameraForTheFirstTime());
        }

        private IEnumerator RenderCameraForTheFirstTime()
        {
            yield return new WaitForEndOfFrame();

            foreach(var t in listTransform)
            {
                TextureRendererCamera.transform.position = t.Key.position - new Vector3(0, 0, 1);
                TextureRendererCamera.targetTexture = t.Value;
                TextureRendererCamera.Render();
            }
        }

        private Transform CreateItemRendererCamera(Transform prefab, Vector2 positionOffset)
        {
            var item = Instantiate(prefab);
            item.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            item.gameObject.layer = LayerMask.NameToLayer(LayerManager.LAYER_ITEM_PROJECTION);
            item.GetComponent<MeshRenderer>().sharedMaterial = GameManager.ItemMaterial;
            item.gameObject.AddComponent<Item_Preview>().SetCameraRender(TextureRendererCamera);

            item.position = new Vector3(0, 1000, 1) + (Vector3)positionOffset;
            item.rotation = Quaternion.Euler(0, 180, 0);

            return item;
        }
    }
}
