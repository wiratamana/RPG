using UnityEngine;
using System.Collections;

namespace Tamana
{
    public enum MainStatus
    {
        HP, MP, ST, AT, DF
    }

    public class Status_Information : ScriptableObject
    {
        [Status_Attribute_MainStatus(MainStatus.HP)]
        public int HP;
        [Status_Attribute_MainStatus(MainStatus.MP)]
        public int MP;
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

            var fields = System.Array.Find(ClassManager.Types, x => x.Name == nameof(Status_Information)).GetFields();
            if(fields.Length != length)
            {
                Debug.Log($"The number fields in '{nameof(Status_Information)}' type not same with length of '{nameof(MainStatus)}' enum!", 
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
