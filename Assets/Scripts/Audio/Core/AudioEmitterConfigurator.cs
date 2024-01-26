using Audio.Helper;
using UnityEngine;

namespace Audio
{
    public static class AudioEmitterConfigurator
    {
        public static void SetBasicData(AudioEmitter emit, AudioData data)
        {
            emit.name = data.id;
            emit.AudioSrcRef.outputAudioMixerGroup = data.mixedGroupRef;
            emit.AudioSrcRef.playOnAwake = false;
            emit.AudioSrcRef.loop = false;
            emit.AudioSrcRef.rolloffMode = AudioRolloffMode.Custom;
            emit.AudioSrcRef.priority = 125;

            emit.AudioSrcRef.maxDistance = float.MaxValue;
        }

        public static void ApplyStartShift(AudioEmitter emitter, float shiftVal)
        {
            //Debug.Log($"- Shift: {emitter.name} - {(Mathf.Min(emitter.AudioSrcRef.clip.length * shiftVal, emitter.AudioSrcRef.clip.length - 0.01f))}");
            emitter.AudioSrcRef.time = Mathf.Min(
                emitter.AudioSrcRef.clip.length * shiftVal,
                emitter.AudioSrcRef.clip.length - 0.01f);
        }

        public static void SetAudioParams(AudioEmitter emitter, AudioData data)
        {
            emitter.AudioSrcRef.pitch = Random.Range(
                AudioHelper.SemitonToLinear(data.pitch - data.pitchDelta),
                AudioHelper.SemitonToLinear(data.pitch + data.pitchDelta));

            emitter.AudioSrcRef.volume = Random.Range(
                AudioHelper.DecibelToLinear(data.volume - data.volumeDelta),
                AudioHelper.DecibelToLinear(data.volume + data.volumeDelta));
        }
    }
}