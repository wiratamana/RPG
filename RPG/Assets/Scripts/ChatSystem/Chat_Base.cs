using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public abstract class Chat_Base : ScriptableObject
    {
        public abstract ChatType Type { get; }
    }
}
