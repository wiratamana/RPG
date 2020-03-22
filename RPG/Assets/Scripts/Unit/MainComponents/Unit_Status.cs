using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Tamana
{
    public class Unit_Status : MonoBehaviour
    {
        private Unit_Status_Information mainStatus;
        private List<Unit_Status_Information> additionalStatus;

        public bool IsInitialized => mainStatus != null;

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

        private Unit_Status_HP hp;
        public Unit_Status_HP HP
        {
            get
            {
                if (hp == null)
                {
                    hp = new Unit_Status_HP(mainStatus, Unit.CombatHandler.DamageReceiveHandler.OnReceivedDamageEvent);
                }

                return hp;
            }
        }

        private Unit_Status_ST st;
        public Unit_Status_ST ST
        {
            get
            {
                if (st == null)
                {
                    st = new Unit_Status_ST(mainStatus);
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

        public void Initialize(Unit_Status_Information mainStatus)
        {
            if(this.mainStatus != null)
            {
                Debug.Log("This component was already initialized", Debug.LogType.Warning);
                return;
            }

            this.mainStatus = mainStatus;
        }
    }
}
