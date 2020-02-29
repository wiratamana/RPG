using UnityEngine;
using System.Collections;

namespace Tamana
{
    public enum ItemType
    {
        Weapon, 
        Armor, 
        Attachment, 
        Consumable
    }

    public enum Gender 
    { 
        All = 0, 
        Male, 
        Female 
    }

    public enum MainStatus
    {
        HP, 
        ST,
        AT, 
        DF
    }
}
