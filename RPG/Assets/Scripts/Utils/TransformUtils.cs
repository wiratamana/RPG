using UnityEngine;
using System.Collections;

namespace Tamana
{
    public static class TransformUtils
    {
        public static Transform GetChildRecursive(string childName, Transform parent)
        {
            for(int i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                if (child.name != childName)
                {
                    child = GetChildRecursive(childName, child);
                }

                if(child == null)
                {
                    continue;
                }

                if(child.name == childName)
                {
                    return child;
                }
            }

            return null;
        }
    }
}
