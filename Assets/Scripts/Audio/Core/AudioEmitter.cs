using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEmitter : MonoBehaviour
    {
        [SerializeField] private Transform _owner;
        private AudioData _data;
        [SerializeField] public AudioSource AudioSrcRef; //{ get; private set; }
        
        private float _duration;
        private float _lastTimePlayed = 0;
        private float _cooldown;
        
        private bool _needReplay = false;
        private bool _isStopped = false;

        #region Data
        
        public float GetLastTimePlayed()
        {
            return _lastTimePlayed;
        }

        public bool IsPlaying()
        {
            return gameObject.activeInHierarchy && AudioSrcRef != null && AudioSrcRef.isPlaying;
        }

        public string GetId()
        {
            return (_data != null) ? _data.id : string.Empty;
        }

        public float GetTime()
        {
            return (AudioSrcRef != null) ? AudioSrcRef.time : 0f;
        }

        private bool IsLooped()
        {
            return _data != null && _data.type == AudioData.AudioTypeE.Looped;
        }

        public void SetOwner(Transform owner)
        {
            _owner = owner;
        }
        
        public int GetOwnerId()
        {
            if (_owner == null) return -1;
            return _owner.GetInstanceID();
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
        
        #endregion
        
        public void PlaySound(AudioData data)
        {
            _data = data;
            _needReplay = false;
            _lastTimePlayed = Time.time;
            _isStopped = false;
            _cooldown = data.GetCooldown();
            gameObject.SetActive(true);
            
            AudioSrcRef.clip = data.GetClip();

            switch (data.type)
            {
                case AudioData.AudioTypeE.OneShot:
                    PlayAudioSrc(true);
                    break;

                case AudioData.AudioTypeE.Simple:
                    PlayAudioSrc();
                    break;

                case AudioData.AudioTypeE.Looped:
                    if (IsPlaying())
                        AudioSrcRef.timeSamples = 0;

                    if (data.clipRefList.Count == 1)
                    {
                        AudioSrcRef.clip = _data.clipRefList[0];
                        AudioSrcRef.loop = true;
                    }
                    else
                    {
                        AudioSrcRef.clip = _data.GetClip();
                        _needReplay = true;
                    }

                    PlayAudioSrc();
                    break;
            }
        }

        private void PlayAudioSrc(bool oneShot = false)
        {
            _duration = AudioSrcRef.clip.length;

            if (oneShot)
                AudioSrcRef.PlayOneShot(AudioSrcRef.clip);
            else
                AudioSrcRef.Play();
        }

        public void Stop()
        {
            _owner = null;
            _needReplay = false;
            _isStopped = true;
            
            AudioSrcRef.Stop();
        }

        public void LateUpdate()
        {
            if (_data == null)
                return;
            
            //stop
            if (_isStopped)
                gameObject.SetActive(false);

            //playing
            if (AudioSrcRef.isPlaying)
                return;

            //simple
            if (!_needReplay)
                Stop();
            
            //loop
            if (_needReplay && (Time.time - _lastTimePlayed) >= _duration + _cooldown)
                PlaySound(_data);
        }
    }
}