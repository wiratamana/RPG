using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    public class UI_Battle_EnemyLock : MonoBehaviour
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => this.GetAndAssignComponent(ref rectTransform);

        private UI_Battle uiBattle;
        public UI_Battle UIBattle => this.GetAndAssignComponentInParent(ref uiBattle);

        private Image dotImage;
        public Image DotImage => this.GetAndAssignComponentInChildren(ref dotImage);

        public Unit_Base TargetUnit { get; private set; }

        private void OnValidate()
        {
            Deactivate();
        }

        private void Awake()
        {
            GameManager.Player.EnemyCatcher.OnEnemyCatched.AddListener(Activate);
            GameManager.Player.EnemyCatcher.OnCatchedEnemyReleased.AddListener(Deactivate);
        }

        private void Update()
        {
            var spine = TargetUnit.BodyTransform.Spine;
            var mainCamera = GameManager.Player.TPC.CameraHandler.MainCamera;
            var screenPosition = mainCamera.WorldToScreenPoint(spine.position);

            RectTransform.position = screenPosition;
        }

        private void Activate(Unit_AI_Hostile targetUnit)
        {
            TargetUnit = targetUnit;
            DotImage.enabled = true;
            enabled = true;
        }

        private void Deactivate()
        {
            enabled = false;
            DotImage.enabled = false;
            TargetUnit = null;
        }
    }
}