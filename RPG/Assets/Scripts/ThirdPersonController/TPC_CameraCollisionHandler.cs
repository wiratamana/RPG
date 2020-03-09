using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraCollisionHandler : MonoBehaviour
    {
        private TPC_CameraHandler cameraHandler;
        public TPC_CameraHandler CameraHandler => this.GetAndAssignComponent(ref cameraHandler);

        private void Update()
        {
            var lookPosition = CameraHandler.CameraLookPlayer.CameraLookTargetTransform.position;
            var cameraPosition = GameManager.MainCameraTransform.position;
            var dirToCam = (cameraPosition - lookPosition).normalized;
            var layer = LayerMask.GetMask(LayerManager.LAYER_ENVIRONMENT);
            var distanceToCam = Vector3.Distance(lookPosition, CameraHandler.CameraDefaultPositionTransform.position);

            Physics.Raycast(lookPosition, dirToCam, out RaycastHit info, 20, layer);

            if(info.transform != null)
            {
                var distanceToHitPoint = Vector3.Distance(lookPosition, info.point);
                if(distanceToHitPoint > distanceToCam)
                {
                    UnityEngine.Debug.DrawLine(lookPosition, lookPosition + (dirToCam * distanceToCam), Color.green);

                    GameManager.MainCameraTransform.localPosition = Vector3.Lerp(GameManager.MainCameraTransform.localPosition,
                        new Vector3(0,0,CameraHandler.OffsetZ), 10 * Time.deltaTime);
                }
                else
                {
                    UnityEngine.Debug.DrawLine(lookPosition, info.point, Color.red);

                    GameManager.MainCameraTransform.position = Vector3.Lerp(GameManager.MainCameraTransform.position,
                        info.point, 10 * Time.deltaTime);
                }                
            }
            else
            {
                UnityEngine.Debug.DrawLine(lookPosition, lookPosition + (dirToCam * distanceToCam), Color.green);

                GameManager.MainCameraTransform.localPosition = Vector3.Lerp(GameManager.MainCameraTransform.localPosition,
                    new Vector3(0, 0, CameraHandler.OffsetZ), 10 * Time.deltaTime);
            }            
        }
    }
}
