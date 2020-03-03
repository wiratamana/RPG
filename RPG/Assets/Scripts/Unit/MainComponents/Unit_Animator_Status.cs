using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tamana
{
    public class Unit_Animator_Status
    {
        public struct Result
        { 
            public readonly int paramValue; 
            public readonly string stateName;

            public Result(object constructor)
            {
                paramValue = (int)constructor;
                stateName = constructor.ToString();
            }
        }

        public Unit_Animator UnitAnimator { get; }

        private Dictionary<AnimHit_1H, bool> hitDic_1H;
        private Dictionary<AnimHit_2H, bool> hitDic_2H;
        private Dictionary<AnimParry_1H, bool> parryDic_1H;
        private Dictionary<AnimParry_2H, bool> parryDic_2H;
        private Dictionary<AnimDodge_1H, bool> dodgeDic_1H;
        private Dictionary<AnimDodge_2H, bool> dodgeDic_2H;

        public IReadOnlyDictionary<AnimHit_1H, bool> HitDic_1H => hitDic_1H;
        public IReadOnlyDictionary<AnimHit_2H, bool> HitDic_2H => hitDic_2H;
        public IReadOnlyDictionary<AnimParry_1H, bool> ParryDic_1H => parryDic_1H;
        public IReadOnlyDictionary<AnimParry_2H, bool> ParryDic_2H => parryDic_2H;
        public IReadOnlyDictionary<AnimDodge_1H, bool> DodgeDic_1H => dodgeDic_1H;
        public IReadOnlyDictionary<AnimDodge_2H, bool> DodgeDic_2H => dodgeDic_2H;

        public Unit_Animator_Status(Unit_Animator unitAnimator)
        {
            UnitAnimator = unitAnimator;

            InitDictionary<AnimHit_1H, Dictionary<AnimHit_1H, bool>>(ref hitDic_1H);
            InitDictionary<AnimHit_2H, Dictionary<AnimHit_2H, bool>>(ref hitDic_2H);

            InitDictionary<AnimParry_1H, Dictionary<AnimParry_1H, bool>>(ref parryDic_1H);
            InitDictionary<AnimParry_2H, Dictionary<AnimParry_2H, bool>>(ref parryDic_2H);

            InitDictionary<AnimDodge_1H, Dictionary<AnimDodge_1H, bool>>(ref dodgeDic_1H);
            InitDictionary<AnimDodge_2H, Dictionary<AnimDodge_2H, bool>>(ref dodgeDic_2H);
        }

        public Result GetAnimationHitData(int[] statesName)
        {
            var weaponType = UnitAnimator.Unit.Equipment.EquippedWeapon == null ?
                WeaponType.OneHand : UnitAnimator.Unit.Equipment.EquippedWeapon.WeaponType;

            object dicKey = null;

            switch (weaponType)
            {
                case WeaponType.OneHand:
                    dicKey = GetDicKeyWithFalseValue<AnimHit_1H, Dictionary<AnimHit_1H, bool>>(ref hitDic_1H, statesName);
                    break;

                case WeaponType.TwoHand:
                    dicKey = GetDicKeyWithFalseValue<AnimHit_2H, Dictionary<AnimHit_2H, bool>>(ref hitDic_2H, statesName);
                    break;
            }

            return new Result(dicKey);
        }

        private Enum GetDicKeyWithFalseValue<Enum, Dictionary>(ref Dictionary dic, int[] statesName)
            where Enum : System.Enum
            where Dictionary : Dictionary<Enum, bool>
        {
            var filteredDic = dic.Where(x => x.Value == false &&
                                System.Array.Exists(statesName, a => a == (int)(object)x.Key));

            var val = filteredDic.ElementAt(Random.Range(0, filteredDic.Count())).Key;

            dic[val] = true;
            return val;
        }

        public void SetToFalse<Enum>(Enum key)
        {
            switch (key.GetType().Name)
            {
                case nameof(AnimHit_1H):
                    hitDic_1H[(AnimHit_1H)(object)key] = false;
                    break;
                case nameof(AnimHit_2H):
                    hitDic_2H[(AnimHit_2H)(object)key] = false;
                    break;
                case nameof(AnimParry_1H):
                    parryDic_1H[(AnimParry_1H)(object)key] = false;
                    break;
                case nameof(AnimParry_2H):
                    parryDic_2H[(AnimParry_2H)(object)key] = false;
                    break;
                case nameof(AnimDodge_1H):
                    dodgeDic_1H[(AnimDodge_1H)(object)key] = false;
                    break;
                case nameof(AnimDodge_2H):
                    dodgeDic_2H[(AnimDodge_2H)(object)key] = false;
                    break;
            }
        }

        private void InitDictionary<Enum, Dictionary>(ref Dictionary dic) 
            where Enum : System.Enum
            where Dictionary : Dictionary<Enum, bool>
        {
            dic = (Dictionary)new Dictionary<Enum, bool>();
            var enumValue = System.Enum.GetValues(typeof(Enum));
            
            foreach(var e in enumValue)
            {
                dic.Add((Enum)System.Enum.Parse(typeof(Enum), e.ToString()), false);
            }
        }
    }
}
