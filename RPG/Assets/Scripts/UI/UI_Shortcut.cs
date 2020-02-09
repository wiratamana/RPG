using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Shortcut : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _img;

        private Button _button;
        public Button Button
        {
            get
            {
                if(_button == null)
                {
                    _button = GetComponent<Button>();

                    if(_button == null)
                    {
                        Debug.Log($"Component '{nameof(UnityEngine.UI.Button)}' doesn't exist", Debug.LogType.Error);
                    }
                }

                return _button;
            }
        }

        public string Name
        {
            get
            {
                return _name.text;
            }

            set
            {
                _name.text = value;
            }
        }
    }
}
