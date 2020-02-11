using UnityEngine;
using System.Collections;

namespace Tamana
{
    public sealed class LayerManager : SingletonMonobehaviour<LayerManager>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void InstantiateMyself()
        {
            var go = new GameObject(nameof(LayerManager));
            var component = go.AddComponent<LayerManager>();

            DontDestroyOnLoad(go);
        }

        public const string LAYER_MENU_3D_PORTRAIT = "Menu3DPortrait";
    }

}
