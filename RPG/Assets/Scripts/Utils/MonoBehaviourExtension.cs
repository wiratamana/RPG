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
            if (value == null)
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
        public static T[] GetAndAssignComponents<T>(this MonoBehaviour component, ref T[] values) where T : Component
        {
            if (values == null || values.Length == 0)
            {
                values = component.GetComponents<T>();
            }

            return values;
        }
        public static T GetFindAndAssignComponent<T>(this MonoBehaviour component, ref T values, System.Predicate<T> predicate) where T : Component
        {
            if (values == null)
            {
                var components = component.GetComponents<T>();
                var comp = System.Array.Find(components, predicate);

                if (comp == null)
                {
                    Debug.Log($"Could not find '{typeof(T).FullName}' from '{component.name}' gameObject");
                    return null;
                }

                values = comp;
            }

            return values;
        }
        public static T GetAndAssignComponentInChildren<T>(this MonoBehaviour component, ref T value) where T : Component
        {
            if (value == null)
            {
                value = component.GetComponentInChildren<T>();
            }

            return value;
        }
        public static T[] GetAndAssignComponentsInChildren<T>(this MonoBehaviour component, ref T[] values) where T : Component
        {
            if (values == null || values.Length == 0)
            {
                values = component.GetComponentsInChildren<T>();
            }

            return values;
        }
        public static T GetFindAndAssignComponentFromChildren<T>(this MonoBehaviour component, ref T values, System.Predicate<T> predicate) where T : Component
        {
            if (values == null)
            {
                var components = component.GetComponentsInChildren<T>();
                var comp = System.Array.Find(components, predicate);

                if (comp == null)
                {
                    Debug.Log($"Could not find '{typeof(T).FullName}' from '{component.name}' Children");
                    return null;
                }

                values = comp;
            }

            return values;
        }
        public static T GetAndAssignComponentInParent<T>(this MonoBehaviour component, ref T value) where T : Component
        {
            if (value == null)
            {
                value = component.GetComponentInParent<T>();
            }

            return value;
        }
        public static T[] GetAndAssignComponentsInParent<T>(this MonoBehaviour component, ref T[] values) where T : Component
        {
            if (values == null || values.Length == 0)
            {
                values = component.GetComponentsInParent<T>();
            }

            return values;
        }
        public static T GetFindAndAssignComponentFromParent<T>(this MonoBehaviour component, ref T values, System.Predicate<T> predicate) where T : Component
        {
            if (values == null)
            {
                var components = component.GetComponentsInParent<T>();
                var comp = System.Array.Find(components, predicate);

                if (comp == null)
                {
                    Debug.Log($"Could not find '{typeof(T).FullName}' from '{component.name}' parent");
                    return null;
                }

                values = comp;
            }

            return values;
        }

        public static Transform GetChildWithNameAndAssign(this MonoBehaviour component, string childName, ref Transform value)
        {
            if (value == null)
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
            if (value == null)
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
