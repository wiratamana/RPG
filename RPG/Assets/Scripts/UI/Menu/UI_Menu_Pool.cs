﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace Tamana
{
    public class UI_Menu_Pool : SingletonMonobehaviour<UI_Menu_Pool>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            var go = new GameObject(nameof(UI_Menu_Pool));
            DontDestroyOnLoad(go);
            go.AddComponent<UI_Menu_Pool>();
        }

        private Stack<Image> imagesPool = new Stack<Image>();        
        private Stack<RawImage> rawImagesPool = new Stack<RawImage>();
        private Stack<TextMeshProUGUI> textsPool = new Stack<TextMeshProUGUI>();

        public Image GetImage(Transform parent, int width, int height, string objName)
        {
            if(imagesPool.Count == 0)
            {
                return UIManager.CreateImage(parent, width, height, objName);
            }

            var img = imagesPool.Pop();
            img.rectTransform.SetParent(parent);
            img.rectTransform.sizeDelta = new Vector2(width, height);
            img.gameObject.name = objName;

            return img;
        }

        public RawImage GetRawImage(Transform parent, int width, int height, string objName)
        {
            if (rawImagesPool.Count == 0)
            {
                return UIManager.CreateRawImage(parent, width, height, objName);
            }

            var img = rawImagesPool.Pop();
            img.rectTransform.SetParent(parent);
            img.rectTransform.sizeDelta = new Vector2(width, height);
            img.gameObject.name = objName;

            return img;
        }

        public TextMeshProUGUI GetText(Transform parent, int width, int height, string text, string objName)
        {
            if (textsPool.Count == 0)
            {
                var txt = UIManager.CreateText(parent, width, height, text, objName);
                txt.alignment = TextAlignmentOptions.Center;
                txt.color = Color.white;
                txt.raycastTarget = false;
                txt.richText = false;
                txt.text = text;
                return txt;
            }

            var t = textsPool.Pop();
            t.rectTransform.SetParent(parent);
            t.rectTransform.sizeDelta = new Vector2(width, height);
            t.gameObject.name = objName;
            t.text = text;

            return t;
        }

        public void RemoveImage(Image image)
        {
            ResetRectTransform(image.rectTransform);
            image.transform.SetParent(transform);
            image.type = Image.Type.Simple;
            image.sprite = null;
            image.color = Color.white;
            image.raycastTarget = false;
            imagesPool.Push(image);
        }

        public void RemoveRawImage(RawImage rawImage)
        {
            if(rawImage.texture !=  null)
            {
                DestroyImmediate(rawImage.texture);
            }

            ResetRectTransform(rawImage.rectTransform);
            rawImage.transform.SetParent(transform);
            rawImage.texture = null;
            rawImage.raycastTarget = false;
            rawImagesPool.Push(rawImage);
        }

        public void RemoveText(TextMeshProUGUI text)
        {
            ResetRectTransform(text.rectTransform);
            text.transform.SetParent(transform);
            text.fontSize = 36;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;
            text.text = null;
            text.raycastTarget = false;
            textsPool.Push(text);
        }

        private void ResetRectTransform(RectTransform rt)
        {
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
        }
    }
}

