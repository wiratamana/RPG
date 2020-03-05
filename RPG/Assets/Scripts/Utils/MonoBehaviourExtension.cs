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

        public static T GetOrAddAndAssignComponent<T>(this MonoBehaviour component, ref T value) where T : Component
        {
            if(value == null)
            {
                value = component.gameObject.GetOrAddComponent<T>();
            }

            return value;
        }

        public static T GetAndAssignComponent<T>(this MonoBehaviour component, ref T value) where T : Component
        {
            if (value == null)
            {
                value = component.GetComponent<T>();
            }

            return value;
        }

        public static T GetAndAssignComponentInChildren<T>(this MonoBehaviour component, ref T value) where T : Component
        {
            if (value == null)
            {
                value = component.GetComponentInChildren<T>();
            }

            return value;
        }

        public static T GetAndAssignComponentInParent<T>(this MonoBehaviour component, ref T value) where T : Component
        {
            if (value == null)
            {
                value = component.GetComponentInParent<T>();
            }

            return value;
        }

        public static Transform GetChildWithNameAndAssign(this MonoBehaviour component, string childName, ref Transform value)
        {
            if(value == null)
            {
                value = GetChildWithNameRecursive(childName, component.transform);
            }

            return value;
        }
        public static Transform GetChildWithNameFromParentAndAssign(this MonoBehaviour component, 
            string childName, Transform parent, ref Transform value)
        {
            if (value == null)
            {
                value = GetChildWithNameRecursive(childName, parent);
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

        private static Transform GetChildWithNameRecursive(string childName, Transform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                if (child.name != childName)
                {
                    child = GetChildWithNameRecursive(childName, child);
                }

                if (child == null)
                {
                    continue;
                }

                if (child.name == childName)
                {
                    return child;
                }
            }

            return null;
        }
    }
}
