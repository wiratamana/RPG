using UnityEngine;
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
            Debug.Log($"RuntimeInitializeOnLoadMethod - {nameof(GameManager)}");

            var go = new GameObject(nameof(GameManager));
            go.AddComponent<GameManager>();
            DontDestroyOnLoad(go);
        }

        private const int FULLHD = FULLHD_HEIGHT * FULLHD_WIDTH;
        public const int FULLHD_WIDTH = 1920;
        public const int FULLHD_HEIGHT = 1080;
        public static bool IsScreenResolutionGreaterOrEqualThanFHD => Screen.width * Screen.height >= FULLHD;

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

        public static Unit_Status PlayerStatus => Player.Status;

        public static Camera MainCamera => Player.TPC.CameraHandler.MainCamera;
        public static Transform MainCameraTransform => Player.TPC.CameraHandler.MainCamera.transform;

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
                    Resources.UnloadAsset(materialFromResources);
                }

                return itemMaterial;
            }
        }
    }
}
