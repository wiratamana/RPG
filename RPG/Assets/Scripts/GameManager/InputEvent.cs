using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class InputEvent : SingletonMonobehaviour<InputEvent>
    {
        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            var go = new GameObject(nameof(InputEvent));
            DontDestroyOnLoad(go);
            go.AddComponent<InputEvent>();
        }

        public const char ACTION_PICK_UP_ITEM = 'E';
        public const char ACTION_OPEN_MENU_INVENTORY = 'B';

        public EventManager Event_PickUpItem { private set; get; } = new EventManager();
        public EventManager Event_OpenMenuInventory { private set; get; } = new EventManager();

        public EventManager Event_DoAttackLight { private set; get; } = new EventManager();
        public EventManager Event_DoAttackHeavy { private set; get; } = new EventManager();

        public EventManager Event_Holster { private set; get; } = new EventManager();
        public EventManager Event_Equip { private set; get; } = new EventManager();

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E) == true)
            {
                Event_PickUpItem.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.B) == true)
            {
                Event_OpenMenuInventory.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Space) == true)
            {
                Event_Holster.Invoke();
                Event_Equip.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Mouse0) == true)
            {
                Event_DoAttackLight.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Mouse1) == true)
            {
                Event_DoAttackHeavy.Invoke();
            }
        }
    }
}
