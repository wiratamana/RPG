using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tamana
{
    [CustomEditor(typeof(AnimState_SetHitAnimationStatus))]
    public class AnimState_SetHitAnimationStatusEditor : Editor
    {
        SerializedProperty weaponType;
        SerializedProperty animHit_1H;
        SerializedProperty animHit_2H;

        void OnEnable()
        {
            weaponType = serializedObject.FindProperty(nameof(AnimState_SetHitAnimationStatus.weaponType));
            animHit_1H = serializedObject.FindProperty(nameof(AnimState_SetHitAnimationStatus.animHit_1H));
            animHit_2H = serializedObject.FindProperty(nameof(AnimState_SetHitAnimationStatus.animHit_2H));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(weaponType);

            if (weaponType.intValue == (int)WeaponType.OneHand)
            {
                if(animHit_1H.intValue == 0)
                {
                    animHit_1H.intValue = (int)AnimHit_1H.Sword1h_Hit_Head_Right;
                }

                EditorGUILayout.PropertyField(animHit_1H);
            }
            else if (weaponType.intValue == (int)WeaponType.TwoHand)
            {
                EditorGUILayout.PropertyField(animHit_2H);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
