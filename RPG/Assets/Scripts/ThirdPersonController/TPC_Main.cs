using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_Main : MonoBehaviour
    {
        private Unit_Player unit;
        private TPC_Movement playerMovement;
        private TPC_CameraHandler cameraHandler;

        public Unit_Player UnitPlayer => this.GetAndAssignComponent(ref unit);
        public TPC_Movement PlayerMovement => this.GetOrAddAndAssignComponent(ref playerMovement);
        public TPC_CameraHandler CameraHandler => this.GetOrAddAndAssignComponent(ref cameraHandler);

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(PlayerMovement);
            this.LogErrorIfComponentIsNull(CameraHandler);
        }

        private void Awake()
        {
            this.LogErrorIfComponentIsNull(PlayerMovement);
            this.LogErrorIfComponentIsNull(CameraHandler);
        }
    }
}
