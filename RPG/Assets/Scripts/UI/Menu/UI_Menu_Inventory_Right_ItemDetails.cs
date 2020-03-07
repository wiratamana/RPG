using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Right_ItemDetails : MonoBehaviour
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

        public const float HEIGHT = 200.0f;
        public const float POSITION_Y_OFFSET = 50.0f;
        public const float CONTENT_OFFSET = 10.0f;

        public EventManager OnBecomeDeactiveEvent { private set; get; } = new EventManager();
        public EventManager OnBecomeActiveEvent { private set; get; } = new EventManager();

        public UI_Menu_Inventory_Right_ItemDetails_Background Background { private set; get; }
        public UI_Menu_Inventory_Right_ItemDetails_Description Description { private set; get; }
        public UI_Menu_Inventory_Right_ItemDetails_Effect Effect { private set; get; }
        public UI_Menu_Inventory_Right_ItemDetails_Name Name { private set; get; }

        private void Awake()
        {
            // ===============================================================================================
            // Background
            // ===============================================================================================
            var backgroundGO = new GameObject(nameof(UI_Menu_Inventory_Right_ItemDetails_Background));
            var backgroundRT = backgroundGO.AddComponent<RectTransform>();
            backgroundRT.SetParent(RectTransform);
            backgroundRT.localPosition = Vector3.zero;
            backgroundRT.sizeDelta = RectTransform.sizeDelta;
            Background = backgroundGO.AddComponent<UI_Menu_Inventory_Right_ItemDetails_Background>();

            // ===============================================================================================
            // Name
            // ===============================================================================================
            var nameGO = new GameObject(nameof(UI_Menu_Inventory_Right_ItemDetails_Name));
            var nameRT = nameGO.AddComponent<RectTransform>();
            nameRT.SetParent(RectTransform);
            nameRT.sizeDelta = new Vector2(RectTransform.sizeDelta.x - (CONTENT_OFFSET * 2), RectTransform.sizeDelta.y * 0.2f);
            nameRT.localPosition = new Vector3(0, (RectTransform.sizeDelta.y * 0.5f) -
                (nameRT.sizeDelta.y * 0.5f) - CONTENT_OFFSET * 2);
            Name = nameGO.AddComponent<UI_Menu_Inventory_Right_ItemDetails_Name>();

            // ===============================================================================================
            // Effect
            // ===============================================================================================
            var effectGO = new GameObject(nameof(UI_Menu_Inventory_Right_ItemDetails_Effect));
            var effectRT = effectGO.AddComponent<RectTransform>();
            effectRT.SetParent(RectTransform);
            effectRT.sizeDelta = new Vector2(RectTransform.sizeDelta.x - (CONTENT_OFFSET * 2), RectTransform.sizeDelta.y * 0.2f);
            effectRT.localPosition = new Vector3(0, nameRT.localPosition.y - (nameRT.sizeDelta.y * 0.5f) -
                (effectRT.sizeDelta.y * 0.5f));
            Effect = effectGO.AddComponent<UI_Menu_Inventory_Right_ItemDetails_Effect>();

            // ===============================================================================================
            // Description
            // ===============================================================================================
            var descriptionGO = new GameObject(nameof(UI_Menu_Inventory_Right_ItemDetails_Description));
            var descriptionRT = descriptionGO.AddComponent<RectTransform>();
            descriptionRT.SetParent(RectTransform);
            descriptionRT.localPosition = Vector3.zero;
            Description = descriptionGO.AddComponent<UI_Menu_Inventory_Right_ItemDetails_Description>();
            Description.InitPosition(true);

            Deactivate();

            UI_Menu_Inventory.OnMenuInventoryClosed.AddListener(Deactivate);
        }

        public void Activate(Item_Base item)
        {
            OnBecomeActiveEvent.Invoke();
            gameObject.SetActive(true);
            SetItemDetails(item.ItemDetails);
        }

        public void Deactivate()
        {
            OnBecomeDeactiveEvent.Invoke();
            gameObject.SetActive(false);
        }

        private void SetItemDetails(Item_ItemDetails itemDetails)
        {
            Name.ItemName.text = itemDetails.ItemName;
            Description.SetItemDescription(itemDetails.ItemDescription);

            var isWithoutEffect = itemDetails.ItemEffects == null || itemDetails.ItemEffects.Length == 0;
            Description.InitPosition(isWithoutEffect);

            if(isWithoutEffect == false)
            {
                Effect.SetEffects(itemDetails.ItemEffects);
            }
        }
    }
}
