using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    [CreateAssetMenu(fileName = "LightingPreset", menuName = "LightingPreset", order = 1 )]
    public class DN_LightingPreset : ScriptableObject
    {
        public Gradient AmbientColor;
        public Gradient DirectionalColor;
        public Gradient FogColor;
        public Gradient LightShadowStrength;
    }
}
