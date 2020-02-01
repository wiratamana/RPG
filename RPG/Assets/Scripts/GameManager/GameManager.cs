﻿using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Tamana
{
    public sealed class GameManager : SingletonMonobehaviour<GameManager>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            var go = new GameObject(nameof(GameManager));
            go.AddComponent<GameManager>();
            DontDestroyOnLoad(go);
        }

        private static Transform _player;
        public static Transform Player
        {
            get
            {
                if(_player == null)
                {
                    var gameObjectWithPlayerTag = GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER);
                    if(gameObjectWithPlayerTag == null)
                    {
                        Debug.Log($"Cannot find gameObject with '{TagManager.TAG_PLAYER}' tag.", Debug.LogType.ForceQuit);
                        return null;
                    }

                    _player = gameObjectWithPlayerTag.transform;
                }

                return _player;
            }
        }

        private static Transform _mainCamera;
        public static Transform MainCamera
        {
            get
            {
                if (_mainCamera == null)
                {
                    var gameObjectWithMainCameraTag = GameObject.FindGameObjectWithTag(TagManager.TAG_MAIN_CAMERA);
                    if (gameObjectWithMainCameraTag == null)
                    {
                        Debug.Log($"Cannot find gameObject with '{TagManager.TAG_MAIN_CAMERA}' tag.", Debug.LogType.ForceQuit);
                    }

                    _mainCamera = gameObjectWithMainCameraTag.transform;
                }

                return _mainCamera;
            }
        }
    }
}