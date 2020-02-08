using UnityEngine;
using System.Collections;
using System;

namespace Tamana
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GM_AttributeInstantiator : GM_AttributeBase
    {
        public Type Type { get; private set; }

        public GM_AttributeInstantiator(Type type)
        {
            type = Type;
        }
    }
}
