using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AI_Neuron_PlayerDetector : AI_Neuron
    {
        public EventManager OnPlayerEnteredHostileRange { private set; get; } = new EventManager();
        public EventManager OnPlayerExitedHostileRange { private set; get; } = new EventManager();

        private readonly float hostileRangeEnter;
        private readonly float hostileRangeExit;
        private bool isInsideHostileRange = false;

        public AI_Neuron_PlayerDetector(AI_Brain brain, float hostileRangeEnter, float hostileRangeExit) : base(brain)
        {
            this.hostileRangeEnter = hostileRangeEnter;
            this.hostileRangeExit = hostileRangeExit;
        }

        public override void Update()
        {
            var playerDistance = Vector3.Distance(brain.AI.transform.position, GameManager.Player.position);

            if(isInsideHostileRange == false && playerDistance < hostileRangeEnter)
            {
                isInsideHostileRange = true;
                OnPlayerEnteredHostileRange.Invoke();
            }
            
            else if(isInsideHostileRange == true && playerDistance > hostileRangeExit)
            {
                isInsideHostileRange = false;
                OnPlayerExitedHostileRange.Invoke();
            }
        }
    }
}
