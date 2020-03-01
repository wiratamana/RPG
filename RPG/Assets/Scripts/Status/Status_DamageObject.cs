using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [CreateAssetMenu(fileName ="New Damage Object", menuName = "Create/Damage Object")]
    public class Status_DamageObject : ScriptableObject
    {
        [SerializeField] private string[] hitAnimations_1H;
        [SerializeField] private string[] hitAnimations_2H;

        public string[] GetHitAnimations(AnimationState animationState)
        {
            switch (animationState)
            {
                case AnimationState.Combat_1H:
                    return hitAnimations_1H;
                case AnimationState.Combat_2H:
                    return hitAnimations_2H;

                default:
                    Benchmarker.Start();
                    var newArrays = new string[hitAnimations_1H.Length + hitAnimations_2H.Length];
                    System.Array.Copy(hitAnimations_1H, 0, newArrays, 0, hitAnimations_1H.Length);
                    System.Array.Copy(hitAnimations_2H, 0, newArrays, hitAnimations_1H.Length, hitAnimations_2H.Length);
                    Benchmarker.Stop();
                    return newArrays;
            }
        }
    }
}