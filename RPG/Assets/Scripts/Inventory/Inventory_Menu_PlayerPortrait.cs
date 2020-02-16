using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class Inventory_Menu_PlayerPortrait : SingletonMonobehaviour<Inventory_Menu_PlayerPortrait>
    {
        private Camera portraitCamera;
        public Camera PortraitCamera
        {
            get
            {
                if(portraitCamera == null)
                {
                    var go = GameObject.FindWithTag(TagManager.TAG_PLAYER_MENU_PORTRAIT_CAMERA);
                    portraitCamera = go.GetComponent<Camera>();
                }

                return portraitCamera;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            ChangeMaterial();

            UI_Menu_Inventory.OnMenuInventoryOpened.AddListener(ActivateCameraAndModel);
            UI_Menu_Inventory.OnMenuInventoryClosed.AddListener(DeactivateCameraAndModel);
            DeactivateCameraAndModel();
        }

        private void ChangeMaterial()
        {
            var meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>(true);
            foreach(var mr in meshRenderers)
            {
                mr.sharedMaterial = new Material(GameManager.ItemMaterial);
                mr.sharedMaterial.SetVector("_CamDir", PortraitCamera.transform.forward);
            }
        }

        private void ActivateCameraAndModel()
        {
            PortraitCamera.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        private void DeactivateCameraAndModel()
        {
            PortraitCamera.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
