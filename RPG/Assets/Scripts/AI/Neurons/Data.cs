using UnityEngine;
using System.Collections.Generic;

namespace Tamana.AI
{
    public class Data
    {
        public readonly float HostileAreaRadius = 5.0f;
        public readonly float MinimumDistanceToIdlePosition = 0.5f;
        public readonly float MaximumDistanceToIdlePosition = 35.0f;
        public readonly float DistanceStop = 1.6f;
        public readonly Unit_Animator_Params Params;

        public Unit_AI_Hostile Myself { get; private set; }
        public Unit_Player Player { get; private set; }
        public Unit_Animator_Params PlayerParams { get; private set; }
        private PF_Unit unitpf;

        public IReadOnlyCollection<PF_Node> NeighbourNodes => unitpf.nodeParent.neighbours;
        public Vector3 PlayerPosition { get; private set; }
        public Vector3 PlayerForward { get; private set; }
        public float DistanceToPlayer { get; private set; }
        public Vector3 MyPosition { get; private set; }
        public Vector3 MyForward { get; private set; }
        public float DistanceFromPlayerToIdlePosition { get; private set; }
        public float DistanceToIdlePosition { get; private set; }
        public float MovementVelocity { get; private set; }
        public float RotationSpeed { get; private set; }
        public Vector3 NodeParentPosition { get; private set; }
        public float DistanceToNodeParent { get; private set; }
        public Vector3 IdlePosition { get; private set; }
        public Vector3 DirectionTowardPlayer { get; private set; }
        public float DotProductTowardPlayer { get; private set; }
        public bool isPlayerOnAttackAnimationStarted { private set; get; }

        public Vector3 NextDestination;
        public Quaternion MyRotation;
        public bool IsAlert;
        public AIState State;

        public Data(Unit_AI_Hostile myself)
        {
            Myself = myself;
            Player = GameManager.Player;
            PlayerParams = Player.UnitAnimator.Params;
            unitpf = myself.PF;
            IdlePosition = myself.transform.position;

            Player.CombatHandler.AttackHandler.OnAttackAnimationStarted.AddListener(SetOnAttackAnimationStartedTrue);
            Player.CombatHandler.AttackHandler.OnConsecutiveAttack.AddListener(SetOnAttackAnimationStartedTrue);

            Params = myself.UnitAnimator.Params;
        }

        public void Update()
        {
            PlayerPosition = Player.transform.position;
            PlayerForward = Player.transform.forward;
            MyPosition = Myself.transform.position;
            MyForward = Myself.transform.forward;
            DotProductTowardPlayer = Vector3.Dot(MyForward, PlayerForward);

            DistanceToPlayer = Vector3.Distance(PlayerPosition, MyPosition);
            DistanceFromPlayerToIdlePosition = Vector3.Distance(PlayerPosition, IdlePosition);
            DistanceToIdlePosition = Vector3.Distance(MyPosition, IdlePosition);
            DirectionTowardPlayer = Vector3.Normalize(PlayerPosition - MyPosition);

            var deltaTime = Time.deltaTime;
            MovementVelocity = 5.0f * deltaTime;
            RotationSpeed = 5.0f * deltaTime;

            NodeParentPosition = unitpf.nodeParent.transform.position;
            DistanceToNodeParent = Vector3.Distance(MyPosition, NodeParentPosition);
            MyRotation = Myself.transform.rotation;
        }

        public void UpdateNode()
        {
            DistanceToNodeParent = (MyPosition - NodeParentPosition).sqrMagnitude;

            foreach (var i in unitpf.nodeParent.neighbours)
            {
                if (i == null)
                {
                    continue;
                }

                var distance = (MyPosition - i.transform.position).sqrMagnitude;
                if (distance < DistanceToNodeParent)
                {
                    unitpf.nodeParent = i;
                    DistanceToNodeParent = distance;
                }
            }

            DistanceToNodeParent = Mathf.Sqrt(DistanceToNodeParent);
        }

        public void ResetOnAttackAnimationStarted()
        {
            isPlayerOnAttackAnimationStarted = false;
        }

        private void SetOnAttackAnimationStartedTrue()
        {
            isPlayerOnAttackAnimationStarted = true;
        }
    }
}
