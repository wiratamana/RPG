using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Menu_Body : SingletonMonobehaviour<UI_Menu_Body>
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }
        public UI_Menu_Header Header
        {
            get
            {
                return UI_Menu.Instance.Header;
            }
        }

        public UI_Menu_Navigator Navigator
        {
            get
            {
                return UI_Menu.Instance.Navigator;
            }
        }
    }
}
