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
            var entry = new EventTrigger.Entry();

            entry.eventID = triggerType;
            entry.callback.AddListener(callback);

            eventTrigger.triggers.Add(entry);
        }        
    }

}
