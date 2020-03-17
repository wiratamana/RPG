using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tamana
{
    public class UI_Navigator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI letter;
        [SerializeField] private TextMeshProUGUI text;

        public void SetValue(string text, char letter)
        {
            this.text.text = text;
            this.letter.enableAutoSizing = false;
            this.letter.text = letter.ToString();
        }

        public void SetValue(string text, string letter)
        {
            this.text.text = text;
            this.letter.enableAutoSizing = true;
            this.letter.text = letter;
        }
    }
}

