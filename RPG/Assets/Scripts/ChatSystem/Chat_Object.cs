using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [CreateAssetMenu(fileName = "DialogueObject", menuName = "Create/Dialogue/Dialogue Object")]
    public class Chat_Object : ScriptableObject
    {
        [SerializeField] private List<Chat_Base> dialogue;

        public IReadOnlyCollection<Chat_Base> Dialogue => dialogue;
    }
}
