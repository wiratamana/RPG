using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Shop : MonoBehaviour
    {
        private static UI_Shop instance;
        public static UI_Shop Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<UI_Shop>();
                }

                return instance;
            }
        }

        private UI_Shop_Left left;
        private UI_Shop_Right right;

        public UI_Shop_Left Left => this.GetAndAssignComponentInChildren(ref left);
        public UI_Shop_Right Right => this.GetAndAssignComponentInChildren(ref right);

        public IReadOnlyCollection<Item_Product> Products { get; private set; }

        private Canvas canvas;
        public Canvas Canvas => this.GetAndAssignComponent(ref canvas);

        public EventManager OnOpened { get; } = new EventManager();
        public EventManager OnClosed { get; } = new EventManager();

        public void Open(IReadOnlyCollection<Item_Product> products)
        {
            if(GameManager.IsScreenResolutionGreaterOrEqualThanFHD == false)
            {
                var canvasScaler = GetComponent<CanvasScaler>();
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = new Vector2(GameManager.FULLHD_WIDTH, GameManager.FULLHD_HEIGHT);
            }
            else
            {
                var screenSize = new Vector2(Screen.width, Screen.height);

                Left.RectTransform.sizeDelta = new Vector2(screenSize.x * 0.5f, screenSize.y);
                Left.RectTransform.localPosition = new Vector3(screenSize.x * -0.25f, 0.0f);

                Right.RectTransform.sizeDelta = Left.RectTransform.sizeDelta;
                Right.RectTransform.localPosition = new Vector3(screenSize.x * 0.25f, 0.0f);
            }

            Products = products;
            Left.Activate();
            Right.Activate();
        }
    }
}
