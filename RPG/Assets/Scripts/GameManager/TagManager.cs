﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public sealed class TagManager : SingletonMonobehaviour<TagManager>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void InstantiateMyself()
        {
            var go = new GameObject(nameof(TagManager));
            var component = go.AddComponent<TagManager>();

            DontDestroyOnLoad(go);
        }

        public const string TAG_PLAYER = "Player";
        public const string TAG_PLAYER_CAMERA_LOOK_POINT = "PlayerCameraLookPoint";
        public const string TAG_MAIN_CAMERA = "MainCamera";
    }
}