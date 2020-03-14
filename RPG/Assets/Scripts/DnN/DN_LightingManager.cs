using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tamana
{
    public class DN_LightingManager : MonoBehaviour
    {
        [SerializeField] private DN_LightingPreset LightingPreset;
        private Light directionalLight;
        private DN_Main main;
        public DN_Main Main => this.GetAndAssignComponent(ref main);
        private int dateDay = 0;
        private float TimeOfDay = 8;

        public DN_Date Date { get; private set; }

        private void OnValidate()
        {
            directionalLight = GetComponentInChildren<Light>();
        }

        private void Awake()
        {
            directionalLight = GetComponentInChildren<Light>();
        }

        private void Update()
        {
            TimeOfDay += Time.deltaTime / 60.0f;
            var day = TimeOfDay % 24;
            if (day < TimeOfDay)
            {
                dateDay++;
            }
            var dateHour = (int)day;
            var dateMinute = (day - dateHour) * 60.0f;
            Date = new DN_Date(dateDay, dateHour, (int)dateMinute);

            TimeOfDay = day;
            UpdateLighting(TimeOfDay / 24.0f);
        }

        private void UpdateLighting(float timePercent)
        {
            RenderSettings.ambientLight = LightingPreset.AmbientColor.Evaluate(timePercent);
            RenderSettings.fogColor = LightingPreset.FogColor.Evaluate(timePercent);

            directionalLight.color = LightingPreset.DirectionalColor.Evaluate(timePercent);
            directionalLight.shadowStrength = LightingPreset.LightShadowStrength.Evaluate(timePercent).a;
            directionalLight.transform.rotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170, 0));
        }
    }
}
