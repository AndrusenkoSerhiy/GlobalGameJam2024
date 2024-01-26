using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    public class AudioInstanceManager
    {
        private AudioController _audioController;
        private AudioEmitter _sample;
        private Transform _root;
        public List<AudioEmitter> audioEmitters { get; private set; }

        public AudioInstanceManager(AudioController audioController)
        {
            _audioController = audioController;
            _sample = _audioController.emitterSample;
            _root = _audioController.emittersRoot;

            audioEmitters = new List<AudioEmitter>();

            PreFillPool(_audioController.initialPoolSize);
        }

        private void PreFillPool(int size)
        {
            for (int i = 0; i < size; i++)
                InstantiateEmitter();
        }

        private AudioEmitter InstantiateEmitter()
        {
            var emitter = GameObject.Instantiate(_sample, _root, true);
            emitter.gameObject.SetActive(false);
            audioEmitters.Add(emitter);
            return emitter;
        }

        public AudioEmitter GetVacant()
        {
            foreach (var emitter in audioEmitters)
            {
                if (!emitter.gameObject.activeInHierarchy)
                    return emitter;
            }
            
            return InstantiateEmitter();
        }

        public AudioEmitter GetVacant(AudioData data)
        {
            if (!IsLimited(data))
                return GetVacant();
            
            return (data.type == AudioData.AudioTypeE.OneShot)
                ? null 
                : GetPooled(data);
        }
        
        private bool IsLimited(AudioData data)
        {
            if (data.maxClipCount == 0) return false;
            return data.maxClipCount <= audioEmitters.Count(e => e.GetId().Equals(data.id));
        }

        private AudioEmitter GetPooled(AudioData data)
        {
            var selected = audioEmitters.Where(e => e.GetId().Equals(data.id));
            
            return selected.OrderBy(e => e.GetLastTimePlayed())
                .LastOrDefault();
        }
    }
}