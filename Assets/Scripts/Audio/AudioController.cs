using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
	[RequireComponent(typeof(AudioDataManager))]
	public class AudioController : MonoBehaviour
	{
		[SerializeField] private AudioListener listener;
		[SerializeField] private AudioMixer mixer;
		[SerializeField] internal AudioEmitter emitterSample;

		[Header("Pool Settings")] [Range(0, 100)] 
		public int initialPoolSize;

		[Header("Roots")] 
		[SerializeField] internal Transform emittersRoot;
		
		public AudioDataManager dataManager { get; private set; }
		public AudioInstanceManager instanceManager { get; private set; }
		public AudioPlayer player { get; private set; }

		private bool _isInited = false;

		private void Start()
		{
			Init();
			
			StartMenu();
		}

		private void Init()
		{
			if (_isInited)
				return;

			DontDestroyOnLoad(this);

			dataManager = gameObject.GetComponent<AudioDataManager>();
			dataManager.Init();

			instanceManager = new AudioInstanceManager(this);

			player = new AudioPlayer(this);
			
			_isInited = true;
		}

		public void OnApplicationQuit()
		{
			player.StopAll();

			_isInited = false;
		}

		public Vector3 GetListenerPosition()
		{
			return listener.transform.position;
		}

		#region States

		public void StartMenu()
		{
			var _themeEm = player.Play("Theme", transform);
		}
		
		#endregion

		#region Preset

		public void UiClick() => player.Play("Click", owner: transform);

		#endregion

	}
}