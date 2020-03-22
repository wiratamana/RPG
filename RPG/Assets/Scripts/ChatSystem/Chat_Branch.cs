using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [CreateAssetMenu(fileName = "Branch", menuName = "Create/Dialogue/Branch")]
    public class Chat_Branch : Chat_Base
    {
        public override ChatType Type => ChatType.Branch;

        [SerializeField] private Chat_Object[] dialogues;
        public IReadOnlyCollection<Chat_Object> Dialogues => dialogues;
    }
}
