using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class UI_Battle : SingletonMonobehaviour<UI_Battle>
    {
        private Canvas canvas;
        public Canvas Canvas => this.GetAndAssignComponent(ref canvas);

        private UI_Battle_EnemyLock enemyLock;
        public UI_Battle_EnemyLock EnemyLock => this.GetAndAssignComponentInChildren(ref enemyLock);

        private UI_Battle_TargetHP targetHP;
        public UI_Battle_TargetHP TargetHP => this.GetAndAssignComponentInChildren(ref targetHP);
    }
}
