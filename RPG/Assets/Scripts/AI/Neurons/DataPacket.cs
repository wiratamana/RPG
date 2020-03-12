using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana.AI
{
    public struct DataPacket
    {
        public AIState State;
        public readonly Unit_Animator_Params Params;
        public bool IsAlert;
        public Vector3 destination;

        public DataPacket(AIState state, Unit_Animator_Params @params)
        {
            State = state;
            Params = @params;
            IsAlert = false;
            destination = Vector3.zero;
        }
    }
}
