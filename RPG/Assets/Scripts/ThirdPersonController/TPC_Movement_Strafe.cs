using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class TPC_Movement_Strafe : MonoBehaviour
    {
        private TPC_Movement movement;
        public TPC_Movement Movement => this.GetAndAssignComponent(ref movement);
        private TPC_RotateTowardEnemyHandler rotateTowardEnemyHandler;
        public TPC_RotateTowardEnemyHandler RotateTowardEnemyHandler => this.GetOrAddAndAssignComponent(ref rotateTowardEnemyHandler);

        private Unit_Animator_Params param;
        private float acceleration = 5.0f;
       
        private const float MAX_VAL = 1.0f;
        private void OnValidate()
        {
            param = Movement.TPC.UnitPlayer.UnitAnimator.Params;
            this.LogErrorIfComponentIsNull(RotateTowardEnemyHandler);
        }

        private void Update()
        {
            param = Movement.TPC.UnitPlayer.UnitAnimator.Params;
            this.LogErrorIfComponentIsNull(RotateTowardEnemyHandler);

            var strafeHorizontal = param.StrafeHorizontal;
            var strafeVertical = param.StrafeVertical;
            var deltaTime = Time.deltaTime;
            var acc = acceleration * deltaTime;

            var w = Input.GetKey(KeyCode.W);
            var s = Input.GetKey(KeyCode.S);
            var a = Input.GetKey(KeyCode.A);
            var d = Input.GetKey(KeyCode.D);

            if(w == true)
            {
                strafeVertical = Mathf.Min(MAX_VAL, strafeVertical + acc);
            }
            else if(s == true)
            {
                strafeVertical = Mathf.Max(-MAX_VAL, strafeVertical - acc);
            }
            else
            {
                strafeVertical = Mathf.MoveTowards(strafeVertical, 0.0f, acc);
            }

            if (d == true)
            {
                strafeHorizontal = Mathf.Min(MAX_VAL, strafeHorizontal + acc);
            }
            else if (a == true)
            {
                strafeHorizontal = Mathf.Max(-MAX_VAL, strafeHorizontal - acc);
            }
            else
            {
                strafeHorizontal = Mathf.MoveTowards(strafeHorizontal, 0.0f, acc);
            }

            if(strafeHorizontal == MAX_VAL || strafeVertical == MAX_VAL)
            {
                Vector2 direction = new Vector2(strafeHorizontal, strafeVertical).normalized;

                param.StrafeHorizontal = direction.x;
                param.StrafeVertical = direction.y;
            }
            else
            {
                param.StrafeHorizontal = strafeHorizontal;
                param.StrafeVertical = strafeVertical;
            }
        }
    }
}
