using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Menu_Navigator : SingletonMonobehaviour<UI_Menu_Navigator>
    {
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
    }
}
