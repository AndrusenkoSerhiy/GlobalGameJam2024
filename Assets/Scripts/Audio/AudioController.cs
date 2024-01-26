using UnityEngine;
using UnityEngine.Audio;

//esttAlex
namespace Audio
{
	[RequireComponent(typeof(AudioDataManager))]
	public class AudioController : MonoBehaviour
	{
		public static AudioController Instance;
		
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

		#region Init
		
		private void Start()
		{
			Init();
			
			var themeEm = player.Play("Theme", transform);
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

			Instance = this;
		}

		public void OnApplicationQuit()
		{
			player.StopAll();

			_isInited = false;
		}
		
		private AudioEmitter PlayAudio(string id) => player.Play(id, owner: transform);
		
		#endregion

		#region Preset
		
		public void Play() => PlayAudio("Start");
		
		public void Curtains() => PlayAudio("Curtains");
		
		public AudioEmitter Interact() => PlayAudio("Interact");

		public void CardFlip() => PlayAudio("Card_Flip");
		
		public void CardSuccess() => PlayAudio("Card_Success");
		
		public void CardFail() => PlayAudio("Card_Fail");
		
		public void Joke() => PlayAudio("Clown_Joke");
		
		public void Win() => PlayAudio("Clown_Win");
		public void Lose() => PlayAudio("Clown_Lose");
		
		public void Fail() => PlayAudio("Clown_Fail");
		
		public void Suspicious() => PlayAudio("Guest_Suspicious");
		
		#endregion
	}
}