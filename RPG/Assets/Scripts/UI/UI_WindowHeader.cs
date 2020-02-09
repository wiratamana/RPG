using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_WindowHeader : MonoBehaviour
    {
        public const float HEIGHT = 32.0f;

        [SerializeField] protected TextMeshProUGUI _title;
        [SerializeField] protected Image _headerBackgroundImg;
        [SerializeField] protected Image _closeImg;

        protected UI_WindowBase _windowBase;
        public UI_WindowBase WindowBase
        {
            get
            {
                if (_windowBase == null)
                {
                    _windowBase = GetComponentInParent<UI_WindowBase>();
                }

                return _windowBase;
            }
        }

        protected Button _buttonClose;
        public Button ButtonClose
        {
            get
            {
                if(_buttonClose == null)
                {
                    _buttonClose = GetComponentInChildren<Button>();

                    if(_buttonClose == null)
                    {
                        Debug.Log($"Component '{nameof(UnityEngine.UI.Button)}' doesn't exist", Debug.LogType.Error);
                    }
                }

                return _buttonClose;
            }
        }

        public string HeaderName
        {
            get
            {
                return _title.text;
            }

            set
            {
                _title.text = value;
            }
        }

        private bool isDragging;
        private Vector3 positionLastFrame;

        public void OnPointerDown()
        {
            positionLastFrame = Input.mousePosition;
            isDragging = true;
        }

        public void OnPointerUp()
        {
            isDragging = false;
        }

        public void OnDrag()
        {
            if(isDragging == false)
            {
                return;
            }

            var deltaMousePosition = Input.mousePosition - positionLastFrame;
            WindowBase.RectTransform.transform.position = WindowBase.RectTransform.transform.position + deltaMousePosition;
            positionLastFrame = Input.mousePosition;
        }
    }
}
