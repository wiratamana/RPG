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

        public Unit_AI Myself { get; private set; }
        public Unit_Player Player { get; private set; }
        public Unit_Animator_Params PlayerParams { get; private set; }
        private PF_Unit unitpf;

        private Vector3 playerPosition;
        private Vector3 playerForward;
        private float distanceToPlayer;
        private Vector3 myPosition;
        private Vector3 myForward;
        private float distanceFromPlayerToIdlePosition;
        private float distanceToIdlePosition;
        private float movementVelocity;
        private float rotationSpeed;
        private Vector3 nodeParentPosition;
        private float distanceToNodeParent;
        private Vector3 idlePosition;
        private Vector3 directionTowardPlayer;
        private float dotProductTowardPlayer;
        private bool isPlayerOnAttackAnimationStarted;

        public IReadOnlyCollection<PF_Node> NeighbourNodes => unitpf.nodeParent.neighbours;
        public Vector3 PlayerPosition => playerPosition;
        public Vector3 PlayerForward => playerForward;
        public float DistanceToPlayer => distanceToPlayer;
        public Vector3 MyPosition => myPosition;
        public Vector3 MyForward => myForward;
        public float DistanceFromPlayerToIdlePosition => distanceFromPlayerToIdlePosition;
        public float DistanceToIdlePosition => distanceToIdlePosition;
        public float MovementVelocity => movementVelocity;
        public float RotationSpeed => rotationSpeed;
        public Vector3 NodeParentPosition => nodeParentPosition;
        public float DistanceToNodeParent => distanceToNodeParent;
        public Vector3 IdlePosition => idlePosition;
        public Vector3 DirectionTowardPlayer => directionTowardPlayer;
        public float DotProductTowardPlayer => dotProductTowardPlayer;
        public bool IsPlayerOnAttackAnimationStarted => isPlayerOnAttackAnimationStarted;

        public Vector3 NextDestination;
        public Quaternion MyRotation;
        public bool IsAlert;
        public AIState State;

        public Data(Unit_AI myself)
        {
            Myself = myself;
            Player = GameManager.Player;
            PlayerParams = Player.UnitAnimator.Params;
            unitpf = myself.PF;
            idlePosition = myself.transform.position;

            Player.CombatHandler.AttackHandler.OnAttackAnimationStarted.AddListener(SetOnAttackAnimationStartedTrue);
            Player.CombatHandler.AttackHandler.OnConsecutiveAttack.AddListener(SetOnAttackAnimationStartedTrue);

            Params = myself.UnitAnimator.Params;
        }

        public void Update()
        {
            playerPosition = Player.transform.position;
            playerForward = Player.transform.forward;
            myPosition = Myself.transform.position;
            myForward = Myself.transform.forward;
            dotProductTowardPlayer = Vector3.Dot(MyForward, PlayerForward);

            distanceToPlayer = Vector3.Distance(PlayerPosition, MyPosition);
            distanceFromPlayerToIdlePosition = Vector3.Distance(PlayerPosition, IdlePosition);
            distanceToIdlePosition = Vector3.Distance(MyPosition, IdlePosition);
            directionTowardPlayer = Vector3.Normalize(PlayerPosition - MyPosition);

            var deltaTime = Time.deltaTime;
            movementVelocity = 5.0f * deltaTime;
            rotationSpeed = 5.0f * deltaTime;

            nodeParentPosition = unitpf.nodeParent.transform.position;
            distanceToNodeParent = Vector3.Distance(MyPosition, NodeParentPosition);
            MyRotation = Myself.transform.rotation;
        }

        public void UpdateNode()
        {
            distanceToNodeParent = (MyPosition - NodeParentPosition).sqrMagnitude;

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
                    distanceToNodeParent = distance;
                }
            }

            distanceToNodeParent = Mathf.Sqrt(DistanceToNodeParent);
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
