using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tamana
{
    public class Editor_ItemEditorWindow_ItemList : Editor_EditorWindowDrawer
    {
        private Vector2 scrollPosition;
        private Editor_ItemEditorWindow window;

        public Editor_ItemEditorWindow_ItemList(Editor_ItemEditorWindow window)
        {
            this.window = window;
        }

        public override void Draw()
        {
            EditorGUILayout.BeginVertical(GUILayout.MaxWidth(300)); // Item List

            EditorGUILayout.LabelField("Item List", EditorStyles.boldLabel);

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            try
            {
                DrawItemList();
            }
            catch (System.Exception e)
            {
                Debug.Log($"Exception : {e.Message}");
            }

            EditorGUILayout.EndScrollView();


            EditorGUILayout.EndVertical(); // Item List
        }

        private void DrawItemList()
        {
            if (window.itemDics == null || window.itemDics.Count == 0)
            {
                GetAllItemData();
            }

            foreach (var i in window.itemDics)
            {
                Rect rectPosition = EditorGUILayout.GetControlRect(false, 16);

                if (i.Value == window.selectedItem)
                {
                    EditorGUI.DrawRect(rectPosition, Color.blue);

                    if (GUI.Button(rectPosition, i.Value.itemBase.ItemName, EditorStyles.whiteLabel))
                    {
                        i.Value.isSelected = true;
                    }
                }
                else
                {
                    if (GUI.Button(rectPosition, i.Value.itemBase.ItemName, EditorStyles.label))
                    {
                        i.Value.isSelected = true;
                        window.selectedItem = i.Value;
                    }
                }
            }
        }

        private void GetAllItemData()
        {
            var itemPath = Editor_ItemEditorManager.ASSET_PATH_ITEM;
            var allAssetsPath = Directory.GetFiles(itemPath, "*.*", SearchOption.AllDirectories)
                .Where(x => x.EndsWith(".meta") == false).ToList();

            List<Item_Base> items = new List<Item_Base>();
            foreach (var path in allAssetsPath)
            {
                var Path = path.Replace("\\", "/");
                items.Add(AssetDatabase.LoadAssetAtPath<Item_Base>(Path));
            }

            foreach (var i in items)
            {
                var item = i as Item_Base;
                if (string.IsNullOrEmpty(item.ItemName) == true)
                {
                    continue;
                }

                window.itemDics.Add(item.ItemName, new Editor_ItemEditorItemMetadata()
                {
                    itemBase = item,
                    tags = new List<string>(),
                    isSelected = false
                });
            }
        }
    }
}
