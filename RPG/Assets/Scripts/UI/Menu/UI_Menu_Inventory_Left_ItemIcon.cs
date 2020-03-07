using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Menu_Inventory_Left_ItemIcon : MonoBehaviour
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

        public UI_Menu_Inventory_Left_ItemIcon_Background Background { private set; get; }
        public UI_Menu_Inventory_Left_ItemIcon_Ring Ring { private set; get; }
        public UI_Menu_Inventory_Left_ItemIcon_Renderer ItemRenderer { private set; get; }
        public Item_Base Item { private set; get; }

        public void Init(Item_Base item)
        {
            Item = item;

            if(Background != null && Ring != null && ItemRenderer != null)
            {
                return;
            }

            // ===============================================================================================
            // Local variable declaration
            // ===============================================================================================
            var iconSize = 128;
            var ringSize = 120;
            var rawImageSize = 100;
            var renderTextureSize = 175;

            // ===============================================================================================
            // Create Background
            // ===============================================================================================
            var backgroundImg = UI_Menu_Pool.Instance.GetImage(RectTransform, iconSize, iconSize, nameof(Background));
            backgroundImg.rectTransform.localPosition = Vector2.zero;
            backgroundImg.raycastTarget = false;
            Background = backgroundImg.gameObject.AddComponent<UI_Menu_Inventory_Left_ItemIcon_Background>();

            // ===============================================================================================
            // Create Ring
            // ===============================================================================================
            var ringImg = UI_Menu_Pool.Instance.GetImage(RectTransform, ringSize, ringSize, nameof(Ring));
            ringImg.rectTransform.localPosition = Vector2.zero;
            ringImg.sprite = UI_Menu.Instance.MenuResources.InventoryItemIconRing_Sprite;
            ringImg.raycastTarget = false;
            Ring = ringImg.gameObject.AddComponent<UI_Menu_Inventory_Left_ItemIcon_Ring>();

            // ===============================================================================================
            // Create RawTexture to render the item
            // ===============================================================================================
            var itemImage = UI_Menu_Pool.Instance.GetRawImage(RectTransform, rawImageSize, rawImageSize, nameof(ItemRenderer));
            itemImage.rectTransform.localPosition = Vector2.zero;
            itemImage.color = Color.white;
            itemImage.raycastTarget = true;
            itemImage.texture = new RenderTexture(renderTextureSize, renderTextureSize, 16, RenderTextureFormat.ARGBHalf);
            ItemRenderer = itemImage.gameObject.AddComponent<UI_Menu_Inventory_Left_ItemIcon_Renderer>();   
        }

        private void Start()
        {
            ItemRenderer.OnMouseEnter.AddListener(OnMouseEnter);
            ItemRenderer.OnMouseExit.AddListener(OnMouseExit);
            
            ItemRenderer.OnMouseLeftClick.AddListener(OpenItemOption);
        }

        private void OnMouseEnter()
        {
            UI_Menu_Selection.CreateInstance(RectTransform, 32);
        }

        private void OnMouseExit()
        {
            UI_Menu_Selection.DestroyInstance();
        }

        public void ReturnToPool()
        {
            Destroy(Background);
            Destroy(ItemRenderer);
            Destroy(Ring);

            UI_Menu_Pool.Instance.RemoveImage(Background.Background);
            UI_Menu_Pool.Instance.RemoveImage(Ring.Ring);
            UI_Menu_Pool.Instance.RemoveRawImage(ItemRenderer.RawImage);

            Destroy(gameObject);
        }

        private void OpenItemOption()
        {
            // ===============================================================================================
            // Clear current registerd generic menu.
            // ===============================================================================================
            UI_Menu_Inventory_ItemOption.Instance.ClearRegisteredGenericMenu();

            // ===============================================================================================
            // Create Equip and Cancel generic menu instance.
            // ===============================================================================================
            var equip = UI_Menu_Inventory_ItemOption_GenericMenu.CreateGenericMenu("Equip");
            var cancel = UI_Menu_Inventory_ItemOption_GenericMenu.CreateGenericMenu("Cancel");

            // ===============================================================================================
            // Register on equip callback
            // ===============================================================================================
            var equipment = ItemRenderer.ItemPreview.ItemBase as Item_Equipment;
            var isEquipped = GameManager.Player.Equipment.IsCurrentlyEquipped(equipment);
            if (isEquipped == true)
            {
                equip.Text.text = "Unequip";
                equip.OnMouseLeftClick.AddListener(equipment.Unequip);
                equip.OnMouseLeftClick.AddListener(UnregisterThisItemIcon);
            }
            else
            {
                equip.OnMouseLeftClick.AddListener(equipment.Equip);
                equip.OnMouseLeftClick.AddListener(RegisterThisItemIcon);
            }

            if(GameManager.Player.UnitAnimator.Params.IsInCombatState == false)
            {
                equip.OnMouseLeftClick.AddListener(Ring.UpdateColor);
                equip.OnMouseLeftClick.AddListener(Background.UpdateColor);
                equip.OnMouseLeftClick.AddListener(UI_Menu_Inventory_ItemOption.Instance.Close);
                equip.OnMouseLeftClick.AddListener(equip.OnMouseLeftClick.RemoveAllListener);
            }      
            
            else
            {
                equip.OnMouseLeftClick.RemoveListener(equipment.Unequip);
                equip.OnMouseLeftClick.RemoveListener(equipment.Equip);

                equip.Text.color = Color.red;
                equip.Ring.color = Color.red;
            }

            // ===============================================================================================
            // Register on cancel callback
            // ===============================================================================================
            cancel.OnMouseLeftClick.AddListener(UI_Menu_Inventory_ItemOption.Instance.Close);
            cancel.OnMouseLeftClick.AddListener(cancel.OnMouseLeftClick.RemoveAllListener);

            // ===============================================================================================
            // Register instantiated Equip and Cancel generic menu to ItemOption.
            // ===============================================================================================
            UI_Menu_Inventory_ItemOption.Instance.RegisterGenericMenu(equip);
            UI_Menu_Inventory_ItemOption.Instance.RegisterGenericMenu(cancel);

            // ===============================================================================================
            // Open the menu.
            // ===============================================================================================
            UI_Menu_Inventory_ItemOption.Instance.Open(this);
        }

        private void RegisterThisItemIcon()
        {
            if(Item is Item_Armor)
            {
                var armor = Item as Item_Armor;
                UI_Menu_Inventory_Left_EquippedItemIcon.SetArmor(armor.Type, this);
            }
            else if (Item is Item_Attachment)
            {
                var attachment = Item as Item_Attachment;
                UI_Menu_Inventory_Left_EquippedItemIcon.SetAttachment(attachment.Type, this);
            }
            else if (Item is Item_Weapon)
            {
                UI_Menu_Inventory_Left_EquippedItemIcon.SetEquippedWeapon(this);
            }
        }

        private void UnregisterThisItemIcon()
        {
            if (Item is Item_Armor)
            {
                var armor = Item as Item_Armor;
                UI_Menu_Inventory_Left_EquippedItemIcon.SetArmor(armor.Type, null);
            }
            else if (Item is Item_Attachment)
            {
                var attachment = Item as Item_Attachment;
                UI_Menu_Inventory_Left_EquippedItemIcon.SetAttachment(attachment.Type, null);
            }
            else if (Item is Item_Weapon)
            {
                UI_Menu_Inventory_Left_EquippedItemIcon.SetEquippedWeapon(null);
            }
        }
    }
}
