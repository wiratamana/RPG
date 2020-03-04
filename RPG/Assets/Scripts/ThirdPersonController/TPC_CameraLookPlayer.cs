using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraLookPlayer : MonoBehaviour
    {
        private TPC_CameraHandler cameraHandler;
        public TPC_CameraHandler CameraHandler => this.GetAndAssignComponent(ref cameraHandler);

        // Update is called once per frame
        void Update()
        {
            var playerPosition = transform.position;
            var directionToPlayer = (playerPosition - CameraHandler.MainCamera.transform.position).normalized;

            var lookRotation = Quaternion.LookRotation(directionToPlayer);
            CameraHandler.MainCamera.transform.rotation = Quaternion.Slerp(CameraHandler.MainCamera.transform.rotation, lookRotation, 4 * Time.deltaTime);
        }
    }
}
