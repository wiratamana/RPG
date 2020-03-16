using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Tamana
{
    [CustomEditor(typeof(Chat_Event))]
    public class Chat_EventEditor : Editor
    {
        SerializedProperty _event;
        SerializedProperty products;

        private void OnEnable()
        {
            _event = serializedObject.FindProperty(Chat_Event.FIELD_EVENT);
            products = serializedObject.FindProperty(Chat_Event.FIELD_PRODUCTS);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var chatEvent = (ChatEvent)_event.intValue;

            EditorGUILayout.PropertyField(_event);

            switch (chatEvent)
            {
                case ChatEvent.Shop:
                    EditorGUILayout.PropertyField(products);
                    break;
                case ChatEvent.Blacksmith:
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
