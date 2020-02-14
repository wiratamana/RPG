using UnityEngine;
using UnityEditor;


namespace Tamana
{
    public class Editor_ItemEditorWindow_AddItemAndSearch : Editor_EditorWindowDrawer
    {
        public enum AddItemType { None, Weapon, Armor, Attachment, Consumable }
        public AddItemType ItemType { private set; get; }

        private Editor_ItemEditorWindow window;

        public Editor_ItemEditorWindow_AddItemAndSearch(Editor_ItemEditorWindow window)
        {
            this.window = window;
        }

        public override void Draw()
        {
            EditorGUILayout.BeginHorizontal(); // | Item List | Item Inspector |

            if (GUILayout.Button("Add Item", GUILayout.Width(100)) == true)
            {
                GenericMenu menu = new GenericMenu();

                // forward slashes nest menu items under submenus
                AddMenuItemForColor(menu, AddItemType.Weapon);
                AddMenuItemForColor(menu, AddItemType.Armor);
                AddMenuItemForColor(menu, AddItemType.Attachment);
                AddMenuItemForColor(menu, AddItemType.Consumable);
                
                // display the menu
                menu.ShowAsContext();

                ItemType = AddItemType.None;
            }


            EditorGUILayout.TextField("Search", "All", EditorStyles.toolbarSearchField);

            EditorGUILayout.EndHorizontal(); // | Item List | Item Inspector |
        }

        void AddMenuItemForColor(GenericMenu menu, AddItemType itemType)
        {
            // the menu item is marked as selected if it matches the current value of m_Color
            menu.AddItem(new GUIContent(itemType.ToString()), ItemType.Equals(itemType), OnSelectedAddItem, itemType);
        }

        void OnSelectedAddItem(object itemType)
        {
            var item = (AddItemType)itemType;

            Debug.Log(item);

            switch (item)
            {
                case AddItemType.Weapon:
                    break;
                case AddItemType.Armor:
                    break;
                case AddItemType.Attachment:
                    break;
                case AddItemType.Consumable:
                    break;
            }
        }
    }
}