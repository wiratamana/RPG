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
                data.IsAlert = new IsPlayerInsideHostileArea(data);
            }
            else
            {
                data.UpdateNode();

                if (data.State == AIState.Return || new IsOutsideChaseArea(data))
                {
                    if (new IsWeaponEquipped(data) == true)
                    {
                        new DoHolsterWeapon(data);
                    }

                    new DoReturnToIdlePosition(data);
                }
                else if (data.State == AIState.Idle || data.State == AIState.Chase)
                {
                    if(new IsTakingDamage(data) == false)
                    {
                        if (new IsWeaponEquipped(data) == false)
                        {
                            new DoDrawWeapon(data);
                        }

                        else if (new IsPlayerAttackingMe(data))
                        {
                            new DoDodge(data);
                        }

                        else if(new IsPlayerInsideAttackRange(data, 2.0f))
                        {
                            new DoAttack(data, "AI_Good_Attack");
                        }

                        new DoMoveTowardPlayerPosition(data);
                    }                    
                }                
            }

            Unit.transform.rotation = data.MyRotation;
            Benchmarker.Stop($"State = {data.State} | IsAlert = {data.IsAlert}");
            data.ResetOnAttackAnimationStarted();
        }        
    }
}
