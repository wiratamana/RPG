using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public abstract class SingletonMonobehaviour<T> : MonoBehaviour where T : class
    {
        public static T Instance { private set; get; }

        protected virtual void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this as T;
        }
    }
}
