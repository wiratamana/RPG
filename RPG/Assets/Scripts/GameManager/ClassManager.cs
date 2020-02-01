using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Tamana
{
    public sealed class ClassManager : SingletonMonobehaviour<ClassManager>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            var go = new GameObject(nameof(GameManager));
            go.AddComponent<GameManager>();
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

        private static List<System.Type> _animAttributes;
        public static List<System.Type> AnimAttributes
        {
            get
            {
                if (_animAttributes == null)
                {
                    _animAttributes = new List<System.Type>();

                    foreach (var t in Types)
                    {
                        if (t.IsSubclassOf(typeof(System.Attribute)) == false)
                        {
                            continue;
                        }

                        _animAttributes.Add(t);
                    }
                }

                return _animAttributes;
            }
        }
    }
}

