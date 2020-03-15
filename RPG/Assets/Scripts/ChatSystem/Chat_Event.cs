using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [CreateAssetMenu(fileName = "Event", menuName = "Create/Dialogue/Event")]
    public class Chat_Event : Chat_Base
    {
        public override ChatType Type => ChatType.Event;
        [SerializeField] private ChatEvent _event;
        public ChatEvent Event => _event;
    }
}
