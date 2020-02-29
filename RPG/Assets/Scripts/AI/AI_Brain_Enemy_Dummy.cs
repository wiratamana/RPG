using UnityEngine;
using System.Collections;

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

        public override void Init(AI_Enemy_Base ai)
        {
            base.Init(ai);

            PlayerDetector.OnPlayerEnteredHostileRange.AddListener(AI.EnemyAnimator.PlayEquipAnimation);
            playerDetector.OnPlayerEnteredHostileRange.AddListener(RotateTowardPlayer.ResumeNeuron);

            PlayerDetector.OnPlayerExitedHostileRange.AddListener(AI.EnemyAnimator.PlayHolsterAnimation);
            playerDetector.OnPlayerExitedHostileRange.AddListener(RotateTowardPlayer.StopNeuron);
        }

        public override void Update()
        {
            UpdateNeuron();
        }
    }
}

