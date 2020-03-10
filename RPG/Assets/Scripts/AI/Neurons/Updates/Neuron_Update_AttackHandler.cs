using UnityEngine;
using System.Collections;

namespace Tamana.AI
{
    public class Neuron_Update_AttackHandler : AI_Neuron_Update
    {
        private readonly string stateName;
        private readonly float cooldown;
        private readonly float minimumRangeToAttack;
        private float currentDistanceToPlayer;
        private float waitingTime;

        public Neuron_Update_AttackHandler(AI_Brain brain, string stateName, float cooldown, 
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
                var playAnimation = new Neuron_Trigger_PlayAnimation(brain, stateName);
                playAnimation.Execute();
                playAnimation.StopNeuron();

                waitingTime = cooldown;
            }
        }
    }
}
