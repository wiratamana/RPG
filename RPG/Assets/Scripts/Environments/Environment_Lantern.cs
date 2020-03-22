using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{   
    public class Environment_Lantern : MonoBehaviour
    {
        private Light pointLight;
        private MeshRenderer meshRenderer;

        private static Material lanternMaterial;

        private void Awake()
        {
            pointLight = GetComponentInChildren<Light>();
            meshRenderer = GetComponent<MeshRenderer>();

            if(lanternMaterial == null)
            {
                var material = meshRenderer.sharedMaterial;
                lanternMaterial = new Material(material);
            }
            
            meshRenderer.sharedMaterial = lanternMaterial;
        }

        private void Update()
        {
            var date = DN_Main.Instance.LightingManager.Date;

            if(date.Hour < 5 || date.Hour >= 20)
            {
                if(pointLight.gameObject.activeInHierarchy == false)
                {
                    pointLight.gameObject.SetActive(true);
                    meshRenderer.sharedMaterial.SetColor("_EmissionColor", new Color(191 / 255f, 107 / 255f, 60 / 255f) * 1);
                }
            }
            else
            {
                if (pointLight.gameObject.activeInHierarchy == true)
                {
                    meshRenderer.sharedMaterial.SetColor("_EmissionColor", Color.black);
                    pointLight.gameObject.SetActive(false);
                }
            }
        }
    }
}
