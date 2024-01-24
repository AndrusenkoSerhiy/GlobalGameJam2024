using Inventory.Inventory;
using UnityEditor;
using UnityEngine;

namespace FarrokhGames.Inventory
{
    [CustomEditor(typeof(ItemDefinition))]
    public class ItemDefinitionEditor : Editor
    {
        private ItemDefinition _item;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            _item = (ItemDefinition)target;

            if (_item.sprite != null)
            {
                Rect previewRect = GUILayoutUtility.GetRect(100, 100);
                EditorGUI.DrawTextureTransparent(previewRect, _item.sprite.texture, ScaleMode.ScaleToFit);
            }
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorApplication.projectWindowItemOnGUI += ProjectWindowItemOnGUI;
        }

        private static void ProjectWindowItemOnGUI(string guid, Rect rect)
        {
            var obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(AssetDatabase.GUIDToAssetPath(guid));

            if (obj is ItemDefinition itemData && itemData.sprite != null)
            {
                // Get the Texture2D from the Sprite
                var iconTexture = itemData.sprite.texture;

                // Draw the texture in the Project window
                GUI.DrawTexture(rect, iconTexture, ScaleMode.ScaleToFit);
            }
        }
    }
}