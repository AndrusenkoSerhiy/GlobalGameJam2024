using UnityEditor;
using UnityEngine;

namespace Audio
{
    [CustomEditor(typeof(AudioDataManager))]
    public class AudioDataManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update List"))
            {
                (target as AudioDataManager).UpdateList();
            }
        }
    }
}
