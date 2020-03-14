using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Tamana.AI
{
    [CreateAssetMenu(fileName = "Dummy AI", menuName = "Create/Brain/Dummy AI")]
    public class AI_Brain_Enemy_Dummy : AI_Brain
    {
        private Neuron_Update_PlayerDetector playerDetector;
        public Neuron_Update_PlayerDetector PlayerDetector
        {
            get
            {
                if(playerDetector == null)
                {
                    playerDetector = new Neuron_Update_PlayerDetector(this, 7.0f, 9.0f);
                }

                return playerDetector;
            }
        }

        public Neuron_Update_RotateTowardPlayer rotateTowardPlayer;
        public Neuron_Update_RotateTowardPlayer RotateTowardPlayer
        {
            get
            {
                if (rotateTowardPlayer == null)
                {
                    rotateTowardPlayer = new Neuron_Update_RotateTowardPlayer(this, 5.0f);
                    rotateTowardPlayer.StopNeuron();
                }
        
                return rotateTowardPlayer;
            }
        }

        private Neuron_Update_AttackHandler attackHandler;
        public Neuron_Update_AttackHandler AttackHandler
        {
            get
            {
                if(attackHandler == null)
                {
                    attackHandler = new Neuron_Update_AttackHandler(
                        brain                : this, 
                        stateName            : "2xAttack_Move_med_whirl_Rhi_Rhi_1",
                        cooldown             : 5.0f,
                        minimumRangeToAttack : 1.25f);

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

