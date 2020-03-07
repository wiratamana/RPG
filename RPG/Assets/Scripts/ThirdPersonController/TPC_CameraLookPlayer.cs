using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraLookPlayer : MonoBehaviour
    {
        private TPC_CameraHandler cameraHandler;
        public TPC_CameraHandler CameraHandler => this.GetAndAssignComponent(ref cameraHandler);

        private Transform playerSpine;

        private void OnValidate()
        {
            playerSpine = CameraHandler.TPC.UnitPlayer.BodyTransform.Spine;
        }

        // Update is called once per frame
        void Update()
        {
            var playerPosition = playerSpine.position;
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
