using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

namespace Tamana
{
    public static class EventTriggerExtension
    {
        public static void AddListener(this EventTrigger eventTrigger, 
            EventTriggerType triggerType, UnityAction<BaseEventData> callback)
        {
            var entry = eventTrigger.triggers.Find(x => x.eventID == triggerType);
            if(entry == null)
            {
                entry = new EventTrigger.Entry();
                entry.eventID = triggerType;

                eventTrigger.triggers.Add(entry);
            }

            entry.callback.AddListener(callback);
        }

        public static void RemoveListener(this EventTrigger eventTrigger,
            EventTriggerType triggerType, UnityAction<BaseEventData> callback)
        {
            var entry = eventTrigger.triggers.Find(x => x.eventID == triggerType);
            entry.callback.RemoveListener(callback);
        }

        public static void RemoveEntry(this EventTrigger eventTrigger,
            EventTriggerType triggerType)
        {
            var entry = eventTrigger.triggers.Find(x => x.eventID == triggerType);

            if (entry != null)
            {
                entry.callback.RemoveAllListeners();
                entry.callback = null;
                eventTrigger.triggers.Remove(entry);
            }
        }
    }

}
