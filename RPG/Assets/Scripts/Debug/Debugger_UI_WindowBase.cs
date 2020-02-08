using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class Debugger_UI_WindowBase : UI_WindowBase
    {
        public new Debugger_UI_WindowHeader Header
        {
            get
            {
                if (_header == null)
                {
                    _header = GetComponentInChildren<Debugger_UI_WindowHeader>();

                    if (_header == null)
                    {
                        Debug.Log($"Couldn't find '{nameof(Debugger_UI_WindowHeader)}' component from child", Debug.LogType.Error);
                    }
                }

                return _header as Debugger_UI_WindowHeader;
            }
        }

        public bool IsWindowPinned { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Header.ButtonPin.onClick.AddListener(Pin);
        }

        public void Pin()
        {
            IsWindowPinned = !IsWindowPinned;

            if(IsWindowPinned == true)
            {
                Header.SetPinButtonColor(Header.ColorPinned);
            }
            else
            {
                Header.SetPinButtonColor(Header.ColorUnpinned);
            }
        }
    }
}
