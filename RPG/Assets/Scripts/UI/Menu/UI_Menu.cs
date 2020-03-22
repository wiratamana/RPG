using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    [GM_AttributeInstantiator(typeof(UI_Menu))]
    public class UI_Menu : SingletonMonobehaviour<UI_Menu>
    {
        public static bool IsMenuActive
        {
            get
            {
                if (Instance == null)
                {
                    return false;
                }

                return Instance.gameObject.activeInHierarchy;
            }
        }

        private UI_Menu_Background _background;
        public UI_Menu_Background Background
        {
            get
            {
                if(_background == null)
                {
                    _background = GetComponentInChildren<UI_Menu_Background>();

                    if(_background == null)
                    {
                        Debug.Log($"Component {nameof(UI_Menu_Background)} not exist.", Debug.LogType.ForceQuit);
                        return null;
                    }
                }

                return _background;
            }
        }

        private UI_Menu_Header _header;
        public UI_Menu_Header Header
        {
            get
            {
                if (_header == null)
                {
                    _header = GetComponentInChildren<UI_Menu_Header>();

                    if (_header == null)
                    {
                        Debug.Log($"Component {nameof(UI_Menu_Header)} not exist.", Debug.LogType.ForceQuit);
                        return null;
                    }
                }

                return _header;
            }
        }

        private UI_Menu_Navigator _navigator;
        public UI_Menu_Navigator Navigator
        {
            get
            {
                if (_navigator == null)
                {
                    _navigator = GetComponentInChildren<UI_Menu_Navigator>();

                    if (_navigator == null)
                    {
                        Debug.Log($"Component {nameof(UI_Menu_Navigator)} not exist.", Debug.LogType.ForceQuit);
                        return null;
                    }
                }

                return _navigator;
            }
        }

        private UI_Menu_Body _body;
        public UI_Menu_Body Body
        {
            get
            {
                if(_body == null)
                {
                    _body = GetComponentInChildren<UI_Menu_Body>();

                    if(_body == null)
                    {
                        Debug.Log($"Component {nameof(UI_Menu_Body)} not exist.", Debug.LogType.ForceQuit);
                        return null;
                    }                    
                }

                return _body;
            }
        }

        [SerializeField] private UI_Menu_Resources menuResourcesPrefab;
        public UI_Menu_Resources MenuResources { private set; get; }

        public UI_Menu_Inventory Inventory { private set; get; }

        public static EventManager OnBeforeOpen { private set; get; } = new EventManager();
        public static EventManager OnAfterOpen { private set; get; } = new EventManager();
        public static EventManager OnBeforeClose { private set; get; } = new EventManager();
        public static EventManager OnAfterClose { private set; get; } = new EventManager();

        protected override void Awake()
        {
            base.Awake();

            MenuResources = Instantiate(menuResourcesPrefab);

            OnAfterOpen.AddListener(OnOpened);
            OnAfterClose.AddListener(OnClosed);
        }

        private void OnOpened()
        {
            InputEvent.Instance.Event_OpenOrCloseMenuInventory.RemoveListener(OpenMenuInventory);
            InputEvent.Instance.Event_OpenOrCloseMenuInventory.AddListener(CloseMenuInventory);

            InputEvent.Instance.SetCursorToVisible();
        }

        private void OnClosed()
        {
            InputEvent.Instance.Event_OpenOrCloseMenuInventory.RemoveListener(CloseMenuInventory);
            InputEvent.Instance.Event_OpenOrCloseMenuInventory.AddListener(OpenMenuInventory);

            InputEvent.Instance.SetCursorToInvisible();
        }

        public enum MenuItem
        {
            Inventory, Character
        }

        public void OpenMenu(MenuItem item)
        {
            OnBeforeOpen.Invoke();

            switch (item)
            {
                case MenuItem.Inventory:
                    Inventory = UI_Menu_Inventory.CreateMenuInventory(Body, Header, Navigator);
                    break;
                case MenuItem.Character:
                    break;
            }

            OnAfterOpen.Invoke();
        }

        public void CloseMenu()
        {
            OnBeforeClose.Invoke();

            Inventory.gameObject.SetActive(false);
            gameObject.SetActive(false);

            OnAfterClose.Invoke();
        }

        public static void OpenMenuInventory()
        {
            if(Instance == null)
            {
                ResourcesLoader.Instance.InstantiatePrefabWithReturnValue<UI_Menu>();
            }

            Instance.gameObject.SetActive(true);
            Instance.OpenMenu(MenuItem.Inventory);
        }

        public static void CloseMenuInventory()
        {
            if (Instance.gameObject.activeInHierarchy == true)
            {
                Instance.CloseMenu();
            }
        }
    }
}