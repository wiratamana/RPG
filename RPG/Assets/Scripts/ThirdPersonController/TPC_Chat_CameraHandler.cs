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

            GameManager.MainCameraTransform.SetParent(null);

            GameManager.MainCameraTransform.position = GetCameraPosition(30, out Vector3 dir);
            GameManager.MainCameraTransform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }

        public void ActivateTarget(Unit_AI ai)
        {
            this.ai = ai;

            GameManager.MainCameraTransform.SetParent(null);

            GameManager.MainCameraTransform.position = GetCameraPosition(150, out Vector3 dir);
            GameManager.MainCameraTransform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }

        public void Deactivate()
        {
            ResetCameraPosition(out Quaternion rot);
            CameraHandler.CameraMovement.SetRotation(rot);
            GameManager.MainCameraTransform.SetParent(CameraHandler.CameraLookPoint);

            ai = null;
        }

        private Vector3 GetPositionBetween2Head(out Vector3 dir2ai)
        {
            var playerHead = GameManager.Player.BodyTransform.Head.position;
            var aiHead = ai.BodyTransform.Head.position;
            var halfDistance = Vector3.Distance(playerHead, aiHead) * 0.5f;
            dir2ai = Vector3.Normalize(aiHead - playerHead);
            var position = playerHead + (dir2ai * halfDistance);

            return position;
        }

        private Vector3 GetCameraPosition(float angle, out Vector3 dir2mid)
        {
            var length = 2.0f;
            var layer = LayerMask.GetMask(LayerManager.LAYER_ENVIRONMENT);
            var posBetween2Head = GetPositionBetween2Head(out Vector3 dir);

            VectorHelper.FastNormalize(Quaternion.AngleAxis(-angle, Vector3.up) * dir, out Vector3 left);
            VectorHelper.Add(posBetween2Head, left * length, out Vector3 leftCampos);
            Physics.Linecast(posBetween2Head, leftCampos, out RaycastHit hit, layer);

            float leftHitDistance;
            if (hit.transform == null)
            {
                dir2mid = -left;
                return leftCampos;
            }
            else
            {
                VectorHelper.FastDistance(posBetween2Head, hit.point, out leftHitDistance);
            }

            var right = Quaternion.AngleAxis(angle, Vector3.up) * dir;
            VectorHelper.FastNormalize(ref right);
            var rightCampos = posBetween2Head + (right * length);
            Physics.Linecast(posBetween2Head, rightCampos, out hit, layer);

            if(hit.transform == null)
            {
                VectorHelper.FastNormalizeDirection(posBetween2Head, rightCampos, out dir2mid);
                return rightCampos;
            }
            else
            {
                VectorHelper.FastDistance(posBetween2Head, hit.point, out float rightHitDistance);
                if (rightHitDistance > leftHitDistance)
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

        private void ResetCameraPosition(out Quaternion rotation)
        {
            var spinePos = CameraHandler.TPC.UnitPlayer.BodyTransform.Spine.position;
            var camPos = GameManager.MainCameraTransform.position;
            var midPos = GetPositionBetween2Head(out _);
            VectorHelper.FastNormalizeDirection(camPos, midPos, out Vector3 dir);
            VectorHelper.FastDistance(spinePos, CameraHandler.CameraDefaultPositionTransform.position, out float dis);
            VectorHelper.Mul(dir, dis, out dir);
            VectorHelper.Add(spinePos, dir, out camPos);
            camPos.y += 1.0f;
            VectorHelper.FastNormalizeDirection(spinePos, camPos, out dir);
            rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
    }
}