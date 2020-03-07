using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Tamana
{
    public class Status_HP
    {
        private Status_Information information;
        public int MaxHealth
        {
            get
            {
                return information.HP;
            }
        }

        public int CurrentHealth { get; private set; }
        private EventManager onDeadEvent;
        private EventManager<Status_DamageData> onDamageReceivedEvent;

        public Status_HP(Status_Information information, EventManager onDeadListener, 
            EventManager<Status_DamageData> onDamageReceivedListener)
        {
            this.information = information;
            CurrentHealth = this.information.HP;

            onDeadEvent = onDeadListener;
            onDamageReceivedEvent = onDamageReceivedListener;
        }

        public void Damage(Status_DamageData damageData)
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