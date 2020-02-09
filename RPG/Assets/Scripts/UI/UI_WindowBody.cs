using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class UI_WindowBody : MonoBehaviour
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
