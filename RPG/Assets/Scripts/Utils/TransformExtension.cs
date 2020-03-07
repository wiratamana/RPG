using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public static class TransformExtension
    {
        public static bool IsInsideCameraFrustum(this Transform transform, Camera camera)
        {
            var upperBound = 0.95f;
            var lowerBound = 0.05f;
            var zero = 0.0f;

            var viewPortPosition = camera.WorldToViewportPoint(transform.position);
            if (viewPortPosition.x > lowerBound && viewPortPosition.x < upperBound &&
                        viewPortPosition.y > lowerBound && viewPortPosition.y < upperBound &&
                        viewPortPosition.z > zero)
            {
                return true;
            }

            return false;
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
