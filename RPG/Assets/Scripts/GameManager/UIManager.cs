using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace Tamana
{
    public class UIManager : SingletonMonobehaviour<UIManager>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void InstantiateMe()
        {
            var go = new GameObject(nameof(UIManager));
            go.AddComponent<UIManager>();
            go.tag = TagManager.TAG_UI_MANAGER;
            DontDestroyOnLoad(go);
        }

        private Dictionary<string, UI_WindowBase> runningWindow;

        public bool IsUIBlockingController { private set; get; }

        public bool IsAnyWindowRunning
        {
            get
            {
                return runningWindow.Count > 0;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            runningWindow = new Dictionary<string, UI_WindowBase>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.B))
            {
                var menu = ResourcesLoader.Instance.InstantiatePrefabWithReturnValue<UI_Menu>();
                menu.OpenMenu(UI_Menu.MenuItem.Inventory);
            }
        }

        public void RegisterWindow(UI_WindowBase window)
        {
            if (IsWindowRunning(window) == true)
            {
                return;
            }

            var blockUIDic = ClassManager.GetTypesDefinedWith<GM_AttributeBlockController>();
            var windowName = window.GetType().Name;
            if (blockUIDic.ContainsKey(windowName) == true)
            {
                IsUIBlockingController = true;
            }

            runningWindow.Add(window.GetType().Name, window);
        }

        public void UnregisterWindow(UI_WindowBase window)
        {
            if (IsWindowRunning(window) == false)
            {
                return;
            }

            runningWindow.Remove(window.GetType().Name);

            var blockUIDic = ClassManager.GetTypesDefinedWith<GM_AttributeBlockController>();
            var turnOffUIBlocker = true;
            foreach(var w in runningWindow)
            {
                if(blockUIDic.ContainsKey(w.Key) == true)
                {
                    turnOffUIBlocker = false;
                    break;
                }
            }

            IsUIBlockingController = !turnOffUIBlocker;
        }

        public T GetRunningWindow<T>() where T : UI_WindowBase
        {
            if(IsWindowRunning<T>() == false)
            {
                return null;
            }

            return runningWindow[typeof(T).Name] as T;
        }

        public List<T> GetRunningWindows<T>() where T : UI_WindowBase
        {
            var returnValue = new List<T>();
            var Ts = runningWindow.Where(x => x.Value is T);
            foreach(var t in Ts)
            {
                returnValue.Add(t.Value as T);
            }

            return returnValue;
        }

        public bool IsWindowRunning<T>() where T : UI_WindowBase
        {
            return runningWindow.ContainsKey(typeof(T).Name);
        }

        public bool IsWindowRunning(UI_WindowBase windowBase)
        {       
            return runningWindow.ContainsKey(windowBase.GetType().Name);
        }

        public static Image CreateImage(Transform parent, int width, int height, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent);
            var img = go.AddComponent<Image>();
            img.rectTransform.sizeDelta = new Vector2(width, height);

            return img;
        }
        public static Image CreateImage()
        {
            return CreateImage(null, 100, 100, "Image");
        }
    }
}
