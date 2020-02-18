using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Header_MenuName : SingletonMonobehaviour<UI_Menu_Header_MenuName>
    {
        private TextMeshProUGUI menuName;
        private Image[] circles;

        protected override void Awake()
        {
            base.Awake();
            InstantiateText();
            InstantiateCircles();
        }

        private void InstantiateText()
        {
            var go = new GameObject(nameof(menuName));
            go.transform.SetParent(transform);
            go.AddComponent<RectTransform>();

            menuName = go.AddComponent<TextMeshProUGUI>();
            menuName.raycastTarget = false;
            menuName.richText = false;
            menuName.rectTransform.anchorMax = new Vector2(1.0f, 0.5f);
            menuName.rectTransform.anchorMin = new Vector2(0.0f, 0.5f);
            menuName.rectTransform.offsetMax = new Vector2(0.0f, 100.0f);
            menuName.rectTransform.offsetMin = new Vector2(0.0f, 50.0f);
            menuName.rectTransform.localPosition = Vector3.zero + new Vector3(0.0f, UI_Menu_Header.Instance.RectTransform.sizeDelta.y * 0.1f);
            menuName.alignment = TextAlignmentOptions.Center;

            menuName.text = "Inventory";
        }

        private void InstantiateCircles()
        {
            var size = 12;
            var spacing = 5;
            var xOffset = -size - spacing;
            var sprite = UI_Menu_Resources.Instance.HeaderSmallCircle_Sprite;

            circles = new Image[3];
            for(int i = 0; i < circles.Length; i++)
            {
                var img = UIManager.CreateImage(transform, size, size, "circle");
                img.rectTransform.localPosition = Vector3.zero + new Vector3(xOffset, -menuName.rectTransform.sizeDelta.y * 0.5f);
                img.sprite = sprite;
                
                xOffset += (size + spacing);
            }
        }
    }
}