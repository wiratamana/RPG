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

        [SerializeField] private Transform hips;
        public Transform Hips
        {
            get
            {
                return hips;
            }
        }

        private Transform weaponTransform;
        public Transform WeaponTransform 
        {
            get
            {
                return weaponTransform;
            }

            set
            {
                if(weaponTransform != null)
                {
                    Destroy(weaponTransform.gameObject);
                }

                weaponTransform = value;
                if (value == null)
                {
                    return;
                }

                weaponTransform.gameObject.layer = LayerMask.NameToLayer(LayerManager.LAYER_PLAYER_MENU_PORTRAIT);
                var meshRenderer = weaponTransform.GetComponent<MeshRenderer>();
                meshRenderer.sharedMaterial = new Material(GameManager.ItemMaterial);
                meshRenderer.sharedMaterial.SetVector("_CamDir", PortraitCamera.transform.forward);
                meshRenderer.sharedMaterial.SetFloat("_Intensity", 1.5f);
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
                mr.sharedMaterial.SetFloat("_Intensity", 1.5f);
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
