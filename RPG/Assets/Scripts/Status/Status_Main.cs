using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace Tamana
{
    public class Status_Main : MonoBehaviour
    {
        [SerializeField] protected Status_Information mainStatus;
        [SerializeField] protected List<Status_Information> additionalStatus;

        public EventManager<Status_DamageData> OnDamageReceived { private set; get; } = new EventManager<Status_DamageData>();
        public EventManager OnDeadEvent { private set; get; } = new EventManager();

        private Status_HP hp;
        public Status_HP HP
        {
            get
            {
                if (hp == null)
                {
                    hp = new Status_HP(mainStatus, OnDeadEvent, OnDamageReceived);
                }

                return hp;
            }
        }

        public bool IsDead
        {
            get
            {
                return HP.CurrentHealth == 0.0f;
            }
        }
    }
}
