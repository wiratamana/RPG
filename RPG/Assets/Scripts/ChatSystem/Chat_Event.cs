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

        public override ChatType Type => ChatType.Event;
        [SerializeField] private ChatEvent _event;
        public ChatEvent Event => _event;

        [SerializeField] private Item_ShopProducts products;

        public T GetEventObject<T>() where T : ScriptableObject
        {
            switch (_event)
            {
                case ChatEvent.Shop:        return products as T;
                case ChatEvent.Blacksmith:  return null;
                default:                    return null;
            }
        }
    }
}