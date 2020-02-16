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
            var obj = Instance as MonoBehaviour;
            try
            {
                var myname = obj.name;
            }
            catch (MissingReferenceException)
            {
                Instance = null;
            }
            catch (System.NullReferenceException)
            {

            }

            if(Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this as T;
        }
    }
}
