using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Tamana
{
    public class EventManager
    {
        private Dictionary<string, UnityAction> callbacksDic;
        private Queue<UnityAction> invokes;

        public EventManager()
        {
            callbacksDic = new Dictionary<string, UnityAction>();
            invokes = new Queue<UnityAction>();
        }

        public void AddListener(UnityAction callback)
        {
            var key = GetCallbackKey(callback);
            if(string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == true)
            {
                Debug.Log($"Already contain key. key : {key}");
                return;
            }

            callbacksDic.Add(key, callback);
        }

        public void AddListener(UnityAction callback, object uniqueID)
        {
            var key = $"{uniqueID}.{GetCallbackKey(callback)}";
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == true)
            {
                Debug.Log($"Already contain key. key : {key}");
                return;
            }

            callbacksDic.Add(key, callback);
        }

        public void RemoveListener(UnityAction callback)
        {
            var key = GetCallbackKey(callback);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == false)
            {
                Debug.Log($"Key not found!! key : {key}", Debug.LogType.Error);
                return;
            }

            callbacksDic.Remove(key);
        }

        public void RemoveListener(UnityAction callback, object uniqueID)
        {
            var key = $"{uniqueID}.{GetCallbackKey(callback)}";
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == false)
            {
                Debug.Log($"Key not found!! key : {key}", Debug.LogType.Error);
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
            foreach(var cb in callbacksDic)
            {
                invokes.Enqueue(cb.Value);
            }


            while(invokes.Count > 0)
            {
                var cb = invokes.Dequeue();

                try
                {
                    cb?.Invoke();
                }
                catch (System.Exception e)
                {
                    Debug.Log($"Message : {e.Message}", Debug.LogType.Error);
                    Debug.Log($"Stack Trace : {e.StackTrace}", Debug.LogType.Error);
                }
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

    public class EventManager<T>
    {
        private Dictionary<string, UnityAction<T>> callbacksDic;
        private Queue<UnityAction<T>> invokes;

        public EventManager()
        {
            callbacksDic = new Dictionary<string, UnityAction<T>>();
            invokes = new Queue<UnityAction<T>>();
        }

        public void AddListener(UnityAction<T> callback)
        {
            var key = GetCallbackKey(callback);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == true)
            {
                Debug.Log($"Already contain key. key : {key}", Debug.LogType.Error);
                return;
            }

            callbacksDic.Add(key, callback);
        }

        public void AddListener(UnityAction<T> callback, object uniqueID)
        {
            var key = $"{uniqueID}.{GetCallbackKey(callback)}";
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == true)
            {
                Debug.Log($"Already contain key. key : {key}", Debug.LogType.Error);
                return;
            }

            callbacksDic.Add(key, callback);
        }

        public void RemoveListener(UnityAction<T> callback)
        {
            var key = GetCallbackKey(callback);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == false)
            {
                Debug.Log($"Key not found!! key : {key}", Debug.LogType.Error);
                return;
            }

            callbacksDic.Remove(key);
        }

        public void RemoveListener(UnityAction<T> callback, object uniqueID)
        {
            var key = $"{uniqueID}.{GetCallbackKey(callback)}";
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == false)
            {
                Debug.Log($"Key not found!! key : {key}", Debug.LogType.Error);
                return;
            }

            callbacksDic.Remove(key);
        }

        public void RemoveAllListener()
        {
            callbacksDic.Clear();
        }

        public void Invoke(T param)
        {
            foreach(var cb in callbacksDic)
            {
                invokes.Enqueue(cb.Value);
            }


            while (invokes.Count > 0)
            {
                var cb = invokes.Dequeue();

                try
                {
                    cb?.Invoke(param);
                }
                catch (System.Exception e)
                {
                    Debug.Log($"Message : {e.Message}", Debug.LogType.Error);
                    Debug.Log($"Stack Trace : {e.StackTrace}", Debug.LogType.Error);
                }
            }
        }

        private string GetCallbackKey(UnityAction<T> callback)
        {
            if (callback == null)
            {
                return null;
            }

            var declaringTypeFullName = callback.Method.DeclaringType.FullName;
            var methodName = callback.Method.Name;

            return $"{declaringTypeFullName}.{methodName}";
        }
    }

    public class EventManager<T0, T1>
    {
        private Dictionary<string, UnityAction<T0, T1>> callbacksDic;
        private Queue<UnityAction<T0, T1>> invokes;

        public EventManager()
        {
            callbacksDic = new Dictionary<string, UnityAction<T0, T1>>();
            invokes = new Queue<UnityAction<T0, T1>>();
        }

        public void AddListener(UnityAction<T0, T1> callback)
        {
            var key = GetCallbackKey(callback);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == true)
            {
                Debug.Log($"Already contain key. key : {key}", Debug.LogType.Error);
                return;
            }

            callbacksDic.Add(key, callback);
        }

        public void AddListener(UnityAction<T0, T1> callback, object uniqueID)
        {
            var key = $"{uniqueID}.{GetCallbackKey(callback)}";
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == true)
            {
                Debug.Log($"Already contain key. key : {key}", Debug.LogType.Error);
                return;
            }

            callbacksDic.Add(key, callback);
        }

        public void RemoveListener(UnityAction<T0, T1> callback)
        {
            var key = GetCallbackKey(callback);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == false)
            {
                Debug.Log($"Key not found!! key : {key}", Debug.LogType.Error);
                return;
            }

            callbacksDic.Remove(key);
        }

        public void RemoveListener(UnityAction<T0, T1> callback, object uniqueID)
        {
            var key = $"{uniqueID}.{GetCallbackKey(callback)}";
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == false)
            {
                Debug.Log($"Key not found!! key : {key}", Debug.LogType.Error);
                return;
            }

            callbacksDic.Remove(key);
        }

        public void RemoveAllListener()
        {
            callbacksDic.Clear();
        }

        public void Invoke(T0 param0, T1 param1)
        {
            foreach (var cb in callbacksDic)
            {
                invokes.Enqueue(cb.Value);
            }


            while (invokes.Count > 0)
            {
                var cb = invokes.Dequeue();

                try
                {
                    cb?.Invoke(param0, param1);
                }
                catch (System.Exception e)
                {
                    Debug.Log($"Message : {e.Message}", Debug.LogType.Error);
                    Debug.Log($"Stack Trace : {e.StackTrace}", Debug.LogType.Error);
                }
            }
        }

        private string GetCallbackKey(UnityAction<T0, T1> callback)
        {
            if (callback == null)
            {
                return null;
            }

            var declaringTypeFullName = callback.Method.DeclaringType.FullName;
            var methodName = callback.Method.Name;

            return $"{declaringTypeFullName}.{methodName}";
        }
    }
}

