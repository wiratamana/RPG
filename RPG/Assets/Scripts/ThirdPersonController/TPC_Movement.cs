using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public class TPC_Movement : MonoBehaviour
    {
        private TPC_Main tpc;
        public TPC_Main TPC => this.GetAndAssignComponent(ref tpc);

        private TPC_Movement_Normal normal;
        private TPC_Movement_Strafe strafe;

        public TPC_Movement_Normal Normal => this.GetOrAddAndAssignComponent(ref normal);
        public TPC_Movement_Strafe Strafe => this.GetOrAddAndAssignComponent(ref strafe);

        public EventManager<MovementType> OnMovementTypeChanged { get; } = new EventManager<MovementType>();

        public MovementType MovementType { get; private set; }

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(Normal);
            this.LogErrorIfComponentIsNull(Strafe);

            SetMovementType(MovementType.Normal);
        }

        private void Awake()
        {
            this.LogErrorIfComponentIsNull(Normal);
            this.LogErrorIfComponentIsNull(Strafe);

            SetMovementType(MovementType.Normal);

            TPC.UnitPlayer.EnemyCatcher.OnEnemyCatched.AddListener(SetMovementTypeToStrafe);
            TPC.UnitPlayer.EnemyCatcher.OnCatchedEnemyReleased.AddListener(SetMovementTypeToNormal);
        }

        private void SetMovementType(MovementType movementType)
        {
            if(MovementType == movementType)
            {
                return;
            }

            switch (movementType)
            {
                case MovementType.Normal:
                    Strafe.enabled = false;
                    Normal.enabled = true;
                    TPC.UnitPlayer.UnitAnimator.DisableStrafing();
                    break;

                case MovementType.Strafe:
                    Normal.enabled = false;
                    Strafe.enabled = true;
                    TPC.UnitPlayer.UnitAnimator.EnableStrafing();
                    break;
            }

            MovementType = movementType;
            OnMovementTypeChanged.Invoke(movementType);
        }

        private void SetMovementTypeToStrafe(Unit_AI enemy)
        {
            SetMovementType(MovementType.Strafe);
        }

        private void SetMovementTypeToNormal()
        {
            SetMovementType(MovementType.Normal);
        }
    }
}