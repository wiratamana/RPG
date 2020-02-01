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

    }

}
