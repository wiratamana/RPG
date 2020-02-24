using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tamana
{
    public class Status_Player : Status_Main
    {
        private Dictionary<MainStatus, FieldInfo> statusDic;
        private Dictionary<MainStatus, FieldInfo> StatusDic
        {
            get
            {
                if(statusDic == null || statusDic.Count == 0)
                {
                    statusDic = new Dictionary<MainStatus, FieldInfo>();

                    var type = mainStatus.GetType();
                    var fields = type.GetFields();

                    foreach(var f in fields)
                    {
                        if(f.IsDefined(typeof(Status_Attribute_MainStatus)) == false)
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

        private void Awake()
        {
            mainStatus = ResourcesLoader.Instance.GetPlayerBaseStatus();
        }

        public int GetStatus(MainStatus status)
        {
            var effects = Inventory_EquipmentManager.Instance.GetEquippedItemEffects();
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
