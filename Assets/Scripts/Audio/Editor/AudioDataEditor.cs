using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Audio.Editor
{
    [CustomEditor(typeof(AudioData))]
    public class AudioDataEditor : UnityEditor.Editor
    {
        private AudioData data;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            data = (AudioData)target;

            if (GUILayout.Button("Fill Up with same"))
                FillUpWithSame();

            if (GUILayout.Button("Get ID from Name"))
                GetIdFromName();
        }

        private void FillUpWithSame()
        {
            if (data.clipRefList.Count == 0)
                return;

            var fullFileName = AssetDatabase.GetAssetPath(data.clipRefList[0]);
            var folderPath = Path.GetDirectoryName(fullFileName);
            var fileName = Path.GetFileNameWithoutExtension(fullFileName);

            var fileNamePattern = Regex.Replace(fileName, @"\d+$", "");
            string[] files = Directory.GetFiles(folderPath, "*.*")
                .Where(file => !file.EndsWith(".meta") && file.Contains(fileNamePattern)).ToArray();

            for (int i = 0; i < files.Length; i++)
            {
                var clip = AssetDatabase.LoadAssetAtPath<AudioClip>($"{files[i]}");
                if (clip != null && !data.clipRefList.Contains(clip))
                    data.clipRefList.Add(clip);
            }

            Save();
        }

        private void GetIdFromName()
        {
            data.id = serializedObject.targetObject.name;

            Save();
        }

        private void Save()
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(serializedObject.targetObject);
            AssetDatabase.SaveAssets();
        }
    }
}