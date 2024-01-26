using UnityEngine;

namespace Audio.Helper
{
	public static class AudioHelper
	{
		public static float LinearToDecibel(float linear)
		{
			if (linear == 0)
				return -80f;
			return 20f * Mathf.Log10(linear);
		}

		public static float DecibelToLinear(float dB)
		{
			return Mathf.Pow(10f, dB / 20f);
		}

		public static float SemitonToLinear(float st)
		{
			return Mathf.Pow(1.05946309435928f, st);
		}
	}
}