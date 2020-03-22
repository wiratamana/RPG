using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Tamana
{
    public class TPC_CameraMovementManager : MonoBehaviour
    {
        private TPC_CameraHandler cameraHandler;
        public TPC_CameraHandler CameraHandler => this.GetAndAssignComponent(ref cameraHandler);

        private void Awake()
        {
            UI_Menu.OnBeforeOpen.AddListener(Disable);
            UI_Menu.OnAfterClose.AddListener(Enable);
        }

        private void Update()
        {
            CameraHandler.CameraLookPoint.transform.position = Vector3.Lerp(CameraHandler.CameraLookPoint.transform.position, 
                CameraHandler.TPC.UnitPlayer.BodyTransform.Spine.position, 5 * Time.deltaTime);

            RotateCamera();                
        }

        public void SetRotation(Quaternion rotation)
        {
            CameraHandler.CameraLookPoint.transform.rotation = rotation;
        }

        private void RotateCamera()
        {
            var eulerAngle = CameraHandler.CameraLookPoint.eulerAngles;
            if(eulerAngle.x > 180)
            {
                eulerAngle.x /= 360.0f;
            }

            eulerAngle.x = Mathf.Clamp(eulerAngle.x + KeyboardController.MouseVertical, 0.0f, 75.0f);
            eulerAngle.y += KeyboardController.MouseHorizontal;

            CameraHandler.CameraLookPoint.transform.rotation = Quaternion.Euler(eulerAngle.x, eulerAngle.y, 0);
        }

        private void Disable()
        {
            enabled = false;
        }

        private void Enable()
        {
            enabled = true;
        }
    }
}
