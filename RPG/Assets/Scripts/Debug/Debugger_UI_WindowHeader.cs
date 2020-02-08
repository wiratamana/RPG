using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class Debugger_UI_WindowHeader : UI_WindowHeader
    {
        [SerializeField] private Image _pinImg;
        [SerializeField] private Color _colorPinned;
        [SerializeField] private Color _colorUnpinned;

        public Color ColorPinned { get { return _colorPinned; } }
        public Color ColorUnpinned { get { return _colorUnpinned; } }

        public new Debugger_UI_WindowBase WindowBase
        {
            get
            {
                if (_windowBase == null)
                {
                    _windowBase = GetComponentInParent<Debugger_UI_WindowBase>();
                }

                return _windowBase as Debugger_UI_WindowBase;
            }
        }

        public new Button ButtonClose
        {
            get
            {
                if (_buttonClose == null)
                {
                    _buttonClose = _closeImg.GetComponentInChildren<Button>();

                    if (_buttonClose == null)
                    {
                        Debug.Log($"Component '{nameof(Button)}' doesn't exist", Debug.LogType.Error);
                    }
                }

                return _buttonClose;
            }
        }


        private Button _buttonPin;
        public Button ButtonPin
        {
            get
            {
                if (_buttonPin == null)
                {
                    _buttonPin = _pinImg.GetComponentInChildren<Button>();

                    if (_buttonPin == null)
                    {
                        Debug.Log($"Component '{nameof(Button)}' doesn't exist", Debug.LogType.Error);
                    }
                }

                return _buttonPin;
            }
        }

        public void SetPinButtonColor(Color color)
        {
            _pinImg.color = color;
        }
    }
}
