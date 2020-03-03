using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class InputEvent : SingletonMonobehaviour<InputEvent>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void CreateInstance()
        {
            Debug.Log($"RuntimeInitializeOnLoadMethod - {nameof(InputEvent)}");

            var go = new GameObject(nameof(InputEvent));
            DontDestroyOnLoad(go);
            go.AddComponent<InputEvent>();
        }

        public const char ACTION_PICK_UP_ITEM = 'E';
        public const char ACTION_OPEN_MENU_INVENTORY = 'B';

        public EventManager Event_PickUpItem { get; } = new EventManager();
        public EventManager Event_OpenOrCloseMenuInventory { private set; get; } = new EventManager();

        public EventManager Event_DoAttackLight { get; } = new EventManager();
        public EventManager Event_DoAttackHeavy { get; } = new EventManager();

        public EventManager Event_Holster { get; } = new EventManager();
        public EventManager Event_Equip { get; } = new EventManager();

        public EventManager Event_BeginMove { get; } = new EventManager();
        public EventManager Event_StopMove { get; } = new EventManager();
        public EventManager Event_Parry { get; } = new EventManager();
        public EventManager Event_Dodge { get; } = new EventManager();

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E) == true)
            {
                Event_PickUpItem.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.B) == true)
            {
                Event_OpenOrCloseMenuInventory.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Space) == true)
            {
                Event_Holster.Invoke();
                Event_Equip.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Mouse0) == true)
            {
                Event_DoAttackHeavy.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Mouse1) == true)
            {
                Event_Parry.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.W) == true)
            {
                Event_BeginMove.Invoke();
            }

            if(Input.GetKeyUp(KeyCode.W) == true)
            {
                Event_StopMove.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.LeftAlt) == true)
            {
                Event_Dodge.Invoke();
            }
        }
    }
}
