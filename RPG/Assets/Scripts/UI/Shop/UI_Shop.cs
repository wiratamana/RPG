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

        private Canvas canvas;
        public Canvas Canvas => this.GetAndAssignComponent(ref canvas);

        private const int FULLHD = FULLHD_HEIGHT * FULLHD_WIDTH;
        private const int FULLHD_WIDTH = 1920;
        private const int FULLHD_HEIGHT = 1080;

        public EventManager OnOpened { get; } = new EventManager();
        public EventManager OnClosed { get; } = new EventManager();

        private void Start()
        {
            Canvas.enabled = true;
            Instance.gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);

            if(Screen.width * Screen.height < FULLHD)
            {
                var canvasScaler = GetComponent<CanvasScaler>();
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScaler.referenceResolution = new Vector2(FULLHD_WIDTH, FULLHD_HEIGHT);
                return;
            }

            var screenSize = new Vector2(Screen.width, Screen.height);

            Left.RectTransform.sizeDelta = new Vector2(screenSize.x * 0.5f, screenSize.y);
            Left.RectTransform.localPosition = new Vector3(screenSize.x * -0.25f, 0.0f);

            Right.RectTransform.sizeDelta = Left.RectTransform.sizeDelta;
            Right.RectTransform.localPosition = new Vector3(screenSize.x * 0.25f, 0.0f);

            Left.Activate();
        }
    }
}
