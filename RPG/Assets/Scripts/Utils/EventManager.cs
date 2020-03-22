using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Text;

namespace Tamana
{
    public class EventManager
    {
        private Dictionary<string, UnityAction> callbacksDic;
        private Queue<(string, string, UnityAction)> listenerEditorQueue;
        private static readonly StringBuilder keyBuilder = new StringBuilder();
        private const string ADD_LISTENER = "ADD";
        private const string REMOVE_LISTENER = "REMOVE";
        private bool isInvoking = false;

        public EventManager()
        {
            callbacksDic = new Dictionary<string, UnityAction>();
            listenerEditorQueue = new Queue<(string, string, UnityAction)>();
        }

        public void AddListener(UnityAction callback)
        {
            AddListener(callback, null);
        }

        public void AddListener(UnityAction callback, object uniqueID)
        {
            var key = GetCallbackKey(callback, uniqueID);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == true)
            {
                Debug.Log($"Already contain key. key : {key}", Debug.LogType.Error);
                return;
            }

            if(isInvoking == true)
            {
                listenerEditorQueue.Enqueue((ADD_LISTENER, key, callback));
            }
            else
            {
                callbacksDic.Add(key, callback);
            }
        }

        public void RemoveListener(UnityAction callback)
        {
            RemoveListener(callback, null);
        }

        public void RemoveListener(UnityAction callback, object uniqueID)
        {
            var key = GetCallbackKey(callback, uniqueID);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == false)
            {
                Debug.Log($"Key not found!! key : {key}", Debug.LogType.Error);
                return;
            }

            if (isInvoking == true)
            {
                listenerEditorQueue.Enqueue((REMOVE_LISTENER, key, null));
            }
            else
            {
                callbacksDic.Remove(key);
            }
        }

        public void RemoveAllListener()
        {
            if(isInvoking == false)
            {
                callbacksDic.Clear();
                return;
            }

            foreach (var listener in callbacksDic)
            {
                listenerEditorQueue.Enqueue((REMOVE_LISTENER, listener.Key, null));
            }
        }

        public void Invoke()
        {
            isInvoking = true;

            while (listenerEditorQueue.Count > 0)
            {
                var listener = listenerEditorQueue.Dequeue();

                if (listener.Item1 == ADD_LISTENER)
                {
                    callbacksDic.Add(listener.Item2, listener.Item3);
                }

                else if (listener.Item1 == REMOVE_LISTENER)
                {
                    callbacksDic.Remove(listener.Item2);
                }
            }

            foreach (var listener in callbacksDic)
            {
                try
                {
                    listener.Value.Invoke();
                }
                catch (System.Exception e)
                {
                    Debug.Log($"Key : {listener.Key}", Debug.LogType.Error);
                    Debug.Log($"Message : {e.Message}", Debug.LogType.Error);
                    Debug.Log($"Stack Trace : {e.StackTrace}", Debug.LogType.Error);
                }
            }

            while (listenerEditorQueue.Count > 0)
            {
                var listener = listenerEditorQueue.Dequeue();

                if (listener.Item1 == ADD_LISTENER)
                {
                    callbacksDic.Add(listener.Item2, listener.Item3);
                }

                else if (listener.Item1 == REMOVE_LISTENER)
                {
                    callbacksDic.Remove(listener.Item2);
                }
            }

            isInvoking = false;
        }

        private static string GetCallbackKey(UnityAction callback, object uniqueID = null)
        {
            if (callback == null)
            {
                return null;
            }

            var declaringTypeFullName = callback.Method.DeclaringType.FullName;
            var methodName = callback.Method.Name;
            var separator = '.';

            if (uniqueID != null && string.IsNullOrEmpty(uniqueID.ToString()) == false)
            {
                keyBuilder.Append(uniqueID);
                keyBuilder.Append(separator);
            }
            keyBuilder.Append($"{declaringTypeFullName}.{methodName}");

            var result = keyBuilder.ToString();
            keyBuilder.Clear();

            return result;
        }
    }

    public class EventManager<T>
    {
        private Dictionary<string, UnityAction<T>> callbacksDic;
        private Queue<(string, string, UnityAction<T>)> listenerEditorQueue;
        private static readonly StringBuilder keyBuilder = new StringBuilder();
        private const string ADD_LISTENER = "ADD";
        private const string REMOVE_LISTENER = "REMOVE";
        private bool isInvoking = false;

        public EventManager()
        {
            callbacksDic = new Dictionary<string, UnityAction<T>>();
            listenerEditorQueue = new Queue<(string, string, UnityAction<T>)>();
        }

        public void AddListener(UnityAction<T> callback)
        {
            AddListener(callback, null);
        }

        public void AddListener(UnityAction<T> callback, object uniqueID)
        {
            var key = GetCallbackKey(callback, uniqueID);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == true)
            {
                Debug.Log($"Already contain key. key : {key}", Debug.LogType.Error);
                return;
            }

            if (isInvoking == true)
            {
                listenerEditorQueue.Enqueue((ADD_LISTENER, key, callback));
            }
            else
            {
                callbacksDic.Add(key, callback);
            }
        }

        public void RemoveListener(UnityAction<T> callback)
        {
            RemoveListener(callback, null);
        }

        public void RemoveListener(UnityAction<T> callback, object uniqueID)
        {
            var key = GetCallbackKey(callback, uniqueID);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == false)
            {
                Debug.Log($"Key not found!! key : {key}", Debug.LogType.Error);
                return;
            }

            if (isInvoking == true)
            {
                listenerEditorQueue.Enqueue((REMOVE_LISTENER, key, null));
            }
            else
            {
                callbacksDic.Remove(key);
            }
        }

        public void RemoveAllListener()
        {
            if (isInvoking == false)
            {
                callbacksDic.Clear();
                return;
            }

            foreach (var listener in callbacksDic)
            {
                listenerEditorQueue.Enqueue((REMOVE_LISTENER, listener.Key, null));
            }
        }

        public void Invoke(T param)
        {
            isInvoking = true;

            while (listenerEditorQueue.Count > 0)
            {
                var listener = listenerEditorQueue.Dequeue();

                if (listener.Item1 == ADD_LISTENER)
                {
                    callbacksDic.Add(listener.Item2, listener.Item3);
                }

                else if (listener.Item1 == REMOVE_LISTENER)
                {
                    callbacksDic.Remove(listener.Item2);
                }
            }

            foreach (var listener in callbacksDic)
            {
                try
                {
                    listener.Value.Invoke(param);
                }
                catch (System.Exception e)
                {
                    Debug.Log($"Key : {listener.Key}", Debug.LogType.Error);
                    Debug.Log($"Message : {e.Message}", Debug.LogType.Error);
                    Debug.Log($"Stack Trace : {e.StackTrace}", Debug.LogType.Error);
                }
            }

            while (listenerEditorQueue.Count > 0)
            {
                var listener = listenerEditorQueue.Dequeue();

                if (listener.Item1 == ADD_LISTENER)
                {
                    callbacksDic.Add(listener.Item2, listener.Item3);
                }

                else if (listener.Item1 == REMOVE_LISTENER)
                {
                    callbacksDic.Remove(listener.Item2);
                }
            }

            isInvoking = false;
        }

        private static string GetCallbackKey(UnityAction<T> callback, object uniqueID = null)
        {
            if (callback == null)
            {
                return null;
            }

            var declaringTypeFullName = callback.Method.DeclaringType.FullName;
            var methodName = callback.Method.Name;
            var separator = '.';

            if (uniqueID != null && string.IsNullOrEmpty(uniqueID.ToString()) == false)
            {
                keyBuilder.Append(uniqueID);
                keyBuilder.Append(separator);
            }
            keyBuilder.Append($"{declaringTypeFullName}.{methodName}");

            var result = keyBuilder.ToString();
            keyBuilder.Clear();

            return result;
        }
    }

    public class EventManager<T0, T1>
    {
        private Dictionary<string, UnityAction<T0, T1>> callbacksDic;
        private Queue<(string, string, UnityAction<T0, T1>)> listenerEditorQueue;
        private static readonly StringBuilder keyBuilder = new StringBuilder();
        private const string ADD_LISTENER = "ADD";
        private const string REMOVE_LISTENER = "REMOVE";
        private bool isInvoking = false;

        public EventManager()
        {
            callbacksDic = new Dictionary<string, UnityAction<T0, T1>>();
            listenerEditorQueue = new Queue<(string, string, UnityAction<T0, T1>)>();
        }

        public void AddListener(UnityAction<T0, T1> callback)
        {
            AddListener(callback, null);
        }

        public void AddListener(UnityAction<T0, T1> callback, object uniqueID)
        {
            var key = GetCallbackKey(callback, uniqueID);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == true)
            {
                Debug.Log($"Already contain key. key : {key}", Debug.LogType.Error);
                return;
            }

            if (isInvoking == true)
            {
                listenerEditorQueue.Enqueue((ADD_LISTENER, key, callback));
            }
            else
            {
                callbacksDic.Add(key, callback);
            }
        }

        public void RemoveListener(UnityAction<T0, T1> callback)
        {
            RemoveListener(callback, null);
        }

        public void RemoveListener(UnityAction<T0, T1> callback, object uniqueID)
        {
            var key = GetCallbackKey(callback, uniqueID);
            if (string.IsNullOrEmpty(key) == true || callbacksDic.ContainsKey(key) == false)
            {
                Debug.Log($"Key not found!! key : {key}", Debug.LogType.Error);
                return;
            }

            if (isInvoking == true)
            {
                listenerEditorQueue.Enqueue((REMOVE_LISTENER, key, null));
            }
            else
            {
                callbacksDic.Remove(key);
            }
        }

        public void RemoveAllListener()
        {
            if (isInvoking == false)
            {
                callbacksDic.Clear();
                return;
            }

            foreach (var listener in callbacksDic)
            {
                listenerEditorQueue.Enqueue((REMOVE_LISTENER, listener.Key, null));
            }
        }

        public void Invoke(T0 param0, T1 param1)
        {
            isInvoking = true;

            while (listenerEditorQueue.Count > 0)
            {
                var listener = listenerEditorQueue.Dequeue();

                if (listener.Item1 == ADD_LISTENER)
                {
                    callbacksDic.Add(listener.Item2, listener.Item3);
                }

                else if (listener.Item1 == REMOVE_LISTENER)
                {
                    callbacksDic.Remove(listener.Item2);
                }
            }

            foreach (var listener in callbacksDic)
            {
                try
                {
                    listener.Value.Invoke(param0, param1);
                }
                catch (System.Exception e)
                {
                    Debug.Log($"Key : {listener.Key}", Debug.LogType.Error);
                    Debug.Log($"Message : {e.Message}", Debug.LogType.Error);
                    Debug.Log($"Stack Trace : {e.StackTrace}", Debug.LogType.Error);
                }
            }

            while (listenerEditorQueue.Count > 0)
            {
                var listener = listenerEditorQueue.Dequeue();

                if (listener.Item1 == ADD_LISTENER)
                {
                    callbacksDic.Add(listener.Item2, listener.Item3);
                }

                else if (listener.Item1 == REMOVE_LISTENER)
                {
                    callbacksDic.Remove(listener.Item2);
                }
            }

            isInvoking = false;
        }

        private static string GetCallbackKey(UnityAction<T0, T1> callback, object uniqueID = null)
        {
            if (callback == null)
            {
                return null;
            }

            var declaringTypeFullName = callback.Method.DeclaringType.FullName;
            var methodName = callback.Method.Name;
            var separator = '.';

            if (uniqueID != null && string.IsNullOrEmpty(uniqueID.ToString()) == false)
            {
                keyBuilder.Append(uniqueID);
                keyBuilder.Append(separator);
            }
            keyBuilder.Append($"{declaringTypeFullName}.{methodName}");

            var result = keyBuilder.ToString();
            keyBuilder.Clear();

            return result;
        }
    }
}

