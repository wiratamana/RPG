using UnityEngine;
using System.Collections.Generic;

namespace Tamana.AI
{
    public abstract class AI_Brain : ScriptableObject
    {
        protected Data data;
        public readonly Prop Prop = new Prop();

        private bool isInitialized = false;
        public Unit_AI Unit { protected set; get; }

        public virtual void Initialize(Unit_AI unit)
        {
            if(isInitialized == true)
            {
                Debug.Log($"You cannot initialize {nameof(AI_Brain)} twice.");
                return;
            }

            Unit = unit;
            data = new Data(Unit);
            isInitialized = true;
        }

        public abstract void Update();
    }
}