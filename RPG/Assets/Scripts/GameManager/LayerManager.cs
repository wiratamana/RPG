using UnityEngine;
using System.Collections;

namespace Tamana
{
    public sealed class LayerManager : SingletonMonobehaviour<LayerManager>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            var go = new GameObject(nameof(LayerManager));
            var component = go.AddComponent<LayerManager>();

            DontDestroyOnLoad(go);
        }

        public const string LAYER_PLAYER_MENU_PORTRAIT = "PlayerMenuPortrait";
        public const string LAYER_PLAYER = "Player";
        public const string LAYER_ITEM_PROJECTION = "ItemProjection";
    }

}
