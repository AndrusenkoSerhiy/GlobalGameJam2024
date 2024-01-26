using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TypeE = Audio.AudioData.AudioTypeE;

namespace Audio
{
    public class AudioPlayer
    {
        private bool _isInited = false;
        private AudioController _audioController;

        //audioId, entityId, cooldown
        private Dictionary<string, Dictionary<int, float>> _cooldownDict = new ();

        private Dictionary<string, float> _realtimeDelay = new ();

        public AudioPlayer(AudioController audioController)
        {
            _audioController = audioController;

            _cooldownDict.Clear();

            _isInited = true;
        }

        #region Cooldown/DistCulling

        private bool CheckCooldown(AudioData audioData, int entityId)
        {
            if (Time.timeScale == 0 && CanIgnoreTimeScale(audioData))
                return true;

            if (_cooldownDict.ContainsKey(audioData.id) && _cooldownDict[audioData.id].ContainsKey(entityId))
                return (Time.time - _cooldownDict[audioData.id][entityId]) > audioData.GetCooldown();

            UpdateCooldown(audioData, entityId);
            return true;
        }

        private void UpdateCooldown(AudioEntity audioData, int entityId = -1)
        {
            if (!_cooldownDict.ContainsKey(audioData.id))
                _cooldownDict.Add(audioData.id, new Dictionary<int, float>());

            if (!_cooldownDict[audioData.id].ContainsKey(entityId))
                _cooldownDict[audioData.id].Add(entityId, 0f);

            _cooldownDict[audioData.id][entityId] = Time.time;
        }

        private bool CanIgnoreTimeScale(AudioData data)
        {
            if (!data.ignoreTimeScale)
                return false;

            if (!_realtimeDelay.ContainsKey(data.id))
            {
                _realtimeDelay.Add(data.id, Time.unscaledTime);
                return true;
            }

            var res = (Time.unscaledTime - _realtimeDelay[data.id]) > data.GetCooldown();
            //Debug.Log($"{(Time.unscaledTime - _realtimeDelay[data.Id]).ToString("F")} > {data.GetCooldown()} = {res}");
            _realtimeDelay[data.id] = Time.unscaledTime;
            return res;
        }

        #endregion

        #region PlayEmitter

        private AudioEmitter TryPlay(AudioData audioData, Vector3 position, Transform owner = null)
        {
            if (audioData == null)
                return null;

            if (!audioData.clipRefList.Any())
            {
                Debug.Log("No audio clips for AudioData: " + audioData.id);
                return null;
            }

            int entityId = (owner == null) ? -1 : owner.GetInstanceID();

            if (!CheckCooldown(audioData, entityId))
                return null;

            UpdateCooldown(audioData, entityId);

            return PlayEmitter(audioData, position, owner);
        }

        private AudioEmitter PlayEmitter(AudioData data, Vector3 position, Transform owner, float shiftVal = 0f)
        {
            var emitter = _audioController.instanceManager.GetVacant(data);

            if (emitter == null)
                return null;

            emitter.SetOwner(owner);

            AudioEmitterConfigurator.SetBasicData(emitter, data);

            if (shiftVal > 0f)
                AudioEmitterConfigurator.ApplyStartShift(emitter, shiftVal);

            AudioEmitterConfigurator.SetAudioParams(emitter, data);

            emitter.PlaySound(data);

            return emitter;
        }

        #endregion

        #region Play/Stop

        public AudioEmitter Play(AudioData data, Vector3 pos, Transform owner = null)
        {
            if (!_isInited || data == null) return null;

            return TryPlay(data, pos, owner);
        }

        public AudioEmitter Play(string audioId, Transform owner)
        {
            if (!_isInited) return null;

            return Play(_audioController.dataManager.GetAudioByName(audioId), Vector3.zero, owner);
        }

        public void StopAll()
        {
            if (!_isInited) return;

            foreach (var emitter in _audioController.instanceManager.audioEmitters)
                emitter.Stop();
        }

        #endregion
    }
}