using UnityEngine;
using System.Collections.Generic;

namespace Tamana
{
    public class ResourcesLoader : SingletonMonobehaviour<ResourcesLoader>
    {
        [SerializeField] private List<GameObject> _prefabs;

        private Dictionary<System.Type, GameObject> instantiatorDic;
        public const string ITEM_MODULAR_BODY_METADATA_PATH = "ItemModularBodyMetadata";
        public const string PLAYER_BASE_STATUS = "PlayerBaseStatus";

        protected override void Awake()
        {
            base.Awake();

            instantiatorDic = new Dictionary<System.Type, GameObject>();

            foreach(var p in _prefabs)
            {
                foreach(var i in ClassManager.GetTypesDefinedWith<GM_AttributeInstantiator>())
                {
                    if(instantiatorDic.ContainsKey(i.Value) == true)
                    {
                        continue;
                    }

                    if (p.GetComponent(i.Value) != null)
                    {
                        instantiatorDic.Add(i.Value, p);
                        continue;
                    }

                    if (p.GetComponentInChildren(i.Value) != null)
                    {
                        instantiatorDic.Add(i.Value, p);
                        continue;
                    }
                }
            }
        }

        public void InstantiatePrefabs<T>() where T : MonoBehaviour
        {
            foreach(var i in instantiatorDic)
            {
                var nameofT = typeof(T).Name;
                if (i.Key.Name == nameofT)
                {
                    Instantiate(i.Value);
                    return;
                }
            }
        }

        public T InstantiatePrefabWithReturnValue<T>() where T : MonoBehaviour
        {
            foreach (var i in instantiatorDic)
            {
                var nameofT = typeof(T).Name;
                if (i.Key.Name == nameofT)
                {
                    var obj = Instantiate(i.Value);
                    var component = obj.GetComponent<T>();
                    if(component == null)
                    {
                        component = obj.GetComponentInChildren<T>();
                    }

                    return component;
                }
            }

            return null;
        }

        public ArrayObject<Item_ModularBodyPart_Metadata> LoadModularBodyMetadata()
        {
            var wira = Resources.Load(ITEM_MODULAR_BODY_METADATA_PATH) as TextAsset;
            var json = wira.text;
            Resources.UnloadAsset(wira);

            var returnValue = JsonUtility.FromJson<ArrayObject<Item_ModularBodyPart_Metadata>>(json);
            return returnValue;
        }
        
        public Unit_Status_Information GetPlayerBaseStatus()
        {
            var playerBaseStatus = Resources.Load<Unit_Status_Information>(PLAYER_BASE_STATUS);
            var newInstance = Instantiate(playerBaseStatus);
            Resources.UnloadAsset(playerBaseStatus);

            return newInstance;
        }
    }
}
