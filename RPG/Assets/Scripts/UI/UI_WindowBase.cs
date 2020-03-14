using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_WindowBase : MonoBehaviour
    {
        protected UI_WindowHeader _header;
        public UI_WindowHeader Header
        {
            get
            {
                if(_header == null)
                {
                    _header = GetComponentInChildren<UI_WindowHeader>();

                    if(_header == null)
                    {
                        Debug.Log($"Couldn't find '{nameof(UI_WindowHeader)}' component from child", Debug.LogType.Error);
                    }
                }

                return _header;
            }
        }

        private UI_WindowBody _body;
        public UI_WindowBody Body
        {
            get
            {
                if(_body == null)
                {
                    _body = GetComponentInChildren<UI_WindowBody>();

                    if (_body == null)
                    {
                        Debug.Log($"Couldn't find '{nameof(UI_WindowBody)}' component from child", Debug.LogType.Error);
                    }
                }

                return _body;
            }
        }

        [SerializeField] 
        [Range(400.0f, 1000.0f)]
        private float height = 400.0f;

        private const float MIN_HEIGHT = 400.0f;
        private const float MAX_HEIGHT = 1000.0f;

        private RectTransform _rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if(_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        private void OnValidate()
        {
            if(RectTransform != null)
            {
                RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, height);
                Body.RectTransform.sizeDelta = new Vector2(Body.RectTransform.sizeDelta.x, height - UI_WindowHeader.HEIGHT);
            }

            Header.HeaderName = transform.parent.name; 
        }

        protected virtual void Awake()
        {
            if (RectTransform != null)
            {
                RectTransform.sizeDelta = new Vector2(RectTransform.sizeDelta.x, height);
                Body.RectTransform.sizeDelta = new Vector2(Body.RectTransform.sizeDelta.x, height - UI_WindowHeader.HEIGHT);
            }

            Header.HeaderName = transform.parent.name;

            if (UIManager.Instance.IsWindowRunning(this) == true)
            {
                Close();
            }

            UIManager.Instance.RegisterWindow(this);
            Header.ButtonClose.onClick.AddListener(Close);
        }

        public void Close()
        {
            UIManager.Instance.UnregisterWindow(this);
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
