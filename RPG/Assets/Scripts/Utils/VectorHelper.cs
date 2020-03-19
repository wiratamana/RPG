using UnityEngine;
using System.Collections;

namespace Tamana
{
    public static class VectorHelper
    {
        public static Vector3 GetForwardWithY0(Vector3 forward)
        {
            forward.y = 0;
            forward = forward.normalized;
            return forward;
        }

        public static Vector2 GetForward2DWithZ0(Vector3 forward)
        {
            var fwd = GetForwardWithY0(forward);
            fwd = new Vector2(fwd.x, fwd.z);
            return fwd;
        }

        public static void FastDistance(in Vector3 a, in Vector3 b, out float distance)
        {
            distance = Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z));
        }

        public static void FastNormalize(ref Vector3 v)
        {
            var distance = Mathf.Sqrt((v.x * v.x) + (v.y * v.y) + (v.z * v.z));
            v.x /= distance;
            v.y /= distance;
            v.z /= distance;
        }

        public static void FastNormalize(in Vector3 v, out Vector3 o)
        {
            var distance = Mathf.Sqrt((v.x * v.x) + (v.y * v.y) + (v.z * v.z));
            o.x = v.x / distance;
            o.y = v.y / distance;
            o.z = v.z / distance;
        }
    }

}
