using UnityEngine;
using System.Collections.Generic;

namespace Tamana.AI
{
    public abstract class AI_Brain : ScriptableObject
    {
        private bool isInitialized = false;
        public Unit_AI Unit { protected set; get; }

        public virtual void Initialize(Unit_AI unit)
        {
            if(isInitialized == true)
            {
                Debug.Log($"You cannot initialize {nameof(AI_Brain)} twice.");
                return;
            }
        
            isInitialized = true;
            Unit = unit;
        }

        public abstract void Update();
    }
}