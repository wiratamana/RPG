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

        [SerializeField] private float _offsetY;
        [SerializeField] private float _offsetZ;
        [SerializeField] private float _cameraLookHeight;

        public float OffsetY => _offsetY;
        public float OffsetZ => _offsetZ;
        public float CameraLookHeight => _cameraLookHeight;

        private void Update()
        {
            CameraHandler.CameraLookPoint.transform.position = Vector3.Lerp(CameraHandler.CameraLookPoint.transform.position, transform.position, 5 * Time.deltaTime);

            RotateCamera();                
        }

        private void RotateCamera()
        {
            var eulerAngle = CameraHandler.CameraLookPoint.eulerAngles;
            eulerAngle.x = Mathf.Clamp(eulerAngle.x + KeyboardController.MouseVertical, 0.0f, 75.0f);
            eulerAngle.y += KeyboardController.MouseHorizontal;

            CameraHandler.CameraLookPoint.transform.rotation = Quaternion.Euler(eulerAngle.x, eulerAngle.y, 0);
        }      
    }
}
