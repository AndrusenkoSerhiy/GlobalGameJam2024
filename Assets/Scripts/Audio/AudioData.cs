using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Audio/AudioData")]
    [Serializable]
    public class AudioData : AudioEntity
    {
        public enum AudioTypeE
        {
            OneShot = 0,
            Simple = 1,
            Looped = 2,
        }
        
        //Params
        public AudioTypeE type = AudioTypeE.OneShot;

        public float volume = 0f;
        public float volumeDelta = 0f;
        
        public float pitch = 0f;
        public float pitchDelta = 0f;
        
        public int cooldown = 150;
        
        public int maxClipCount = 0;
        
        public AudioMixerGroup mixedGroupRef;

        //Core
        public bool ignoreTimeScale = false;

        //Files
        //[InlineEditor(InlineEditorModes.LargePreview)]
        public List<AudioClip> clipRefList = new List<AudioClip>();

        public float GetCooldown()
        {
            return cooldown * 0.001f;
        }

        public AudioClip GetClip()
        {
            if (!clipRefList.Any())
            {
                Debug.Log("Broke AudioData for: " + id);
                return null;
            }

            return (clipRefList.Count == 1) 
                ? clipRefList[0] 
                : clipRefList[Random.Range(0, clipRefList.Count)];
        }
    }
}