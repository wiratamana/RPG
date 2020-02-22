using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Status_DamageHandler : MonoBehaviour
    {
        public EventManager<Status_DamageData> OnReceivedDamageEvent { private set; get; } = new EventManager<Status_DamageData>();

        public void SendDamage(Status_DamageData damage)
        {
            OnReceivedDamageEvent.Invoke(damage);
        }
    }
}
