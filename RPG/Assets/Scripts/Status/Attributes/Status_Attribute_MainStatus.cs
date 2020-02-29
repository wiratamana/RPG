using System;

namespace Tamana
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Status_Attribute_MainStatus : Attribute
    {
        public MainStatus MainStatus { private set; get; }

        public Status_Attribute_MainStatus(MainStatus mainStatus)
        {
            MainStatus = mainStatus;
        }
    }
}
