using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraCombatCollisionHandler : MonoBehaviour
    {
        private TPC_CameraHandler cameraHandler;
        public TPC_CameraHandler CameraHandler => this.GetAndAssignComponent(ref cameraHandler);

        private void Update()
        {
            var camData = CameraHandler.CameraCombatHandler.CameraCombatData;

            var layer = LayerMask.GetMask(LayerManager.LAYER_ENVIRONMENT);
            Physics.Linecast(camData.CameraLookPosition, camData.CameraPosition, out RaycastHit hitInfo, layer);

            GameManager.MainCameraTransform.transform.rotation = camData.CameraRotation;

            if(hitInfo.transform != null && hitInfo.transform.tag != TagManager.TAG_NOT_COLLIDE_WITH_CAMERA)
            {
                var camPos = GameManager.MainCameraTransform.transform.position;
                var dirToHitInfoPoint = (GameManager.PlayerTransform.position - hitInfo.point).normalized;
                var camLookDir = (camData.CameraLookPosition - hitInfo.point).normalized;
                var dot = Vector3.Dot(dirToHitInfoPoint, camLookDir);

                camPos = Vector3.Lerp(camPos, hitInfo.point, 10 * Time.deltaTime);
                GameManager.MainCameraTransform.transform.position = camPos;

                if(dot < 0.5f)
                {
                    GameManager.MainCameraTransform.transform.position = camData.CameraPosition;
                }
                else
                {   
                    GameManager.MainCameraTransform.transform.position = camPos;
                }
            }
            else
            {
                GameManager.MainCameraTransform.transform.position = camData.CameraPosition;
            }
        }
    }
}
