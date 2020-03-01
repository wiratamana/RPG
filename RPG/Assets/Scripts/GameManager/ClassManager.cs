using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

namespace Tamana
{
    public sealed class ClassManager : SingletonMonobehaviour<ClassManager>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void CreateInstance()
        {
            Debug.Log($"RuntimeInitializeOnLoadMethod - {nameof(ClassManager)}");

            var go = new GameObject(nameof(ClassManager));
            go.AddComponent<ClassManager>();
            DontDestroyOnLoad(go);
        }

        private static System.Type[] _types;
        public static System.Type[] Types
        {
            get
            {
                if (_types == null)
                {
                    Assembly asm = Assembly.GetExecutingAssembly();
                    _types = asm.GetTypes();
                }

                return _types;
            }            
        }
        //--------------------------------------------------------------------------------------
        private static Dictionary<string, List<System.Type>> filteredAttribute;
        public static List<System.Type> GetAttributesFromClass<T>() where T : System.Attribute
        {
            if(filteredAttribute == null)
            {
                filteredAttribute = new Dictionary<string, List<System.Type>>();
            }

            var nameofT = typeof(T).Name;
            if(filteredAttribute.ContainsKey(nameofT) == true)
            {
                if (filteredAttribute[nameofT].Count == 0)
                {
                    Debug.Log("Dictionary count is 0 !!", Debug.LogType.Warning);
                }

                return filteredAttribute[nameofT];
            }

            filteredAttribute.Add(nameofT, new List<System.Type>());
            foreach(var t in Types)
            {
                if(t is T || t.IsSubclassOf(typeof(T)) == true)
                {
                    filteredAttribute[nameofT].Add(t);
                }
            }

            if (filteredAttribute[nameofT].Count == 0)
            {
                Debug.Log("Dictionary count is 0 !!", Debug.LogType.Warning);
            }

            return filteredAttribute[nameofT];
        }
        //--------------------------------------------------------------------------------------
        private static Dictionary<string, Dictionary<string, System.Type>> filteredTypesDefinedWith;
        public static Dictionary<string, System.Type> GetTypesDefinedWith<T>() where T : System.Attribute
        {
            if(filteredTypesDefinedWith == null)
            {
                filteredTypesDefinedWith = new Dictionary<string, Dictionary<string, System.Type>>();
            }

            var nameofT = typeof(T).Name;

            if(filteredTypesDefinedWith.ContainsKey(nameofT) == true)
            {
                if (filteredTypesDefinedWith[nameofT].Count == 0)
                {
                    Debug.Log("Dictionary count is 0 !!", Debug.LogType.Warning);
                }

                return filteredTypesDefinedWith[nameofT];
            }

            filteredTypesDefinedWith.Add(nameofT, new Dictionary<string, System.Type>());
            foreach(var t in Types)
            {
                if(t.IsDefined(typeof(T)) == false)
                {
                    continue;
                }

                filteredTypesDefinedWith[nameofT].Add(t.Name, t);
            }

            if(filteredTypesDefinedWith[nameofT].Count == 0)
            {
                Debug.Log("Dictionary count is 0 !!", Debug.LogType.Warning);
            }

            return filteredTypesDefinedWith[nameofT];
        }

        public static Dictionary<string, TPC_Anim_AnimInfo<bool>> GetAllConstValueFromStaticClass<Attribute>(System.Type sources)
            where Attribute : System.Attribute
        {
            var constVars = sources.GetFields(BindingFlags.Public
                | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            var LAYER = "LAYER";
            var layerField = System.Array.Find(constVars, x => x.Name == LAYER)?.GetValue(null).ToString();
            if(string.IsNullOrEmpty(layerField))
            {
                Debug.Log($"Couldn't find const variable with name '{LAYER}' inside '{sources.FullName}' type.", Debug.LogType.ForceQuit);
                return null;
            }

            var returnValue = new Dictionary<string, TPC_Anim_AnimInfo<bool>>();

            foreach (var constVar in constVars)
            {
                if(constVar.IsDefined(typeof(TPC_Anim_AttributeNotAnim)) == true)
                {
                    continue;
                }

                var value = new TPC_Anim_AnimInfo<bool>(false, layerField);
                foreach (var t in GetAttributesFromClass<Attribute>())
                {
                    value.StateDic[t.Name] = constVar.IsDefined(t);
                }

                var fieldName = constVar.GetValue(null).ToString();
                returnValue.Add(fieldName, value);
            }

            return returnValue;
        }
    }
}

