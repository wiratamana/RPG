using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Right_PlayerPortrait : SingletonMonobehaviour<UI_Menu_Inventory_Right_PlayerPortrait>
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

        private RawImage img;

        protected override void Awake()
        {
            base.Awake();

            img = UIManager.CreateRawImage(transform, (int)RectTransform.sizeDelta.x, (int)RectTransform.sizeDelta.y, 
                nameof(UI_Menu_Inventory_Right_PlayerPortrait));
            img.rectTransform.localPosition = Vector3.zero;          
        }

        private void OnEnable()
        {
            Inventory_Menu_PlayerPortrait.Instance.PortraitCamera.targetTexture = new RenderTexture((int)img.rectTransform.sizeDelta.x, (int)img.rectTransform.sizeDelta.y,
                16, RenderTextureFormat.ARGBHalf);

            img.texture = Inventory_Menu_PlayerPortrait.Instance.PortraitCamera.targetTexture;
        }

        private void OnDisable()
        {
            Inventory_Menu_PlayerPortrait.Instance.PortraitCamera.targetTexture = null;
            var renderTexture = (img.texture as RenderTexture);
            renderTexture.Release();
            DestroyImmediate(renderTexture);
        }
    }
}
