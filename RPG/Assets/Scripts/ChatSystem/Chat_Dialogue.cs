using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Create/Dialogue/Dialogue")]
    public class Chat_Dialogue : Chat_Base 
    {
        [SerializeField] ChatType type;
        [SerializeField] private string dialogue;

        public string Dialogue => dialogue;
        public override ChatType Type => type;
    }
}
