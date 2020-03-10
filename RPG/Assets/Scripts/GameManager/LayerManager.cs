using UnityEngine;
using System.Collections;

namespace Tamana
{
    public sealed class LayerManager : SingletonMonobehaviour<LayerManager>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void CreateInstance()
        {
            Debug.Log($"RuntimeInitializeOnLoadMethod - {nameof(LayerManager)}");

            var go = new GameObject(nameof(LayerManager));
            var component = go.AddComponent<LayerManager>();

            DontDestroyOnLoad(go);
        }

        public const string LAYER_PLAYER_MENU_PORTRAIT = "PlayerMenuPortrait";
        public const string LAYER_PLAYER = "Player";
        public const string LAYER_ITEM_PROJECTION = "ItemProjection";
        public const string LAYER_ENEMY = "Enemy";
        public const string LAYER_ENVIRONMENT = "Environment";
    }

}
