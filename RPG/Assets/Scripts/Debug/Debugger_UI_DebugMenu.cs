using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tamana
{
    [GM_AttributeInstantiator(typeof(Debugger_UI_DebugMenu))]
    [GM_AttributeBlockController]
    public class Debugger_UI_DebugMenu : Debugger_UI_WindowBase
    {
        [SerializeField] private Transform drawer;
        [SerializeField] private Debugger_UI_Shortcut shortcutPrefab;
        [SerializeField] private List<Transform> debugMenus;

        public bool IsThisWindowPinned { get; private set; }

        private void OnValidate()
        {
            if(debugMenus != null)
            {
                for(int i = 0; i < debugMenus.Count; i++)
                {
                    if(debugMenus[i] == null)
                    {
                        continue;
                    }

                    if(debugMenus[i].GetComponentInChildren<UI_WindowBase>() == null)
                    {
                        debugMenus.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            foreach(var m in debugMenus)
            {
                var obj = Instantiate(shortcutPrefab, drawer);
                obj.Prefab = m;
                obj.Name = m.name;
            }
        }

        public void PinThisWindow()
        {
            IsThisWindowPinned = true;
        }

        public void UnpinThisWindow()
        {
            IsThisWindowPinned = false;
        }
    }
}
