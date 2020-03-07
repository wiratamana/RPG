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
        public float CurrentHealthRate => (float)CurrentHealth / MaxHealth;
        public EventManager<float> OnCurrentHealthUpdated { get; } = new EventManager<float>();

        public Unit_Status_HP(Unit_Status_Information information, 
            EventManager<Unit_Status_DamageData> onDamageReceivedListener)
        {
            this.information = information;
            CurrentHealth = this.information.HP;

            onDamageReceivedListener.AddListener(Damage);
        }

        public void Damage(Unit_Status_DamageData damageData)
        {
            if(CurrentHealth == 0)
            {
                Debug.Log($"'{information.name}' is already dead !!");
                return;
            }

            CurrentHealth = Mathf.Max(0, CurrentHealth - damageData.damagePoint);

            Debug.Log($"Damage Received : {damageData.damagePoint} | Remaining HP : {CurrentHealth}");

            if (CurrentHealth == 0)
            {
                Debug.Log("Dead!!");
            }

            OnCurrentHealthUpdated.Invoke(CurrentHealthRate);
        }
    }
}