using UnityEngine;
using System.Collections;

namespace Tamana
{
    [TPC_Animset_AttributeAnimClip(nameof(TPC_Anim_RunAnimsetBasic))]
    public static class TPC_Anim_RunAnimsetBasic
    {
        [TPC_Anim_AttributeNotAnim]
        public const string LAYER               = "RunAnimsetBasic";

        [TPC_Anim_AttributeIdle]
        public const string RunAnimsetBasic_Idle = "RunAnimsetBasic_Idle";

        [TPC_Anim_AttributeMoving]
        public const string RunFwdStart         = "RunFwdStart";

        [TPC_Anim_AttributeMoving] 
        public const string RunFwdLoop          = "RunFwdLoop";

        [TPC_Anim_AttributeMoving] 
        [TPC_Anim_AttributeMoveStarting] 
        public const string RunFwdStart90_L     = "RunFwdStart90_L";

        [TPC_Anim_AttributeMoving] 
        [TPC_Anim_AttributeMoveStarting] 
        public const string RunFwdStart90_R     = "RunFwdStart90_R";

        [TPC_Anim_AttributeMoving] 
        [TPC_Anim_AttributeMoveStarting] 
        public const string RunFwdStart180_R    = "RunFwdStart180_R";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStarting] 
        public const string RunFwdStart180_L    = "RunFwdStart180_L";

        [TPC_Anim_AttributeMoving]
        [TPC_Anim_AttributeMoveStopping] 
        public const string RunFwdStop_LU       = "RunFwdStop_LU";

        [TPC_Anim_AttributeMoving] 
        [TPC_Anim_AttributeMoveStopping] 
        public const string RunFwdStop_RU       = "RunFwdStop_RU";

        public const string IsMoving = "IsMoving";
    }
}
