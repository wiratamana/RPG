using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public sealed class Chat_ReturnTo : ScriptableObject
    {
        public Chat_Object ReturnToObject { get; private set; }
        public int ReturnToIndex { get; private set; }

        public Chat_ReturnTo Initialize(Chat_Object returnToObject, int returnToIndex)
        {
            ReturnToObject = returnToObject;
            ReturnToIndex = returnToIndex;

            return this;
        }
    }
}
