using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class AI_Neuron_AttackHandler : AI_Neuron_Update
    {
        private readonly string stateName;
        private readonly float cooldown;
        private readonly float minimumRangeToAttack;
        private float currentDistanceToPlayer;
        private float waitingTime;

        public AI_Neuron_AttackHandler(AI_Brain brain, string stateName, float cooldown, 
            float minimumRangeToAttack) : base(brain)
        {
            this.stateName = stateName;
            this.cooldown = cooldown;
            this.minimumRangeToAttack = minimumRangeToAttack;

            currentDistanceToPlayer = float.MaxValue;
            waitingTime = this.cooldown;
        }

        public void UpdateDistanceToPlayer(float distanceToPlayer)
        {
            currentDistanceToPlayer = distanceToPlayer;
        }

        public override void Update()
        {          
            waitingTime = Mathf.Max(0.0f, waitingTime - Time.deltaTime);

            if (currentDistanceToPlayer > minimumRangeToAttack)
            {
                return;
            }

            if (waitingTime == 0.0f)
            {
                var playAnimation = new AI_Neuron_PlayAnimation(brain, stateName);
                playAnimation.Execute();
                playAnimation.StopNeuron();

                waitingTime = cooldown;
            }
        }
    }
}
