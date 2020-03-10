using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Tamana.AI
{
    public class Neuron_Update_WaitForSeconds : AI_Neuron_Update
    {
        private readonly AI_Neuron afterWait;
        private readonly UnityAction afterWaitAction;
        private float waitForSeconds;

        public Neuron_Update_WaitForSeconds(AI_Brain brain, float waitForSeconds, AI_Neuron afterWait) : base(brain)
        {
            this.afterWait = afterWait;
            this.waitForSeconds = waitForSeconds;
        }

        public Neuron_Update_WaitForSeconds(AI_Brain brain, float waitForSeconds, UnityAction afterWaitAction) : base(brain)
        {
            this.afterWaitAction = afterWaitAction;
            this.waitForSeconds = waitForSeconds;
        }

        public override void Update()
        {
            waitForSeconds = Mathf.Max(0.0f, waitForSeconds - Time.deltaTime);
            if(waitForSeconds == 0)
            {
                if(afterWait != null)
                {
                    if (afterWait is AI_Neuron_Trigger)
                    {
                        (afterWait as AI_Neuron_Trigger).Execute();
                        afterWait.StopNeuron();
                    }

                    StopNeuron();
                }

                afterWaitAction?.Invoke();
            }
        }
    }
}
