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

    public enum StateMachineBehaviourPosition
    {
        OnStateEnter,
        OnStateUpdate,
        OnStateExit
    }

    public enum Direction
    {
        Forward,
        Backward,
        Left,
        Right,
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
        Longs_Hit_R = 1 << 0,
        Longs_Hit_R2 = 1 << 1,
        Longs_Hit_U = 1 << 2
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
        Longs_Dodge_Bwd = 1 << 0,
    }

    public enum AnimDeath_1H
    {
        Sword1h_Death_1 = 1 << 0,
        Sword1h_Death_2 = 1 << 1,
        Sword1h_Death_Front = 1 << 2,
        Sword1h_Death_L = 1 << 3,
        Sword1h_Death_R = 1 << 4
    }

    public enum AnimDeath_2H
    {
        Longs_KO_1 = 1 << 0,
        Longs_KO_2 = 1 << 1,
        Longs_KO_Air = 1 << 2,
        Longs_KO_AirStrong = 1 << 3,
    }
}
