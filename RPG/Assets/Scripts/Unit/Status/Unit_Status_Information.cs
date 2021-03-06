﻿using UnityEngine;
using System.Collections;

namespace Tamana
{
    [CreateAssetMenu(fileName = "New Status", menuName = "NewStatus", order = 1)]
    [System.Serializable]
    public class Unit_Status_Information : ScriptableObject
    {
        [Status_Attribute_MainStatus(MainStatus.HP)]
        public int HP;
        [Status_Attribute_MainStatus(MainStatus.ST)]
        public int ST;

        [Status_Attribute_MainStatus(MainStatus.AT)]
        public int AT;
        [Status_Attribute_MainStatus(MainStatus.DF)]
        public int DF;

        public static string[] GetMainStatusFieldsName()
        {
            var length = System.Enum.GetValues(typeof(MainStatus)).Length;
            var retVal = new string[length];
            var index = 0;

            var fields = System.Array.Find(ClassManager.Types, x => x.Name == nameof(Unit_Status_Information)).GetFields();
            if (fields.Length != length)
            {
                Debug.Log($"The number fields in '{nameof(Unit_Status_Information)}' type not same with length of '{nameof(MainStatus)}' enum!", 
                    Debug.LogType.ForceQuit);
                return null;
            }

            foreach(var f in fields)
            {
                retVal[index] = f.Name;
                index++;
            }

            return retVal;
        }
    }
}
