using UnityEngine;
using System.Collections.Generic;

namespace Tamana
{
    [CreateAssetMenu(fileName = "New Brain", menuName = "Create New Brain")]
    public abstract class AI_Brain : ScriptableObject
    {
        private const string ADD_NEURON = "ADD";
        private const string REMOVE_NEURON = "REMOVE";

        private bool isInitialized = false;
        public AI_Enemy_Base AI { protected set; get; }

        protected readonly Dictionary<string, AI_Neuron> neuronsDic = new Dictionary<string, AI_Neuron>();
        private readonly Queue<(string, AI_Neuron)> queue = new Queue<(string, AI_Neuron)>();
        
        protected AI_Neuron_PlayerDetector playerDetector;
        public abstract AI_Neuron_PlayerDetector PlayerDetector { get; }

        protected AI_Neuron_RotateTowardPlayer rotateTowardPlayer;
        public abstract AI_Neuron_RotateTowardPlayer RotateTowardPlayer { get; }

        public virtual void Init(AI_Enemy_Base ai)
        {
            if(isInitialized == true)
            {
                Debug.Log($"You cannot initialize {nameof(AI_Brain)} twice.");
                return;
            }
        
            isInitialized = true;
            AI = ai;
        }

        public abstract void Update();

        protected void UpdateNeuron()
        {
            while (queue.Count > 0)
            {
                var neuron = queue.Dequeue();

                var dictionaryKey = neuron.Item2.GetType().Name;
                if (neuron.Item1 == ADD_NEURON)
                {
                    if (neuronsDic.ContainsKey(dictionaryKey) == true)
                    {
                        Debug.Log($"Unable to insert neuron with name '{dictionaryKey}'!", Debug.LogType.Error);
                        continue;
                    }

                    neuronsDic.Add(dictionaryKey, neuron.Item2);
                    Debug.Log($"Neuron with name '{dictionaryKey}' was added!");
                }

                else if (neuron.Item1 == REMOVE_NEURON)
                {
                    if (neuronsDic.ContainsKey(dictionaryKey) == false)
                    {
                        Debug.Log($"Neuron with name '{dictionaryKey}' is not exist!", Debug.LogType.Error);
                        return;
                    }

                    neuronsDic.Remove(dictionaryKey);
                    Debug.Log($"Neuron with name '{dictionaryKey}' was removed!");
                }
            }

            foreach (var neuron in neuronsDic)
            {
                neuron.Value.Update();
            }
        }

        public void AddNeuron(AI_Neuron neuron)
        {
            queue.Enqueue((ADD_NEURON, neuron));
        }

        public void RemoveNeuron(AI_Neuron neuron)
        {            
            queue.Enqueue((REMOVE_NEURON, neuron));
        }
    }
}