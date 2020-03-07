using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Tamana
{
    public class Unit_Status : MonoBehaviour
    {
        [SerializeField] protected Status_Information mainStatus;
        [SerializeField] protected List<Status_Information> additionalStatus;
        private Unit_Base unit;
        public Unit_Base Unit => this.GetAndAssignComponent(ref unit);

        private Dictionary<MainStatus, FieldInfo> statusDic;
        private Dictionary<MainStatus, FieldInfo> StatusDic
        {
            get
            {
                if (statusDic == null || statusDic.Count == 0)
                {
                    statusDic = new Dictionary<MainStatus, FieldInfo>();

                    var type = mainStatus.GetType();
                    var fields = type.GetFields();

                    foreach (var f in fields)
                    {
                        if (f.IsDefined(typeof(Status_Attribute_MainStatus)) == false)
                        {
                            continue;
                        }

                        var customAttribute = f.GetCustomAttribute<Status_Attribute_MainStatus>();
                        statusDic.Add(customAttribute.MainStatus, f);
                    }
                }

                return statusDic;
            }
        }

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

        private Status_ST st;
        public Status_ST ST
        {
            get
            {
                if (st == null)
                {
                    st = new Status_ST(mainStatus);
                }

                return st;
            }
        }

        public bool IsDead
        {
            get
            {
                return HP.CurrentHealth == 0.0f;
            }
        }

        public int GetStatus(MainStatus status)
        {
            var effects = Unit.Equipment.GetEquippedItemEffects();
            var filteredEffects = effects.Where(x => x.type == status);
            int totalAdditionalStatus = 0;
            foreach (var item in filteredEffects)
            {
                totalAdditionalStatus += item.value;
            }

            return (int)StatusDic[status].GetValue(mainStatus) + totalAdditionalStatus;
        }
    }
}
