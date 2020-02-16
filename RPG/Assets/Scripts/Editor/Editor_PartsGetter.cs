﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.Linq;

namespace Tamana
{
    public class Editor_PartsGetter : EditorWindow
    {
        private Transform prefab;

        [MenuItem("CustomWindow/Parts Getter")]
        private static void Init()
        {
            // Get existing open window or if none, make a new one:
            Editor_PartsGetter window = GetWindow<Editor_PartsGetter>();
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("PartsGetter Prefab", EditorStyles.boldLabel);
            prefab = (Transform)EditorGUILayout.ObjectField(prefab, typeof(Transform), true);
            if (GUILayout.Button(nameof(CreateAssets)))
            {
                if(prefab == null)
                {
                    Debug.Log($"{nameof(prefab)} is null !!");
                }

                try
                {
                    AssetDatabase.StartAssetEditing();
                    CreateAssets(prefab);
                }
                catch (System.Exception e)
                {
                    Debug.Log("Exception");
                    Debug.Log(e.Message);
                    Debug.Log(e.StackTrace);
                }
                finally
                {
                    Debug.Log("Finally");
                    AssetDatabase.StopAssetEditing();
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }

            if (GUILayout.Button(nameof(OpenItemEditorWindow)))
            {
                OpenItemEditorWindow();
            }
        }

        private void CreateAssets(Transform prefab)
        {
            var armorPath = Editor_ItemEditorManager.ASSET_PATH_ITEM_ARMOR;
            var attachmentPath = Editor_ItemEditorManager.ASSET_PATH_ITEM_ATTACHMENT;
            var extension = Editor_ItemEditorManager.FILE_EXTENSION_ASSET;

            if (Directory.Exists(armorPath) == false)
            {
                Directory.CreateDirectory(armorPath);
            }

            if (Directory.Exists(attachmentPath) == false)
            {
                Directory.CreateDirectory(attachmentPath);
            }

            var markers = prefab.GetComponentsInChildren<Editor_PartsGetter_Marker>();
            if (markers.Length == 0)
            {
                Debug.Log($"{nameof(markers)}.Length is 0 ! {nameof(markers)} is an array of {nameof(Editor_PartsGetter_Marker)}.");
                return;
            }

            var prefabsDic = GetPrefabs();

            foreach (var m in markers)
            {
                for(int i = 0; i < m.transform.childCount; i++)
                {
                    Item_Base itemObject;
                    var rootPath = m.IsAttachment ? attachmentPath : armorPath;
                    var assetWritePath = $"{rootPath}/{m.transform.GetChild(i).name}.{extension}";

                    if (File.Exists(assetWritePath) == false)
                    {
                        if (m.IsAttachment == true)
                        {
                            itemObject = CreateInstance<Item_Attachment>();
                            var itemAttachment = itemObject as Item_Attachment;
                            itemAttachment.SetType(m.AttachmentPart);
                        }
                        else
                        {
                            itemObject = CreateInstance<Item_Armor>();
                            var itemArmor = itemObject as Item_Armor;
                            itemArmor.SetType(m.ArmorPart);
                        }

                        itemObject.SetItemName(m.transform.GetChild(i).name);
                        itemObject.SetItemDescription($"Item '{itemObject.ItemName}' description");

                        var itemCharacterBody = itemObject as Item_ModularBodyPart;
                        itemCharacterBody.SetPartLocation(GetPartLocation(m.transform));
                        //if (prefabsDic.ContainsKey(m.transform.GetChild(i).name) == true)
                        //{
                        //    itemCharacterBody.SetPrefab(prefabsDic[m.transform.GetChild(i).name]);
                        //}

                        AssetDatabase.CreateAsset(itemObject, assetWritePath);
                    }
                    else
                    {
                        itemObject = AssetDatabase.LoadAssetAtPath<Item_Base>(assetWritePath);
                        if(itemObject == null)
                        {
                            continue;
                        }

                        EditorUtility.SetDirty(itemObject);

                        if (m.IsAttachment == true)
                        {
                            var itemAttachment = itemObject as Item_Attachment;
                            itemAttachment.SetType(m.AttachmentPart);
                        }
                        else
                        {
                            var itemArmor = itemObject as Item_Armor;
                            itemArmor.SetType(m.ArmorPart);
                        }

                        itemObject.SetItemName(m.transform.GetChild(i).name);
                        itemObject.SetItemDescription($"Item '{itemObject.ItemName}' description!!");

                        var itemCharacterBody = itemObject as Item_ModularBodyPart;
                        itemCharacterBody.SetPartLocation(GetPartLocation(m.transform));
                        if (prefabsDic.ContainsKey(m.transform.GetChild(i).name) == true)
                        {
                            itemCharacterBody.SetPrefab(prefabsDic[m.transform.GetChild(i).name]);
                        }                        
                    }
                }                
            }
        }

        private string GetPartLocation(Transform t)
        {
            var sb = new StringBuilder();
            sb.Insert(0, t.name);

            System.Func<Transform, Transform> repeater = (T) =>
            {
                if (T.parent != null)
                {
                    sb.Insert(0, $"{T.parent.name}/");
                    return T.parent;
                }

                return null;
            };

            var parent = t.parent;

            while(parent != null)
            {
                parent = repeater.Invoke(parent);
            }

            return sb.ToString();
        }

        private Dictionary<string, Transform> GetPrefabs()
        {
            var modularParts = Editor_ItemEditorManager.ASSET_PATH_MODULAR_PART_STATIC_PREFAB;
            var retVal = new Dictionary<string, Transform>();

            if (Directory.Exists(modularParts) == false)
            {
                Debug.Log($"Path not exist. Path : '{modularParts}'", Debug.LogType.Error);
                return retVal;
            }

            var filesPath = Directory.GetFiles(modularParts)
                .Where(x => x.EndsWith(".meta") == false).ToList();

            foreach(var p in filesPath)
            {
                var transform = AssetDatabase.LoadAssetAtPath<Transform>(p);
                if(transform == null)
                {
                    Debug.Log($"{nameof(AssetDatabase.LoadAssetAtPath)} failed !! Path : {p}");
                    continue;
                }

                retVal.Add(transform.name.Replace("_Static", string.Empty), transform);
            }

             return retVal;
        }

        private void OpenItemEditorWindow()
        {
            Close();

            Editor_ItemEditorWindow window = GetWindow<Editor_ItemEditorWindow>();
            window.Show();
        }
    }
}