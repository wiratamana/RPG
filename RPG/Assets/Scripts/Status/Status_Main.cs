using UnityEngine;
using System.Collections.Generic;

namespace Tamana
{
    public class Status_Main : MonoBehaviour
    {
        public Status_Information mainStatus;
        public List<Status_Information> additionalStatus;

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
