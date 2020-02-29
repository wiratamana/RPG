using UnityEngine;
using System.Collections;

namespace Tamana
{
    public static class MonoBehaviourExtension
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            return go.GetComponent<T>() ?? go.AddComponent<T>();
        }

        public static T GetOrAddAndAssignComponent<T>(this MonoBehaviour component, T value) where T : Component
        {
            if(value == null)
            {
                value = component.gameObject.GetOrAddComponent<T>();
            }

            return value;
        }

        public static T GetAndAssignComponent<T>(this MonoBehaviour component, T value) where T : Component
        {
            if (value == null)
            {
                value = component.GetComponent<T>();
            }

            return value;
        }

        public static void LogErrorIfComponentIsNull<T>(this MonoBehaviour component, T value) where T : Component
        {
            if(value == null)
            {
                Debug.Log($"Component '{typeof(T).Name}' is not exist on GameObject with name '{component.name}'.", Debug.LogType.Error);
            }
        }
    }
}
