using UnityEngine;
using UnityEditor;

namespace Tamana
{
    public class Editor_ItemEditorWindow_ItemInspector : Editor_EditorWindowDrawer
    {
        private Editor_ItemEditorWindow window;

        public Editor_ItemEditorWindow_ItemInspector(Editor_ItemEditorWindow window)
        {
            this.window = window;
        }

        public override void Draw()
        {
            EditorGUILayout.BeginVertical(); // Item Inspector

            EditorGUILayout.LabelField("Item Inspector", EditorStyles.boldLabel);

            try
            {
                DrawItemInformation();
            }
            catch(System.Exception e)
            {
                Debug.Log($"Message : {e.Message}");
                Debug.Log($"StackTrace : {e.StackTrace}");
            }
            

            EditorGUILayout.EndVertical(); // Item Inspector
        }
        private void DrawItemInformation()
        {
            if (window.selectedItem == null)
            {
                return;
            }

            System.Func<string, string, string> repeater = (labelName, inputValue) =>
            {
                if(string.IsNullOrEmpty(inputValue) == true)
                {
                    return string.Empty;
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(labelName, GUILayout.MaxWidth(100));
                var output = GUILayout.TextArea(inputValue);
                EditorGUILayout.EndHorizontal();

                return output;
            };

            repeater("Name", window.selectedItem.itemBase.ItemName);
            repeater("Description", window.selectedItem.itemBase.ItemDescription);
            repeater("Location", (window.selectedItem.itemBase as Item_ModularBodyPart)?.PartLocation ?? null);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Prefab", GUILayout.MaxWidth(100));
            var transform = EditorGUILayout.ObjectField(window.selectedItem.itemBase.Prefab as Transform,
                typeof(Transform), false) as Transform;
            window.selectedItem.itemBase.SetPrefab(transform);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Save"))
            {

            }
        }

    }
}