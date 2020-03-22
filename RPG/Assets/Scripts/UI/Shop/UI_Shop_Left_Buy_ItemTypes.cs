using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tamana
{
    public class UI_Shop_Left_Buy_ItemTypes : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private RectTransform dropdown;
        [SerializeField] private TextMeshProUGUI text;
        private RectTransform rectTransform;

        public Image Background => background;
        public RectTransform Dropdown => dropdown;
        public TextMeshProUGUI Text => text;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);
    }
}
