using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Tamana
{
    public class Unit_Status_HP
    {
        private Unit_Status_Information information;
        public int MaxHealth
        {
            get
            {
                return information.HP;
            }
        }

        public int CurrentHealth { get; private set; }
        private EventManager onDeadEvent;
        private EventManager<Unit_Status_DamageData> onDamageReceivedEvent;

        public Unit_Status_HP(Unit_Status_Information information, EventManager onDeadListener, 
            EventManager<Unit_Status_DamageData> onDamageReceivedListener)
        {
            this.information = information;
            CurrentHealth = this.information.HP;

            onDeadEvent = onDeadListener;
            onDamageReceivedEvent = onDamageReceivedListener;
        }

        public void Damage(Unit_Status_DamageData damageData)
        {
            if(CurrentHealth == 0)
            {
                Debug.Log($"'{information.name}' is already dead !!");
                return;
            }

            onDamageReceivedEvent.Invoke(damageData);
            CurrentHealth = Mathf.Max(0, CurrentHealth - damageData.damagePoint);

            Debug.Log($"Damage Received : {damageData.damagePoint} | Remaining HP : {CurrentHealth}");

            if (CurrentHealth == 0)
            {
                Debug.Log("Dead!!");
                onDeadEvent.Invoke();
            }
        }
    }
}