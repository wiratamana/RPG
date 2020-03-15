using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_Chat_CameraHandler : MonoBehaviour
    {
        private TPC_CameraHandler cameraHandler;
        private TPC_Chat_CameraLookPlayer cameraLookPlayer;
        private TPC_Chat_CameraLookTarget cameraLookTarget;

        public TPC_CameraHandler CameraHandler => this.GetAndAssignComponent(ref cameraHandler);
        public TPC_Chat_CameraLookPlayer CameraLookPlayer => this.GetOrAddAndAssignComponent(ref cameraLookPlayer);
        public TPC_Chat_CameraLookTarget CameraLookTarget => this.GetOrAddAndAssignComponent(ref cameraLookTarget);

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(CameraLookPlayer);
            this.LogErrorIfComponentIsNull(CameraLookTarget);
        }
    }
}