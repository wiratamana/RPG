using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tamana
{
    public class DN_UI_Clock : MonoBehaviour
    {
        [SerializeField] private Image sun;
        [SerializeField] private Image moon;

        public Image Sun => sun;
        public Image Moon => moon;

        private DN_Main main;
        private DN_Main Main => this.GetAndAssignComponent(ref main);

        private float startSun = 360.0f; // 6:00
        private float durationSun = 180.0f; // 6:00 ~ 9:00
        private float startMoon = 1080.0f; // 18:00
        private float durationMoon = 180.0f; // 18:00 ~ 21:00

        private void Update()
        {
            var date = Main.LightingManager.Date;
            var val = (date.Hour * 60) + date.Minute;
            var sunSibling = sun.transform.GetSiblingIndex();
            var moonSibling = moon.transform.GetSiblingIndex();

            if(sunSibling != 0 && val >= startSun && val <= startSun + durationSun)
            {
                var sunfill = Mathf.Min(1.0f, (val - startSun) / durationSun);
                Sun.fillAmount = sunfill;

                if(sunfill == 1.0f)
                {
                    Sun.transform.SetAsFirstSibling();
                    Moon.fillAmount = 0.0f;
                }
            }

            if (moonSibling != 0 && val >= startMoon && val <= startMoon + durationMoon)
            {
                var moonfill = Mathf.Min(1.0f, (val - startMoon) / durationMoon);
                Moon.fillAmount = moonfill;

                if (moonfill == 1.0f)
                {
                    Moon.transform.SetAsFirstSibling();
                    Sun.fillAmount = 0.0f;
                }
            }
        }
    }
}
