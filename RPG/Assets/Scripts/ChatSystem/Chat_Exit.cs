using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [CreateAssetMenu(fileName = "Exit", menuName = "Create/Dialogue/Exit")]
    public class Chat_Exit : Chat_Base
    {
        public override ChatType Type => ChatType.Exit; 
    }
}
