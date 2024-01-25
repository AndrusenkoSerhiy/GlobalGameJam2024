using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Audio
{
    public class AudioDataManager : MonoBehaviour
    {
        [SerializeField] private List<AudioData> _datas = new();

        private Dictionary<string, AudioData> _audioDatas = new();

        public void Init()
        {
            _audioDatas.Clear();
            
            foreach (var data in _datas)
                _audioDatas.Add(data.id, data);
        }

        public AudioData GetAudioByName(string audioId)
        {
            if (string.IsNullOrEmpty(audioId))
                return null;

            return (_audioDatas.ContainsKey(audioId))
                ? _audioDatas[audioId]
                : null;
        }
        
        
#if UNITY_EDITOR

        public void UpdateList()
        {
            _datas.Clear();

            string[] guids = AssetDatabase.FindAssets("t:AudioData", null);

            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var data = AssetDatabase.LoadAssetAtPath<AudioData>(path);
                _datas.Add( data);
            }
            
            Debug.Log("[AudioEmitterManager] Cnt: " + _datas.Count);
        }

#endif
    }
}