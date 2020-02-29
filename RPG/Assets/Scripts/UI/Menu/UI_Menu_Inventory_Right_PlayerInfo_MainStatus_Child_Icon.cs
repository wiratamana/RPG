using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Right_PlayerInfo_MainStatus_Child_Icon : MonoBehaviour
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        public Image Background { private set; get; }
        public Image Icon { private set; get; }

        private void Awake()
        {
            Init();

            UI_Menu_Inventory.OnMenuInventoryOpened.AddListener(Init, GetInstanceID());
            UI_Menu_Inventory.OnMenuInventoryClosed.AddListener(ReturnToPool, GetInstanceID());
        }

        private void Init()
        {
            var backgroundSize = new Vector2(UI_Menu_Inventory_Right_PlayerInfo_MainStatus.HEIGHT,
                UI_Menu_Inventory_Right_PlayerInfo_MainStatus.HEIGHT);
            var iconSize = backgroundSize - new Vector2(10, 10);
            var sprite = UI_Menu.Instance.MenuResources.GetMainStatusSprites((MainStatus)System.Enum.Parse(typeof(MainStatus),
                transform.parent.name));

            Background = UI_Menu_Pool.Instance.GetImage(RectTransform, (int)backgroundSize.x, (int)backgroundSize.y, nameof(Background));
            Background.rectTransform.localPosition = Vector3.zero;
            Background.color = Color.white;

            Icon = UI_Menu_Pool.Instance.GetImage(RectTransform, (int)iconSize.x, (int)iconSize.y, nameof(Icon));
            Icon.rectTransform.localPosition = Vector3.zero;
            Icon.color = Color.white;
            Icon.sprite = sprite;
        }

        private void ReturnToPool()
        {
            UI_Menu_Pool.Instance.RemoveImage(Background);
            UI_Menu_Pool.Instance.RemoveImage(Icon);

            Background = null;
            Icon = null;
        }
    }
}
