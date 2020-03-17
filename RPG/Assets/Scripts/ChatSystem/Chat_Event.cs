using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [CreateAssetMenu(fileName = "Event", menuName = "Create/Dialogue/Event")]
    public class Chat_Event : Chat_Base
    {
        public const string FIELD_EVENT = nameof(_event);
        public const string FIELD_PRODUCTS = nameof(products);
        public const string FIELD_RETURN_TO_OBJECT = nameof(returnToObjcet);
        public const string FIELD_RETURN_TO_INDEX = nameof(returnToIndex);

        public override ChatType Type => ChatType.Event;
        [SerializeField] private ChatEvent _event;
        public ChatEvent Event => _event;

        [SerializeField] private Item_ShopProducts products;
        [SerializeField] private Chat_Object returnToObjcet;
        [SerializeField] private int returnToIndex;

        public T GetEventObject<T>() where T : ScriptableObject
        {
            switch (_event)
            {
                case ChatEvent.Shop:        return products as T;
                case ChatEvent.Blacksmith:  return null;
                case ChatEvent.ReturnTo:    return CreateInstance<Chat_ReturnTo>().Initialize(returnToObjcet, returnToIndex) as T;
                default:                    return null;
            }
        }
    }
}