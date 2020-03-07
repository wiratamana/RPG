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

    public enum MovementType
    {
        Normal,
        Strafe,
    }

    public enum WeaponType
    {
        OneHand,
        TwoHand
    }

    public enum AnimHit_1H
    {
        Sword1h_Hit_Head_Right = 1 << 0,
        Sword1h_Hit_Head_RightDown = 1 << 1,
        Sword1h_Hit_Head_RightUp = 1 << 2,
        Sword1h_Hit_Torso_Left = 1 << 3,
        Sword1h_Hit_Torso_Front = 1 << 4,
    }

    public enum AnimHit_2H
    {

    }

    public enum AnimParry_1H
    {

    }

    public enum AnimParry_2H
    {

    }

    public enum AnimDodge_1H
    {
        Sword1h_Dodge = 1 << 0,
        Sword1h_ShortDodge_Mid2 = 1 << 1,
        Sword1h_ShortDodge_Mid = 1 << 2
    }

    public enum AnimDodge_2H
    {
        Sword1h_Dodge = 1 << 0,
        Sword1h_ShortDodge_Mid2 = 1 << 1,
        Sword1h_ShortDodge_Mid = 1 << 2
    }
}
