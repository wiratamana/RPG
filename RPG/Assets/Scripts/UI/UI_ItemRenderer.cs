using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_ItemRenderer : MonoBehaviour
    {
        private static UI_ItemRenderer instance;
        public static UI_ItemRenderer Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GameObject(nameof(UI_ItemRenderer)).AddComponent<UI_ItemRenderer>();
                }

                return instance;
            }
        }

        private const float DEFAULT_PREVIEW_CAMERA_ORTHO_SIZE = 0.4f;

        private Camera _textureRendererCamera;
        public Camera TextureRendererCamera
        {
            get
            {
                if (_textureRendererCamera == null)
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

        public static void ResetCameraPositionAndRotation(Item_Base item, Transform itemTransform)
        {
            SetOrthoSize(DEFAULT_PREVIEW_CAMERA_ORTHO_SIZE);
            SetPosition(itemTransform.position - new Vector3(0, 0, 1));
            SetRotation(Quaternion.identity);
            if (item is Item_Weapon)
            {
                var weapon = item as Item_Weapon;
                SetOrthoSize(weapon.CustomOrthoSize);
                SetPosition(GetPosition() + weapon.MenuCameraOffset);
                SetRotation(Quaternion.Euler(weapon.MenuDefaultCameraRotation));
            }
        }

        public static Transform GetCameraTransform()
        {
            return Instance.TextureRendererCamera.transform;
        }

        public static void SetPosition(Vector3 position)
        {
            Instance.TextureRendererCamera.transform.position = position;
        }

        public static Vector3 GetPosition()
        {
            return Instance.TextureRendererCamera.transform.position;
        }

        public static void SetRotation(Quaternion rotation)
        {
            Instance.TextureRendererCamera.transform.rotation = rotation;
        }

        public static void SetOrthoSize(float size)
        {
            Instance.TextureRendererCamera.orthographicSize = size;
        }

        public static void Deactivate()
        {
            Instance.TextureRendererCamera.enabled = false;
            Instance.TextureRendererCamera.targetTexture = null;
        }

        public static void Activate()
        {
            Instance.TextureRendererCamera.enabled = true;
        }

        public static void SetTexture(RenderTexture renderTexture)
        {
            Instance.TextureRendererCamera.targetTexture = renderTexture;
        }

        public static void Render()
        {
            Instance.TextureRendererCamera.Render();
        }        
    }
}
