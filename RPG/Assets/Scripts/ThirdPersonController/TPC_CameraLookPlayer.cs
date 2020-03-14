﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraLookPlayer : MonoBehaviour
    {
        private TPC_CameraHandler cameraHandler;
        public TPC_CameraHandler CameraHandler => this.GetAndAssignComponent(ref cameraHandler);

        public Transform CameraLookTargetTransform { get; private set; }

        private void OnValidate()
        {
            CameraLookTargetTransform = CameraHandler.TPC.UnitPlayer.BodyTransform.Spine;
        }

        private void Awake()
        {
            CameraLookTargetTransform = CameraHandler.TPC.UnitPlayer.BodyTransform.Spine;
        }

        // Update is called once per frame
        void Update()
        {
            var a = InputEvent.Instance;
            var b = GameManager.Instance;
            var c = TagManager.Instance;
            var d = LayerManager.Instance;

            var playerPosition = CameraLookTargetTransform.position;
            var directionToPlayer = (playerPosition - CameraHandler.MainCamera.transform.position).normalized;

            var lookRotation = Quaternion.LookRotation(directionToPlayer);
            CameraHandler.MainCamera.transform.rotation = Quaternion.Slerp(CameraHandler.MainCamera.transform.rotation, lookRotation, 4 * Time.deltaTime);
        }

        public void SetCameraLocalPositionToZero()
        {
            StartCoroutine(ReturnCameraLocalPositionToZero());
        }

        private IEnumerator ReturnCameraLocalPositionToZero()
        {
            var cameraTransform = CameraHandler.MainCamera.transform;
            var speed = 1.5f;
            var baseLocalPos = new Vector3(0, 0, CameraHandler.OffsetZ);

            while (true)
            {
                var camPos = cameraTransform.localPosition;
                camPos = Vector3.MoveTowards(camPos, baseLocalPos, speed * Time.deltaTime);
                cameraTransform.localPosition = camPos;

                if(camPos == baseLocalPos)
                {
                    yield break;
                }

                yield return null;
            }
        }
    }
}
