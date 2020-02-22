using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraLookPlayer : SingletonMonobehaviour<TPC_CameraLookPlayer>
    {
        private Transform _playerCameraLookPoint;
        public Transform PlayerCameraLookPoint
        {
            get
            {
                if(_playerCameraLookPoint == null)
                {
                    var gameObjectWithPlayerTag = GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER_CAMERA_LOOK_POINT);
                    if(gameObjectWithPlayerTag == null)
                    {
                        Debug.Log($"GameObject with tag '{TagManager.TAG_PLAYER_CAMERA_LOOK_POINT}' doesn't exist", Debug.LogType.ForceQuit);
                        return null;
                    }

                    _playerCameraLookPoint = gameObjectWithPlayerTag.transform;
                }

                return _playerCameraLookPoint;
            }
        }

        // Update is called once per frame
        void Update()
        {
            var playerPosition = PlayerCameraLookPoint.transform.position;
            var directionToPlayer = (playerPosition - transform.position).normalized;

            var lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 4 * Time.deltaTime);
        }
    }
}
