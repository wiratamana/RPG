using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Selection : MonoBehaviour
    {
        private Image selectionImage;
        public Image SelectionImage
        {
            get
            {
                if(selectionImage == null)
                {
                    selectionImage = GetComponent<Image>();
                }

                return selectionImage;
            }
        }

        private static UI_Menu_Selection instance;

        public static void CreateInstance(RectTransform parent, float maxExpand)
        {
            Image img = null;
            if(instance == null)
            {
                img = UI_Menu_Pool.Instance.GetImage(parent, (int)parent.sizeDelta.x, (int)parent.sizeDelta.y, nameof(UI_Menu_Selection));

                var selection = img.gameObject.AddComponent<UI_Menu_Selection>();
                selection.maxExpand = maxExpand;
                selection.selectionImage = img;

                instance = selection;
            }
            else
            {
                img = instance.SelectionImage;

                img.rectTransform.SetParent(parent);
                img.rectTransform.sizeDelta = parent.sizeDelta;
                img.name = nameof(UI_Menu_Selection);

                instance.maxExpand = maxExpand;
                instance.defaultSizeDelta = parent.sizeDelta;
                instance.selectionImage = img;
            }

            img.rectTransform.localPosition = Vector3.zero;
            img.sprite = UI_Menu.Instance.MenuResources.InventoryItemOnPointerOver_Sprite;
            img.type = Image.Type.Sliced;
            img.raycastTarget = false;
        }

        public static void DestroyInstance()
        {
            Destroy(instance);
            UI_Menu_Pool.Instance.RemoveImage(instance.SelectionImage);
            instance = null;
        }

        private Vector2 defaultSizeDelta;
        private bool onMouseEnter;
        private float value;
        private float maxExpand;

        private void Start()
        {
            defaultSizeDelta = SelectionImage.rectTransform.sizeDelta;
        }

        private void Update()
        {
            value += Time.deltaTime * 32;
            var pingpong = Mathf.PingPong(value, 16);
            SelectionImage.rectTransform.sizeDelta = defaultSizeDelta + new Vector2(pingpong, pingpong);
        }
    }
}
