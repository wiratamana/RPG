using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tamana
{
    public class Editor_ItemEditorWindow : EditorWindow
    {
        public Dictionary<string, Editor_ItemEditorItemMetadata> itemDics { get; private set; } = new Dictionary<string, Editor_ItemEditorItemMetadata>();
        public Editor_ItemEditorItemMetadata selectedItem { get; set; }

        private Editor_ItemEditorWindow_ItemList _itemList;
        public Editor_ItemEditorWindow_ItemList ItemList
        {
            get
            {
                if(_itemList == null)
                {
                    _itemList = new Editor_ItemEditorWindow_ItemList(this);
                }

                return _itemList;
            }
        }

        private Editor_ItemEditorWindow_ItemInspector _itemInspector;
        public Editor_ItemEditorWindow_ItemInspector ItemInspector
        {
            get
            {
                if(_itemInspector == null)
                {
                    _itemInspector = new Editor_ItemEditorWindow_ItemInspector(this);
                }

                return _itemInspector;
            }
        }

        private void Awake()
        {
            minSize = new Vector2(650, 300);
            autoRepaintOnSceneChange = true;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal(); // | Item List | Item Inspector |

            ItemList.Draw();
            ItemInspector.Draw();

            EditorGUILayout.EndHorizontal(); // | Item List | Item Inspector |
        }                       
    }
}
