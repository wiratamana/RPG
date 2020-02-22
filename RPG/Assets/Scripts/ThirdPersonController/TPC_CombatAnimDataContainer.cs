using UnityEngine;
using System.Collections;

namespace Tamana
{
    [CreateAssetMenu(fileName = "New Combat Data", menuName = "CombatDataContainer", order = 1)]
    public class TPC_CombatAnimDataContainer : ScriptableObject
    {
        [SerializeField] private TPC_CombatAnimData[] combatDatas;
        public TPC_CombatAnimData[] CombatDatas => combatDatas;
    }
}
