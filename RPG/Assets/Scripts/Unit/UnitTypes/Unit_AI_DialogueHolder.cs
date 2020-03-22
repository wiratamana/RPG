using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Unit_AI_DialogueHolder : MonoBehaviour
    {
        [SerializeField] private Chat_Object chatObject;

        public IReadOnlyCollection<Chat_Base> Dialogues => chatObject.Dialogue;
    }
}
