using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class GM_AttributeValueIsSetWithReflection : GM_AttributeBase
    {
        public string MethodName { private set; get; }
        public GM_AttributeValueIsSetWithReflection(string methodName)
        {
            MethodName = methodName;
        }
    }
}
