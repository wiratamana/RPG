using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public sealed class TagManager : SingletonMonobehaviour<TagManager>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void CreateInstance()
        {
            Debug.Log($"RuntimeInitializeOnLoadMethod - {nameof(TagManager)}");

            var go = new GameObject(nameof(TagManager));
            var component = go.AddComponent<TagManager>();

            DontDestroyOnLoad(go);
        }

        public const string TAG_PLAYER = "Player";
        public const string TAG_PLAYER_CAMERA_LOOK_POINT = "PlayerCameraLookPoint";
        public const string TAG_MAIN_CAMERA = "MainCamera";
        public const string TAG_UI_MANAGER = "UIManager";
        public const string TAG_PLAYER_MENU_PORTRAIT = "PlayerMenuPortrait";
        public const string TAG_PLAYER_MENU_PORTRAIT_CAMERA = "PlayerMenuPortraitCamera";
        public const string TAG_FLOATING_ITEM_PARENT = "FloatingItemParent";
    }
}
