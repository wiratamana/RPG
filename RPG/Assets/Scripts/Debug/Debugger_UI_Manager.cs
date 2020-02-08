using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Debugger_UI_Manager : SingletonMonobehaviour<Debugger_UI_Manager>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void InstantiateMe()
        {
            var go = new GameObject(nameof(Debugger_UI_Manager));
            DontDestroyOnLoad(go);

            go.AddComponent<Debugger_UI_Manager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                if(UIManager.Instance.IsWindowRunning<Debugger_UI_DebugMenu>() == true)
                {
                    var debugMenus = UIManager.Instance.GetRunningWindows<Debugger_UI_WindowBase>();
                    foreach(var menu in debugMenus)
                    {
                        if(menu.IsWindowPinned == true)
                        {
                            continue;
                        }

                        menu.Close();
                    }
                }
                else
                {
                    ResourcesLoader.Instance.InstantiatePrefabs<Debugger_UI_DebugMenu>();
                }                
            }
        }
    }
}
