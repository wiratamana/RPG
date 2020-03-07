using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Battle_TargetHP_Child : MonoBehaviour
    {
        private bool isInitialized = false;
        private Unit_AI_Hostile enemy;
        private Camera mainCamera;
        private UnityAction<int> deregisterOnDestroyAction;
        private int enemyInstanceID;
        private Transform enemyTransform;
        private RectTransform rt;
        private Image frame;
        private Image fill;
        private Vector3 offset = new Vector3(0, Screen.height * 0.05f, 0);

        public void Init(Unit_AI_Hostile enemy, UnityAction<int> deregisterOnDestroyAction)
        {
            if (isInitialized == true)
            {
                Debug.Log("This component was already initialized");
                return;
            }

            this.enemy = enemy;
            this.deregisterOnDestroyAction = deregisterOnDestroyAction;
            enemyInstanceID = enemy.GetInstanceID();
            mainCamera = GameManager.MainCamera;
            enemyTransform = enemy.BodyTransform.Head;
            rt = GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(202, 18);

            frame = UI_Pool.Instance.GetImage(transform, 202, 18, nameof(frame));
            frame.rectTransform.sizeDelta = rt.sizeDelta;
            frame.rectTransform.localPosition = Vector3.zero;
            frame.sprite = UI_Battle.Instance.TargetHP.HPST_Sprite;
            frame.type = Image.Type.Sliced;
            frame.enabled = true;

            fill = UI_Pool.Instance.GetImage(transform, 200, 16, nameof(fill));
            fill.color = Color.red;
            fill.sprite = UI_Battle.Instance.TargetHP.HPST_Sprite;
            fill.type = Image.Type.Sliced;
            fill.rectTransform.pivot = new Vector2(0.0f, 0.5f);
            fill.rectTransform.localPosition = Vector3.zero + new Vector3(1, 0 ,0);
            fill.rectTransform.anchorMax = new Vector2(0.0f, 0.5f);
            fill.rectTransform.anchorMin = new Vector2(0.0f, 0.5f);
            fill.enabled = true;

            isInitialized = true;
        }

        private void Update()
        {
            if(enemyTransform.IsInsideCameraFrustum(mainCamera) == false)
            {
                Destroy(gameObject);
                return;
            }

            rt.position = mainCamera.WorldToScreenPoint(enemyTransform.position) + offset;
        }

        private void OnDestroy()
        {
            UI_Pool.Instance.RemoveImage(fill);
            UI_Pool.Instance.RemoveImage(frame);

            deregisterOnDestroyAction.Invoke(enemyInstanceID);
        }
    }
}
