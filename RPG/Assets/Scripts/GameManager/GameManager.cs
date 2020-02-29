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

        private static Transform _playerTransform;
        public static Transform PlayerTransform
        {
            get
            {
                if(_playerTransform == null)
                {
                    var gameObjectWithPlayerTag = GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER);
                    if(gameObjectWithPlayerTag == null)
                    {
                        Debug.Log($"Cannot find gameObject with '{TagManager.TAG_PLAYER}' tag.", Debug.LogType.ForceQuit);
                        return null;
                    }

                    _playerTransform = gameObjectWithPlayerTag.transform;
                }

                return _playerTransform;
            }
        }

        public static Status_Player PlayerStatus => Player.Status;

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

        private static Unit_Player player;
        public static Unit_Player Player
        {
            get
            {
                if(player == null)
                {
                    player = PlayerTransform.GetComponent<Unit_Player>();
                }

                return player;
            }
        }

        private static Material itemMaterial;
        public static Material ItemMaterial
        {
            get
            {
                if(itemMaterial == null)
                {
                    var loadPath = "ItemShader/ItemMaterial";
                    var materialFromResources = Resources.Load<Material>(loadPath);

                    if(materialFromResources == null)
                    {
                        Debug.Log($"Failed to load '{nameof(Material)}' from Resources folder at path '{loadPath}'", Debug.LogType.ForceQuit);
                        return null;
                    }

                    itemMaterial = new Material(materialFromResources);
                }

                return itemMaterial;
            }
        }
    }
}
