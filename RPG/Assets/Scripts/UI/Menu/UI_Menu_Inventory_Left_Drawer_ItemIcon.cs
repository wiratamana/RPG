using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_Drawer_ItemIcon : SingletonMonobehaviour<UI_Menu_Inventory_Left_Drawer_ItemIcon>
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
                    var go = new GameObject(nameof(TextureRendererCamera));
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

        private Dictionary<Item_Preview, RenderTexture> instantiatedPrefabDic = new Dictionary<Item_Preview, RenderTexture>();
        private List<UI_Menu_Inventory_Left_ItemIcon> itemIconsList = new List<UI_Menu_Inventory_Left_ItemIcon>();
        private Stack<UI_Menu_Inventory_Left_ItemIcon> itemIconsPool = new Stack<UI_Menu_Inventory_Left_ItemIcon>();

        private void Start()
        {
            InstantiateItemIconBackground();
            UI_Menu_Inventory.OnMenuInventoryOpened.AddListener(InstantiateItemIconBackground);
            UI_Menu_Inventory.OnMenuInventoryClosed.AddListener(CleanItem);
            UI_Menu.Instance.Inventory.Left.ItemTypeDrawer.OnActiveItemTypeValueChanged.AddListener(OnItemTypeChanged);
        }

        private void OnItemTypeChanged(ItemType itemType)
        {
            TextureRendererCamera.enabled = false;
            TextureRendererCamera.targetTexture = null;

            foreach (var i in itemIconsList)
            {
                i.ItemRenderer.DestroyItemPreview();
                i.ItemRenderer.gameObject.SetActive(false);
                i.gameObject.SetActive(false);
                itemIconsPool.Push(i);
            }

            instantiatedPrefabDic.Clear();
            itemIconsList.Clear();

            InstantiateItemIconBackground();

            TextureRendererCamera.enabled = true;
        }

        private void CleanItem()
        {
            TextureRendererCamera.targetTexture = null;

            foreach (var itemIcon in itemIconsList)
            {
                itemIcon.ReturnToPool();
            }

            instantiatedPrefabDic.Clear();
            itemIconsList.Clear();
            itemIconsPool.Clear();

            UI_Menu_Inventory_Left_EquippedItemIcon.SetToNull();
        }

        private void InstantiateItemIconBackground()
        {
            var itemList = Item_Inventory.Instance.GetItemListAsReadOnly(x => 
                x.ItemType == UI_Menu.Instance.Inventory.Left.ItemTypeDrawer.CurrentlyActiveItemType);
            var itemCount = itemList.Count;
            var spacing = 10.0f;
            var iconSize = 128;
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

            for (int i = 0; i < itemCount; i++)
            {
                UI_Menu_Inventory_Left_ItemIcon itemIcon = null;
                if (itemIconsPool.Count == 0)
                {
                    // ===============================================================================================
                    // Create Parent
                    // ===============================================================================================
                    var go = new GameObject(itemList[i].ItemName);
                    var rt = go.AddComponent<RectTransform>();
                    rt.SetParent(RectTransform);
                    rt.localPosition = position;
                    rt.sizeDelta = new Vector2(iconSize, iconSize);
                    itemIcon = go.AddComponent<UI_Menu_Inventory_Left_ItemIcon>();
                }
                else
                {
                    itemIcon = itemIconsPool.Pop();
                    itemIcon.gameObject.SetActive(true);
                    itemIcon.RectTransform.SetParent(RectTransform);
                    itemIcon.RectTransform.localPosition = position;
                }

                // ===============================================================================================
                // Init ItemIcon and register it to itemiconlist
                // ===============================================================================================
                itemIcon.Init(itemList[i]);
                itemIconsList.Add(itemIcon);
                var itemPreview = itemIcon.ItemRenderer.InstantiateItemPreview(new Vector2(i, offsetY));
                instantiatedPrefabDic.Add(itemPreview, itemIcon.ItemRenderer.RawImage.texture as RenderTexture);
 
                // ===============================================================================================
                // Register this to EquippedItemIcon
                // ===============================================================================================
                UI_Menu_Inventory_Left_EquippedItemIcon.MarkItemAsEquippedItem(itemIcon);

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

            foreach(var t in instantiatedPrefabDic)
            {
                TextureRendererCamera.transform.position = t.Key.transform.position - new Vector3(0, 0, 1);
                TextureRendererCamera.targetTexture = t.Value;

                t.Key.ItemIcon.ItemRenderer.RenderCamera();
                t.Key.ResetRotation();
                t.Key.UpdateMaterial();
                TextureRendererCamera.Render();

                t.Key.ItemIcon.ItemRenderer.gameObject.SetActive(true);
            }
        }
    }
}
