using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace Audio.Helper
{
    public static class AudioTweenHelper
    {
        public static TweenerCore<float, float, FloatOptions> DOVolume(this AudioEmitter target, float endValue, float duration)
        {
            var t = DOTween.To(() => target.AudioSrcRef.volume, x => target.AudioSrcRef.volume = x, endValue, duration);
            t.SetTarget(target);
            return t;
        }
    }
}
