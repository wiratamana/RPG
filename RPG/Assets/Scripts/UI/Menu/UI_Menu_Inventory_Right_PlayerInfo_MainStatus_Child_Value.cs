﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace Tamana
{
    public class UI_Menu_Inventory_Right_PlayerInfo_MainStatus_Child_Value : MonoBehaviour
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
        public TextMeshProUGUI Value { private set; get; }

        private void Awake()
        {
            Init();

            UI_Menu_Inventory.OnMenuInventoryOpened.AddListener(Init, GetInstanceID());
            UI_Menu_Inventory.OnMenuInventoryClosed.AddListener(ReturnToPool, GetInstanceID());
        }

        private void Init()
        {
            Background = UI_Pool.Instance.GetImage(RectTransform, (int)RectTransform.sizeDelta.x, (int)RectTransform.sizeDelta.y,
                nameof(Background));
            Background.rectTransform.sizeDelta = RectTransform.sizeDelta;
            Background.rectTransform.localPosition = Vector3.zero;
            Background.color = Color.black;

            Value = UI_Pool.Instance.GetText(RectTransform, 0, 0, null, nameof(Background));
            Value.rectTransform.sizeDelta = RectTransform.sizeDelta - new Vector2(20, 10);
            Value.rectTransform.localPosition = Vector2.zero;
            Value.text = GameManager.PlayerStatus.GetStatus((MainStatus)System.Enum.Parse(typeof(MainStatus), transform.parent.name)).ToString();
            Value.color = Color.white;
            Value.fontSize = Value.rectTransform.sizeDelta.y;
            Value.alignment = TextAlignmentOptions.MidlineLeft;
        }

        private void ReturnToPool()
        {
            UI_Pool.Instance.RemoveImage(Background);
            UI_Pool.Instance.RemoveText(Value);

            Background = null;
            Value = null;
        }

        public void UpdateValue()
        {
            Value.text = GameManager.PlayerStatus.GetStatus((MainStatus)System.Enum.Parse(typeof(MainStatus), transform.parent.name)).ToString();
        }
    }
}
