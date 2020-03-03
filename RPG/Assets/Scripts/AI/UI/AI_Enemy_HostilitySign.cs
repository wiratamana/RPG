using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    public class AI_Enemy_HostilitySign : MonoBehaviour
    {
        [SerializeField] private Image color;

        private RectTransform rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if(rectTransform == null)
                {
                    rectTransform = GetComponent<RectTransform>();    
                }

                return rectTransform;
            }
        }

        public void SetColor(Color color)
        {
            this.color.color = color;
        }

        public void Deactiave()
        {
            if(gameObject.activeInHierarchy == true)
            {
                gameObject.SetActive(false);
            }
        }

        public void Activate()
        {
            if (gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(true);
            }
        }

        public void UpdatePosition(Vector3 aiPosition)
        {
            var uiPos = GameManager.MainCamera.GetComponent<Camera>().WorldToScreenPoint(aiPosition);
            RectTransform.position = uiPos;
        }
    }
}
