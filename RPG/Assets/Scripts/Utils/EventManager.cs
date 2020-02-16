using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Tamana
{
    public class EventManager
    {
        private Dictionary<string, UnityAction> callbacksDic;

        public EventManager()
        {
            callbacksDic = new Dictionary<string, UnityAction>();
        }

        public void AddListener(UnityAction callback)
        {
            var key = GetCallbackKey(callback);
            if(string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == true)
            {
                return;
            }

            callbacksDic.Add(key, callback);
        }

        public void RemoveListener(UnityAction callback)
        {
            var key = GetCallbackKey(callback);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == false)
            {
                return;
            }

            callbacksDic.Remove(key);
        }

        public void RemoveAllListener()
        {
            callbacksDic.Clear();
        }

        public void Invoke()
        {
            try
            {
                foreach (var cb in callbacksDic)
                {
                    try
                    {
                        cb.Value?.Invoke();
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log($"Method : {cb.Key}", Debug.LogType.Error);
                        Debug.Log($"Message : {e.Message}", Debug.LogType.Error);
                        Debug.Log($"Stack Trace : {e.StackTrace}", Debug.LogType.Error);
                    }

                }
            }
            catch (System.InvalidOperationException)
            {
                Invoke();
            }            
        }

        private string GetCallbackKey(UnityAction callback)
        {
            if(callback == null)
            {
                return null;
            }

            var declaringTypeFullName = callback.Method.DeclaringType.FullName;
            var methodName = callback.Method.Name;

            return $"{declaringTypeFullName}.{methodName}";
        }
    }
}

