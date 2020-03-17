using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class InputEvent : MonoBehaviour
    {
        private static InputEvent instance;
        public static InputEvent Instance
        {
            get
            {
                if (instance == null)
                {
                    var go = new GameObject(nameof(InputEvent));
                    DontDestroyOnLoad(go);
                    instance = go.AddComponent<InputEvent>();
                }

                return instance;
            }
        }

        protected void Awake()
        {
            SetCursorToInvisible();
        }

        private void Start()
        {
            var wira = Instance;
        }

        public void SetCursorToVisible()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        public void SetCursorToInvisible()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public const char ACTION_PICK_UP_ITEM = 'E';
        public const char ACTION_OPEN_MENU_INVENTORY = 'B';
        public const string ACTION_CLOSE_SHOP_MENU = "Esc";

        public EventManager Event_Chat { get; } = new EventManager();
        public EventManager Event_NextDialogue { get; } = new EventManager();

        public EventManager Event_CloseShop { get; } = new EventManager();

        public EventManager Event_PickUpItem { get; } = new EventManager();
        public EventManager Event_OpenOrCloseMenuInventory { private set; get; } = new EventManager();

        public EventManager Event_DoAttackHeavy { get; } = new EventManager();

        public EventManager Event_Holster { get; } = new EventManager();
        public EventManager Event_Equip { get; } = new EventManager();

        public EventManager<Direction> Event_BeginMove { get; } = new EventManager<Direction>();
        public EventManager<Direction> Event_StopMove { get; } = new EventManager<Direction>();
        public EventManager Event_Parry { get; } = new EventManager();
        public EventManager Event_Dodge { get; } = new EventManager();

        public EventManager Event_CatchEnemy { get; } = new EventManager();

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E) == true)
            {
                Event_PickUpItem.Invoke();
                Event_Chat.Invoke();
                Event_NextDialogue.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.B) == true)
            {
                Event_OpenOrCloseMenuInventory.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Escape) == true)
            {
                Event_CloseShop.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Space) == true)
            {
                Event_Holster.Invoke();
                Event_Equip.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Mouse0) == true)
            {
                Event_DoAttackHeavy.Invoke();
                Event_NextDialogue.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Mouse1) == true)
            {
                Event_Parry.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.W) == true)
            {
                Event_BeginMove.Invoke(Direction.Forward);
            }

            if(Input.GetKeyUp(KeyCode.W) == true)
            {
                Event_StopMove.Invoke(Direction.Forward);
            }

            if (Input.GetKeyDown(KeyCode.S) == true)
            {
                Event_BeginMove.Invoke(Direction.Backward);
            }

            if (Input.GetKeyUp(KeyCode.S) == true)
            {
                Event_StopMove.Invoke(Direction.Backward);
            }

            if (Input.GetKeyDown(KeyCode.A) == true)
            {
                Event_BeginMove.Invoke(Direction.Left);
            }

            if (Input.GetKeyUp(KeyCode.A) == true)
            {
                Event_StopMove.Invoke(Direction.Left);
            }

            if (Input.GetKeyDown(KeyCode.D) == true)
            {
                Event_BeginMove.Invoke(Direction.Right);
            }

            if (Input.GetKeyUp(KeyCode.D) == true)
            {
                Event_StopMove.Invoke(Direction.Right);
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt) == true)
            {
                Event_Dodge.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.Mouse2) == true)
            {
                Event_CatchEnemy.Invoke();
            }
        }
    }
}
