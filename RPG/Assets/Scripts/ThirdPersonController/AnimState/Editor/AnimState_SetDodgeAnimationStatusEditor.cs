using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tamana
{
    [CustomEditor(typeof(AnimState_SetDodgeAnimationStatus))]
    public class AnimState_SetDodgeAnimationStatusEditor : Editor
    {
        SerializedProperty weaponType;
        SerializedProperty animDodge_1H;
        SerializedProperty animDodge_2H;

        void OnEnable()
        {
            weaponType = serializedObject.FindProperty(nameof(AnimState_SetDodgeAnimationStatus.weaponType));
            animDodge_1H = serializedObject.FindProperty(nameof(AnimState_SetDodgeAnimationStatus.animDodge_1H));
            animDodge_2H = serializedObject.FindProperty(nameof(AnimState_SetDodgeAnimationStatus.animDodge_2H));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(weaponType);

            if (weaponType.intValue == (int)WeaponType.OneHand)
            {
                if (animDodge_1H.intValue == 0)
                {
                    animDodge_1H.intValue = (int)AnimDodge_1H.Sword1h_Dodge;
                }

                EditorGUILayout.PropertyField(animDodge_1H);
                EditorGUILayout.LabelField($"Value : {animDodge_1H.intValue}");
            }
            else if (weaponType.intValue == (int)WeaponType.TwoHand)
            {
                EditorGUILayout.PropertyField(animDodge_2H);

                if (animDodge_2H.intValue == 0)
                {
                    animDodge_2H.intValue = (int)AnimDodge_2H.Longs_Dodge_Bwd;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
