using UnityEngine;
using System.Collections;
using TMPro;
using System.Text;

namespace Tamana
{
    public class UI_Menu_Inventory_Right_ItemDetails_Description : MonoBehaviour
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

        private UI_Menu_Inventory_Right_ItemDetails itemDetails;
        public UI_Menu_Inventory_Right_ItemDetails ItemDetails
        {
            get
            {
                if (itemDetails == null)
                {
                    itemDetails = transform.parent.GetComponent<UI_Menu_Inventory_Right_ItemDetails>();
                }

                return itemDetails;
            }
        }

        public TextMeshProUGUI ItemDescription { private set; get; }

        private void Awake()
        {
            Init();

            UI_Menu_Inventory.OnMenuInventoryOpened.AddListener(Init);
            UI_Menu_Inventory.OnMenuInventoryClosed.AddListener(ReturnToPool);
        }

        private void Init()
        {
            ItemDescription = UI_Menu_Pool.Instance.GetText(RectTransform, 0, 0, "", nameof(ItemDescription));
            ItemDescription.rectTransform.localPosition = Vector3.zero;
            ItemDescription.rectTransform.anchorMax = Vector2.one;
            ItemDescription.rectTransform.anchorMin = Vector2.zero;
            ItemDescription.rectTransform.offsetMax = new Vector2(-10, 0);
            ItemDescription.rectTransform.offsetMin = new Vector2(10, 0);
            ItemDescription.alignment = TextAlignmentOptions.TopLeft;
            ItemDescription.fontSize = 24;
        }

        public void ReturnToPool()
        {
            UI_Menu_Pool.Instance.RemoveText(ItemDescription);

            ItemDescription = null;
        }

        public void SetItemDescription(string itemDescription)
        {
            if(string.IsNullOrEmpty(itemDescription) == true)
            {
                Debug.Log("Item description is null or empty!!");
            }

            StartCoroutine(PlayItemDescription(itemDescription));
        }

        private IEnumerator PlayItemDescription(string itemDescription)
        {
            var delay = new WaitForSecondsRealtime(0.5f / itemDescription.Length);
            var textToShow = new StringBuilder();
            ItemDescription.text = string.Empty;

            foreach (var c in itemDescription)
            {
                textToShow.Append(c);
                ItemDescription.text = textToShow.ToString();

                yield return delay;
            }
        }

        public void InitPosition(bool isWithoutEffect)
        {
            // ===============================================================================================
            // Variables declaration
            // ===============================================================================================
            var itemDetailsRT = ItemDetails.RectTransform;
            float botPos = itemDetailsRT.position.y - (itemDetailsRT.sizeDelta.y * 0.5f);
            float topPos;
            float ySize;
            float offset = UI_Menu_Inventory_Right_ItemDetails.CONTENT_OFFSET * 2;

            // ===============================================================================================
            // Get top
            // ===============================================================================================
            if (isWithoutEffect == true)
            {
                var nameRT = ItemDetails.Name.RectTransform;

                topPos = nameRT.position.y - (nameRT.sizeDelta.y * 0.5f);
            }
            else
            {
                var effectRT = ItemDetails.Effect.RectTransform;

                topPos = effectRT.position.y - (effectRT.sizeDelta.y * 0.5f);
            }

            // ===============================================================================================
            // Calculate and set size
            // ===============================================================================================
            ySize = Mathf.Abs(topPos - botPos) - offset;
            RectTransform.sizeDelta = new Vector2(itemDetailsRT.sizeDelta.x - offset, ySize);

            // ===============================================================================================
            // Set position
            // ===============================================================================================
            if (isWithoutEffect == true)
            {
                var nameRT = ItemDetails.Name.RectTransform;
                RectTransform.position = nameRT.position - new Vector3(0, (ySize * 0.5f) + offset);
            }
            else
            {
                var effectRT = ItemDetails.Effect.RectTransform;
                RectTransform.position = effectRT.position - new Vector3(0, (ySize * 0.5f) + (effectRT.sizeDelta.y * 0.5f));
            }
        }
    }
}
