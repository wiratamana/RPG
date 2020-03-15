using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Chat_Bubble : MonoBehaviour
    {
        private Unit_AI ai;
        private Transform headTransform;
        private Transform aiTransform;

        private Vector3 offset = new Vector2(0, .5f);
        private Camera mainCamera => GameManager.MainCamera;
        private Image image;
        private Image Image => this.GetAndAssignComponent(ref image);

        private void OnValidate()
        {
            Deactivate();
        }

        private void Awake()
        {
            Deactivate();
        }

        private void Update()
        {
            var dirToPlayer = GameManager.PlayerTransform.position - headTransform.transform.position;
            dirToPlayer.y = 0;
            dirToPlayer = Vector3.Normalize(dirToPlayer);

            var playerForward = GameManager.PlayerTransform.forward;
            playerForward.y = 0;
            playerForward = Vector3.Normalize(playerForward);

            var dot = Vector3.Dot(playerForward, dirToPlayer);

            if (dot > -0.9f)
            {
                Deactivate();
                return;
            }

            var distance = Vector3.Distance(GameManager.PlayerTransform.position, aiTransform.position);
            if (distance > 3.0f)
            {
                Deactivate();
                return;
            }

            var position = mainCamera.WorldToScreenPoint(headTransform.position + offset);
            Image.rectTransform.position = position;
        }

        public async void Activate(Unit_AI ai)
        {
            if(this.ai == ai)
            {
                return;
            }

            headTransform = ai.BodyTransform.Head;
            aiTransform = ai.transform;
            gameObject.SetActive(true);

            await AsyncManager.WaitForFrame(1);

            Image.enabled = true;
        }

        public void Deactivate()
        {
            ai = null;
            headTransform = null;
            aiTransform = null;

            Image.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
