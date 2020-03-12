using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Tamana.AI.Neuron;

namespace Tamana.AI
{
    public enum AIState
    {
        Idle,
        Chase,
        Return,
        Attack,
        Dodge,
        Holster,
        Equip
    }

    [CreateAssetMenu(fileName = "Good AI", menuName = "Create/Brain/Good AI")]
    public class Brain_Enemy_Good : AI_Brain
    {
        private float hostileAreaRadius = 10.0f;
        private float minimumDistanceToIdlePosition = 0.5f;
        private float maximumDistanceToIdlePosition = 35.0f;
        private Vector3 idlePosition;
        private Stack<Vector3> trails = new Stack<Vector3>();
        private DataPacket packet;

        public override void Initialize(Unit_AI_Hostile unit)
        {
            base.Initialize(unit);

            idlePosition = unit.transform.position;
            Unit.PF.nodeParent = PF_Master.Instance.GetNearestNode(idlePosition);
            packet = new DataPacket(AIState.Idle, Unit.UnitAnimator.Params);
        }

        public override void Update()
        {
            Benchmarker.Start();
            var PlayerData = new DataPlayer(Unit, GameManager.Player);
            var MyData = new DataMyself(Unit, PlayerData.Position, idlePosition);
            var myRotation = Unit.transform.rotation;
            var distanceToNodeParent = float.MaxValue;
            var velocity = 5 * Time.deltaTime;
            var distanceStop = 1.6f;

            if (packet.IsAlert == false)
            {
                packet.IsAlert = new IsPlayerInsideHostileArea(MyData.DistanceFromPlayerPositionToIdlePosition
                    , hostileAreaRadius).Result;
            }
            else
            {
                UpdateNode(MyData.CurrentPosition, ref distanceToNodeParent);

                if (packet.State == AIState.Return || new IsDistanceIsFromMyPositionToIdlePositionIsGreaterThan(ref MyData, maximumDistanceToIdlePosition).Result)
                {
                    new DoReturnToIdlePosition(ref packet, ref myRotation, MyData.CurrentPosition, idlePosition, velocity, velocity
                        , MyData.DistanceToIdlePosition, minimumDistanceToIdlePosition, Unit.PF.nodeParent, distanceToNodeParent);
                }
                else if (packet.State == AIState.Idle || packet.State == AIState.Chase)
                {
                    new DoMoveTowardPlayerPosition(ref packet, ref myRotation, MyData.DirectionTowardPlayer
                        , velocity, velocity, PlayerData.Distance, distanceStop);
                }                
            }

            Unit.transform.rotation = myRotation;
            Benchmarker.Stop($"State = {packet.State} | IsAlert = {packet.IsAlert}");
        }

        private void UpdateNode(Vector3 myPosition, ref float distanceToNodeParent)
        {
            distanceToNodeParent = (myPosition - Unit.PF.nodeParent.transform.position).sqrMagnitude;

            foreach(var i in Unit.PF.nodeParent.neighbours)
            {
                if(i == null)
                {
                    continue;
                }

                var distance = (myPosition - i.transform.position).sqrMagnitude;
                if(distance < distanceToNodeParent)
                {
                    Unit.PF.nodeParent = i;
                    distanceToNodeParent = distance;
                }
            }

            distanceToNodeParent = Mathf.Sqrt(distanceToNodeParent);
        }
    }
}
