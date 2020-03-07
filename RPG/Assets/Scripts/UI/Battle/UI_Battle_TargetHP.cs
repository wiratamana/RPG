using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Battle_TargetHP : MonoBehaviour
    {
        [SerializeField] private Sprite hpst;
        public Sprite HPST_Sprite => hpst;

        private Dictionary<int, Unit_AI_Hostile> registeredEnemyDic;
        private Canvas canvas;
        public Canvas Canvas => this.GetAndAssignComponentInParent(ref canvas);

        public void RegisterEnemy(Unit_AI_Hostile enemy)
        {
            if (registeredEnemyDic == null)
            {
                registeredEnemyDic = new Dictionary<int, Unit_AI_Hostile>();
            }

            var instanceID = enemy.GetInstanceID();
            if (registeredEnemyDic.ContainsKey(instanceID) == true)
            {
                return;
            }

            registeredEnemyDic.Add(instanceID, enemy);

            var targetHP = new GameObject($"{nameof(UI_Battle_TargetHP)}-{enemy.name}");
            targetHP.AddComponent<RectTransform>();
            targetHP.transform.SetParent(transform);
            var comp = targetHP.gameObject.AddComponent<UI_Battle_TargetHP_Child>();
            comp.Initialize(enemy, Deregister);
        }

        private void Deregister(int instanceID)
        {
            registeredEnemyDic.Remove(instanceID);
        }
    }
}
