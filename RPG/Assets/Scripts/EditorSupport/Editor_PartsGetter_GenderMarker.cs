using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
#if UNITY_EDITOR
    public class Editor_PartsGetter_GenderMarker : MonoBehaviour
    {
        [SerializeField] private Gender gender;
        public Gender Gender
        {
            get
            {
                return gender;
            }
        }
    }
#endif
}
