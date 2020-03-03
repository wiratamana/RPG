using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AI_Neuron_PlayerDetector : AI_Neuron_Update
    {
        public EventManager OnPlayerEnteredHostileRange { private set; get; } = new EventManager();
        public EventManager OnPlayerExitedHostileRange { private set; get; } = new EventManager();
        public EventManager<float> OnPlayerInsideHostileRange { private set; get; } = new EventManager<float>();

        private readonly float hostileRangeEnter;
        private readonly float hostileRangeExit;
        private bool isInsideHostileRange = false;
        public float DistanceToPlayer { private set; get; }

        public AI_Neuron_PlayerDetector(AI_Brain brain, float hostileRangeEnter, float hostileRangeExit) : base(brain)
        {
            this.hostileRangeEnter = hostileRangeEnter;
            this.hostileRangeExit = hostileRangeExit;
        }

        public override void Update()
        {
            DistanceToPlayer = Vector3.Distance(brain.AI.transform.position, GameManager.PlayerTransform.position);

            if(isInsideHostileRange == false && DistanceToPlayer < hostileRangeEnter)
            {
                isInsideHostileRange = true;
                OnPlayerEnteredHostileRange.Invoke();
            }
            
            else if(isInsideHostileRange == true)
            {
                if(DistanceToPlayer > hostileRangeExit)
                {
                    isInsideHostileRange = false;
                    OnPlayerExitedHostileRange.Invoke();
                }

                else
                {
                    OnPlayerInsideHostileRange.Invoke(DistanceToPlayer);
                }
            }
        }
    }
}
