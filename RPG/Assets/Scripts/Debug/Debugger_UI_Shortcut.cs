using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Debugger_UI_Shortcut : UI_Shortcut
    {
        public Transform Prefab { get; set; }

        private void Awake()
        {
            Button.onClick.AddListener(InstantiatePrefab);
        }

        private void InstantiatePrefab()
        {
            Instantiate(Prefab);
        }
    }
}
