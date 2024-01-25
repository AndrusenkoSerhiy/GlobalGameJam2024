using Inventory.Inventory;
using UnityEditor;
using UnityEngine;

namespace Inventory.Editor
{
    [CustomEditor(typeof(ItemManager))]
    public class ItemManagerEditor : UnityEditor.Editor
    {
        ItemManager _manager;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            _manager = (ItemManager)target;
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Update List"))
                UpdateList();
        }

        private void UpdateList()
        {
            string[] guids = AssetDatabase.FindAssets("t:ItemDefinition");

            ItemDefinition[] objects = new ItemDefinition[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                objects[i] = AssetDatabase.LoadAssetAtPath<ItemDefinition>(path);
            }

            _manager.Items.Clear();
            _manager.Items.AddRange(objects);

            EditorUtility.SetDirty(_manager);
            AssetDatabase.SaveAssets();
        }
    }
}
