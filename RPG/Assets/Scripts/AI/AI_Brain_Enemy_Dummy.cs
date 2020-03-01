using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Tamana
{
    public class AI_Brain_Enemy_Dummy : AI_Brain
    {
        public override AI_Neuron_PlayerDetector PlayerDetector
        {
            get
            {
                if(playerDetector == null)
                {
                    playerDetector = new AI_Neuron_PlayerDetector(this, 7.0f, 9.0f);
                }

                return playerDetector;
            }
        }

        public override AI_Neuron_RotateTowardPlayer RotateTowardPlayer
        {
            get
            {
                if (rotateTowardPlayer == null)
                {
                    rotateTowardPlayer = new AI_Neuron_RotateTowardPlayer(this, 5.0f);
                    rotateTowardPlayer.StopNeuron();
                }
        
                return rotateTowardPlayer;
            }
        }

        private AI_Neuron_AttackHandler attackHandler;
        public AI_Neuron_AttackHandler AttackHandler
        {
            get
            {
                if(attackHandler == null)
                {
                    attackHandler = new AI_Neuron_AttackHandler(
                        brain                : this, 
                        stateName            : "2xAttack_Move_med_whirl_Rhi_Rhi_1",
                        cooldown             : 5.0f,
                        minimumRangeToAttack : 1.25f);

                    attackHandler.StopNeuron();
                }

                return attackHandler;
            }
        }

        public override void Init(AI_Enemy_Base ai)
        {
            base.Init(ai);

            PlayerDetector.OnPlayerEnteredHostileRange.AddListener(AI.Unit.CombatHandler.PlayEquipAnimation);
            PlayerDetector.OnPlayerEnteredHostileRange.AddListener(RotateTowardPlayer.ResumeNeuron, RotateTowardPlayer.GetInstanceID());
            PlayerDetector.OnPlayerEnteredHostileRange.AddListener(AttackHandler.ResumeNeuron, AttackHandler.GetInstanceID());

            PlayerDetector.OnPlayerExitedHostileRange.AddListener(AI.Unit.CombatHandler.PlayHolsterAnimation);
            PlayerDetector.OnPlayerExitedHostileRange.AddListener(RotateTowardPlayer.StopNeuron, RotateTowardPlayer.GetInstanceID());
            PlayerDetector.OnPlayerExitedHostileRange.AddListener(AttackHandler.StopNeuron, AttackHandler.GetInstanceID());

            PlayerDetector.OnPlayerInsideHostileRange.AddListener(AttackHandler.UpdateDistanceToPlayer, AttackHandler.GetInstanceID());
        }

        public override void Update()
        {
            UpdateNeuron();
        }
    }
}

