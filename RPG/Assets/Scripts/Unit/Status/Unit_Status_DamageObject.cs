using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [CreateAssetMenu(fileName ="New Damage Object", menuName = "Create/Damage Object")]
    public class Unit_Status_DamageObject : ScriptableObject
    {
        [SerializeField] private AnimHit_1H[] hitAnimations_1H;
        [SerializeField] private AnimHit_2H[] hitAnimations_2H;

        public int[] GetHitAnimations(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.OneHand:
                    return GetIntArrayFromEnumArray(hitAnimations_1H);

                case WeaponType.TwoHand: 
                    return GetIntArrayFromEnumArray(hitAnimations_2H);

                default: return null;
            }
        }

        private int[] GetIntArrayFromEnumArray<T>(T[] objs)
            where T : System.Enum
        {
            var retVal = new int[objs.Length];
            for(int i = 0; i < objs.Length; i++)
            {
                retVal[i] = (int)(object)objs[i];
            }

            return retVal;
        }
    }
}