﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_NavigatorManager : SingletonMonobehaviour<UI_NavigatorManager>
    {
        [SerializeField] private UI_Navigator prefab;

        private Canvas canvas;
        public Canvas Canvas => this.GetAndAssignComponent(ref canvas);

        private Stack<UI_Navigator> navigatorPool;
        private Transform poolTransform;

        private const float LOCAL_POSITION_OFFSET = 20;
        private const float SPACING = 40;

        protected override void Awake()
        {
            base.Awake();

            navigatorPool = new Stack<UI_Navigator>();
            poolTransform = new GameObject(nameof(navigatorPool)).transform;
            DontDestroyOnLoad(poolTransform.gameObject);

            UI_Menu.OnBeforeOpen.AddListener(DisableCanvas);
            UI_Menu.OnAfterClose.AddListener(EnableCanvas);
        }

        private void DisableCanvas()
        {
            Canvas.enabled = false;
        }

        private void EnableCanvas()
        {
            Canvas.enabled = true;
        }

        public void Remove(ref UI_Navigator navigator)
        {
            if(navigator == null)
            {
                return;
            }

            navigator.transform.SetParent(poolTransform);
            navigatorPool.Push(navigator);
            Reposition();

            navigator = null;
        }

        public UI_Navigator Add(string text, char letter)
        {
            if(navigatorPool.Count == 0)
            {
                navigatorPool.Push(Instantiate(prefab, poolTransform));
            }

            var nav = navigatorPool.Pop();
            nav.SetValue(text, letter);

            nav.transform.SetParent(transform);
            Reposition();

            return nav;
        }

        public void Add(ref UI_Navigator nav, string text, string letter)
        {
            if (navigatorPool.Count == 0)
            {
                navigatorPool.Push(Instantiate(prefab, poolTransform));
            }

            nav = navigatorPool.Pop();
            nav.SetValue(text, letter);

            nav.transform.SetParent(transform);
            Reposition();
        }

        private void Reposition()
        {
            var basePos = new Vector3((Screen.width * 0.5f) - LOCAL_POSITION_OFFSET,
                    (Screen.height * -0.5f) + LOCAL_POSITION_OFFSET);

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localPosition = basePos + new Vector3(0, i * SPACING);
            }
        }
    }
}
