using UnityEngine;
using System.Collections;

namespace Tamana
{
    public enum AnimationState
    {
        IsNotPlaying,
        IsCurrentlyPlaying,
        IsFinishedPlaying,
    }

    [System.Serializable]
    public class TPC_CombatAnimData
    {
        [SerializeField] private string myAnimStateName;
        [SerializeField] private float receivingInputStart;
        [SerializeField] private float receivingInputFinish;

        [SerializeField] private float transitionTimingToIdleAnim;
        [SerializeField] private float transitionCrossFadeTimeIdle;

        [SerializeField] private string idleAnimStateName;
        [SerializeField] private bool isLastAnimation;

        public bool IsInputReceived { get; set; }
        public bool IsCurrentlyReceivingInput { get; set; }

        public string MyAnimStateName => myAnimStateName;

        public float ReceivingInputStart => receivingInputStart;
        public float ReceivingInputFinish => receivingInputFinish;

        public float TransitionToIdleAnim => transitionTimingToIdleAnim;
        public float TransitionTimeIdle => transitionCrossFadeTimeIdle;

        public string IdleAnimStateName => idleAnimStateName;

        public bool IsLastAnimation => isLastAnimation;
    }
}