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

        private Unit_AI ai;

        private void OnValidate()
        {
            this.LogErrorIfComponentIsNull(CameraLookPlayer);
            this.LogErrorIfComponentIsNull(CameraLookTarget);
        }

        public void ActivatePlayer(Unit_AI ai)
        {
            this.ai = ai;
            GameManager.MainCameraTransform.position = GetCameraPosition(30, out Vector3 dir);
            GameManager.MainCameraTransform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }

        public void ActivateTarget(Unit_AI ai)
        {
            this.ai = ai;
            GameManager.MainCameraTransform.position = GetCameraPosition(150, out Vector3 dir);
            GameManager.MainCameraTransform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }

        public void Deactivate()
        {
            ai = null;
        }

        private Vector3 GetPositionBetween2Head(out Vector3 dir2ai)
        {
            var playerHead = GameManager.Player.BodyTransform.Head.position;
            var aiHead = ai.BodyTransform.Head.position;
            var halfDistance = Vector3.Distance(playerHead, aiHead) * 0.5f;
            dir2ai = Vector3.Normalize(aiHead - playerHead);
            var position = playerHead + (dir2ai * halfDistance);

            Debug.Log(position);

            return position;
        }

        private Vector3 GetCameraPosition(float angle, out Vector3 dir2mid)
        {
            var length = 2.0f;
            var layer = LayerMask.GetMask(LayerManager.LAYER_ENVIRONMENT);
            var posBetween2Head = GetPositionBetween2Head(out Vector3 dir);
            var left = Vector3.Normalize(Quaternion.AngleAxis(-angle, Vector3.up) * dir);
            var leftCampos = posBetween2Head + (left * length);
            Physics.Linecast(posBetween2Head, leftCampos, out RaycastHit hit, layer);

            float leftHitDistance;
            if (hit.transform == null)
            {
                dir2mid = -left;
                return leftCampos;
            }
            else
            {
                leftHitDistance = Vector3.Distance(posBetween2Head, hit.point);
            }

            var right = Vector3.Normalize(Quaternion.AngleAxis(angle, Vector3.up) * dir);
            var rightCampos = posBetween2Head + (right * length);
            Physics.Linecast(posBetween2Head, leftCampos, out hit, layer);

            if(hit.transform == null)
            {
                dir2mid = left;
                return rightCampos;
            }
            else
            {
                var rightHitDistance = Vector3.Distance(posBetween2Head, hit.point);
                if(rightHitDistance > leftHitDistance)
                {
                    dir2mid = left;
                    return rightCampos;
                }
                else
                {
                    dir2mid = -left;
                    return leftCampos;
                }
            }
        }
    }
}