using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tamana.AI.Neuron;

namespace Tamana.AI
{
    [CreateAssetMenu(fileName = "Better AI", menuName = "Create/Brain/Better AI")]
    public class Brain_Enemy_Better : AI_Brain
    {
        private readonly string attackName = "AI_Good_Attack";
        private readonly string counterName = "AI_Better_Counter1";

        public override void Initialize(Unit_AI unit)
        {
            base.Initialize(unit);

            Unit.PF.nodeParent = PF_Master.Instance.GetNearestNode(unit.transform.position);

            Prop.attackDelay_cur = 2.0f;
            Prop.attackDelay_val = 2.0f;
        }

        public override void Update()
        {
            data.Update();

            if (new IsHostileToPlayer(data) == false)
            {
                if (new IsTakingDamage(data) == false)
                {
                    return;
                }

                new DoSwitchToHostile(data);
            }

            if (data.IsAlert == false)
            {
                if (new IsWeaponEquipped(data))
                {
                    new DoHolsterWeapon(data);
                }

                data.IsAlert = new IsPlayerInsideHostileArea(data);
            }
            else
            {
                data.UpdateNode();

                if (data.State == AIState.Return || new IsOutsideChaseArea(data))
                {
                    if (new IsWeaponEquipped(data))
                    {
                        new DoHolsterWeapon(data);
                    }

                    new DoReturnToIdlePosition(data);
                }
                else if (data.State == AIState.Idle || data.State == AIState.Chase)
                {
                    if (new IsPlayerDead(data))
                    {
                        new DoPlayVictoryAnimation(data, Prop);
                    }

                    else if (new IsTakingDamage(data) == false)
                    {
                        Prop.attackDelay_cur = Mathf.Max(0, Prop.attackDelay_cur - Time.deltaTime);

                        if (new IsWeaponEquipped(data) == false)
                        {
                            new DoDrawWeapon(data);
                        }

                        else if (new IsPlayerAttackingMe(data, minimumDistance: 4.0f, dotValue: 0))
                        {
                            if(data.DistanceToPlayer < 1.8f)
                            {
                                new DoCounter(data, Prop, counterName);
                            }
                            else
                            {
                                new DoDodge(data);
                            }                            
                        }

                        else if (new IsPlayerInsideAttackRange(data, 2.0f))
                        {
                            if (Prop.attackDelay_cur == 0)
                            {
                                new DoAttack(data, Prop, attackName);
                            }
                        }

                        new DoMoveTowardPlayerPosition(data);
                    }
                }
            }

            Unit.transform.rotation = data.MyRotation;
            data.ResetOnAttackAnimationStarted();
        }
    }
}
