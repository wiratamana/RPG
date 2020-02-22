using System;

namespace Tamana
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TPC_Animset_AttributeAnimClip : Attribute
    {
        public string AnimsetName { private set; get; }
        public TPC_Animset_AttributeAnimClip(string animsetName)
        {
            AnimsetName = animsetName;
        }
    }
}
