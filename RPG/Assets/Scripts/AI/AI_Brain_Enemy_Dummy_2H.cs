using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AI_Brain_Enemy_Dummy_2H : AI_Brain
    {
        public override AI_Neuron_PlayerDetector PlayerDetector
        {
            get
            {
                if (playerDetector == null)
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
                if (attackHandler == null)
                {
                    attackHandler = new AI_Neuron_AttackHandler(
                        brain: this,
                        stateName: "Longs_Attack_DoubleRR",
                        cooldown: 5.0f,
                        minimumRangeToAttack: 1.25f);

                    attackHandler.StopNeuron();
                }

                return attackHandler;
            }
        }

        public override void Initialize(Unit_AI_Hostile ai)
        {
            base.Initialize(ai);

            PlayerDetector.OnPlayerEnteredHostileRange.AddListener(Unit.CombatHandler.Equip);
            PlayerDetector.OnPlayerEnteredHostileRange.AddListener(RotateTowardPlayer.ResumeNeuron, RotateTowardPlayer.GetInstanceID());
            PlayerDetector.OnPlayerEnteredHostileRange.AddListener(AttackHandler.ResumeNeuron, AttackHandler.GetInstanceID());

            PlayerDetector.OnPlayerExitedHostileRange.AddListener(Unit.CombatHandler.Holster);
            PlayerDetector.OnPlayerExitedHostileRange.AddListener(RotateTowardPlayer.StopNeuron, RotateTowardPlayer.GetInstanceID());
            PlayerDetector.OnPlayerExitedHostileRange.AddListener(AttackHandler.StopNeuron, AttackHandler.GetInstanceID());

            PlayerDetector.OnPlayerInsideHostileRange.AddListener(AttackHandler.UpdateDistanceToPlayer, AttackHandler.GetInstanceID());

            Unit.CombatHandler.UnitAnimator.OnHitAnimationStarted.AddListener(AttackHandler.StopNeuron);
            Unit.CombatHandler.UnitAnimator.OnHitAnimationFinished.AddListener(AttackHandler.ResumeNeuron);
        }

        public override void Update()
        {
            UpdateNeuron();
        }
    }
}
