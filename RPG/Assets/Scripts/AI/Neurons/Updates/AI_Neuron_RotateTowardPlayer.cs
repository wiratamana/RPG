using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AI_Neuron_RotateTowardPlayer : AI_Neuron_Update
    {
        private readonly float rotationSpeed;

        public AI_Neuron_RotateTowardPlayer(AI_Brain brain, float rotationSpeed) : base(brain)
        {
            this.rotationSpeed = rotationSpeed;
        }

        public override void Update()
        {
            var playerPos = GameManager.Player.position;

            var myTransform = brain.AI.transform;
            var myPosition = myTransform.position;
            var myRotation = myTransform.rotation;

            var dirToPlayer = playerPos - myPosition;
            dirToPlayer.y = 0;
            var lookRotation = Quaternion.LookRotation(dirToPlayer.normalized, Vector3.up);

            myTransform.rotation = Quaternion.Slerp(myRotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
