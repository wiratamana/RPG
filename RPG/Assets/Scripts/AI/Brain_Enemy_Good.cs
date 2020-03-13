using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Tamana.AI.Neuron;

namespace Tamana.AI
{
    public enum AIState
    {
        Idle,
        Chase,
        Return,
        Attack,
        Dodge,
        Holster,
        Equip
    }

    [CreateAssetMenu(fileName = "Good AI", menuName = "Create/Brain/Good AI")]
    public class Brain_Enemy_Good : AI_Brain
    {
        private Data data;

        public override void Initialize(Unit_AI_Hostile unit)
        {
            base.Initialize(unit);

            Unit.PF.nodeParent = PF_Master.Instance.GetNearestNode(unit.transform.position);
            data = new Data(Unit);
        }

        public override void Update()
        {
            Benchmarker.Start();
            data.Update();

            if (data.IsAlert == false)
            {
                data.IsAlert = new IsPlayerInsideHostileArea(data).Result;
            }
            else
            {
                data.UpdateNode();

                if (data.State == AIState.Return || new IsDistanceIsFromMyPositionToIdlePositionIsGreaterThan(data).Result)
                {
                    new DoReturnToIdlePosition(data);
                }
                else if (data.State == AIState.Idle || data.State == AIState.Chase)
                {
                    new DoMoveTowardPlayerPosition(data);
                }                
            }

            Unit.transform.rotation = data.MyRotation;
            Benchmarker.Stop($"State = {data.State} | IsAlert = {data.IsAlert}");
        }        
    }
}
