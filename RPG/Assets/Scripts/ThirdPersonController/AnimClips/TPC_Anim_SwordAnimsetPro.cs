using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [TPC_Animset_AttributeAnimClip(nameof(TPC_Anim_SwordAnimsetPro))]
    public static class TPC_Anim_SwordAnimsetPro
    {
        [TPC_Anim_AttributeNotAnim]
        public const string LAYER                       = "SwordAnimsetPro";

        [TPC_Anim_AttributeIdle]
        public const string Sword1h_Idle                = "Sword1h_Idle";

        [TPC_Anim_AttributeDisableMovement]
        public const string Sword1h_Equip               = "Sword1h_Equip";

        [TPC_Anim_AttributeDisableMovement]
        public const string Sword1h_Holster             = "Sword1h_Holster";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStarting]
        public const string Sword1h_WalkFwdStart        = "Sword1h_WalkFwdStart";

        [TPC_Anim_AttributeMoving]
        public const string Sword1h_WalkFwdLoop         = "Sword1h_WalkFwdLoop";

        [TPC_Anim_AttributeMoving]
        public const string Sword1h_RunFwdLoop          = "Sword1h_RunFwdLoop";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStarting]
        public const string Sword1h_WalkFwdStart90_L    = "Sword1h_WalkFwdStart90_L";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStarting]
        public const string Sword1h_WalkFwdStart135_L   = "Sword1h_WalkFwdStart135_L";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStarting]
        public const string Sword1h_WalkFwdStart180_L   = "Sword1h_WalkFwdStart180_L";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStarting]
        public const string Sword1h_WalkFwdStart90_R    = "Sword1h_WalkFwdStart90_R";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStarting]
        public const string Sword1h_WalkFwdStart135_R   = "Sword1h_WalkFwdStart135_R";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStarting]
        public const string Sword1h_WalkFwdStart180_R   = "Sword1h_WalkFwdStart180_R";

        [TPC_Anim_AttributeMoving]
        public const string StrafeAnimBlendTree         = "StrafeAnimBlendTree";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStopping]
        public const string Sword1h_StrafeLtStop_LU     = "Sword1h_StrafeLtStop_LU";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStopping]
        public const string Sword1h_StrafeLtStop_RU     = "Sword1h_StrafeLtStop_RU";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStopping]
        public const string Sword1h_StrafeRtStop_LU     = "Sword1h_StrafeRtStop_LU";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStopping]
        public const string Sword1h_StrafeRtStop_RU     = "Sword1h_StrafeRtStop_RU";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStopping]
        public const string Sword1h_WalkStop_LU         = "Sword1h_WalkStop_LU";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStopping]
        public const string Sword1h_WalkStop_RU         = "Sword1h_WalkStop_RU";
    }
}
